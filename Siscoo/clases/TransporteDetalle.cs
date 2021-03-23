using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class TransporteDetalle
    {
        public string id { get; set; }
        public string transporteId { get; set; }
        public string niameIdNiame { get; set; }
        public float cantidad { get; set; }
        public Niame niame { get; set; }
        public Boolean estado { get; set; }
    }
}
