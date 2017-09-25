using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
    public  class VENTA_ARTICULO_DESCUENTO
    {
       
            public int IdPosicion { get; set; }
            public int IdOrden { get; set; }
            public string FPago { get; set; }
            public string FPagoDetalle { get; set; }
            public double ImporteBase { get; set; }
            public double ImporteDto { get; set; }
            public double ImporteDtoPor { get; set; }
            public Nullable<int> PromocionId { get; set; }
            public string PromocionName { get; set; }
            public string PromocionTarjeta { get; set; }
            public Nullable<int> PromocionPuntos { get; set; }
            public string Visto_Descuento { get; set; }
            public string NumCertificado { get; set; }
          
    
    }
}
