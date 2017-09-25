using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionVenta.Models
{

    //MJM 11/03/2014 Añado atributo Serializable
   [Serializable]
   public class CLIENTE9
    {
              
           public System.DateTime Fecha { get; set; }
           public int Id_Cliente { get; set; }
           public string Cliente { get; set; }
           public string Nombre { get; set; }
           public string Apellidos { get; set; }
           public string Telefono { get; set; }
           public string Movil { get; set; }
           public string FechaNacimiento { get; set; }
           public string NumTarjeta { get; set; }
           public Nullable<double> SaldoPuntosAnt { get; set; }
           public Nullable<double> PuntosRedimidos { get; set; }
           public Nullable<double> PuntosObtenidos { get; set; }
           public double SaldoPuntosAct { get; set; }
           public Nullable<double> dblSaldoPares9 { get; set; }
           public double ParesAcumuladosAnt { get; set; }
           public Nullable<double> ParesRedimidos { get; set; }
           public Nullable<double> ParesAcumuladosAct { get; set; }
           public Nullable<double> dblSaldoBolsa5 { get; set; }
           public double BolsasAcumuladasAnt { get; set; }
           public Nullable<double> BolsasRedimidas { get; set; }
           public Nullable<double> BolsasAcumuladasAct { get; set; }
           public string Aniversario { get; set; }
           public string Cumpleaños { get; set; }
           public string NivelActual { get; set; }
           public string CandidataShoeLover { get; set; }
           public Nullable<int> NumConfirmaPuntos9 { get; set; }
           public Nullable<int> NumConfirmaPar9 { get; set; }
           public Nullable<int> NumConfirmaBolsa5 { get; set; }
           public string NumTarjetaNew { get; set; }
           public string BenC9 { get; set; }
           public string Email { get; set; }
           public Empleado Empleado_cliente { get; set; }
           public double PuntosPagados { get; set; }
           public double BolsaPagada { get; set; }
           public double ParPagado { get; set; }
           public bool ParValido { get; set; }
           public bool BolsaValido { get; set; }
           public string CandidatoBasico { get; set; }
           public string CandidatoFirstShoeLover { get; set; }
         
       }
   
    [Serializable]
   public class Empleado
   {
       public Int64 idEmpleado { get; set; }
       public bool EsEmpleado { get; set; }
       public bool NotaEmpleado { get; set; }
       public int NumNotaempleado { get; set; }
       public int NotaEmpleadoGastadas { get; set; }
   }  
      
   }
   
