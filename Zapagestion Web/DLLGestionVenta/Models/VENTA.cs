using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
   public class VENTA
    {
        public string Id_Ticket { get; set; }
        public string Id_Tienda { get; set; }
       
        public Nullable<int> Id_Empleado { get; set; }
        public Nullable<int> Id_Abonado_Empleado { get; set; }
        public Nullable<int> Id_Pago { get; set; }
        public System.DateTime Fecha { get; set; }
        public string NombreTarjeta { get; set; }
        public double TotalEuro { get; set; }
        public Nullable<double> cEfectivoEuro { get; set; }
        public Nullable<double> cValeEuro { get; set; }
        public Nullable<double> cTarjetaEuro { get; set; }
        public Nullable<double> cCuentaEuro { get; set; }
        public Nullable<double> TotalParaValeEuro { get; set; }
        public Nullable<double> cRecibidoEuro { get; set; }
        public double DescuentoEuro { get; set; }
        public Nullable<double> ComisionVenta { get; set; }
        public int Id_Cliente_N { get; set; }
        public string ID_TERMINAL { get; set; }
        public string Id_SubPago { get; set; }
        public Nullable<int> IdCajero { get; set; }
        public string NumFactura { get; set; }
        public string Comentarios { get; set; }
        public string IdMonedaCliente { get; set; }
        public string IdMonedaTienda { get; set; }
        public string CIFSociedad { get; set; }
        public string TipoFactura { get; set; }
        public string CodPostal { get; set; }
        public Nullable<decimal> CambioTienda { get; set; }
        public bool DeslizaTarjeta { get; set; }

        public IEnumerable<VENTA_DETALLE> V_DETALLE  { get; set; }
        public IEnumerable<VENTA_PAGOS> V_PAGO  { get; set; }
        public CLIENTE9 CLient9 { get; set; }
        public IEnumerable<NTRANS> TRANS { get; set; }
           
    }

   public class NTRANS
   {
       public System.DateTime FechaSesion { get; set; }
       public string Concepto { get; set; }
       public string NTicket { get; set; }
       public Nullable<double> Importe { get; set; }
   }  
}
