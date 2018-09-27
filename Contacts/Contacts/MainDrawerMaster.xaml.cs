using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDrawerMaster : ContentPage
    {
        public ListView ListView;

        public MainDrawerMaster()
        {
            InitializeComponent();

            var viewModel = new MainDrawerMasterViewModel();
            BindingContext = viewModel;
            ListView = MenuItemsListView;
            //ListView.SelectedItem = viewModel.MenuItems.FirstOrDefault();
        }

        class MainDrawerMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainDrawerMenuItem> MenuItems { get; set; }
            
            public MainDrawerMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainDrawerMenuItem>(new[]
                {
                    new MainDrawerMenuItem { Id = 1, Title = "Node.js", TargetType = typeof(MainPage) },
                    new MainDrawerMenuItem { Id = 2, Title = ".NET", TargetType = typeof(MainPage) },
                    //new MainDrawerMenuItem { Id = 2, Title = "Page 3" },
                    //new MainDrawerMenuItem { Id = 3, Title = "Page 4" },
                    //new MainDrawerMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}