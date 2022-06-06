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
    public partial class HocaListView : ContentPage
    {
        public HocaListView()
        {
            InitializeComponent();
            BindingContext = new HocaListViewModel(Navigation);
        }

        private async void hocaListView_Appearing(object sender, EventArgs e)
        {
            await (BindingContext as HocaListViewModel).GetItems();
        }
    }
}