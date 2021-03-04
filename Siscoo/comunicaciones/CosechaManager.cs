using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Siscoo.clases;
using Siscoo.dtos;

namespace Siscoo.comunicaciones
{
    class CosechaManager
    {
        const string url = "http://10.0.2.2:3000/api/v1/cultivo_cosecha";

        private async Task<HttpClient> get()
        {

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<ApiResponse> GetAll(string token, string cultivoId)
        {


            HttpClient client = await get();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync(url + "/GetAllByCultivo/" + cultivoId);
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = result.StatusCode.ToString();
            apiResponse.message = await result.Content.ReadAsStringAsync();
            Console.WriteLine("code: " + apiResponse.code);
            Console.WriteLine("messe: " + apiResponse.message);
            return apiResponse;
        }

        public async Task<ApiResponse> Add(string token, string cultivoIdCultivo, string asociadoIdAsociado, DateTime fecha_inicio_cosecha, DateTime fecha_fin_cosecha, float kg_cosechados, float kg_cosechados_bien, float costo_total_cosecha)
        {
            Cosecha cosecha = new Cosecha()
            {
                id = "",
                cultivoIdCultivo = cultivoIdCultivo,
                asociadoIdAsociado = asociadoIdAsociado,
                fecha_inicio_cosecha = fecha_inicio_cosecha,
                fecha_fin_cosecha = fecha_fin_cosecha,
                kg_cosechados = kg_cosechados,
                kg_cosechados_bien = kg_cosechados_bien,
                costo_total_cosecha = costo_total_cosecha
            };
            HttpClient cliente = await get();
            cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(cosecha), Encoding.UTF8, "application/json"));
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = response.StatusCode.ToString();
            apiResponse.message = await response.Content.ReadAsStringAsync();
            return apiResponse;

        }

        public async Task<ApiResponse> update(string token, string id, string cultivoIdCultivo, string asociadoIdAsociado, DateTime fecha_inicio_cosecha, DateTime fecha_fin_cosecha, float kg_cosechados, float kg_cosechados_bien, float costo_total_cosecha)
        {
            Cosecha cosecha = new Cosecha()
            {
                id = id,
                cultivoIdCultivo = cultivoIdCultivo,
                asociadoIdAsociado = asociadoIdAsociado,
                fecha_inicio_cosecha = fecha_inicio_cosecha,
                fecha_fin_cosecha = fecha_fin_cosecha,
                kg_cosechados = kg_cosechados,
                kg_cosechados_bien = kg_cosechados_bien,
                costo_total_cosecha = costo_total_cosecha
            };
            HttpClient client = await get();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.PutAsync(url + "/UpdateCosecha/" + id, new StringContent(JsonConvert.SerializeObject(cosecha), Encoding.UTF8, "application/json"));
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = response.StatusCode.ToString();
            apiResponse.message = await response.Content.ReadAsStringAsync();
            return apiResponse;
        }
    }
}
