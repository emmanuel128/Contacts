using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Contacts.Serializers;
using System.Collections.ObjectModel;

namespace Contacts
{
	public partial class MainPage : ContentPage
	{
        public ObservableCollection<Contact> _contacts;
		public MainPage()
		{
			InitializeComponent();
            _contacts = new ObservableCollection<Contact>()
            {
                new Contact {Id = 1, Email = "test1@test.com", FirstName="First Name 1", LastName="Last Name 1", IsBlocked = false, Phone = "787-787-7878", Status="Status 1" },
                new Contact {Id = 2, Email = "test2@test.com", FirstName="First Name 2", LastName="Last Name 2", IsBlocked = true, Phone = "787-787-7878", Status="Status 1" }
            };
            contactsList.ItemsSource = _contacts;
        }

        async private void ContactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedContact = e.SelectedItem as Contact;
            contactsList.SelectedItem = null;

            var page = new Details(selectedContact);
            page.ContactUpdated += (source, contact) =>
            {
                // When the target page raises ContactUpdated event, we get 
                // notified and update properties of the selected contact. 
                // Here we are dealing with a small class with only a few 
                // properties. If working with a larger class, you may want 
                // to look at AutoMapper, which is a convention-based mapping
                // tool. 
                var item = _contacts.FirstOrDefault(f => f.Id == contact.Id);
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
        }

        async private void Add_Clicked(object sender, EventArgs e)
        {
            var page = new Details(new Contact());

            // We can subscribe to the ContactAdded event using a lambda expression.
            // If you're not familiar with this syntax, watch my C# Advanced course. 
            page.ContactAdded += (source, contact) =>
            {
                // ContactAdded event is raised when the user taps the Done button.
                // Here, we get notified and add this contact to our 
                // ObservableCollection.
                _contacts.Add(contact);
            };
            await Navigation.PushAsync(new Details(new Contact()));
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            var contact = (sender as MenuItem).CommandParameter as Contact;
            if (await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
                _contacts.Remove(contact);
        }
    }
}
