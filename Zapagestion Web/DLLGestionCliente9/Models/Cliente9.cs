
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLGestionCliente9.Models
{
    [Serializable]
    public class Cliente9
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
        public string esEmpleado { get; set; }
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

    [Serializable]
   public class FacturacionC9{
        public string Accion { get; set; }
        public int IdCliente { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Direccion{ get; set; }
        public string NExterior { get; set; }
        public string NInterior { get; set; }
        public string Colonia { get; set; }
        public string Poblacion { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CP { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public int EnvFactMail { get; set; }
    
    }
    [Serializable]
    public class Cliente9Extent{

        public string Cargo { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Poblacion { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Direccion { get; set; }
        public string Talla { get; set; }
        public string TfnoTrabajo { get; set; }
        public string Fax { get; set; }
        public string Contacto { get; set; }
        public string Comentarios { get; set; }
    
    
    }
    [Serializable]
   public class NuevoCambioC9 {
        public int IdCliente { get; set; }
        public string NuevaTarjeta { get; set; }
        public string TarjetaActual { get; set; }
        public string Candidata { get; set; }
        public string Referencia { get; set; }
        public string NuevoNivel { get; set; }
        public string NivelAnterior { get; set; }
    
    }
    [Serializable]
    public class Cliente9Gral{
        public Cliente9 c9 { get; set; }
        public Cliente9Extent c9Extent { get; set; }
    }
}
