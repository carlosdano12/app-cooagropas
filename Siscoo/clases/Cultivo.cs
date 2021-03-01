using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class Cultivo
    {
        public string id_cultivo { get; set; }
        public string id_asociado { get; set; }
        public string nombre { get; set; }
        public string id_niame { get; set; }
        public DateTime fecha_inicio_siembra;
        public DateTime fecha_fin_siembra { get; set; }
        public float hectareas_sembradas { get; set; }
        public float kg_espera_cosechar { get; set; }
        public float costo_total_siembra { get; set; }
        public Boolean estado { get; set; }
    }
}
