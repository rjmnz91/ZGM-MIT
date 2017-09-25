using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{
    public class ENTREGA_CARRITO
    {
        public int idCarritoEntrega  {get; set;}
        public int idCarrito { get; set; }
        public string Nombre  {get; set;}
        public string Apellidos {get; set;}
        public string Email {get; set;}
        public string Direccion {get; set;}
        public string NoInterior {get; set;}
        public string NoExterior {get; set;}
        public string Estado {get; set;}
        public string Ciudad {get; set;}
        public string Colonia { get; set; }
        public string CodPostal { get; set; }
        public string TelfMovil { get; set; }
        public string TelfFijo { get; set; }
        public string Referencia {get; set;}
        public string tiendaEnvio {get; set;}
        public int IdCliente { get; set; }
        public int id_Carrito_Detalle { get; set; }
    }

    public class FACTURACION_CARRITO
    {
        public int idCarritoFacturacion { get; set; }
        public int idCarrito { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string NoInterior { get; set; }
        public string NoExterior { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Colonia { get; set; }
        public string CodPostal { get; set; }
        public string TelfMovil { get; set; }
        public string TelfFijo { get; set;}
        public string Pais { get; set; }
    }

}
