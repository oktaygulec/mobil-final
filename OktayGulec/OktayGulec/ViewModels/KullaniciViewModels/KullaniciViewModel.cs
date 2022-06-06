using OktayGulec.Models;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.KullaniciViewModels
{
    public class KullaniciViewModel : ViewModelBase
    {
        private Kullanici _kullanici;
        public Kullanici Kullanici { get => _kullanici; set => SetProperty(ref _kullanici, value); }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public KullaniciViewModel(Kullanici kullanici)
        {
            this.Kullanici = kullanici;

            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        private void OnOk()
        {
            MessagingCenter.Send<KullaniciViewModel, Kullanici>(this, "OnOk", Kullanici);
        }

        private void OnCancel()
        {
            MessagingCenter.Send<KullaniciViewModel>(this, "OnCancel");
        }
    }
}