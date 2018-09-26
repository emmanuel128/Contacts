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
    public partial class MainDrawer : MasterDetailPage
    {
        public MainDrawer()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainDrawerMenuItem;
            if (item == null)
                return;
            switch (item.Id)
            {
                case 1:
                    Constants.RestUrl = Endpoints.Nodejs;
                    break;
                case 2:
                    Constants.RestUrl = Endpoints.DotNet;
                    break;
            }
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}