using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OktayGulec.Models
{
    [Table("Dersler")]
    public class Ders : ModelBase
    {
        private string _ad;
        public string Ad { get => _ad; set => SetProperty(ref _ad, value); }

        private double _vize;
        public double Vize 
        { 
            get => _vize; 
            set
            {
                SetProperty(ref _vize, value);
                OnPropertyChanged("Ortalama");
            }
        }

        private double _final;
        public double Final
        {
            get => _final;
            set
            {
                SetProperty(ref _final, value);
                OnPropertyChanged("Ortalama");
            }
        }

        private Hoca _hoca;

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Hoca Hoca { get => _hoca; set => SetProperty(ref _hoca, value); }

        [ForeignKey(typeof(Hoca))]
        public int HocaId { get; set; }

        [Ignore]
        public double Ortalama { get => (Vize * 0.4) + (Final * 0.6); }
    }
}
