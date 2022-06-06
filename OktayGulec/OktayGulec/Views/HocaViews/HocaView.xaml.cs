using OktayGulec.ViewModels.HocaViewModels;
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
    public partial class HocaView : ContentPage
    {
        public HocaView(HocaViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}