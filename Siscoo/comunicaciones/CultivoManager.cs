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
    class CultivoManager
    {

        const string url = "http://10.0.2.2:3000/api/v1/cultivo";

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

            var result = await client.GetAsync(url + "/GetAllByAsociado");
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = result.StatusCode.ToString();
            apiResponse.message = await result.Content.ReadAsStringAsync();
            Console.WriteLine("code: " + apiResponse.code);
            Console.WriteLine("messe: " + apiResponse.message);
            return apiResponse;
        }

        public async Task<ApiResponse> Add(string token, string nombre, string id_niame, DateTime fecha_inicio_siembra, DateTime fecha_fin_siembra, float hectareas_sembradas, float kg_espera_cosechar, float costo_total_siembra, Boolean estado)
        {
            Cultivo cultivo = new Cultivo()
            {
                id_cultivo = "",
                nombre = nombre,
                niameIdNiame = id_niame,
                fecha_inicio_siembra = fecha_inicio_siembra,
                fecha_fin_siembra = fecha_fin_siembra,
                hectareas_sembradas = hectareas_sembradas,
                kg_espera_cosechar = kg_espera_cosechar,
                costo_total_siembra = costo_total_siembra,
                estado = estado
            };
            HttpClient cliente = await get();
            cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(cultivo), Encoding.UTF8, "application/json"));
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = response.StatusCode.ToString();
            apiResponse.message = await response.Content.ReadAsStringAsync();
            return apiResponse;

        }

        public async Task<ApiResponse> update(string token, string id_cultivo, string id_asociado, string nombre, string id_niame, DateTime fecha_inicio_siembra, DateTime fecha_fin_siembra, float hectareas_sembradas, float kg_espera_cosechar, float costo_total_siembra, Boolean estado)
        {
            Cultivo cultivo = new Cultivo()
            {
                id_cultivo = id_cultivo,
                id_asociado = id_asociado,
                nombre = nombre,
                niameIdNiame = id_niame,
                fecha_inicio_siembra = fecha_inicio_siembra,
                fecha_fin_siembra = fecha_fin_siembra,
                hectareas_sembradas = hectareas_sembradas,
                kg_espera_cosechar = kg_espera_cosechar,
                costo_total_siembra = costo_total_siembra,
                estado = estado
            };
            HttpClient client = await get();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.PutAsync(url + "/UpdateSiembra/" + cultivo.id_cultivo, new StringContent(JsonConvert.SerializeObject(cultivo), Encoding.UTF8, "application/json"));
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = response.StatusCode.ToString();
            apiResponse.message = await response.Content.ReadAsStringAsync();
            return apiResponse;
        }
    }
}
