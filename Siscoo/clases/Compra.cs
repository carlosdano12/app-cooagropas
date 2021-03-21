using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class Compra
    {
        public string id { get; set; }
        public string asociadoIdAsociado { get; set; }
        public string nota { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaCompra { get; set; }
        public Boolean estado { get; set; }
        public CompraDetalle[] compraDetalles { get; set; }

    }
}
