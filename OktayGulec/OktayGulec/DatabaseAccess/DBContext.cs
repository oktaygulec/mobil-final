using OktayGulec.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OktayGulec.DatabaseAccess
{
    public class DBContext : IDisposable
    {
        protected readonly SQLiteAsyncConnection _connection;

        public SQLiteAsyncConnection Connection { get => _connection; }

        public DBContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "OktayGulecDB.db3");
            _connection = new SQLiteAsyncConnection(path);
        }

        public async Task Init()
        {
            await Connection.CreateTablesAsync<Kullanici, Hoca, Ders>();

            if(await Connection.Table<Kullanici>().CountAsync() == 0)
                await Connection.InsertAsync(new Kullanici { KullaniciAdi = "admin", Parola = "123456", });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
