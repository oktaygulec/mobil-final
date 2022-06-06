using OktayGulec.DatabaseAccess;
using OktayGulec.ViewModels.KullaniciViewModels;
using OktayGulec.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OktayGulec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MessagingCenter.Unsubscribe<LoginViewModel>(this, "OnLogin");
            MessagingCenter.Subscribe<LoginViewModel>(this, "OnLogin", vm => {
                MainPage = new MainPage();
            });
            MainPage = new LoginView();
        }

        protected async override void OnStart()
        {
            using (DBContext context = new DBContext())
            {
                await context.Init();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
