using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Siscoo.clases;

namespace Siscoo.comunicaciones
{
    class NiameManager
    {

        const string url = "http://10.0.2.2/apicooagropas/api_niame.php";

        private async Task<HttpClient> get()
        {

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<List<Niame>> GetAll()
        {


            HttpClient client = await get();
            string result = await client.GetStringAsync(url + "?id=");

            return JsonConvert.DeserializeObject<List<Niame>>(result);
        }

        public async Task<Niame> Add(int id_asignatura, string titulo, string describcion, Boolean estado, DateTime fecha_creacion, DateTime fecha_entrega)
        {
            Niame niame = new Niame();
            HttpClient cliente = await get();
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(niame), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Niame>(await response.Content.ReadAsStringAsync());

        }
    }
}

