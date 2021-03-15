using System;
using System.Collections.Generic;
using System.Text;
using Siscoo.clases;

namespace Siscoo.clases
{
    public class DiaControl
    {
        public string id { get; set; }
        public DateTime fechaControl { get; set; }
        public string descripcion { get; set; }
        public string cultivoIdCultivo { get; set; }
        public DiaControlInsumo[] diasControlInsumos { get; set; }
    }
}
