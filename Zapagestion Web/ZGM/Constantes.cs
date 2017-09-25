using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Descripción breve de Constantes
/// </summary>

    public struct Constantes
    {
        public struct TipoRelacionCS
        {
            public const char Complementario = 'C';
            public const char Sustitutivo = 'S';
        }

        public struct Session
        {
            public const string Usuario = "Usuario";
            public const string IdEmpleado = "IdEmpleado";
            public const string IdTienda = "IdTienda";
            public const string NombreTienda = "NombreTienda";
            public const string FechaUltimoPedido = "FechaPedido";
            public const string FechaSesion = "FechaSesion";

            // MJM 18/03/2014 INICIO
            // CARRITO
            public const string IdSesion = "IdSesion";
            public const string ClienteNine = "Cliente9";
            public const string objCliente = "objCliente";
            public const string FVENTA = "FVENTA";
            // MJM 18/03/2014 FIN

            public const string IdCompany = "id_company";
            public const string IdBranch = "id_branch";
            public const string CdMerchant = "cd_merchant";
            public const string Country = "country";
            public const string CdUser = "cd_user";
            public const string CdPassword = "password";
            public const string Semilla = "semilla";
            public const string MerchantAMEX = "AMEX";
        }
        public struct Paginas
        {
            public const string Configuracion = "Settings.aspx";
            public const string Buscar = "Buscar.aspx";
            public const string BuscarCliente = "BuscarCliente.aspx";
            public const string Cargos = "Cargos.aspx";
            public const string CargosDetalle = "CargosDetalle.aspx";
            public const string CargosEntrada = "CargosEntrada.aspx";
            public const string ConversionMoneda = "ConversionMoneda.aspx";
            public const string EleccionProducto = "EleccionProducto.aspx";
            public const string Foto = "Foto.aspx";
            public const string Inicio = "Inicio.aspx";
            public const string InventarioDetalle = "InventarioDetalle.aspx";
            public const string Inventario = "Inventarios.aspx";
            public const string Login = "Login.aspx";
            public const string OrdenInventarioDetalle = "OrdenInventarioDetalle.aspx";
            public const string Pedidos = "Pedidos.aspx";
            public const string PedidosDetalle = "PedidosDetalle.aspx";
            public const string PedidosEntrada = "PedidosEntrada.aspx";
            public const string SolicitudesAlmacen = "SolicitudesAlmacen.aspx";
            public const string SolicitudesAlmacenDetalle = "SolicitudesAlmacenDetalle.aspx";
            public const string StockEnTienda = "StockEnTienda.aspx";
            public const string RegistroTerminal = "RegistroTerminal.aspx";
            public const string StockEnOtras = "StockOtras.aspx";
            public const string Carrito = "CarritoDetalle.aspx";
            public const string FinalizarCompra = "FinalizaCompra.aspx";
            public const string ConsultaCliente9 = "ConsultaCliente9.aspx";
            public const string AdmCliente9 = "AdmClienteNine.aspx";
            public const string ActivacTjt9 = "ActivacionTarjeta9.aspx";
            public const string CambioPlastico = "CambioPlastico.aspx";
            public const string ActualizaCliente9 = "ActualizarCliente9.aspx";
        }

        public struct CteCookie
        {
            public const string Idioma = "Idioma";
            public const string IdTerminal = "IdTerminal";
        }

        public struct QueryString
        {
            public const string IdArticulo = "IdArticulo";
            public const string IdCabeceroDetalle = "IdCabeceroDetalle";
            public const string IdOrdenInventario = "IdOrdenInventario";
            public const string ImporteConversion = "Importe";
            public const string ReturnUrl = "ReturnUrl";
            public const string FiltroArticulo = "Filtro";
            public const string FiltroTalla = "Talla";
 
        }

        //Constantes individuales
        public const string PrefijoClienteNine = "22114005";
	}

