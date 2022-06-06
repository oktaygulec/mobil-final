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
    public partial class KullaniciView : ContentPage
    {
        public KullaniciView(KullaniciViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}