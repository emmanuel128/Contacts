using Contacts.Models;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Contacts.Interfaces;

namespace Contacts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerManage : ContentPage
	{
        private string customerId = string.Empty;

		public CustomerManage (Customer customer)
		{
			InitializeComponent ();
            if(customer != null)
            {
                customerId = customer.CustomerID;
            }
            else
            {
                BindingContext = new Customer();
                deleteBtn.IsVisible = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                var _customer = await App.CustomerManager.GetByIdTaskAsync(customerId);
                BindingContext = _customer;
                //await DisplayAlert("Acabó", $"Ya llamé al API y este es el primer récord {_customer.FullName}", "OK");
            }
        }

        private async void SaveBtn_Pressed(object sender, EventArgs e)
        {
            var _customer = BindingContext as Customer;
            var response = await App.CustomerManager.SaveTaskAsync(_customer, string.IsNullOrWhiteSpace(customerId));
            if(response.StatusCode == HttpStatusCode.OK)
            {
                // success
                DependencyService.Get<IMessage>().ShortAlert("Customer Updated");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", await response.Content.ReadAsStringAsync(), "OK");
            }
        }

        private async void DeleteBtn_Pressed(object sender, EventArgs e)
        {
            var _c = BindingContext as Customer;
            var response = await App.CustomerManager.DeleteTaskAsync(BindingContext as Customer);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // success
                DependencyService.Get<IMessage>().ShortAlert("Customer Deleted");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", await response.Content.ReadAsStringAsync(), "OK");
            }
        }
    }
}