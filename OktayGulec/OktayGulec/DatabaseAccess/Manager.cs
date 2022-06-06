using OktayGulec.Models;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OktayGulec.DatabaseAccess
{
    public class Manager<T> : IDisposable where T : ModelBase, new()
    {
        protected readonly DBContext context;

        public Manager(DBContext context)
        {
            this.context = context;
        }

        public async Task<T> GetItem(int id)
        {
            return await context.Connection.GetAsync<T>(id);
        }

        public async Task<T> GetItemWithChildren(int id, bool recursive = false)
        {
            return await context.Connection.GetWithChildrenAsync<T>(id, recursive: recursive);
        }

        public async Task<List<T>> GetItems()
        {
            return await context.Connection.Table<T>().ToListAsync();
        }

        public async Task<List<T>> GetItemsWithChildren(bool recursive = false)
        {
            return await context.Connection.GetAllWithChildrenAsync<T>(recursive: recursive);
        }

        public async Task<int> Save(T item)
        {
            if (item.Id == 0)
                return await context.Connection.InsertAsync(item);

            return await context.Connection.UpdateAsync(item);
        }

        public async Task<int> Delete(int id)
        {
            return await context.Connection.DeleteAsync<T>(id);
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
