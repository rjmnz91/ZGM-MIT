using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using DLLGestionVenta.Models;

namespace AVE.Servicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICarritoAVE_WS" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICarritoAVE_WS
    {

        [OperationContract]
        int InsertarEnCarrito(int idCarrito, string referencia, string talla, List<StockTienda> listaStockTiendas,float precioOriginal, float precioActualizado);

        [OperationContract]
        bool EliminarCarrito(int idLineaCarrito);
        [OperationContract]
        string InsertaDevolucion(string IdTicket, string orden, string IdTienda, int IdArticulo , string strTalla);
        [OperationContract]
        int TiendaCamper(int Valor);
    }
}
