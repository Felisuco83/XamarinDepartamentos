using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinDepartamentos.Models;

namespace XamarinDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private string url;
        public ServiceDepartamentos()
        {
            this.url = "https://apicruddepartamentoscorepgs.azurewebsites.net/";
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    T data = JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "api/departamentos";
            List<Departamento> depts = await this.CallApiAsync<List<Departamento>>(request);
            return depts;
        }

        public async Task<Departamento> FindDepartamento(int id)
        {
            string request = "api/departamentos" + id;
            Departamento dept = await this.CallApiAsync<Departamento>(request);
            return dept;
        }

        public async Task InsertDepartamentos (int id, string nombre, string localidad)
        {
            Departamento departamento = new Departamento { IdDepartamento = id, Nombre = nombre, Localidad = localidad };
            string json = JsonConvert.SerializeObject(departamento);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                Uri uri = new Uri(this.url + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateDepartamento (int id, string nombre, string localidad)
        {
            Departamento departamento = await this.FindDepartamento(id);
            string json = JsonConvert.SerializeObject(departamento);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                Uri uri = new Uri(this.url + request);
                await client.PutAsync(uri, content);
            }
        }

        public async Task DeleteDepartamento (int id)
        {
            string request = "api/departamentos/" + id;
            Uri uri = new Uri(this.url + request);
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(uri);
            }
        }

    }
}
