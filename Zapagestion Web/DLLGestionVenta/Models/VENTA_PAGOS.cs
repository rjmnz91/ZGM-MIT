using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
   public class VENTA_PAGOS
    {
   
          
        public int IdOrden { get; set; }
        public System.DateTime Fecha { get; set; }
        public string OtraDivisa { get; set; }
        public string Divisa { get; set; }
        public Nullable<double> OtraDivisaImporte { get; set; }
        public Nullable<double> OtraDivisaCambio { get; set; }
        public Nullable<int> ValeId { get; set; }
        public string ValeTienda { get; set; }
        public string Visto_Pago { get; set; }
        public Nullable<int> IdFP { get; set; }
        public string RefFP { get; set; }
        public Nullable<int> IdConcesionTienda { get; set; }
        public Nullable<double> CuotaIVA { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
        public string NomTitular { get; set; }
        public string NomEntidad { get; set; }
        public Nullable<System.DateTime> FVencimiento { get; set; }
        public string FPago { get; set; }
        public string FPagoDetalle { get; set; }
        public string Tipo { get; set; }
        public string TipoOrigen { get; set; }
        public Nullable<double> Importe { get; set; }
        public string NumTarjeta { get; set; }
        public string NumTarjetaAutoriza { get; set; }
        public string NumTarjetaOperacion { get; set; }

    }
   
   }

