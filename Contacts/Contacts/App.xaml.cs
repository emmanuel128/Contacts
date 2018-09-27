using Contacts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contacts
{
	public partial class App : Application
	{
        public static CustomerManager CustomerManager { get; private set; }

        public App ()
		{
			InitializeComponent();
            CustomerManager = new CustomerManager(new RestService());
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new MainDrawer();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
