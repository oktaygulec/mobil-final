using OktayGulec.Models;
using OktayGulec.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OktayGulec
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();

            var items = new List<FlyoutMenuItem>
            {
                new FlyoutMenuItem{ Id = 1, Text = "Ders Listesi", TargetType = typeof(DersListView) },
                new FlyoutMenuItem{ Id = 2, Text = "Hoca Listesi", TargetType = typeof(HocaListView) },
                new FlyoutMenuItem{ Id = 3, Text = "Kullanıcı Listesi", TargetType = typeof(KullaniciListView) },
            };

            flyoutMenuPage.FlyoutMenuListView.ItemsSource = items;


            flyoutMenuPage.FlyoutMenuListView.ItemSelected += FlyoutMenuListView_ItemSelected; ;
        }

        private void FlyoutMenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            FlyoutMenuItem item = e.SelectedItem as FlyoutMenuItem;
            this.Detail = new NavigationPage((ContentPage)Activator.CreateInstance(item.TargetType));
            IsPresented = false;
        }
    }
}
