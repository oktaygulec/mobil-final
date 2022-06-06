using OktayGulec.ViewModels.DersViewModels;
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
    public partial class DersListView : ContentPage
    {
        public DersListView()
        {
            InitializeComponent();
            BindingContext = new DersListViewModel(Navigation);
        }

        private async void dersListView_Appearing(object sender, EventArgs e)
        {
            await (BindingContext as DersListViewModel).GetItems();
        }
    }
}