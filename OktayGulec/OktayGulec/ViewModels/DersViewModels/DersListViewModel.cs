using OktayGulec.DatabaseAccess;
using OktayGulec.Models;
using OktayGulec.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.DersViewModels
{
    public class DersListViewModel : ViewModelBase
    {
        private ObservableCollection<Ders> _items;
        public ObservableCollection<Ders> Items { get => _items; set => SetProperty(ref _items, value); }

        public INavigation Navigation { get; set; }

        public Command InsertCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public DersListViewModel(INavigation navigation)
        {
            Navigation = navigation;

            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Ders>(OnUpdate);
            DeleteCommand = new Command<Ders>(OnDelete);
        }

        public async Task GetItems()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Items = new ObservableCollection<Ders>(await uow.DersManager.GetItemsWithChildren(true));
            }
        }

        private async void OnInsert()
        {
            var dvm = new DersViewModel(new Ders());
            await dvm.GetHocalar();
            await Navigation.PushModalAsync(new DersView(dvm));

            MessagingCenter.Subscribe<DersViewModel, Ders>(this, "OnOk", async (vm, Ders) =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.DersManager.Save(Ders) > 0)
                    {
                        await Navigation.PopModalAsync();
                        await GetItems();
                    }
                }

                MessagingCenter.Unsubscribe<DersViewModel, Ders>(this, "OnOk");
                MessagingCenter.Unsubscribe<DersViewModel>(this, "OnCancel");
            });

            MessagingCenter.Subscribe<DersViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<DersViewModel, Ders>(this, "OnOk");
                MessagingCenter.Unsubscribe<DersViewModel>(this, "OnCancel");
            });
        }

        private async void OnUpdate(Ders item)
        {
            if (item == null) return;

            var dvm = new DersViewModel(item);
            await dvm.GetHocalar();
            await Navigation.PushModalAsync(new DersView(dvm));

            MessagingCenter.Subscribe<DersViewModel, Ders>(this, "OnOk", async (vm, Ders) =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.DersManager.Save(Ders) > 0)
                    {
                        await Navigation.PopModalAsync();
                        await GetItems();
                    }
                }

                MessagingCenter.Unsubscribe<DersViewModel, Ders>(this, "OnOk");
                MessagingCenter.Unsubscribe<DersViewModel>(this, "OnCancel");
            });

            MessagingCenter.Subscribe<DersViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<DersViewModel, Ders>(this, "OnOk");
                MessagingCenter.Unsubscribe<DersViewModel>(this, "OnCancel");
            });
        }

        private async void OnDelete(Ders item)
        {
            if (item == null) return;

            bool result = await Application.Current.MainPage.DisplayAlert("Ders Sil", item.Ad + " adlı dersi silmek istiyor musunuz?", "EVET", "HAYIR");
            if (!result)
            {
                return;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (await uow.DersManager.Delete(item.Id) > 0)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
