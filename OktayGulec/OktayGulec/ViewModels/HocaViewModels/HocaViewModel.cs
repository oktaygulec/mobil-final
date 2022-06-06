using OktayGulec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.HocaViewModels
{
    public class HocaViewModel : ViewModelBase
    {
        private Hoca _hoca;
        public Hoca Hoca { get => _hoca; set => SetProperty(ref _hoca, value); }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public HocaViewModel(Hoca hoca)
        {
            this.Hoca = hoca;

            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        private void OnOk()
        {
            MessagingCenter.Send<HocaViewModel, Hoca>(this, "OnOk", Hoca);
        }

        private void OnCancel()
        {
            MessagingCenter.Send<HocaViewModel>(this, "OnCancel");
        }
    }
}
