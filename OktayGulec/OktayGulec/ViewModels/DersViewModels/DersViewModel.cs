using OktayGulec.DatabaseAccess;
using OktayGulec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.DersViewModels
{
    public class DersViewModel : ViewModelBase
    {
        private Ders _ders;
        public Ders Ders { get => _ders; set => SetProperty(ref _ders, value); }

        public ObservableCollection<Hoca> Hocalar { get; set; }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public DersViewModel(Ders Ders)
        {
            this.Ders = Ders;

            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        public async Task GetHocalar()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Hocalar = new ObservableCollection<Hoca>(await uow.HocaManager.GetItems());
            }
        }

        private void OnOk()
        {
            Ders.HocaId = Ders.Hoca.Id;
            MessagingCenter.Send<DersViewModel, Ders>(this, "OnOk", Ders);
        }

        private void OnCancel()
        {
            MessagingCenter.Send<DersViewModel>(this, "OnCancel");
        }
    }
}
