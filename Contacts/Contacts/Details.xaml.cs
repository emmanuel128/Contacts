﻿using Contacts.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Details : ContentPage
	{
        public event EventHandler<Contact> ContactAdded;
        public event EventHandler<Contact> ContactUpdated;

        public Details (Contact contact)
		{
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

			InitializeComponent ();

            BindingContext = new Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                Email = contact.Email,
                IsBlocked = contact.IsBlocked,
                Status = contact.Status
            };
        }

        async private void Save_Clicked(object sender, EventArgs e)
        {
            var contact = BindingContext as Contact;

            if (String.IsNullOrWhiteSpace(contact.FullName))
            {
                await DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            if (contact.Id == 0)
            {
                // This is just a temporary hack to differentiate between a
                // new and an existing Contact object. In the next section, 
                // we'll store these Contact objects in a database. So, they
                // will automaticlaly get an Id.
                contact.Id = 1;

                // This is null-conditional operator in C#. It is the same as:
                // 
                // if (ContactAdded != null)
                // 		ContactAdded(this, contact);
                //
                // Read my blog post for more details:
                // http://programmingwithmosh.com/csharp/csharp-6-features-that-help-you-write-cleaner-code/
                //
                ContactAdded?.Invoke(this, contact);
            }
            else
            {
                ContactUpdated?.Invoke(this, contact);
            }

            await Navigation.PopAsync();
        }
    }
}