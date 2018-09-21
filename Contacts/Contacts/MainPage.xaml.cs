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

namespace Contacts
{
	public partial class MainPage : ContentPage
	{
        private string url = (App.Current as App).url;
        private ObservableCollection<Customer> _customers;
        private HttpClient client = new HttpClient();

		public MainPage()
		{
			InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _customers = new ObservableCollection<Customer>(await App.CustomerManager.GetTasksAsync());
            customerList.ItemsSource = _customers;
            await DisplayAlert("Acabó", $"Ya llamé al API y este es el primer récord {_customers[0].FullName}", "OK");
        }

        async private void CustomerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedContact = e.SelectedItem as Customer;
            customerList.SelectedItem = null;
            /*
            var page = new Details(selectedContact);
            page.ContactUpdated += (source, contact) =>
            {
                // When the target page raises ContactUpdated event, we get 
                // notified and update properties of the selected contact. 
                // Here we are dealing with a small class with only a few 
                // properties. If working with a larger class, you may want 
                // to look at AutoMapper, which is a convention-based mapping
                // tool. 
                var item = _customers.FirstOrDefault(f => f.Id == contact.Id);
                if(item != null)
                {
                    item.Id = contact.Id;
                    item.FirstName = contact.FirstName;
                    item.LastName = contact.LastName;
                    item.Phone = contact.Phone;
                    item.Email = contact.Email;
                    item.IsBlocked = contact.IsBlocked;
                }
            };

            await Navigation.PushAsync(new Details(selectedContact));
            */
        }

        async private void Add_Clicked(object sender, EventArgs e)
        {
            /*
            var page = new Details(new Contact());

            // We can subscribe to the ContactAdded event using a lambda expression.
            // If you're not familiar with this syntax, watch my C# Advanced course. 
            page.ContactAdded += (source, contact) =>
            {
                // ContactAdded event is raised when the user taps the Done button.
                // Here, we get notified and add this contact to our 
                // ObservableCollection.
                _customers.Add(contact);
            };
            await Navigation.PushAsync(new Details(new Contact()));
            */
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            var customer = (sender as MenuItem).CommandParameter as Customer;
            if (await DisplayAlert("Warning", $"Are you sure you want to delete {customer.FullName}?", "Yes", "No"))
                _customers.Remove(customer);
        }
    }
}
