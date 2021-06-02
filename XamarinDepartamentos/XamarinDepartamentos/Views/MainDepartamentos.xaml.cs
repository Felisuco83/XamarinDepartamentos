using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDepartamentos.Code;

namespace XamarinDepartamentos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDepartamentos : MasterDetailPage
    {
        public MainDepartamentos()
        {
            InitializeComponent();
            ObservableCollection<MenuPageItem> menu =
                new ObservableCollection<MenuPageItem>();
            MenuPageItem insertView =
                new MenuPageItem
                {
                    Titulo = "Nuevo departamento"
                ,
                    Tipo = typeof(InsertDepartamentoView)
                };
            menu.Add(insertView);
            MenuPageItem departamentosView =
                new MenuPageItem
                {
                    Titulo = "Departamentos"
                ,
                    Tipo = typeof(DepartamentosView)
                };
            menu.Add(departamentosView);
            this.listviewMenu.ItemsSource = menu;
            Detail =
                new NavigationPage((Page)Activator.CreateInstance
                (typeof(MainPage)));
            this.listviewMenu.ItemSelected += ListviewMenu_ItemSelected;
        }

        private void ListviewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPageItem item =
                e.SelectedItem as MenuPageItem;
            Type tipo = item.Tipo;
            Detail =
                new NavigationPage((Page)Activator.CreateInstance(tipo));
            IsPresented = false;
        }
    }

}