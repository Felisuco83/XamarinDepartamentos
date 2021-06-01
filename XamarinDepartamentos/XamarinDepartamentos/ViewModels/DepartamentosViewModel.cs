using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinDepartamentos.Base;
using XamarinDepartamentos.Models;
using XamarinDepartamentos.Services;

namespace XamarinDepartamentos.ViewModels
{
    public class DepartamentosViewModel: ViewModelBase
    {
        private ServiceDepartamentos ServiceDepartamentos;

        public DepartamentosViewModel(ServiceDepartamentos serviceDepartamentos)
        {
            this.ServiceDepartamentos = serviceDepartamentos;
            Task.Run(async () =>
            {
                await this.CargarDepartamentosAsync();
            });
        }
        private ObservableCollection<Departamento> _Departamentos;
        public ObservableCollection<Departamento> Departamentos
        {
            get { return this._Departamentos; }
            set
            {
                this._Departamentos = value;
                OnPropertyChanged("Departamentos");
            }
        }

        private async Task CargarDepartamentosAsync()
        {
            List<Departamento> lista = await this.ServiceDepartamentos.GetDepartamentosAsync();
            this.Departamentos = new ObservableCollection<Departamento>(lista);
        }

        public Command MostrarDetalles
        {
            get
            {
                return new Command(async(dept) =>
                {
                    Departamento departamento = dept as Departamento;
                    await Application.Current.MainPage.DisplayAlert("Alert", "Detalles" + departamento.Localidad, "OK");
                });
            }
        }

        public Command ModificarDepartamento
        {
            get
            {
                return new Command(async(dept) =>
                {
                    Departamento departamento = dept as Departamento;
                    await Application.Current.MainPage.DisplayAlert
                    ("Alert", "Modificar " + departamento.Localidad, "OK");
                });
            }
        }
    }
}
