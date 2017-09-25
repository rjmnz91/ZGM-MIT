using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
    public class Carrito_Pago
    {
        public Int64  IdCarrito { get; set; }
        public string TipoPago { get; set; }
        public string TipoPagoDetalle { get; set; }
        public string NumTarjeta { get; set; }
        public float Importe { get; set; }
    }
}
