using System;
using System.Collections.Generic;
using System.Text;
using Siscoo.clases;
using Siscoo.dtos;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Siscoo.comunicaciones
{
    class DiaControlManager
    {
         const string url = "http://10.0.2.2:3000/api/v1/dia-control";

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

            var result = await client.GetAsync(url + "/" + cultivoId);
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.code = result.StatusCode.ToString();
            apiResponse.message = await result.Content.ReadAsStringAsync();
            Console.WriteLine("code: " + apiResponse.code);
            Console.WriteLine("messe: " + apiResponse.message);
            return apiResponse;
        }
    }
}
