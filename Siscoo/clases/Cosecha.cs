using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class Cosecha
    {
        public int id_cultivo { get; set; }
        public int id_asociado { get; set; }
        public DateTime fecha_inicio_cosecha { get; set; }
        public DateTime fecha_fin_cosecha { get; set; }
        public float kg_cosechados { get; set; }
        public float kg_cosechados_bien { get; set; }
        public float costo_total_cosecha { get; set; }
    }
}
