using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionCliente9.Models
{
    class Venta
    {
        public string Id_Tienda { get; set; }
        public string ID_TERMINAL { get; set; }
        public string Id_Empleado { get; set; }
        public System.DateTime Fecha { get; set; }
        public bool DeslizaTarjeta { get; set; }
    }
}
