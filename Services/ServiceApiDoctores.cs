using ApiCrudCoreDoctores.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiClientDoctores.Services
{
    public class ServiceApiDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiDoctores(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiDoctores");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>>
            GetDoctoresAsync()
        {
            string request = "api/doctores";
            List<Doctor> data = await this.CallApiAsync<List<Doctor>>(request);
            return data;
        }

        public async Task<Doctor> FindDoctorAsync(int idDoctor)
        {
            string request = "api/doctores/" + idDoctor;
            Doctor data = await this.CallApiAsync<Doctor>(request);
            return data;
        }
        public async Task DeleteDoctorAsync(int idDoctor)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores/" + idDoctor;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //No seria necesario
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.DeleteAsync(request);
                //return response.StatusCode;
            }
        }
        public async Task InsertDoctorAsync
            (int id, string apellido, string especialidad, int salario, int idhospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                Doctor doc = new Doctor();
                doc.IdDoctor = id;
                doc.Apellido = apellido;
                doc.Especialidad = especialidad;
                doc.Salario = salario;
                doc.IdHospital = idhospital;
                string json = JsonConvert.SerializeObject(doc);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);

            }
        }
        public async Task UpdateDoctorAsync
            (int id, string apellido, string especialidad, int salario, int idhospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                Doctor doc = new Doctor();
                doc.IdDoctor = id;
                doc.Apellido = apellido;
                doc.Especialidad = especialidad;
                doc.Salario = salario;
                doc.IdHospital = idhospital;
                string json = JsonConvert.SerializeObject(doc);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);

            }
        }
    }
}
