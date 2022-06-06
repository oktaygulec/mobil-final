using OktayGulec.ViewModels.KullaniciViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OktayGulec.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KullaniciListView : ContentPage
    {
        public KullaniciListView()
        {
            InitializeComponent();
            BindingContext = new KullaniciListViewModel(Navigation);
        }

        private async void kullaniciListView_Appearing(object sender, EventArgs e)
        {
            await (BindingContext as KullaniciListViewModel).GetItems();
        }
    }
}