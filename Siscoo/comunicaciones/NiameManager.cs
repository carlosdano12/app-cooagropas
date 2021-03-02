using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Siscoo.clases;
using Siscoo.dtos;

namespace Siscoo.comunicaciones
{
    class NiameManager
    {

        const string url = "http://10.0.2.2:3000/api/v1/niame";

        private async Task<HttpClient> get()
        {

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<ApiResponse> GetAll(string token)
        {

            HttpClient client = await get();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync(url);
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = result.StatusCode.ToString();
            apiResponse.message = await result.Content.ReadAsStringAsync();
            Console.WriteLine("code: " + apiResponse.code);
            Console.WriteLine("messe: " + apiResponse.message);
            return apiResponse;
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

