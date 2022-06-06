using OktayGulec.DatabaseAccess;
using OktayGulec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.KullaniciViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private Kullanici _kullanici = new Kullanici();
        public Kullanici Kullanici { get => _kullanici; set => SetProperty(ref _kullanici, value); }

        private string _errorText = "";
        public string ErrorText { get => _errorText; set => SetProperty(ref _errorText, value); }

        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if (await uow.KullaniciManager.Login(Kullanici.KullaniciAdi, Kullanici.Parola))
                {
                    ErrorText = "";
                    MessagingCenter.Send<LoginViewModel>(this, "OnLogin");
                }
                else
                {
                    ErrorText = "Kullanıcı adı veya parola hatalı.";
                }
            }
        }
    }
}
