using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Siscoo.clases;

namespace Siscoo.comunicaciones
{
    class CultivoManager
    {

        const string url = "http://10.0.2.2/apicooagropas/api_cultivos.php";

        private async Task<HttpClient> get()
        {

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<List<Cultivo>> GetAll(int id)
        {


            HttpClient client = await get();
            string result = await client.GetStringAsync(url + "?id=" + id);

            return JsonConvert.DeserializeObject<List<Cultivo>>(result);
        }

        public async Task<Cultivo> Add(int id_asociado, string nombre, int id_niame, DateTime fecha_inicio_siembra, DateTime fecha_fin_siembra, float hectareas_sembradas, float kg_espera_cosechar, float costo_total_siembra, Boolean estado)
        {
            Cultivo cultivo = new Cultivo()
            {
                id_cultivo = 0,
                id_asociado = id_asociado,
                nombre = nombre,
                id_niame = id_niame,
                fecha_inicio_siembra = fecha_inicio_siembra,
                fecha_fin_siembra = fecha_fin_siembra,
                hectareas_sembradas = hectareas_sembradas,
                kg_espera_cosechar = kg_espera_cosechar,
                costo_total_siembra = costo_total_siembra,
                estado = estado
            };
            HttpClient cliente = await get();
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(cultivo), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Cultivo>(await response.Content.ReadAsStringAsync());

        }

        public async Task<Cultivo> update(int id_cultivo, int id_asociado, string nombre, int id_niame, DateTime fecha_inicio_siembra, DateTime fecha_fin_siembra, float hectareas_sembradas, float kg_espera_cosechar, float costo_total_siembra, Boolean estado)
        {
            Cultivo cultivo = new Cultivo()
            {
                id_cultivo = id_cultivo,
                id_asociado = id_asociado,
                nombre = nombre,
                id_niame = id_niame,
                fecha_inicio_siembra = fecha_inicio_siembra,
                fecha_fin_siembra = fecha_fin_siembra,
                hectareas_sembradas = hectareas_sembradas,
                kg_espera_cosechar = kg_espera_cosechar,
                costo_total_siembra = costo_total_siembra,
                estado = estado
            };
            HttpClient client = await get();
            var response = await client.PutAsync(url + "?id_cultivo=" + cultivo.id_cultivo, new StringContent(JsonConvert.SerializeObject(cultivo), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Cultivo>(await response.Content.ReadAsStringAsync());
        }
    }
}
