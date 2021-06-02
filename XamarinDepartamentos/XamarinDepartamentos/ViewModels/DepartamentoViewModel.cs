using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinDepartamentos.Base;
using XamarinDepartamentos.Models;
using XamarinDepartamentos.Services;

namespace XamarinDepartamentos.ViewModels
{
    public class DepartamentoViewModel: ViewModelBase
    {
        ServiceDepartamentos ServiceDepartamentos;

        public DepartamentoViewModel(ServiceDepartamentos service)
        {
            this.ServiceDepartamentos = service;
            if (this.Departamento == null)
            {
                this.Departamento = new Departamento();
            }
        }
        private Departamento _Departamento;
        public Departamento Departamento
        {
            get { return this._Departamento; }
            set
            {
                this._Departamento = value;
                OnPropertyChanged("Departamento");
            }
        }

        public Command EliminarDepartamento
        {
            get
            {
                return new Command(async () =>
                {
                    await this.ServiceDepartamentos.DeleteDepartamento(this.Departamento.IdDepartamento);
                    MessagingCenter.Send(App.ServiceLocator.DepartamentosViewModel, "RELOAD");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
        public Command ModificarDepartamento
        {
            get
            {
                return new Command(async () =>
                {
                    await this.ServiceDepartamentos.UpdateDepartamento(this.Departamento.IdDepartamento, this.Departamento.Nombre,
                        this.Departamento.Localidad);
                    MessagingCenter.Send(App.ServiceLocator.DepartamentosViewModel, "RELOAD");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        public Command InsertarDepartamento
        {
            get
            {
                return new Command(async () =>
                {
                    await this.ServiceDepartamentos.InsertDepartamentos(this.Departamento.IdDepartamento, this.Departamento.Nombre,
                        this.Departamento.Localidad);
                    MessagingCenter.Send(App.ServiceLocator.DepartamentosViewModel, "RELOAD");
                    await Application.Current.MainPage.DisplayAlert("Alert", "Departamento insertado", "OK");
                });
            }
        }
    }
}
