using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestorPromocionesModel
{

    public class Promocion
    {

        public int Id_Articulo {get;set;}
        public int Id_cabecero_detalle { get; set; }
        public double Pvp_Vig  { get; set; }
        public double Pvp_Or  { get; set; }
        public double Pvp_Venta { get; set; }
        public Nullable<double> DtoEuro { get; set; }
        public int NumeroLineaRecalculo { get; set; }
        public TipoAccion Tipo { get; set; }
        public Nullable<int> ClienteID { get; set; }
        public string Id_Tienda { get; set; }
        public string Unidades { get; set; }
        public DateTime FSesion { get; set; }
        public Nullable<double> TotalVenta { get; set; }
        public IEnumerable<DetallePromo> Promo { get; set; }
       
    }
    public enum TipoAccion
    {
        VENTA = 1,
        RECALCULO = 2
    }

    public class DetallePromo
    {

        public Nullable<int> Idpromo { get; set; }
        public Nullable<double> DtoPromo { get; set; }
        public string DescriPromo { get; set; }
        public string DescriAmpliaPromo { get; set; }
        public bool PromocionSelecionada { get; set; }
    }

}
