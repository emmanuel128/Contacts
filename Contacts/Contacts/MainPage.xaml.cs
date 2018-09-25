using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Contacts.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Contacts.Interfaces;
using Acr.UserDialogs;

namespace Contacts
{
	public partial class MainPage : ContentPage
	{
        private ObservableCollection<Customer> _customers;

		public MainPage()
		{
			InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Gradient))
            {
                _customers = new ObservableCollection<Customer>(await App.CustomerManager.GetTasksAsync());
                var config = new ToastConfig("Finished")
                {
                    Duration = TimeSpan.FromSeconds(2.5),
                    Message = "Get customers",
                    BackgroundColor = Color.DimGray
                };
                UserDialogs.Instance.Toast(config);
                //var date = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig() { Title = "Select Date", CancelText = "Cancel", OkText = "Set" });
            }
            //UserDialogs.Instance.ShowLoading("test", MaskType.Gradient);
            //UserDialogs.Instance.Confirm(new ConfirmConfig { Title = "Title", Message = "Message", OkText = "ok", CancelText = "cancel" });
            //UserDialogs.Instance.Login(new LoginConfig { Title = "Title", CancelText = "Cancel", LoginPlaceholder = "l place", PasswordPlaceholder = "p place" });
            customerList.ItemsSource = _customers;
            if (_customers.Count == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("No Customer Found");
            }
            //await DisplayAlert("Acabó", $"Ya llamé al API y este es el primer récord {_customers[0].FullName}", "OK");
        }

        async private void CustomerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedCustomer = e.SelectedItem as Customer;
            customerList.SelectedItem = null;
            
            var page = new CustomerManage(selectedCustomer);
            await Navigation.PushAsync(page);
        }

        async private void Add_Clicked(object sender, EventArgs e)
        {
            var page = new CustomerManage(null);
            await Navigation.PushAsync(new CustomerManage(null));
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            var _c = (sender as MenuItem).CommandParameter as Customer;
            if (await DisplayAlert("Warning", $"Are you sure you want to delete {_c.FullName}?", "Yes", "No"))
            {
                using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Gradient))
                {
                    var response = await App.CustomerManager.DeleteTaskAsync(_c);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // success
                        DependencyService.Get<IMessage>().ShortAlert("Customer Deleted");
                        _customers.Remove(_c);
                    }
                    else
                    {
                        await DisplayAlert("Error", await response.Content.ReadAsStringAsync(), "OK");
                    }
                }
            }
        }
    }
}
