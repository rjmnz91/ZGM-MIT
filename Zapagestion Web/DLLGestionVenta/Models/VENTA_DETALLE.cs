using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
   public class VENTA_DETALLE
    {
        public int Id_Articulo { get; set; }
        public int Id_cabecero_detalle { get; set; }
        public int id_Carrito_Detalle { get; set; }
        public double ImporteEuros { get; set; }
        public short Estado { get; set; }
        public string MotivoCambioPrecio { get; set; }
        public double DtoEuroArticulo { get; set; }
        public Nullable<double> ComisionPremio { get; set; }
        public Nullable<double> ImporteBase { get; set; }
        public string motivo_devolucion { get; set; }
        public string Comentarios { get; set; }
        public Nullable<double> IVA { get; set; }
        public Nullable<double> Pvp_Vig { get; set; }
        public Nullable<double> Pvp_Or { get; set; }
        public int IdPosicion { get; set; }
        public string Asesor { get; set; }
        public Nullable<double> ComisionAsesor { get; set; }
        public Nullable<int> IdConcesionTienda { get; set; }
        public Nullable<double> Coste_Articulo { get; set; }
        public Nullable<int> idAlmacen { get; set; }
        public string Cancelado { get; set; }
        public string CorteArt { get; set; }
        public double ImporteBaseP { get; set; }
        public double DescuentoP { get; set; }
        

        public IEnumerable<VENTA_ARTICULO_DESCUENTO>  Detalle_Descuento { get; set; }
          
    }

   public class IVA
   {
       public int Id_Articulo { get; set; }
       public Nullable<double> Iva { get; set; }
       public int IdGrupo { get; set; }
       public string IVAnombre { get; set; }

   } 

}
