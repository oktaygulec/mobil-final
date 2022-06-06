using OktayGulec.DatabaseAccess;
using OktayGulec.Models;
using OktayGulec.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OktayGulec.ViewModels.KullaniciViewModels
{
    public class KullaniciListViewModel : ViewModelBase
    {
        private ObservableCollection<Kullanici> _items;
        public ObservableCollection<Kullanici> Items { get => _items; set => SetProperty(ref _items, value); }

        public INavigation Navigation { get; set; }

        public Command InsertCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public KullaniciListViewModel(INavigation navigation)
        {
            Navigation = navigation;

            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Kullanici>(OnUpdate);
            DeleteCommand = new Command<Kullanici>(OnDelete);
        }

        public async Task GetItems()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Items = new ObservableCollection<Kullanici>(await uow.KullaniciManager.GetItems());
            }
        }

        private async void OnInsert()
        {
            await Navigation.PushModalAsync(new KullaniciView (new KullaniciViewModel(new Kullanici())));

            MessagingCenter.Subscribe<KullaniciViewModel, Kullanici>(this, "OnOk", async (vm, kullanici) =>
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.KullaniciManager.IsUserExists(kullanici.KullaniciAdi))
                        await Application.Current.MainPage.DisplayAlert("Kullanıcı Ekle", kullanici.KullaniciAdi + " adlı kullanıcı zaten mevcut.", "TAMAM");
                    else
                    {
                        if (await uow.KullaniciManager.Save(kullanici) > 0)
                        {
                            await Navigation.PopModalAsync();
                            await GetItems();
                            MessagingCenter.Unsubscribe<KullaniciViewModel, Kullanici>(this, "OnOk");
                            MessagingCenter.Unsubscribe<KullaniciViewModel>(this, "OnCancel");
                        }
                    }
                }

            });

            MessagingCenter.Subscribe<KullaniciViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<KullaniciViewModel, Kullanici>(this, "OnOk");
                MessagingCenter.Unsubscribe<KullaniciViewModel>(this, "OnCancel");
            });
        }

        private async void OnUpdate(Kullanici item)
        {
            if (item == null) return;

            await Navigation.PushModalAsync(new KullaniciView(new KullaniciViewModel(item)));

            MessagingCenter.Subscribe<KullaniciViewModel, Kullanici>(this, "OnOk", async (vm, kullanici) =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (await uow.KullaniciManager.IsUserExists(kullanici.KullaniciAdi))
                        await Application.Current.MainPage.DisplayAlert("Kullanıcı Güncelle", kullanici.KullaniciAdi + " adlı kullanıcı zaten mevcut.", "TAMAM");
                    else
                    {
                        if (await uow.KullaniciManager.Save(kullanici) > 0)
                        {
                            await Navigation.PopModalAsync();
                            await GetItems();
                            MessagingCenter.Unsubscribe<KullaniciViewModel, Kullanici>(this, "OnOk");
                            MessagingCenter.Unsubscribe<KullaniciViewModel>(this, "OnCancel");
                        }
                    }
                }
            });

            MessagingCenter.Subscribe<KullaniciViewModel>(this, "OnCancel", async (vm) =>
            {
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<KullaniciViewModel, Kullanici>(this, "OnOk");
                MessagingCenter.Unsubscribe<KullaniciViewModel>(this, "OnCancel");
            });
        }

        private async void OnDelete(Kullanici item)
        {
            if (item == null) return;

            bool result = await Application.Current.MainPage.DisplayAlert("Kullanıcı Sil", item.KullaniciAdi + " adlı kullanıcıyı silmek istiyor musunuz?", "EVET", "HAYIR");
            if (!result)
            {
                return;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                if(await uow.KullaniciManager.Delete(item.Id) > 0)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
