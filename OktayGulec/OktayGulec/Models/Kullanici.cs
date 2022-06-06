using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OktayGulec.Models
{
    [Table("Kullanicilar")]
    public class Kullanici : ModelBase
    {
        private string _kullaniciAdi;
        public string KullaniciAdi { get => _kullaniciAdi; set => SetProperty(ref _kullaniciAdi, value); }

        private string _parola;
        public string Parola { get => _parola; set => SetProperty(ref _parola, value); }
    }
}
