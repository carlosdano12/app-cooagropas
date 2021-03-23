using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class Transporte
    {
        public string id { get; set; }
        public string asociadoIdAsociado { get; set; }
        public string nota { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaEntrega { get; set; }
        public Boolean estado { get; set; }
        public TransporteDetalle[] transporteDetalles { get; set; }
    }
}
