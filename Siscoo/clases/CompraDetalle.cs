using System;
using System.Collections.Generic;
using System.Text;

namespace Siscoo.clases
{
    public class CompraDetalle
    {
        public string id { get; set; }
        public string compraEncabezadoId { get; set; }
        public string insumoId { get; set; }
        public float cantidad { get; set; }
        public Insumo insumo { get; set; }
        public Boolean estado { get; set; }
    }
}
