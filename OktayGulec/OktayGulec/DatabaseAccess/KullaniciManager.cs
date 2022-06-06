using OktayGulec.Models;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OktayGulec.DatabaseAccess
{
    public class KullaniciManager : Manager<Kullanici>
    {
        public KullaniciManager(DBContext context) : base(context) {}

        public async Task<bool> Login(string kullaniciAdi, string parola)
        {
            Kullanici kullanici = await context.Connection.Table<Kullanici>().FirstOrDefaultAsync(k => k.KullaniciAdi == kullaniciAdi && k.Parola == parola);

            return kullanici != null ? true : false;
        }

        public async Task<bool> IsUserExists(string kullaniciAdi)
        {
            var kullanici = await context.Connection.Table<Kullanici>().FirstOrDefaultAsync(k => k.KullaniciAdi == kullaniciAdi);
            return kullanici != null ? true : false;
        }
    }
}
