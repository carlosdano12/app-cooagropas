using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class DiaControlInsumo
    {
        public string id { get; set; }
        public string diaControlId { get; set; }
        public string insumoId { get; set; }
        public float cantidad { get; set; }
        public Insumo insumo { get; set; }
    }
}
