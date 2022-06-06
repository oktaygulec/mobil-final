using OktayGulec.DatabaseAccess;
using OktayGulec.Models;
using OktayGulec.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.HocaViewModels
{
    public class HocaListViewModel : ViewModelBase
    {
        private ObservableCollection<Hoca> _items;
        public ObservableCollection<Hoca> Items { get => _items; set => SetProperty(ref _items, value); }

        public INavigation Navigation { get; set; }

        public Command InsertCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public HocaListViewModel(INavigation navigation)
        {
            Navigation = navigation;

            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Hoca>(OnUpdate);
            DeleteCommand = new Command<Hoca>(OnDelete);
        }

        public async Task GetItems()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Items = new ObservableCollection<Hoca>(await uow.HocaManager.GetItems());
            }
        }

        private async void OnInsert()
        {
            await Navigation.PushModalAsync(new HocaView(new HocaViewModel(new Hoca())));

            MessagingCenter.Subscribe<HocaViewModel, Hoca>(this, "OnOk", async (vm, Hoca) =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.HocaManager.Save(Hoca) > 0)
                    {
                        await Navigation.PopModalAsync();
                        await GetItems();
                    }
                }

                MessagingCenter.Unsubscribe<HocaViewModel, Hoca>(this, "OnOk");
                MessagingCenter.Unsubscribe<HocaViewModel>(this, "OnCancel");
            });

            MessagingCenter.Subscribe<HocaViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<HocaViewModel, Hoca>(this, "OnOk");
                MessagingCenter.Unsubscribe<HocaViewModel>(this, "OnCancel");
            });
        }

        private async void OnUpdate(Hoca item)
        {
            if (item == null) return;

            await Navigation.PushModalAsync(new HocaView(new HocaViewModel(item)));

            MessagingCenter.Subscribe<HocaViewModel, Hoca>(this, "OnOk", async (vm, Hoca) =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.HocaManager.Save(Hoca) > 0)
                    {
                        await Navigation.PopModalAsync();
                        await GetItems();
                    }
                }

                MessagingCenter.Unsubscribe<HocaViewModel, Hoca>(this, "OnOk");
                MessagingCenter.Unsubscribe<HocaViewModel>(this, "OnCancel");
            });

            MessagingCenter.Subscribe<HocaViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<HocaViewModel, Hoca>(this, "OnOk");
                MessagingCenter.Unsubscribe<HocaViewModel>(this, "OnCancel");
            });
        }

        private async void OnDelete(Hoca item)
        {
            if (item == null) return;

            bool result = await Application.Current.MainPage.DisplayAlert("Hoca Sil", item.AdSoyad + " adlı hocayı silmek istiyor musunuz?", "EVET", "HAYIR");
            if (!result)
            {
                return;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (await uow.HocaManager.Delete(item.Id) > 0)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
