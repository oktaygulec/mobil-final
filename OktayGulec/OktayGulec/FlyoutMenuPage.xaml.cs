using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OktayGulec
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : ContentPage
    {
        public ListView FlyoutMenuListView { get; private set; }

        public FlyoutMenuPage()
        {
            InitializeComponent();
            FlyoutMenuListView = flyoutMenuListView;
        }
    }
}