using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OktayGulec.Models
{
    [Table("Hocalar")]
    public class Hoca : ModelBase
    {
        private string _ad;
        public string Ad 
        { 
            get => _ad;
            set
            {
                SetProperty(ref _ad, value);
                OnPropertyChanged("AdSoyad");
            }
        }

        private string _soyad;
        public string Soyad
        {
            get => _soyad;
            set
            {
                SetProperty(ref _soyad, value);
                OnPropertyChanged("AdSoyad");
            }
        }

        [Ignore]
        public string AdSoyad { get => Ad + " " + Soyad; }
    }
}
