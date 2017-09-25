﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLLGestionCliente9.CapaDatos;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using DLLGestionCliente9.Models;
using System.Reflection;
using System.Configuration;
using log4net;
using System.Data;


namespace DLLGestionCliente9
{
    class WSCliente9
    {
         private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClsCapaDatos9));        
        
        #region "DECLARACION_VARIABLES_WS"

        private string strConnection; 
        ClsCapaDatos9 objCapaDatos;
        Venta _v;
        
        
        //********************************************************************************************************
        //**** INICIO DECLARACION DE VARIABLES DE PROCESOS INVOCACIONES ****

        //**** 1. CONSULTA BENEFICIOS 
        public string ws_ConsultaBeneficios = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <ConsultaBeneficios xmlns=\"http://tempuri.org/\">" + "      <StrDatos>987654321856547|0|tos</StrDatos>" + "    </ConsultaBeneficios>" + "  </soap:Body>" + "</soap:Envelope>";
        public string url_ConsultaBeneficios = "http://tempuri.org/ConsultaBeneficios";
        public string ws_ConsultaBeneficios_send = "/soap:Envelope/soap:Body";
        public string ws_ConsultaBeneficios_get = "/soap:Envelope/soap:Body";

        //**** 2. ENVIA TICKET 
        public string ws_EnviaTicket = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <EnviaTicket xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" + "    </EnviaTicket>" + "  </soap:Body>" + "</soap:Envelope>";
        public string url_EnviaTicket = "http://tempuri.org/EnviaTicket";
        public string ws_EnviaTicket_send = "/soap:Envelope/soap:Body";
        public string ws_EnviaTicket_get = "/soap:Envelope/soap:Body";

        //**** 3. CHECKPENDING 
        public string ws_CheckPending = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <Checkpending xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" + "    </Checkpending >" + "  </soap:Body>" + "</soap:Envelope>";
        public string url_CheckPending = "http://tempuri.org/Checkpending";
        public string ws_CheckPending_send = "/soap:Envelope/soap:Body";
        public string ws_CheckPending_get = "/soap:Envelope/soap:Body";

        //**** 4. SOLICITA REDENCION
        public string ws_SolicitaRedencion = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <SolicitaRedencion xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" + "    </SolicitaRedencion>" + "  </soap:Body>" + "</soap:Envelope>";
        public string url_SolicitaRedencion = "http://tempuri.org/SolicitaRedencion";
        public string ws_SolicitaRedencion_send = "/soap:Envelope/soap:Body";
        public string ws_SolicitaRedencion_get = "/soap:Envelope/soap:Body";

        //**** 5. CONFIRMAR OPERACION 
        public string ws_ConfirmaRedencion = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <ConfirmaOperacion xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" + "    </ConfirmaOperacion>" + "  </soap:Body>" + "</soap:Envelope>";
        public string url_ConfirmaRedencion = "http://tempuri.org/ConfirmaOperacion";
        public string ws_ConfirmaRedencion_send = "/soap:Envelope/soap:Body";
        public string ws_ConfirmaRedencion_get = "/soap:Envelope/soap:Body";

        //**** 6. ALTA SOCIO
        public string ws_AltaSocio = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <AltaSocio xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" + "    </AltaSocio> " + "  </soap:Body>" + "</soap:Envelope>";
        public string url_AltaSocio = "http://tempuri.org/AltaSocio";
        public string ws_AltaSocio_send = "/soap:Envelope/soap:Body";
        public string ws_AltaSocio_get = "/soap:Envelope/soap:Body";


        //*****7.ACTUALIZA SOCIO

        public string ws_ActualizaSocio = "<?xml version=\"1.0\"?>"+ "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +"  <soap:Body>" + "    <ActualizaSocio xmlns=\"http://tempuri.org/\">"+"      <StrDatos>string</StrDatos>" + "    </ActualizaSocio> " + "  </soap:Body>" + "</soap:Envelope>";
        public string url_ActualizaSocio = "http://tempuri.org/ActualizaSocio";
        public string ws_ActualizaSocio_send = "/soap:Envelope/soap:Body";
        public string ws_ActualizaSocio_get = "/soap:Envelope/soap:Body";

        //*****8.SOLICITUD CAMBIO PLASTICO

        public string ws_SolicitudCambioDeTarjeta = "<?xml version=\"1.0\" ?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" + "  <soap:Body>" + "    <SolicitudCambioTarjeta xmlns=\"http://tempuri.org/\">" + "      <StrDatos>string</StrDatos>" +"    </SolicitudCambioTarjeta> "+"  </soap:Body>" +"</soap:Envelope>";
        public string url_SolicitudCambioDeTarjeta = "http://tempuri.org/SolicitudCambioTarjeta";
        public string ws_SolicitudCambioDeTarjeta_send = "/soap:Envelope/soap:Body";
        public string ws_SolicitudCambioDeTarjeta_get = "/soap:Envelope/soap:Body";
            //*** 9.CONFIRMACIONCAMBIODETARJETA ***
        public string ws_ConfirmacionCambioDeTarjeta = "<?xml version=\"1.0\" ?>" +"<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +"  <soap:Body>" +"    <ConfirmacionCambioTarjeta xmlns=\"http://tempuri.org/\">" +"      <StrDatos>string</StrDatos>" +"    </ConfirmacionCambioTarjeta> " +"  </soap:Body>" +    "</soap:Envelope>";
        public string url_ConfirmacionCambioDeTarjeta = "http://tempuri.org/ConfirmacionCambioTarjeta";
        public string ws_ConfirmacionCambioDeTarjeta_send = "/soap:Envelope/soap:Body";
        public string ws_ConfirmacionCambioDeTarjeta_get = "/soap:Envelope/soap:Body";


        //**** FIN DECLARACION DE VARIABLES DE PROCESOS INVOCACIONES ****
        //********************************************************************************************************               

        //********************************************************************************************************
        //**** INICIO DECLARACION DE VARIABLES DE ESTRUCTURA DE RESPUESTA  ****

        //**** 1. ESTRUCTURA CONSULTA BENEFICIOS 
        public tpeConsultaBeneficios[] arrConsultaBeneficios;

        public struct tpeConsultaBeneficios
        {
            public string strCliente;
            public string strTipoCliente;
            public string strTarjeta;
            public double dblSaldoPuntos9;
            public double dblSaldoPares9;
            public long lngParesAcumulados;
            public double dblSaldoBolsa5;
            public long lngBolsasAcumuladas;
            public string strFechaAniversario;
            public string strFechaCumple;
            public string strNivelActual;
            public bool blnShoeLover;
            public long lngNumConfirmaPuntos9;
            public long lngNumConfirmaPar9;
            public long lngNumConfirmaBolsa5;
            public string strIdSocioC9;
            public string strNombreC9;
            public string strApellidoPatC9;
            public string strApellidoMatC9;
            public string strTelCasaC9;
            public string strTelOficinaC9;
            public string strTelMovilC9;
            public string strMailC9;
        }

        public ConsultaBeneficios cBeneficios;

        public struct ConsultaBeneficios
        {
            public string idTienda;
            public string idTerminal;
            public string idTargeta;
            public string nombre;
            public string mail;
            public string shoelover;
            public string telefono;
            public string celular;
            public string aniversario;
            public string cumpleanos;
            public string puntos;
            public string paresAcumulados;
            public string promedioPar;
            public string bolsasAcumuladas;
            public string promedioBolsa;

            public string strCliente;
            public string strTipoCliente;
            public string strTarjeta;
            public double dblSaldoPuntos9;
            public double dblSaldoPares9;
            public long lngParesAcumulados;
            public double dblSaldoBolsa5;
            public long lngBolsasAcumuladas;
            public string strFechaAniversario;
            public string strFechaCumple;
            public string strNivelActual;
            public bool blnShoeLover;
            public long lngNumConfirmaPuntos9;
            public long lngNumConfirmaPar9;
            public long lngNumConfirmaBolsa5;
            public string strIdSocioC9;
            public string strNombreC9;
            public string strApellidoPatC9;
            public string strApellidoMatC9;
            public string strTelCasaC9;
            public string strTelOficinaC9;
            public string strTelMovilC9;
            public string strMailC9;
            public string strCandBasicoC9;
            public string strCandFirstC9;

        }

        //**** 2. ESTRUCTURA SALDOS NINE 
        public tpeSaldosNine[] arrSaldos_Nine;

        public struct tpeSaldosNine
        {
            public long lngCliente;
            public string strCliente;
            public string strTipoCliente;
            public string strTarjeta;
            public string strIdTicket;
            public string dblSaldoPuntosAnt;
            public double dblPuntosRedimidos;
            public double dblPuntosObtenidos;
            public double dblSaldoPuntosAct;
            public double dblSaldoPares9;
            public long lngParesAcumuladosAnt;
            public long lngParesRedimidos;
            public long lngParesAcumuladosAct;
            public double dblSaldoBolsa5;
            public long lngBolsasAcumuladasAnt;
            public long lngBolsasRedimidas;
            public long lngBolsasAcumuladasAct;
            public string strFechaAniversario;
            public string strFechaCumple;
            public string strNivelActual;
            public bool blnShoeLover;
            public string strTarjetaNew;
            public long lngNumConfirmaPuntos9;
            public long lngNumConfirmaPar9;
            public long lngNumConfirmaBolsa5;
        }


        //**** 3. ESTRUCTURA CHECKPENDING
        public tpeCheckPending[] arrCheckPending;

        public struct tpeCheckPending
        {
            public bool blnPendiente;
            public string strTipoRedencion;
            public string strNoAutorizacion;
            public string strTienda;
            public string strCaja;
            public string strTarjeta;
            public double dblMonto;
            public long lngCajero;
            public string strFRedencion;
        }


        public CheckPending cCheckPending;

        public struct CheckPending
        {
            public bool blnPendiente;
            public string strTipoRedencion;
            public string strNoAutorizacion;
            public string strTienda;
            public string strCaja;
            public string strTarjeta;
            public double dblMonto;
            public long lngCajero;
            public string strFRedencion;
        }


        //**** 4. ESTRUCTURA SOLICITA REDENCION
        public tpeSolicitaRedencion[] arrSolicitaRedencion;
        public tpeSolicitaRedencion[] arrSolicitaRedencionP9;
        public tpeSolicitaRedencion[] arrSolicitaRedencionB5;

        public struct tpeSolicitaRedencion
        {
            public long intTipo;
            public string strBitRedencionP;
            public string strNoAutorizacion;
            public long lngSaldoActual;
            public string strNivelActual;
            public string strNivelApto;
            public string strTienda;
            public string strCaja;
            public double dblMonto;
            public string strTarjeta;
            public long lngCajero;
            public string strFRedencion;
        }


        public SolicitaRedencion cSolicitaRedencion;

        public struct SolicitaRedencion
        {
            public long intTipo;
            public string strBitRedencionP;
            public string strNoAutorizacion;
            public long lngSaldoActual;
            public string strNivelActual;
            public string strNivelApto;
            public string strTienda;
            public string idTerminal;
            public string strCaja;
            public double dblMonto;
            public string strTarjeta;
            public long lngCajero;
            public string strFRedencion;
        }

        //**** 5. CONFIRMAR OPERACION 
        public tpeConfirmaOperacion[] arrConfirmaOperacion;

        public struct tpeConfirmaOperacion
        {
            public string strconfirmacion;
        }


        public ConfirmaOperacion cConfirmaOperacion;

        public struct ConfirmaOperacion
        {
            public string strconfirmacion;
            public string strEntrada;
            public string strSalida;
            public string strTipoRedencion;
            public string strIdTicket;
        }


        //**** 6. ESTRUCTURA ERRORES METODO  
        public tpeErrorWS TipoError;
        public struct tpeErrorWS
        {
            public int intError;
            public string strError;
        }

        //**** 7. ESTRUCTURA ENVIA TICKET

        public EnviaTicket cEnviaTicket;

        public struct EnviaTicket
        {
            public string str_ws_Ticket;
            public long lng_ws_Articulos;
            public string str_ws_TarjetaCLI;
            public string str_ws_DatosFPagos;
            public string str_ws_DatosRef;
            public double dbl_ws_MontoTotal;
            public double dbl_ws_MontoACobrar;
            public string strTienda;
            public long lngCajero;
            public bool blnOk; 
        }

       //*******ALTA SOCIO

        public AltaSocio cAltaSocio;

        public struct AltaSocio
        {
            public int  IdTienda;
            public int IdTerminal;
            public int IdEmpleado;
            public string NumTarjeta;
            public string  strFAniversario;
            public string strFCumpleanios;
            public string strNombre;
            public string strApelPaterno;
            public string strApelMaterno;
            public string strTelefonoCasa;
            public string strTelefonoMovil;
            public string strTelefonoTrabajo;
            public string strEmail;
            public string strEmail2;
 
        }
        //**** FIN DECLARACION DE VARIABLES DE ESTRUCTURA DE RESPUESTA  ****
        //********************************************************************************************************

        #endregion



        public WSCliente9()
        {
        }  

        public String ConexString
        {
            get { return strConnection; }
            set { strConnection = value; }
        }
              
        #region "METODOS_WS_INVOCACION"

        public string InvokeWS_ConsultaBeneficios(ref ConsultaBeneficios st_consultaBeneficios)
        {
            
            string retval = String.Empty;

            try
            {
                //***** CARGAMOS LA CADENA CON LOS DATOS NECESARIOS PARA LA CONSULTA BENEFICIOS 
                string strParam = st_consultaBeneficios.idTargeta + "|" + (st_consultaBeneficios.idTienda.Replace("T-", "")) + "|" + (string.IsNullOrEmpty(st_consultaBeneficios.idTerminal) ? (1).ToString() : st_consultaBeneficios.idTerminal);
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_ConsultaBeneficios);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_ConsultaBeneficios_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_ConsultaBeneficios_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta__ConsultaBeneficios(soapResult, ref st_consultaBeneficios, strParam);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }
        
        public string InvokeWS_SolicitaRedencion(ref SolicitaRedencion st_SolicitaRedencion)
        {
            //***** CARGAMOS LA CADENA CON LOS DATOS NECESARIOS PARA LA SOLICITUD DE REDENCION 

            //*** CADENA ENTRADA A ENVIAR A SOLICITA REDENCION - ZAPA***
            //        strParamWS_IN = intTipoRedencion & "|" & frmTickets.txtCL_Tarjeta.Text & "|" & _
            //             IIf(cboFPago(0).Text = "PUNTOS NINE", EsNuloN(txtPuntos(3).Text), 0) & "|" & _
            //            Val(Replace(gstrTiendaSesion, "T-", "")) & "|" & IIf(gstrTerminalID = "", "1", gstrTerminalID) & "|" & EsNuloN(frmTickets.txtEmpleado.Text)

            string retval = String.Empty;

            try
            {
                string strParam = st_SolicitaRedencion.intTipo + "|" + st_SolicitaRedencion.strTarjeta + "|" + st_SolicitaRedencion.dblMonto + "|";
                strParam += strParam + (st_SolicitaRedencion.strTienda.Replace("T-", "")) + "|" + (string.IsNullOrEmpty(st_SolicitaRedencion.idTerminal) ? (1).ToString() : st_SolicitaRedencion.idTerminal) + "|" + st_SolicitaRedencion.lngCajero;
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_SolicitaRedencion);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_SolicitaRedencion_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_SolicitaRedencion_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta__SolicitaRedencion(soapResult, ref st_SolicitaRedencion, strParam, st_SolicitaRedencion.intTipo);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;

        }

        public string InvokeWS_EnviaTicket(ref EnviaTicket st_EnviaTicket, string StrIdTicket, string StrEntradaAuto, string strAuto)
        {
            //***** CARGAMOS LA CADENA CON LOS DATOS NECESARIOS PARA EL ENVIO TICKET

            //*** CADENA A ENVIO TICKET - ZAPA***
            //    str_ws_Ticket = IIf(gstrTerminalID = "", "1", Val(gstrTerminalID)) & Mid(strIdTicket, 1, InStr(1, strIdTicket, "/") - 1)
            //    lng_ws_Articulos = lng_ws_Articulos + Abs(EsNuloN(.TextMatrix(intI, 5)))
            //    str_ws_TarjetaCLI = GetTarjetaCliente(cnADO, lngClienteID)
            //    str_ws_DatosFPagos = Get_WS_FPagos(cnADO, strIdTicket)
            //    str_ws_DatosRef = str_ws_DatosRef & EsNuloT(.TextMatrix(intI, 3)) & ";" & _
            //                            Abs(EsNuloN(.TextMatrix(intI, 5))) & ";" & EsNuloN(.TextMatrix(intI, 10)) & ";" & _
            //                            (((EsNuloN(.TextMatrix(intI, 6)) + dbl_ws_MontoDtoRef))) & ";"
            //    str_ws_DatosRef = str_ws_DatosRef & CStr("1") & CStr("&")
            //    str_ws_DatosRef = Mid(str_ws_DatosRef, 1, Len(str_ws_DatosRef) - 1)
            //    str_ws_ParamSend = str_ws_Ticket & "|" & Val(Replace(gstrTiendaSesion, "T-", "")) & "|" & Format(CDate(strDateTime), "yyyy/mm/dd hh:mm") & "|"
            //    str_ws_ParamSend = str_ws_ParamSend & Round(EsNuloN(dbl_ws_MontoTotal), 0) & "|" & dbl_ws_MontoACobrar & "|" & _
            //        lng_ws_Articulos & "|" & str_ws_TarjetaCLI & "|" & str_ws_DatosRef & "|" & str_ws_DatosFPagos & "|" & glngCajero & "|"

            string retval = String.Empty;

            try
            {
                string strParam = "";
                string soapResult = "";

                if (strAuto == "AUTO")
                {
                    strParam = StrEntradaAuto.ToString();
                }
                else
                {
                    //strParam = st_EnviaTicket.str_ws_Ticket + "|" + (st_EnviaTicket.strTienda.Replace("T-", "")) + "|" + Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")) + "|";
                    //strParam += strParam + st_EnviaTicket.dbl_ws_MontoTotal + "|" + st_EnviaTicket.dbl_ws_MontoACobrar + "|" + st_EnviaTicket.lng_ws_Articulos + "|";
                    //strParam += strParam + st_EnviaTicket.str_ws_TarjetaCLI + "|" + st_EnviaTicket.str_ws_DatosRef + "|" + st_EnviaTicket.str_ws_DatosFPagos + "|" + st_EnviaTicket.lngCajero + "|";
                    strParam = StrEntradaAuto.ToString();
                }

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_EnviaTicket);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_EnviaTicket_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_EnviaTicket_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta__EnviaTicket(soapResult, ref st_EnviaTicket, strParam, StrIdTicket);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return retval;
        }

        public string InvokeWS_CheckPending(ref CheckPending st_CheckPending)
        {
            //***** CARGAMOS LA CADENA CON LOS DATOS NECESARIOS PARA EL ENVIO TICKET

            //*** CADENA A ENVIO TICKET - ZAPA***
            //    Val(Left(cboRedencion.Text, 1)) & "|" & Val(Replace(gstrTiendaSesion, "T-", "")) & "|" & IIf(gstrTerminalID = "", "1", gstrTerminalID)

            string retval = String.Empty;

            try
            {
                string strParam = st_CheckPending.strTipoRedencion + "|" + Int16.Parse((st_CheckPending.strTienda.Replace("T-", ""))).ToString() + "|" + (string.IsNullOrEmpty(_v.ID_TERMINAL) ? (1).ToString() : _v.ID_TERMINAL);
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_CheckPending);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_CheckPending_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_CheckPending_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }

                retval = ProcesarRespuesta__CheckPending(soapResult, ref st_CheckPending, strParam, st_CheckPending.strTipoRedencion);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }
        
        public string InvokeWS_ConfirmaOperacion(ref ConfirmaOperacion st_ConfirmaOperacion)
        {
            //***** CARGAMOS LA CADENA CON LOS DATOS NECESARIOS PARA EL ENVIO TICKET

            //*** CADENA A ENVIO TICKET - ZAPA***
            //    CADENA ORIGINAL ENVIADA DESDE ZAPA - st_ConfirmaOperacion.strEntrada 

            string retval = String.Empty;

            try
            {

                string strParam = st_ConfirmaOperacion.strEntrada;
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_ConfirmaRedencion);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_ConfirmaRedencion_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_ConfirmaRedencion_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval =  ProcesarRespuesta__ConfirmaOperacion(soapResult, ref st_ConfirmaOperacion, strParam, st_ConfirmaOperacion.strTipoRedencion, st_ConfirmaOperacion.strIdTicket);
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }




        public string Invoke_ConfirmacionDevo(int tipo, string autorizacion, string tipoOperacion) {
            string retval = "0";
            string strRespuesta = "";
            try
            {
                ConfirmaOperacion st_ConfOperacionDev = new ConfirmaOperacion();
                st_ConfOperacionDev.strconfirmacion = "";
                st_ConfOperacionDev.strIdTicket = "";
                st_ConfOperacionDev.strEntrada = tipo.ToString() + "|" + autorizacion + "|" + tipoOperacion;
                st_ConfOperacionDev.strTipoRedencion = tipo.ToString();
                strRespuesta = InvokeWS_ConfirmaOperacion(ref st_ConfOperacionDev);
                retval = "1";

            }
            catch (Exception ex) {
                log.Error(ex.Message.ToString());
            }
            return retval;
        
        
        }

        public string InvokeWS_OperacionesPendientes(long intTipoRedencion ,String NumAutorizacion,bool blnPendTodos)
        {
            //***** ENVIO DE OPERACIONES PENDIENTE *****                                
            //'**** Comprobar si hay alguna redención pendiente con checkpending ***

            string retval = "0";

            try
            {

                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand data = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlDataReader dr = null;


                string strSQL = null;
                string strMSG = "";
                string strObs = "";
                long intI = 0;
                string strTipoRedencion = "";
                long intError = 0;
                string strFilter = "";
                string strEntrada = "";
                string strIdTicket = "";
                string strRespuesta = "";
                CheckPending st_CheckPending = new CheckPending();
                ConfirmaOperacion st_ConfirmaOperacion = new ConfirmaOperacion();
                EnviaTicket st_EnviaTicket = new EnviaTicket();

                //***** ABRIMOS CONEXION *****
                con.ConnectionString = strConnection;
                con.Open();


                //If gblnWSCliente9 Then        
                strMSG = "";
                strObs = "";
                if (intTipoRedencion != -2)
                {
                    for (intI = 1; intI <= 3; intI++)
                    {
                        if ((intTipoRedencion == intI) || (intTipoRedencion == -1))
                        {
                            intError = -1;
                            switch (intI)
                            {
                                case 1:
                                    //'*** PUNTOS NINE ***
                                    strTipoRedencion = "Puntos Nine";
                                    break;
                                case 2:
                                    //'*** PAR 9 ***
                                    strTipoRedencion = "Par 9";
                                    break;
                                case 3:
                                    //'*** BOLSA 5 ***                    
                                    strTipoRedencion = "Bolsas 5";
                                    break;
                                default:
                                    break;
                            }


                            //'*** METODO WS CHECKPENDING  ****

                            st_CheckPending.strTipoRedencion = intI.ToString();
                            st_CheckPending.strTienda = Int16.Parse(_v.Id_Tienda.Replace("T-", "").ToString()).ToString();
                        
                            // MJM 07/05/2014 INICIO
                            // Cambiamos int16 por int32, los IdEmpleado pueden superar el 32767
                            //st_CheckPending.lngCajero = Int16.Parse(_v.Id_Empleado.ToString());
                            st_CheckPending.lngCajero = Int32.Parse(_v.Id_Empleado.ToString());
                            // MJM 07/05/2014 FIN

                            st_CheckPending.strCaja = "";
                            st_CheckPending.strFRedencion = "";
                            st_CheckPending.strNoAutorizacion = "";
                            st_CheckPending.strTarjeta = "";
                            st_CheckPending.blnPendiente = false;
                            st_CheckPending.dblMonto = 0;


                            //strParam = intI  + "|" + (AVE.Contexto.IdTienda.Replace("T-", "")) + "|" + (string.IsNullOrEmpty(AVE.Contexto.IdTerminal) ? (1).ToString() : AVE.Contexto.IdTerminal); 

                            strRespuesta = InvokeWS_CheckPending(ref st_CheckPending);


                            if (arrCheckPending != null)
                            {

                                foreach (tpeCheckPending arrCP in arrCheckPending)
                                {
                                    if (arrCP.blnPendiente)
                                    {
                                        strFilter = arrCP.strTipoRedencion + "|" + arrCP.strNoAutorizacion + "|";
                                        //'*** Si hay redencion pendiente buscar redencion en Tabla Historica ***
                                        strSQL = "SELECT Entrada FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='CONFIRMAOPERACION' ";
                                        strSQL += " AND Entrada like '" + strFilter.ToString() + "%' AND Estado='1'";
                                        data.Connection = con;
                                        data.CommandText = strSQL;
                                        dr = data.ExecuteReader();
                                        dr.Read();
                                        if ((dr.HasRows))
                                        {
                                            //'** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                                            strEntrada = dr["Entrada"].ToString().Trim();

                                        }
                                        else
                                        {
                                            if (blnPendTodos)
                                            {
                                                //'** En caso contrario toma estado de cancelacion ( rollback ) ***'
                                                strEntrada = strFilter + "-1";
                                            }
                                            else
                                            {
                                                //'** En caso contrario toma estado de cancelacion ( rollback ) ***'
                                                strEntrada = strFilter + "1";
                                            }

                                        }
                                        dr.Close();
                                        data.Dispose();

                                        //'*** METODO WS CONFIRMAOPERACION  ****
                                        st_ConfirmaOperacion.strconfirmacion = "";
                                        st_ConfirmaOperacion.strIdTicket = "";
                                        st_ConfirmaOperacion.strEntrada = strEntrada.ToString();
                                        st_ConfirmaOperacion.strTipoRedencion = intI.ToString(); ;

                                        strRespuesta = InvokeWS_ConfirmaOperacion(ref st_ConfirmaOperacion);

                                        if (intError != -1)
                                        {
                                            strObs += strObs + intI + ". - " + strTipoRedencion + "\n";
                                            strObs += strObs + "Cadena de Entrada = [ " + strEntrada + "]" + "\n";
                                            strObs += strObs + "Cadena de Salida = [" + st_ConfirmaOperacion.strSalida.ToString() + "]" + "\n";
                                            strObs += strObs + "Error = (" + intError + ") - " + GetNameError_Sin_CN(intError) + "\n" + "\n";
                                        }


                                    }
                                }
                            }
                        }
                    }
                }

                if ((intTipoRedencion == -1) || (intTipoRedencion == -2))
                {
                    //////    '*** Si hay EnviaTicket Abierto - Se Intenta enviar de Nuevo *** 
                    strSQL = "SELECT DISTINCT Entrada,IdTicket FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='ENVIATICKET' AND Estado='1'";
                    data.Connection = con;
                    data.CommandText = strSQL;
                    dr = data.ExecuteReader();
                    dr.Read();
                    if ((dr.HasRows))
                    {
                        //'** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                        strEntrada = dr["Entrada"].ToString().Trim();
                        strIdTicket = dr["IdTicket"].ToString().Trim();

                        if (strEntrada != "")
                        {
                            strRespuesta = InvokeWS_EnviaTicket(ref st_EnviaTicket, strIdTicket.ToString(), strEntrada.ToString(), "AUTO");
                        }

                    }
                    else
                    {
                        strEntrada = "";
                    }
                    dr.Close();
                    data.Dispose();
                }

                //***** CERRAMOS CONEXION *****
                con.Close();
                con.Dispose();

                retval = "1";
                #region "comentarios"
                
                //            '*** Si hay Alta Abierto - Se Intenta enviar de Nuevo ***
                //            strSQL = "SELECT Entrada FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='ALTASOCIO' AND Estado='1'"
                //            Set rsADO = New ADODB.Recordset
                //            rsADO.Open strSQL, cnADO, adOpenStatic, adLockReadOnly
                //            If Not rsADO.EOF Then
                //                While Not rsADO.EOF
                //                    '** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                //                    strEntrada = EsNuloT(rsADO!Entrada)
                //                    CloseRS rsADO
                //                    If strEntrada <> "" Then
                //                        Call InvokeWS_AltaSocio(ws_AltaSocio, url_AltaSocio, ws_AltaSocio_send, ws_AltaSocio_get, _
                //                            strEntrada, frmTickets.Left, frmTickets.Top, frmTickets.Width, frmTickets.Height, "AUTO", strIdSocioC9)
                //                    End If
                //                    rsADO.MoveNext
                //                Wend
                //                CloseRS rsADO
                //            Else
                //                strEntrada = "": CloseRS rsADO
                //            End If


                //            '*** COMMENT ACT_SOCIO **
                //            '*** Si hay Alta Abierto - Se Intenta enviar de Nuevo ***
                //            strSQL = "SELECT Entrada FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='ACTUALIZASOCIO' AND Estado='1'"
                //            Set rsADO = New ADODB.Recordset
                //            rsADO.Open strSQL, cnADO, adOpenStatic, adLockReadOnly
                //            If Not rsADO.EOF Then
                //                While Not rsADO.EOF
                //                    '** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                //                    strEntrada = EsNuloT(rsADO!Entrada)
                //                    CloseRS rsADO
                //                    If strEntrada <> "" Then
                //                        Call InvokeWS_ActualizaSocio(ws_ActualizaSocio, url_ActualizaSocio, ws_ActualizaSocio_send, ws_ActualizaSocio_get, _
                //                            strEntrada, frmTickets.Left, frmTickets.Top, frmTickets.Width, frmTickets.Height, "AUTO", strIdSocioC9)
                //                    End If
                //                    rsADO.MoveNext
                //                Wend
                //                CloseRS rsADO
                //            Else
                //                strEntrada = "": CloseRS rsADO
                //            End If
                //'

                //            '*** Si hay Confirmacion pendiente de tarjeta Abierto - Se Intenta enviar de Nuevo ***
                //            strSQL = "SELECT Entrada,IdTicket FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='SOLICITACAMBIOTARJETA' AND Estado='1'"
                //            Set rsADO = New ADODB.Recordset
                //            rsADO.Open strSQL, cnADO, adOpenStatic, adLockReadOnly
                //            If Not rsADO.EOF Then
                //                While Not rsADO.EOF
                //                    '** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                //                    strEntrada = EsNuloT(rsADO!Entrada)
                //                    strIdTicket = EsNuloT(rsADO!IdTicket)
                //                    If strEntrada <> "" Then
                //                        StrSalida = InvokeWS_SolicitaCambioTarjeta(ws_SolicitudCambioDeTarjeta, url_SolicitudCambioDeTarjeta, ws_SolicitudCambioDeTarjeta_send, ws_SolicitudCambioDeTarjeta_get, _
                //                            strEntrada, 0, strIdTicket, frmTickets.Left, frmTickets.Top, frmTickets.Width, frmTickets.Height, "AUTO")
                //                        If StrSalida <> "" Then
                //                            arrTruncate = Split(StrSalida, "|")
                //                            strAutorizacion = Trim(EsNuloT(arrTruncate(1)))
                //                            strTicket = "C9AVT" & Val(Replace(gstrTiendaSesion, "T-", "")) & _
                //                                String(2 - Len(IIf(gstrTerminalID = "", "1", Val(gstrTerminalID))), "0") & IIf(gstrTerminalID = "", "1", Val(gstrTerminalID)) & _
                //                                Mid(strIdTicket, 1, InStr(1, strIdTicket, "/") - 1)
                //                            Call InvokeWS_ConfirmacionCambioTarjeta(ws_ConfirmacionCambioDeTarjeta, url_ConfirmacionCambioDeTarjeta, ws_ConfirmacionCambioDeTarjeta_send, ws_ConfirmacionCambioDeTarjeta_get, _
                //                                arrTruncate(1) & "|" & strTicket, EsNuloN(frmTickets.txtEmpleado.Text), strIdTicket, frmTickets.Left, frmTickets.Top, frmTickets.Width, frmTickets.Height, "AUTO")
                //                        End If
                //                    End If
                //                    rsADO.MoveNext
                //                Wend
                //                CloseRS rsADO
                //            Else
                //                strEntrada = "": CloseRS rsADO
                //            End If




                //            '*** Si hay Confirmacion pendiente de tarjeta Abierto - Se Intenta enviar de Nuevo ***
                //            strSQL = "SELECT Entrada,IdTicket FROM WS_HISTORICO WITH (NOLOCK) WHERE Metodo='CONFIRMACIONCAMBIOTARJETA' AND Estado='1'"
                //            Set rsADO = New ADODB.Recordset
                //            rsADO.Open strSQL, cnADO, adOpenStatic, adLockReadOnly
                //            If Not rsADO.EOF Then
                //                While Not rsADO.EOF
                //                    '** Si existe cadena en tabla toma estado de conclusion que encuentre en tabla ***'
                //                    strEntrada = EsNuloT(rsADO!Entrada)
                //                    strIdTicket = EsNuloT(rsADO!IdTicket)
                //                    If strEntrada <> "" Then
                //                        Call InvokeWS_ConfirmacionCambioTarjeta(ws_ConfirmacionCambioDeTarjeta, url_ConfirmacionCambioDeTarjeta, ws_ConfirmacionCambioDeTarjeta_send, ws_ConfirmacionCambioDeTarjeta_get, _
                //                            strEntrada, 0, strIdTicket, frmTickets.Left, frmTickets.Top, frmTickets.Width, frmTickets.Height, "AUTO")
                //                    End If
                //                    rsADO.MoveNext
                //                Wend
                //                CloseRS rsADO
                //            Else
                //                strEntrada = "": CloseRS rsADO
                //            End If

                //        End If
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }
        //Llamada al WS para el ALTA DE CLIENTE 9
        public int InvokeWS_AltaSocio(string strParam, string IdTienda,string IdEmpleado, ref string msgWS, ref string IdSocioC9)
        {
            

            int retval = 0;
            try
            {
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_AltaSocio);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_AltaSocio_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_AltaSocio_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta__AltaSocio(soapResult, strParam, IdTienda,IdEmpleado,  ref msgWS, ref IdSocioC9);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }

       public int InvokeWS_ActualizaSocio(string strParam, string IdTienda,string IdEmpleado, ref string msgWS, ref string IdSocioC9)
        {
            

            int retval = 0;
            try
            {
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_ActualizaSocio);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_ActualizaSocio_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_ActualizaSocio_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta__ActualizaSocio(soapResult, strParam, IdTienda,IdEmpleado,  ref msgWS, ref IdSocioC9);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
        }

       public string InvokeWS_SolicitudCambioTarjeta(string strParam, string IdEmpleado, string strIdTicket, string IdTienda, ref string msgWS)
         {
             
            string retval ="";
            try
            {
                string soapResult = "";

                HttpWebRequest request = CreateWebRequest();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(ws_SolicitudCambioDeTarjeta);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                soapEnvelopeXml.SelectSingleNode(ws_SolicitudCambioDeTarjeta_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapEnvelopeXml.LoadXml(soapResult);
                        soapResult = soapEnvelopeXml.SelectSingleNode(ws_SolicitudCambioDeTarjeta_get, nsmgr).FirstChild.FirstChild.InnerText;
                    }
                }
                retval = ProcesarRespuesta_SolicitudCambioTarjeta(soapResult, strParam, strIdTicket,IdEmpleado,IdTienda, ref msgWS);


            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return retval;
         
         
         }

         public string InvokeWS_ConfirmacionCambioTarjeta(string strParam, string strIdTicket, string IdEmpleado, string IdTienda, ref string msgWS)
         {

             string retval ="";
             try
             {
                 string soapResult = "";

                 HttpWebRequest request = CreateWebRequest();
                 XmlDocument soapEnvelopeXml = new XmlDocument();
                 soapEnvelopeXml.LoadXml(ws_ConfirmacionCambioDeTarjeta);

                 XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
                 nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");


                 soapEnvelopeXml.SelectSingleNode(ws_ConfirmacionCambioDeTarjeta_send, nsmgr).FirstChild.FirstChild.InnerText = strParam;
                 using (Stream stream = request.GetRequestStream())
                 {
                     soapEnvelopeXml.Save(stream);
                 }

                 using (WebResponse response = request.GetResponse())
                 {
                     using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                     {
                         soapResult = rd.ReadToEnd();
                         soapEnvelopeXml.LoadXml(soapResult);
                         soapResult = soapEnvelopeXml.SelectSingleNode(ws_ConfirmacionCambioDeTarjeta_get, nsmgr).FirstChild.FirstChild.InnerText;
                     }
                 }
                 retval = ProcesarRespuesta_ConfirmacionCambioTarjeta(soapResult, strParam, strIdTicket,IdEmpleado, IdTienda, ref msgWS);


             }
             catch (Exception ex)
             {
                 log.Error(ex);
             }

             return retval;


         }


      
        #endregion

        #region "RESPUESTAS_METODOS_WS"

        public string ProcesarRespuesta__ConsultaBeneficios(string strResult, ref ConsultaBeneficios st_consultaBeneficios, string strParam)
        {

            string functionReturnValue = null;

            try
            {
            
                object[] arrTruncate;
                int intError = 0;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;

                arrTruncate = strResult.Split('|');

                if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                {
                    if (strResult == "0|0|0|0|0")
                    {
                        intError = 0;

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( consultabeneficios ) - " + TipoError.strError;

                    }
                    else if (arrTruncate[1].ToString() == "0" & arrTruncate[2].ToString() == "0" & arrTruncate[3].ToString() == "0" & arrTruncate[4].ToString() == "0")
                    {
                        intError = Int16.Parse(arrTruncate[0].ToString());

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( consultabeneficios ) - " + TipoError.strError;

                    }
                    else
                    {
                        Array.Resize(ref arrConsultaBeneficios, 2);

                        arrConsultaBeneficios[1].dblSaldoPuntos9 = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[0].ToString())).ToString());
                        arrConsultaBeneficios[1].dblSaldoPares9 = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[1].ToString())).ToString());
                        arrConsultaBeneficios[1].lngParesAcumulados = long.Parse(Math.Round(Convert.ToDecimal(arrTruncate[2]), 0).ToString());
                        arrConsultaBeneficios[1].dblSaldoBolsa5 = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[3].ToString())));
                        arrConsultaBeneficios[1].lngBolsasAcumuladas = long.Parse(Math.Round(Convert.ToDecimal(arrTruncate[4]), 0).ToString());
                        if (!string.IsNullOrEmpty(arrTruncate[5].ToString()))
                        {
                            arrConsultaBeneficios[1].strFechaAniversario = Convert.ToDateTime(arrTruncate[5]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            arrConsultaBeneficios[1].strFechaAniversario = "";
                        }
                        if (!String.IsNullOrEmpty(arrTruncate[6].ToString()))
                        {
                            arrConsultaBeneficios[1].strFechaCumple = arrTruncate[6].ToString();
                        }
                        if ((arrTruncate.Length) >= 7)
                            arrConsultaBeneficios[1].strNivelActual = arrTruncate[7].ToString().Trim();
                        if ((arrTruncate.Length) >= 8)
                            arrConsultaBeneficios[1].blnShoeLover = (arrTruncate[8].ToString().Trim() == "NO" ? false : true);

                        arrConsultaBeneficios[1].lngNumConfirmaPuntos9 = 0;
                        arrConsultaBeneficios[1].lngNumConfirmaPar9 = 0;
                        arrConsultaBeneficios[1].lngNumConfirmaBolsa5 = 0;
                        if ((arrTruncate.Length) >= 9)
                            arrConsultaBeneficios[1].lngNumConfirmaPuntos9 = long.Parse(arrTruncate[9].ToString().Trim());
                        if ((arrTruncate.Length) >= 10)
                            arrConsultaBeneficios[1].lngNumConfirmaPar9 = long.Parse(arrTruncate[10].ToString().Trim());
                        if ((arrTruncate.Length) >= 11)
                            arrConsultaBeneficios[1].lngNumConfirmaBolsa5 = long.Parse(arrTruncate[11].ToString().Trim()); ;

                        //*** Datos identificativos de Socio
                        arrConsultaBeneficios[1].strIdSocioC9 = "";
                        arrConsultaBeneficios[1].strNombreC9 = "";
                        arrConsultaBeneficios[1].strApellidoPatC9 = "";
                        arrConsultaBeneficios[1].strApellidoMatC9 = "";
                        arrConsultaBeneficios[1].strTelCasaC9 = "";
                        arrConsultaBeneficios[1].strTelOficinaC9 = "";
                        arrConsultaBeneficios[1].strTelMovilC9 = "";
                        arrConsultaBeneficios[1].strMailC9 = "";
                        if ((arrTruncate.Length) >= 12)
                            arrConsultaBeneficios[1].strIdSocioC9 = arrTruncate[12].ToString().Trim();
                        if ((arrTruncate.Length) >= 13)
                            arrConsultaBeneficios[1].strNombreC9 = arrTruncate[13].ToString().Trim();
                        if ((arrTruncate.Length) >= 14)
                            arrConsultaBeneficios[1].strApellidoPatC9 = arrTruncate[14].ToString().Trim();
                        if ((arrTruncate.Length) >= 15)
                            arrConsultaBeneficios[1].strApellidoMatC9 = arrTruncate[15].ToString().Trim();
                        if ((arrTruncate.Length) >= 16)
                            arrConsultaBeneficios[1].strTelCasaC9 = arrTruncate[16].ToString().Trim();
                        if ((arrTruncate.Length) >= 17)
                            arrConsultaBeneficios[1].strTelOficinaC9 = arrTruncate[17].ToString().Trim();
                        if ((arrTruncate.Length) >= 18)
                            arrConsultaBeneficios[1].strTelMovilC9 = arrTruncate[18].ToString().Trim();
                        if ((arrTruncate.Length) >= 19)
                            arrConsultaBeneficios[1].strMailC9 = arrTruncate[19].ToString().Trim();


                        st_consultaBeneficios.nombre = string.Concat(arrConsultaBeneficios[1].strNombreC9, " ", arrConsultaBeneficios[1].strApellidoPatC9, " ", arrConsultaBeneficios[1].strApellidoMatC9);
                        st_consultaBeneficios.mail = arrConsultaBeneficios[1].strMailC9;
                        st_consultaBeneficios.shoelover = arrConsultaBeneficios[1].blnShoeLover.ToString();
                        st_consultaBeneficios.telefono = arrConsultaBeneficios[1].strTelCasaC9;
                        st_consultaBeneficios.celular = arrConsultaBeneficios[1].strTelMovilC9;
                        st_consultaBeneficios.aniversario = arrConsultaBeneficios[1].strFechaAniversario;
                        st_consultaBeneficios.cumpleanos = arrConsultaBeneficios[1].strFechaCumple;
                        st_consultaBeneficios.puntos = arrConsultaBeneficios[1].dblSaldoPuntos9.ToString();
                        st_consultaBeneficios.paresAcumulados = arrConsultaBeneficios[1].lngParesAcumulados.ToString();
                        st_consultaBeneficios.promedioPar = arrConsultaBeneficios[1].dblSaldoPares9.ToString();
                        st_consultaBeneficios.bolsasAcumuladas = arrConsultaBeneficios[1].lngBolsasAcumuladas.ToString();
                        st_consultaBeneficios.promedioBolsa = arrConsultaBeneficios[1].dblSaldoBolsa5.ToString();

                        //st_consultaBeneficios.strCliente = arrConsultaBeneficios[1].strCliente.ToString();
                        //st_consultaBeneficios.strTipoCliente = arrConsultaBeneficios[1].strTipoCliente.ToString();
                        //st_consultaBeneficios.strTarjeta = arrConsultaBeneficios[1].strTarjeta.ToString();
                        st_consultaBeneficios.dblSaldoPuntos9 = arrConsultaBeneficios[1].dblSaldoPuntos9;
                        st_consultaBeneficios.dblSaldoPares9 = arrConsultaBeneficios[1].dblSaldoPares9;
                        st_consultaBeneficios.lngParesAcumulados = arrConsultaBeneficios[1].lngParesAcumulados;
                        st_consultaBeneficios.dblSaldoBolsa5 = arrConsultaBeneficios[1].dblSaldoBolsa5;
                        st_consultaBeneficios.lngBolsasAcumuladas = arrConsultaBeneficios[1].lngBolsasAcumuladas;
                        st_consultaBeneficios.strFechaAniversario = arrConsultaBeneficios[1].strFechaAniversario.ToString();
                        st_consultaBeneficios.strFechaCumple = arrConsultaBeneficios[1].strFechaCumple.ToString();
                        st_consultaBeneficios.strNivelActual = arrConsultaBeneficios[1].strNivelActual.ToString();
                        st_consultaBeneficios.blnShoeLover = arrConsultaBeneficios[1].blnShoeLover;
                        st_consultaBeneficios.lngNumConfirmaPuntos9 = arrConsultaBeneficios[1].lngNumConfirmaPuntos9;
                        st_consultaBeneficios.lngNumConfirmaPar9 = arrConsultaBeneficios[1].lngNumConfirmaPar9;
                        st_consultaBeneficios.lngNumConfirmaBolsa5 = arrConsultaBeneficios[1].lngNumConfirmaBolsa5;
                        st_consultaBeneficios.strIdSocioC9 = arrConsultaBeneficios[1].strIdSocioC9.ToString();
                        st_consultaBeneficios.strNombreC9 = arrConsultaBeneficios[1].strNombreC9.ToString();
                        st_consultaBeneficios.strApellidoPatC9 = arrConsultaBeneficios[1].strApellidoPatC9.ToString();
                        st_consultaBeneficios.strApellidoMatC9 = arrConsultaBeneficios[1].strApellidoMatC9.ToString();
                        st_consultaBeneficios.strTelCasaC9 = arrConsultaBeneficios[1].strTelCasaC9.ToString();
                        st_consultaBeneficios.strTelOficinaC9 = arrConsultaBeneficios[1].strTelOficinaC9.ToString();
                        st_consultaBeneficios.strTelMovilC9 = arrConsultaBeneficios[1].strTelMovilC9.ToString();
                        st_consultaBeneficios.strMailC9 = arrConsultaBeneficios[1].strMailC9.ToString();

                    }
                    intError = -1;
                    TipoError.intError = intError;
                    TipoError.strError = "";
                }

                //GUARDAMOS EN LA TABLA DE WS_HISTORICO
                if (st_consultaBeneficios.nombre != "")
                {
                    // MJM 07/05/2014 INICIO
                    // Cambiamos int16 por int32, los IdEmpleado pueden superar el 32767
                    //objCapaDatos.Save_Hist_HistoricoWS(Int16.Parse(_v.Id_Empleado.ToString()), "CONSULTABENEFICIOS", strParam, strResult, "0", intError, "", _v.Id_Tienda  ,_v.Fecha );
                    objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(_v.Id_Empleado.ToString()), "CONSULTABENEFICIOS", strParam, strResult, "0", intError, "", _v.Id_Tienda, _v.Fecha);
                    // MJM 07/05/2014 FIN

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        
        }

        public string ProcesarRespuesta__SolicitaRedencion(string strResult, ref SolicitaRedencion st_SolicitaRedencion, string strParam, long lngTipo)
        {
            string functionReturnValue = null;

            try
            {
            object[] arrTruncate;
            int intError = 0;
            objCapaDatos = new ClsCapaDatos9();
            objCapaDatos.ConexString = strConnection;

            arrTruncate = strResult.Split('|');

            if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
            {
                if (strResult == "0|0|0|0|0")
                {
                    intError = 0;

                    TipoError.intError = intError;
                    TipoError.strError = GetNameError_Sin_CN(intError);
                    functionReturnValue = "Puntos Net ( SolicitaRedencion ) - " + TipoError.strError;

                }
                else if (arrTruncate[1].ToString() == "0" & arrTruncate[2].ToString() == "0" & arrTruncate[3].ToString() == "0" & arrTruncate[4].ToString() == "0")
                {
                    intError = Int16.Parse(arrTruncate[0].ToString());

                    TipoError.intError = intError;
                    TipoError.strError = GetNameError_Sin_CN(intError);
                    functionReturnValue = "Puntos Net ( SolicitaRedencion ) - " + TipoError.strError;

                }
                else
                {

                    switch (lngTipo)
                    {
                        case 1:
                            //'*** PUNTOS NINE ***
                            Array.Resize(ref arrSolicitaRedencion, 2);
                            arrSolicitaRedencion[1].strBitRedencionP = (arrTruncate[0].ToString().Trim() == "0" ? "0" : "1");
                            arrSolicitaRedencion[1].strNoAutorizacion = arrTruncate[1].ToString();
                            if (arrTruncate[0].ToString().Trim() == "0")
                            {
                                arrSolicitaRedencion[1].lngSaldoActual = long.Parse(FormatoEuros(Double.Parse(arrTruncate[2].ToString())).ToString());
                                arrSolicitaRedencion[1].strNivelActual = arrTruncate[3].ToString();
                                arrSolicitaRedencion[1].strNivelApto = arrTruncate[4].ToString();
                            }
                            else
                            {
                                arrSolicitaRedencion[1].strTienda = arrTruncate[2].ToString();
                                arrSolicitaRedencion[1].strCaja = arrTruncate[3].ToString();
                                arrSolicitaRedencion[1].dblMonto = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[4].ToString())).ToString());
                                arrSolicitaRedencion[1].strTarjeta = arrTruncate[5].ToString();
                                arrSolicitaRedencion[1].lngCajero = long.Parse(FormatoEuros(Double.Parse(arrTruncate[6].ToString())).ToString());
                                arrSolicitaRedencion[1].strFRedencion = Convert.ToDateTime(arrTruncate[7]).ToString("yyyy/MM/dd");
                            }

                            //CARGAMOS LOS VALORES EN EL OBJETO RESULTADO 
                            st_SolicitaRedencion.strBitRedencionP = arrSolicitaRedencion[1].strBitRedencionP;
                            st_SolicitaRedencion.strNoAutorizacion = arrSolicitaRedencion[1].strNoAutorizacion;
                            st_SolicitaRedencion.lngSaldoActual = arrSolicitaRedencion[1].lngSaldoActual;
                            st_SolicitaRedencion.strNivelActual = arrSolicitaRedencion[1].strNivelActual;
                            st_SolicitaRedencion.strNivelApto = arrSolicitaRedencion[1].strNivelApto;
                            st_SolicitaRedencion.strTienda = arrSolicitaRedencion[1].strTienda;
                            st_SolicitaRedencion.strCaja = arrSolicitaRedencion[1].strCaja;
                            st_SolicitaRedencion.dblMonto = arrSolicitaRedencion[1].dblMonto;
                            st_SolicitaRedencion.strTarjeta = arrSolicitaRedencion[1].strTarjeta;
                            st_SolicitaRedencion.lngCajero = arrSolicitaRedencion[1].lngCajero;
                            st_SolicitaRedencion.strFRedencion = arrSolicitaRedencion[1].strFRedencion;

                            break;
                        case 2:
                            //'*** PAR 9 ***
                            Array.Resize(ref arrSolicitaRedencionP9, 2);
                            arrSolicitaRedencionP9[1].strBitRedencionP = (arrTruncate[0].ToString().Trim() == "0" ? "0" : "1");
                            arrSolicitaRedencionP9[1].strNoAutorizacion = arrTruncate[1].ToString();
                            if (arrTruncate[0].ToString().Trim() == "0")
                            {
                                arrSolicitaRedencionP9[1].lngSaldoActual = long.Parse(FormatoEuros(Double.Parse(arrTruncate[2].ToString())).ToString());
                                arrSolicitaRedencionP9[1].strNivelActual = arrTruncate[3].ToString();
                                arrSolicitaRedencionP9[1].strNivelApto = arrTruncate[4].ToString();
                            }
                            else
                            {
                                arrSolicitaRedencionP9[1].strTienda = arrTruncate[2].ToString();
                                arrSolicitaRedencionP9[1].strCaja = arrTruncate[3].ToString();
                                arrSolicitaRedencionP9[1].dblMonto = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[4].ToString())).ToString());
                                arrSolicitaRedencionP9[1].strTarjeta = arrTruncate[5].ToString();
                                arrSolicitaRedencionP9[1].lngCajero = long.Parse(FormatoEuros(Double.Parse(arrTruncate[6].ToString())).ToString());
                                arrSolicitaRedencionP9[1].strFRedencion = Convert.ToDateTime(arrTruncate[7]).ToString("yyyy/MM/dd");
                            }

                            //CARGAMOS LOS VALORES EN EL OBJETO RESULTADO 
                            st_SolicitaRedencion.strBitRedencionP = arrSolicitaRedencionP9[1].strBitRedencionP;
                            st_SolicitaRedencion.strNoAutorizacion = arrSolicitaRedencionP9[1].strNoAutorizacion;
                            st_SolicitaRedencion.lngSaldoActual = arrSolicitaRedencionP9[1].lngSaldoActual;
                            st_SolicitaRedencion.strNivelActual = arrSolicitaRedencionP9[1].strNivelActual;
                            st_SolicitaRedencion.strNivelApto = arrSolicitaRedencionP9[1].strNivelApto;
                            st_SolicitaRedencion.strTienda = arrSolicitaRedencionP9[1].strTienda;
                            st_SolicitaRedencion.strCaja = arrSolicitaRedencionP9[1].strCaja;
                            st_SolicitaRedencion.dblMonto = arrSolicitaRedencionP9[1].dblMonto;
                            st_SolicitaRedencion.strTarjeta = arrSolicitaRedencionP9[1].strTarjeta;
                            st_SolicitaRedencion.lngCajero = arrSolicitaRedencionP9[1].lngCajero;
                            st_SolicitaRedencion.strFRedencion = arrSolicitaRedencionP9[1].strFRedencion;

                            break;
                        case 3:
                            //'*** BOLSA 5 ***                    
                            Array.Resize(ref arrSolicitaRedencionB5, 2);
                            arrSolicitaRedencionB5[1].strBitRedencionP = (arrTruncate[0].ToString().Trim() == "0" ? "0" : "1");
                            arrSolicitaRedencionB5[1].strNoAutorizacion = arrTruncate[1].ToString();
                            if (arrTruncate[0].ToString().Trim() == "0")
                            {
                                arrSolicitaRedencionB5[1].lngSaldoActual = long.Parse(FormatoEuros(Double.Parse(arrTruncate[2].ToString())).ToString());
                                arrSolicitaRedencionB5[1].strNivelActual = arrTruncate[3].ToString();
                                arrSolicitaRedencionB5[1].strNivelApto = arrTruncate[4].ToString();
                            }
                            else
                            {
                                arrSolicitaRedencionB5[1].strTienda = arrTruncate[2].ToString();
                                arrSolicitaRedencionB5[1].strCaja = arrTruncate[3].ToString();
                                arrSolicitaRedencionB5[1].dblMonto = Double.Parse(FormatoEuros(Double.Parse(arrTruncate[4].ToString())).ToString());
                                arrSolicitaRedencionB5[1].strTarjeta = arrTruncate[5].ToString();
                                arrSolicitaRedencionB5[1].lngCajero = long.Parse(FormatoEuros(Double.Parse(arrTruncate[6].ToString())).ToString());
                                arrSolicitaRedencionB5[1].strFRedencion = Convert.ToDateTime(arrTruncate[7]).ToString("yyyy/MM/dd");
                            }

                            //CARGAMOS LOS VALORES EN EL OBJETO RESULTADO 
                            st_SolicitaRedencion.strBitRedencionP = arrSolicitaRedencionB5[1].strBitRedencionP;
                            st_SolicitaRedencion.strNoAutorizacion = arrSolicitaRedencionB5[1].strNoAutorizacion;
                            st_SolicitaRedencion.lngSaldoActual = arrSolicitaRedencionB5[1].lngSaldoActual;
                            st_SolicitaRedencion.strNivelActual = arrSolicitaRedencionB5[1].strNivelActual;
                            st_SolicitaRedencion.strNivelApto = arrSolicitaRedencionB5[1].strNivelApto;
                            st_SolicitaRedencion.strTienda = arrSolicitaRedencionB5[1].strTienda;
                            st_SolicitaRedencion.strCaja = arrSolicitaRedencionB5[1].strCaja;
                            st_SolicitaRedencion.dblMonto = arrSolicitaRedencionB5[1].dblMonto;
                            st_SolicitaRedencion.strTarjeta = arrSolicitaRedencionB5[1].strTarjeta;
                            st_SolicitaRedencion.lngCajero = arrSolicitaRedencionB5[1].lngCajero;
                            st_SolicitaRedencion.strFRedencion = arrSolicitaRedencionB5[1].strFRedencion;

                            break;
                        default:
                            break;
                    }

                }
                intError = -1;
                TipoError.intError = intError;
                TipoError.strError = "";
            }

            //GUARDAMOS EN LA TABLA WS_HISTORICO
            // MJM 07/05/2014 INICIO
            // Cambiamos int16 por int32, los IdEmpleado pueden superar el 32767
            //objCapaDatos.Save_Hist_HistoricoWS(Int16.Parse(_v.Id_Empleado.ToString () ), "SOLICITAREDENCION", strParam, strResult, "0", intError, "", _v.Id_Tienda  ,_v.Fecha );
            objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(_v.Id_Empleado.ToString()), "SOLICITAREDENCION", strParam, strResult, "0", intError, "", _v.Id_Tienda, _v.Fecha);
            // MJM 07/05/2014 FIN
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return functionReturnValue;
        }

        public string ProcesarRespuesta__EnviaTicket(string strResult, ref EnviaTicket st_EnviaTicket, string strParam, string StrIdTicket)
        {

            string functionReturnValue = null;

            try
            {
                object[] arrTruncate;
                int intError = 0;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                

                arrTruncate = strResult.Split('|');

                if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                {
                    if (strResult == "0|0|0|0|0")
                    {
                        intError = 0;
                        st_EnviaTicket.blnOk = false;

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( EnviaTicket ) - " + TipoError.strError;

                    }
                    else if (arrTruncate[0].ToString() != "100" & arrTruncate[1].ToString() == "0" & arrTruncate[2].ToString() == "0" & arrTruncate[3].ToString() == "0" & arrTruncate[4].ToString() == "0")
                    {
                        intError = Int16.Parse(arrTruncate[0].ToString());

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( EnviaTicket ) - " + TipoError.strError;
                        st_EnviaTicket.blnOk = false;

                    }
                    else
                    {

                        //'*** Si la enviar ticket hay tarjeta reemplazada (error 327) reemplazar tarjeta activa con tarjeta enviada en salida del error
                        //if ( long.Parse( arrTruncate[0].ToString().Trim()) = 327)  And Not blnError327 And UBound(arrTruncate) >= 5 Then
                        //    If SaveEditC9(Trim(Split(strParam, "|")(0)), Trim(Split(strParam, "|")(6)), Trim(arrTruncate(5))) Then
                        //        strParam = Replace(strParam, "|" & Trim(Split(strParam, "|")(6)) & "|", "|" & Trim(arrTruncate(5)) & "|")
                        //        Call InvokeWS_EnviaTicket(sv_Name, sSoapAction, sSoapSend, sSoapResult, lngEmpleado, strIdTicket, strParam, lngLeft, lngTop, lngWidth, lngHeight, strOrigen, True)
                        //        Exit Sub
                        //    End If
                        //End If

                        intError = -1;
                        TipoError.intError = intError;
                        TipoError.strError = "";

                    }
               
                
                }

                //GUARDAMOS EN LA TABLA WS_HISTORICO
                // MJM 07/05/2014 INICIO
                // Cambiamos int16 por int32, los IdEmpleado pueden superar el 32767
                objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(_v.Id_Empleado.ToString () ), "ENVIATICKET", strParam, strResult, "0", intError , StrIdTicket.ToString(), _v.Id_Tienda , _v.Fecha );
                // MJM 07/05/2014 FIN
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        public string ProcesarRespuesta__CheckPending(string strResult, ref CheckPending st_CheckPending, string strParam, string lngTipo)
        {
            string functionReturnValue = null;

            try
            {
                object[] arrTruncate;
                string strTipoRedencion = "";
                int intError = 0;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;

                switch (lngTipo)
                {
                    case "1":
                        //'*** PUNTOS NINE ***
                        strTipoRedencion = "Puntos Nine";
                        break;
                    case "2":
                        //'*** PAR 9 ***
                        strTipoRedencion = "Par 9";
                        break;
                    case "3":
                        //'*** BOLSA 5 ***                    
                        strTipoRedencion = "Bolsas 5";
                        break;
                    default:
                        break;
                }


                arrTruncate = strResult.Split('|');

                if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                {
                    if (strResult == "0|0|0|0|0")
                    {
                        intError = 0;

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( CheckPending / " + strTipoRedencion + " ) - " + TipoError.strError;

                    }

                    //else if (arrTruncate[0].ToString() == "0")
                    //{
                    //    intError = -1;
                    //    //no  confirma nada el check pendind

                    //    //TipoError.intError = intError;
                    //    //TipoError.strError = GetNameError_Sin_CN(intError);
                    //    //functionReturnValue = "Puntos Net ( consultabeneficios ) - " + TipoError.strError;

                    //}
                    else if (arrTruncate[0].ToString() != "0" && arrTruncate.Count() == 3) 
                    {
                        intError = Int16.Parse(arrTruncate[0].ToString());

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( CheckPending / " + strTipoRedencion + " ) - " + TipoError.strError;

                    }

                    else if (arrTruncate[1].ToString() == "0" & arrTruncate[2].ToString() == "0" & arrTruncate[3].ToString() == "0" & arrTruncate[4].ToString() == "0")
                    {
                        intError = Int16.Parse(arrTruncate[0].ToString());

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( consultabeneficios ) - " + TipoError.strError;

                    }
                    else
                    {

                        Array.Resize(ref arrCheckPending, 2);
                        if (strResult == "0")
                        {
                            arrCheckPending[1].blnPendiente = false;
                        }
                        else
                        {
                            arrCheckPending[1].blnPendiente = true;
                            arrCheckPending[1].strTipoRedencion = arrTruncate[0].ToString();
                            arrCheckPending[1].strNoAutorizacion = arrTruncate[1].ToString();
                            arrCheckPending[1].strTienda = arrTruncate[2].ToString();
                            arrCheckPending[1].strCaja = arrTruncate[3].ToString();
                            arrCheckPending[1].dblMonto = double.Parse(FormatoEuros(Double.Parse(arrTruncate[4].ToString())).ToString());
                            arrCheckPending[1].strTarjeta = arrTruncate[5].ToString();
                            arrCheckPending[1].lngCajero = long.Parse(arrTruncate[6].ToString());
                            arrCheckPending[1].strFRedencion = Convert.ToDateTime(arrTruncate[7]).ToString("yyyy/mm/dd");
                        }

                        intError = -1;
                        TipoError.intError = intError;
                        TipoError.strError = "";
                    }

               
                }

                //GUARDAMOS EN LA TABLA WS_HISTORICO
                objCapaDatos.Save_Hist_HistoricoWS(0, "CHECKPENDING", strParam, strResult, "0", intError, "", _v.Id_Tienda , _v.Fecha );
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        public string ProcesarRespuesta__ConfirmaOperacion(string strResult, ref ConfirmaOperacion st_ConfirmaOperacion, string strParam, string lngTipo, string strIdTicket)
        {
            string functionReturnValue = null;

            try
            {
                object[] arrTruncate;
                string strTipoRedencion = "";
                int intError = 0;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;


                switch (lngTipo)
                {
                    case "1":
                        //'*** PUNTOS NINE ***
                        strTipoRedencion = "Puntos Nine";
                        break;
                    case "2":
                        //'*** PAR 9 ***
                        strTipoRedencion = "Par 9";
                        break;
                    case "3":
                        //'*** BOLSA 5 ***                    
                        strTipoRedencion = "Bolsas 5";
                        break;
                    default:
                        break;
                }


                arrTruncate = strResult.Split('|');

               if (strResult.Equals("0"))
               {
                    Array.Resize(ref arrConfirmaOperacion, 2);
                    arrConfirmaOperacion[1].strconfirmacion = "-1";
                    st_ConfirmaOperacion.strconfirmacion = "-1";
                    intError = -1;

               }
               else
               {  

                if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                {
                    if (strResult == "0|0|0|0|0")
                    {
                        intError = 0;

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( ConfirmaOperacion / " + strTipoRedencion + " ) - " + TipoError.strError;
                        Array.Resize(ref arrConfirmaOperacion, 2);
                        arrConfirmaOperacion[1].strconfirmacion = "-1";
                        st_ConfirmaOperacion.strconfirmacion = "-1";

                    }
                    else if (arrTruncate[0].ToString() == "0")
                    {
                        intError = -1;

                        //TipoError.intError = intError;
                        //TipoError.strError = GetNameError_Sin_CN(intError);
                        //functionReturnValue = "Puntos Net ( ConfirmaOperacion / " + strTipoRedencion + " ) - " + TipoError.strError;
                        Array.Resize(ref arrConfirmaOperacion, 2);
                        arrConfirmaOperacion[1].strconfirmacion = "-1";
                        st_ConfirmaOperacion.strconfirmacion = "-1";

                    }

                    else if (arrTruncate[1].ToString() == "0" & arrTruncate[2].ToString() == "0" & arrTruncate[3].ToString() == "0" & arrTruncate[4].ToString() == "0")
                    {
                        intError = Int16.Parse(arrTruncate[0].ToString());

                        TipoError.intError = intError;
                        TipoError.strError = GetNameError_Sin_CN(intError);
                        functionReturnValue = "Puntos Net ( consultabeneficios ) - " + TipoError.strError;

                    }

                    //else if (arrTruncate[0].ToString()!= "0" && arrTruncate.Count()== 3) 
                    //{
                    //    intError = Int16.Parse(arrTruncate[0].ToString());

                    //    TipoError.intError = intError;
                    //    TipoError.strError = GetNameError_Sin_CN(intError);
                    //    functionReturnValue = "Puntos Net ( ConfirmaOperacion / " + strTipoRedencion + " ) - " + TipoError.strError;
                    //    Array.Resize(ref arrConfirmaOperacion, 2);
                    //    arrConfirmaOperacion[1].strconfirmacion = "-1";
                    //    st_ConfirmaOperacion.strconfirmacion = "-1";

                    //}
                


                    else
                    {

                        Array.Resize(ref arrConfirmaOperacion, 2);
                        arrConfirmaOperacion[1].strconfirmacion = "-1";
                        st_ConfirmaOperacion.strconfirmacion = "-1";

                        intError = -1;
                        TipoError.intError = intError;
                        TipoError.strError = "";

                    }
                }
                
                }

                //GUARDAMOS EN LA TABLA WS_HISTORICO
               // MJM 07/05/2014 INICIO
               // Cambiamos int16 por int32, los IdEmpleado pueden superar el 32767
                // objCapaDatos.Save_Hist_HistoricoWS(Int16.Parse(_v.Id_Empleado.ToString()  ), "CONFIRMAOPERACION", strParam, strResult, "0", intError, strIdTicket, _v.Id_Tienda , _v.Fecha );
                objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(_v.Id_Empleado.ToString()), "CONFIRMAOPERACION", strParam, strResult, "0", intError, strIdTicket, _v.Id_Tienda, _v.Fecha);
                // MJM 07/05/2014 FIN

                st_ConfirmaOperacion.strSalida = strResult.ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }
        public int ProcesarRespuesta__AltaSocio(string strResult, string strParam, string IdTienda, string IdEmpleado, ref string msgWS, ref string strIdSocioC9) 
        {
             int functionReturnValue = 0;

             try
             {
                 objCapaDatos = new ClsCapaDatos9();
                 objCapaDatos.ConexString = strConnection;
                    object[] arrTruncate;
                    int intError = 0;
                

                    arrTruncate = strResult.Split('|');

                    if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                    {
                        if (strResult == "0|0|0|0|0")
                        {
                            intError = 0;

                            TipoError.intError = intError;
                            TipoError.strError = GetNameError_Sin_CN(intError);
                            msgWS = "Puntos Net ( altasocio ) - " + TipoError.strError;

                        }
                        else if (arrTruncate[0].ToString() != "0" && arrTruncate.Length > 4)
                        {
                            intError = Int16.Parse(arrTruncate[0].ToString());
                            if (intError == 309)
                            {
                                functionReturnValue = 309;
                                msgWS = strResult;
                            }
                            else
                                functionReturnValue = -1;
                                TipoError.intError = intError;
                            TipoError.strError = GetNameError_Sin_CN(intError);
                            msgWS = "Puntos Net ( altasocio ) - " + TipoError.strError;
                        }
                        else
                        {
                            intError = -1;
                            TipoError.intError = intError;
                            TipoError.strError = "";
                               strIdSocioC9 = arrTruncate[0].ToString().Trim();
                               functionReturnValue = 1;
                        }
                    }
                    objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(IdEmpleado), "ALTASOCIO", strParam, strResult, "0", intError , "", IdTienda, DateTime.Now);

             }
             catch (Exception ex) {
                 log.Error(ex.Message.ToString());
             }
             return functionReturnValue;
        }
         public int ProcesarRespuesta__ActualizaSocio(string strResult, string strParam, string IdTienda, string IdEmpleado, ref string msgWS, ref string strIdSocioC9) 
        {
             int functionReturnValue = 0;
             string estado = "0";
             objCapaDatos = new ClsCapaDatos9();
             objCapaDatos.ConexString = strConnection;
             try
             {
                
                    object[] arrTruncate;
                    int intError = 0;
                

                    arrTruncate = strResult.Split('|');

                    if (!string.IsNullOrEmpty(strResult) & strResult != "ERROR")
                    {
                        if (strResult == "0|0|0|0|0")
                        {
                            intError = 0;

                            TipoError.intError = intError;
                            TipoError.strError = GetNameError_Sin_CN(intError);
                            msgWS = "Puntos Net ( ActualizaSocio ) - " + TipoError.strError;

                        }
                        else if (arrTruncate[0].ToString() != "0" & arrTruncate.Length < 17)
                        {
                            intError = Int16.Parse(arrTruncate[0].ToString());
                             if( intError == 309){
                                   functionReturnValue = 309;
                                    msgWS = strResult;
                                    estado = "309";
                             }
                             else{
                                 functionReturnValue = -1;
                                 TipoError.intError = intError;
                                 TipoError.strError = GetNameError_Sin_CN(intError);
                                 msgWS = "Puntos Net ( ActualizaSocio ) - " + TipoError.strError;
                                 estado = intError.ToString();
                             }
                   
                            
                        }
                        else
                        {
                            intError = -1;
                            TipoError.intError = intError;
                            TipoError.strError = "";
                               strIdSocioC9 = arrTruncate[0].ToString().Trim();
                               functionReturnValue = 1;
                               estado = "0";
                        }
                    }
                    objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(IdEmpleado), "ACTUALIZASOCIO", strParam, strResult, estado, intError, "", IdTienda, DateTime.Now);

             }
             catch (Exception ex) {
                 log.Error(ex.Message.ToString());
             }
             return functionReturnValue;
        }

        
         public string ProcesarRespuesta_SolicitudCambioTarjeta(string strResult, string strParam, string strIdTicket,string IdEmpleado, string IdTienda, ref string msgWS)
         { 
         
          string functionReturnValue ="";
             string estado = "0";
             objCapaDatos = new ClsCapaDatos9();
             objCapaDatos.ConexString = strConnection;
             try
             {
                 object[] arrTruncate;
                 int intError = 0;


                 arrTruncate = strResult.Split('|');
                  if (strResult == "0|0|0|0|0")
                  {
                            intError = 0;

                            TipoError.intError = intError;
                            TipoError.strError = GetNameError_Sin_CN(intError);
                            functionReturnValue = "";
                            msgWS = "Puntos Net ( solicitacambiotarjeta ) - " + TipoError.strError;

                  }
                  else if (arrTruncate[0].ToString() != "0" & arrTruncate.Length > 2)
                  {
                      intError = Int16.Parse(arrTruncate[0].ToString());
                      functionReturnValue = "";
                      TipoError.intError = intError;
                      TipoError.strError = GetNameError_Sin_CN(intError);
                      msgWS = "Puntos Net ( solicitacambiotarjeta ) - " + TipoError.strError;
                      estado = intError.ToString();
                            
                            
                  }
                  else
                  {
                     intError = -1;
                     TipoError.intError = intError;
                     TipoError.strError = "";
                     functionReturnValue = strResult;
                     estado = "0";
                 }
                 
                 
                 objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(IdEmpleado), "SOLICITACAMBIOTARJETA", strParam, strResult, estado, intError, strIdTicket, IdTienda, DateTime.Now);
                 
                 

             }
             catch (Exception ex)
             {
                 log.Error(ex.Message.ToString());
             }
             return functionReturnValue;
         
         }

         public string ProcesarRespuesta_ConfirmacionCambioTarjeta(string strResult, string strParam, string strIdTicket,string IdEmpleado, string IdTienda, ref string msgWS)
         { 
         
          string functionReturnValue ="";
             string estado = "0";
             objCapaDatos = new ClsCapaDatos9();
             objCapaDatos.ConexString = strConnection;
             try
             {
                 object[] arrTruncate;
                 int intError = 0;


                 arrTruncate = strResult.Split('|');
                  if (strResult == "0|0|0|0|0")
                  {
                            intError = 0;

                            TipoError.intError = intError;
                            TipoError.strError = GetNameError_Sin_CN(intError);
                            functionReturnValue = "";
                            msgWS = "Puntos Net ( confirmacambiotarjeta ) - " + TipoError.strError;

                  }
                  else if (arrTruncate[0].ToString() != "1" & arrTruncate.Length > 1)
                  {
                      intError = Int16.Parse(arrTruncate[0].ToString());
                      intError = (intError == 400? 0:intError);
                      functionReturnValue = "";
                      TipoError.intError = intError;
                      TipoError.strError = GetNameError_Sin_CN(intError);
                      msgWS = "Puntos Net ( confirmacambiotarjeta ) - " + TipoError.strError;
                      estado = intError.ToString();
                            
                            
                  }
                  else
                  {
                     intError = -1;
                     TipoError.intError = intError;
                     TipoError.strError = "";
                     functionReturnValue = strResult;
                     estado = "0";
                 }


                  objCapaDatos.Save_Hist_HistoricoWS(Int32.Parse(IdEmpleado), "CONFIRMACIONCAMBIOTARJETA", strParam, strResult, estado, intError, strIdTicket, IdTienda, DateTime.Now);
                 
                 

             }
             catch (Exception ex)
             {
                 log.Error(ex.Message.ToString());
             }
             return functionReturnValue;
         
         }
        #endregion

        #region "FUNCIONES_WS"

        public HttpWebRequest CreateWebRequest()
        {
            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://200.110.137.180/fnet3web/FNET3_NINEWEST.wsdl");

            string strURL_WS_C9;
            strURL_WS_C9 = ConfigurationManager.AppSettings["URL_WS_C9"].ToString();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strURL_WS_C9);

            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public string GetNameError_Sin_CN(long intError)
        {
            string functionReturnValue = null;

            try
            {
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand data = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlDataReader dr = null;


                string strSQL = null;
                con.ConnectionString = strConnection;
                con.Open();
                strSQL = "SELECT Descripcion FROM WS_ERRORES_NET WITH (NOLOCK)  WHERE IdError=" + intError;
                data.Connection = con;
                data.CommandText = strSQL;
                dr = data.ExecuteReader();
                dr.Read();
                if ((dr.HasRows))
                {
                    functionReturnValue = String.Concat("Error nº ", intError.ToString(), " - ", dr["Descripcion"].ToString().Trim(), ".");
                }
                else
                {
                    functionReturnValue = "Error no registrado en TpvGestión";
                }
                dr.Close();
                data.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;

        }

        public string FormatoEuros(double cdblImporte)
        {
            return String.Format(Math.Round(cdblImporte, 2).ToString(), "#,##0.00");
        }

        public bool GetBOLSA5(long IdCarrito)
        {
            bool functionReturnValue = false;
            bool blnSigue = false;

            //***** Si forma de Pago Bolsa 5 - Solo disponible si existe venta Bolso y cliente es shoe lover *****

            if (arrConsultaBeneficios.Count() > 0)
            {
                if (arrConsultaBeneficios[1].strNivelActual.ToString() == "shoe lovers" & (arrConsultaBeneficios[1].lngBolsasAcumuladas / 4) > -1 & GetNunTipoArticuloCarrito("HB", IdCarrito) != -1 & GetNunTipoArticuloCarrito("FO", IdCarrito) == -1 & GetNunTipoArticuloCarrito("", IdCarrito) == -1 & GetNunTipoArticuloCarrito("HB", IdCarrito) == 1)
                {
                    blnSigue = true;
                }
            }

            functionReturnValue = blnSigue;
            return functionReturnValue;

        }

        public bool GetPAR9(long IdCarrito)
        {
            bool functionReturnValue = false;
            bool blnSigue = false;

            //***** Si forma de Pago Par 9 - Solo disponible si existe venta Zapato y tarjeta valida *****

            if (arrConsultaBeneficios.Count() > 0)
            {
                if ((arrConsultaBeneficios[1].lngParesAcumulados / 8) > -1 & GetNunTipoArticuloCarrito("FO", IdCarrito) != -1 & GetNunTipoArticuloCarrito("HB", IdCarrito) == -1 & GetNunTipoArticuloCarrito("", IdCarrito) == -1 & GetNunTipoArticuloCarrito("FO", IdCarrito) == 1)
                {
                    blnSigue = true;
                }
            }


            functionReturnValue = blnSigue;
            return functionReturnValue;

        }

        public long GetNunTipoArticuloCarrito(string strTipoArticulo, long IdCarrito)
        {
            long functionReturnValue = 0;

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                strSQL = "Select COUNT(*) as Total from AVE_Carrito_Linea CD ";
                strSQL += " Inner Join Cabeceros_Detalles CA ON CD.Id_Cabecero_detalle = CA.Id_Cabecero_detalle ";
                strSQL += " Inner Join Articulos Art ON CD.idArticulo = Art.IdArticulo ";
                strSQL += " Inner Join Colores Colo ON Art.IdColor = Colo.IdColor And Colo.Colores_Activo = 1";
                strSQL += " Inner Join Ave_Pedidos PE on CD.IdPedido = PE.IdPedido ";
                strSQL += " WHERE CD.id_carrito =" + IdCarrito + " AND CD.TipoArticulo='" + strTipoArticulo.ToString().Trim() + "'";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    functionReturnValue = Int16.Parse(Ds.Tables[0].Rows[0]["Total"].ToString());
                }
                else
                {
                    functionReturnValue = -1;
                }

                Ds.Dispose();

                if (functionReturnValue == 0) { functionReturnValue = -1; }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;

        }

        public string GeneraParamSend_EnviaTicketDevo(string strIdTicket,string strIdTicketOld, string IdTerminal, string idTienda, double dbl_ws_MontoTotal, double dbl_ws_MontoACobrar, long lngClienteID, string idTarjeta, DateTime fecha, string IdEmpleado,DataView dv, string strAutorizacion)
        {
            string functionReturnValue = "";

            try
            {
                string str_ws_ParamSend = "";
                string str_ws_DatosRef = "";
                long lng_ws_Articulos = 0;
                
                string str_ws_DatosFPagos = "";
                string str_ws_TarjetaCLI = "";


                // ***** CREAR PARAMETRO DE ENTRADA ENVIA TICKET *****

                //BUSCAMOS LOS DESCUENTOS DE PUNTOS NINE ACUMULADOS EN EL TICKET 
                dbl_ws_MontoACobrar =dbl_ws_MontoACobrar + Get_WS_DatosRef_Dto(strIdTicket, -1, -1);

                // [ WS - CONFIRMAR OPERACION ] -> ENVIAR TICKET A  PUNTOS NINE ( SI HAY REDENCION PENDIENTE )
                str_ws_DatosRef = Get_WS_DatosRefDevo(strIdTicket);
                str_ws_DatosRef = str_ws_DatosRef.Substring(0, (str_ws_DatosRef.Length - 1));

                str_ws_DatosFPagos = Get_WS_FPagosDevo(strIdTicket,idTienda,fecha, idTarjeta,dv, strAutorizacion);
                //str_ws_DatosFPagos = str_ws_DatosFPagos.Substring(0, (str_ws_DatosFPagos.Length - 1));
                str_ws_TarjetaCLI = GetTarjetaClienteDevo(lngClienteID, fecha);
                lng_ws_Articulos = Get_WS_Num_Art(strIdTicket);
                //ACL, si no le pongo negativo da error el enviaticket
                //lng_ws_Articulos = lng_ws_Articulos * -1;

                str_ws_ParamSend = (IdTerminal.Trim() == "" ? "1" : IdTerminal.Trim()) + strIdTicket.Substring(0, (strIdTicket.IndexOf("/"))) + "|";
                str_ws_ParamSend += int.Parse(idTienda.Replace("T-", "")).ToString() + "|" + String.Format("{0:yyyy/MM/dd HH:mm}",fecha) + "|";
                str_ws_ParamSend += Math.Round(dbl_ws_MontoTotal)*-1 + "|" + Math.Round(dbl_ws_MontoACobrar) + "|";
                str_ws_ParamSend +=  lng_ws_Articulos.ToString() +"|" + str_ws_TarjetaCLI.ToString() + "|" + str_ws_DatosRef.ToString() + "|";
               // str_ws_ParamSend +=  "-1|" + str_ws_TarjetaCLI.ToString() + "|" + str_ws_DatosRef.ToString() + "|";
                str_ws_ParamSend += str_ws_DatosFPagos.ToString() + "|" + IdEmpleado + "|";
                str_ws_ParamSend += "C9AVT" + int.Parse(idTienda.Replace("T-", "")) +"0" +(IdTerminal.Trim() == "" ? "1" : IdTerminal.Trim()) + strIdTicketOld.Substring(0, (strIdTicketOld.IndexOf("/")));
                functionReturnValue = str_ws_ParamSend.ToString().Trim();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return functionReturnValue;
        }
        public string GeneraParamSend_EnviaTicket(string strIdTicket, double dbl_ws_MontoTotal, double dbl_ws_MontoACobrar, long lngClienteID, DataView DvP9)
        {
            string functionReturnValue = "";

            try
            {
                string str_ws_ParamSend = "";
                string str_ws_DatosRef = "";
                long lng_ws_Articulos = 0;
                string str_ws_DatosFPagos = "";
                string str_ws_TarjetaCLI = "";
               

                // ***** CREAR PARAMETRO DE ENTRADA ENVIA TICKET *****

                //BUSCAMOS LOS DESCUENTOS DE PUNTOS NINE ACUMULADOS EN EL TICKET 
                dbl_ws_MontoACobrar = dbl_ws_MontoACobrar + Get_WS_DatosRef_Dto(strIdTicket, -1,-1);

                // [ WS - CONFIRMAR OPERACION ] -> ENVIAR TICKET A  PUNTOS NINE ( SI HAY REDENCION PENDIENTE )
                str_ws_DatosRef = Get_WS_DatosRef(strIdTicket);
                str_ws_DatosRef = str_ws_DatosRef.Substring(0, (str_ws_DatosRef.Length - 1));

                str_ws_DatosFPagos = Get_WS_FPagos(strIdTicket, DvP9);
                //str_ws_DatosFPagos = str_ws_DatosFPagos.Substring(0, (str_ws_DatosFPagos.Length - 1));
                str_ws_TarjetaCLI = (_v.DeslizaTarjeta ? GetTarjetaCliente(lngClienteID) : String.Empty);
                lng_ws_Articulos = Get_WS_Num_Art(strIdTicket);
                

                str_ws_ParamSend = (_v.ID_TERMINAL.ToString().Trim() == "" ? "1" : _v.ID_TERMINAL.ToString().Trim()) + strIdTicket.Substring(0, (strIdTicket.IndexOf("/"))) + "|";
                str_ws_ParamSend += int.Parse(_v.Id_Tienda.Replace("T-", "")).ToString() + "|" + String.Format("{0:yyyy/MM/dd HH:mm}", _v.Fecha) + "|";
                str_ws_ParamSend += Math.Round(dbl_ws_MontoTotal) + "|" + Math.Round(dbl_ws_MontoACobrar) + "|";
                str_ws_ParamSend += lng_ws_Articulos.ToString() + "|" + str_ws_TarjetaCLI.ToString() + "|" + str_ws_DatosRef.ToString() + "|";
                str_ws_ParamSend += str_ws_DatosFPagos.ToString() + "|" + _v.Id_Empleado + "|";

                functionReturnValue = str_ws_ParamSend.ToString().Trim();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return functionReturnValue;
        }

        public string Get_WS_FPagosDevo(string strIdTicket, string idTienda, DateTime fecha, string NumTarjeta, DataView DvPAgos, string AutorizacionDevo)
        {
            string functionReturnValue = "";

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;
                DataSet DsFPKey;

                string strSQL = null;
                long lngPago = 0;
                long lngPagoDetalle = 0;
                double dblImporte = 0;
                long lngFPKey = 0;
                string strAutorizacion = "";
                string WS_FPagos = "";

                strSQL = " SELECT (CASE WHEN IdFP=92 THEN 1 WHEN IdFP=9 THEN 1 ELSE IdFP END) as IdFP,SUM(Importe) as Importe ";
                strSQL += " FROM N_TICKETS_FPAGOS WITH (NOLOCK)  WHERE Id_Tienda='" + idTienda + "'";
                strSQL += " AND Id_Ticket='" + strIdTicket.ToString() + "' AND Fecha BETWEEN CONVERT(DATETIME,'" + fecha.ToShortDateString() + "',103) AND CONVERT(DATETIME,'" + fecha.ToShortDateString() + " 23:59:59',103)";
                strSQL += " GROUP BY (CASE WHEN IdFP=92 THEN 1 WHEN IdFP=9 THEN 1 ELSE IdFP END) ORDER BY 1";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        if (Int16.Parse(row["Importe"].ToString()) != 0 || Int16.Parse(row["IdFP"].ToString()) != 1)
                        {
                            WS_FPagos += (row["IdFP"].ToString().Trim()) + ";" + Int16.Parse(row["Importe"].ToString()) + ";&";
                        }
                    }

                }
                else
                {
                    functionReturnValue = "";
                }

                Ds.Dispose();

                //*** ACTIVAR CONTROL WS ***
                strSQL = "SELECT FPago,FPagoDetalle,SUM(ImporteDto) as Importe FROM N_TICKETS_DESCUENTOS WITH (NOLOCK)  WHERE Id_Tienda='" + idTienda.Trim() + "'";
                strSQL += " AND FPago IN('CERTIFICADO','PUNTOS NINE','PAR 9','BOLSA 5') AND Id_Ticket='" + strIdTicket.ToString() + "' AND Fecha BETWEEN CONVERT(DATETIME,'" + fecha.ToShortDateString() + "',103) AND CONVERT(DATETIME,'" + fecha.ToShortDateString() + " 23:59:59',103)";
                strSQL += " GROUP BY FPago,FPagoDetalle";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        //***** BUSCAMOS EL lngPago Por registro *****
                        strSQL = "SELECT MAX(IdPago) AS MAXIDPAGO FROM TIPOS_DE_PAGO WITH (NOLOCK)  WHERE Descripcion_Pago='" + row["FPago"].ToString().Trim() + "'";
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngPago = Int16.Parse(DsFPKey.Tables[0].Rows[0]["MAXIDPAGO"].ToString());
                        }
                        else
                        {
                            lngPago = 0;
                        }

                        DsFPKey.Dispose();

                        //***** BUSCAMOS EL lngPagoDetalle Por registro *****
                        strSQL = "SELECT MAX(IdOrden) MAXIDORDEN FROM TIPOS_DE_PAGO_DETALLES WITH (NOLOCK)  WHERE IdPago=" + lngPago + " AND  Descripcion='" + row["FpagoDetalle"].ToString().Trim() + "'";
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngPagoDetalle = Int16.Parse(DsFPKey.Tables[0].Rows[0]["MAXIDORDEN"].ToString());
                        }
                        else
                        {
                            lngPagoDetalle = 0;
                        }

                        DsFPKey.Dispose();

                        //***** BUSCAMOS EL lngFPKey Por registro *****
                        strSQL = " SELECT IdFP_MX FROM TIPOS_DE_PAGO_RELA WITH (NOLOCK)  Where IdPago=" + lngPago + " And IdOrden = " + lngPagoDetalle;
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngFPKey = Int16.Parse(DsFPKey.Tables[0].Rows[0]["IdFP_MX"].ToString());
                        }
                        else
                        {
                            lngFPKey = 0;
                        }

                        DsFPKey.Dispose();

                        //***** CUERPO DEL DATASET  *****   

                        if (row["FPago"].ToString().Trim().ToUpper() == "PUNTOS NINE")
                        {
                           // DvPAgos.RowFilter = "TipoPago='PUNTOS NINE' and importe=" + double.Parse(row["Importe"].ToString())*-1;
                            DvPAgos.RowFilter = "TipoPago='PUNTOS NINE'";
                            strAutorizacion = AutorizacionDevo;  
                            dblImporte = double.Parse(row["Importe"].ToString());
                        }
                        else
                        {
                            if (row["FPago"].ToString().Trim().ToUpper() == "PAR 9")
                            {
                                DvPAgos.RowFilter = "TipoPago='PAR 9'";
                                strAutorizacion = AutorizacionDevo;
                                dblImporte = 0;
                            }
                            else
                            {
                                if (row["FPago"].ToString().Trim().ToUpper() == "BOLSA 5")
                                {
                                    DvPAgos.RowFilter = "TipoPago='BOLSA 5'";
                                    strAutorizacion = AutorizacionDevo; 
                                    dblImporte = 0;
                                }
                                else
                                {
                                    if (row["FPago"].ToString().Trim().ToUpper() == "CERTIFICADO")
                                    {
                                        strAutorizacion = "";
                                        dblImporte = Int16.Parse(row["Importe"].ToString());
                                    }
                                }
                            }
                        }

                        WS_FPagos += lngFPKey.ToString().Trim() + ";" + Math.Round(dblImporte) + ";" + strAutorizacion.ToString().Trim() + "&";

                    }



                }


                Ds.Dispose();

                if (WS_FPagos.ToString().Trim() == "") { WS_FPagos = "1;0;&"; }

                if (WS_FPagos.ToString().Trim() != "") { WS_FPagos = WS_FPagos.Substring(0, (WS_FPagos.Length - 1)); }

                functionReturnValue = WS_FPagos;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;

        }

        public string Get_WS_FPagos(string strIdTicket,DataView DvPAgos)
        {
            string functionReturnValue = "";

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;
                DataSet DsFPKey;

                string strSQL = null;
                long lngPago = 0;
                long lngPagoDetalle = 0;
                double dblImporte = 0;
                long lngFPKey = 0;
                string strAutorizacion = "";
                string WS_FPagos = "";

                strSQL = " SELECT (CASE WHEN IdFP=92 THEN 1 WHEN IdFP=9 THEN 1 ELSE IdFP END) as IdFP,SUM(Importe) as Importe ";
                strSQL += " FROM N_TICKETS_FPAGOS WITH (NOLOCK)  WHERE Id_Tienda='" + _v.Id_Tienda.ToString() + "'";
                strSQL += " AND Id_Ticket='" + strIdTicket.ToString() + "' AND Fecha BETWEEN CONVERT(DATETIME,'" + _v.Fecha.ToShortDateString() + "',103) AND CONVERT(DATETIME,'" + _v.Fecha.ToShortDateString() + " 23:59:59',103)";
                strSQL += " GROUP BY (CASE WHEN IdFP=92 THEN 1 WHEN IdFP=9 THEN 1 ELSE IdFP END) ORDER BY 1";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        if (Int16.Parse(row["Importe"].ToString()) != 0 || Int16.Parse(row["IdFP"].ToString()) != 1)
                        {
                            WS_FPagos += (row["IdFP"].ToString().Trim()) + ";" + Int16.Parse(row["Importe"].ToString()) + ";&";
                        }
                    }

                }
                else
                {
                    functionReturnValue = "";
                }

                Ds.Dispose();

                //*** ACTIVAR CONTROL WS ***
                strSQL = "SELECT FPago,FPagoDetalle,SUM(ImporteDto) as Importe FROM N_TICKETS_DESCUENTOS WITH (NOLOCK)  WHERE Id_Tienda='" + _v.Id_Tienda.ToString().Trim() + "'";
                strSQL += " AND FPago IN('CERTIFICADO','PUNTOS NINE','PAR 9','BOLSA 5') AND Id_Ticket='" + strIdTicket.ToString() + "' AND Fecha BETWEEN CONVERT(DATETIME,'" + _v.Fecha.ToShortDateString() + "',103) AND CONVERT(DATETIME,'" + _v.Fecha.ToShortDateString() + " 23:59:59',103)";
                strSQL += " GROUP BY FPago,FPagoDetalle";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        //***** BUSCAMOS EL lngPago Por registro *****
                        strSQL = "SELECT MAX(IdPago) AS MAXIDPAGO FROM TIPOS_DE_PAGO WITH (NOLOCK)  WHERE Descripcion_Pago='" + row["FPago"].ToString().Trim() + "'";
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngPago = Int16.Parse(DsFPKey.Tables[0].Rows[0]["MAXIDPAGO"].ToString());
                        }
                        else
                        {
                            lngPago = 0;
                        }

                        DsFPKey.Dispose();

                        //***** BUSCAMOS EL lngPagoDetalle Por registro *****
                        strSQL = "SELECT MAX(IdOrden) MAXIDORDEN FROM TIPOS_DE_PAGO_DETALLES WITH (NOLOCK)  WHERE IdPago=" + lngPago + " AND  Descripcion='" + row["FpagoDetalle"].ToString().Trim() + "'";
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngPagoDetalle = Int16.Parse(DsFPKey.Tables[0].Rows[0]["MAXIDORDEN"].ToString());
                        }
                        else
                        {
                            lngPagoDetalle = 0;
                        }

                        DsFPKey.Dispose();

                        //***** BUSCAMOS EL lngFPKey Por registro *****
                        strSQL = " SELECT IdFP_MX FROM TIPOS_DE_PAGO_RELA WITH (NOLOCK)  Where IdPago=" + lngPago + " And IdOrden = " + lngPagoDetalle;
                        DsFPKey = objCapaDatos.GEtSQLDataset(strSQL);

                        if (DsFPKey.Tables[0].Rows.Count > 0)
                        {
                            lngFPKey = Int16.Parse(DsFPKey.Tables[0].Rows[0]["IdFP_MX"].ToString());
                        }
                        else
                        {
                            lngFPKey = 0;
                        }

                        DsFPKey.Dispose();

                        //***** CUERPO DEL DATASET  *****   

                        if (row["FPago"].ToString().Trim().ToUpper() == "PUNTOS NINE")
                        {
                            DvPAgos.RowFilter = "TipoPago='PUNTOS NINE' and importe=" +  double.Parse(row["Importe"].ToString());
                            strAutorizacion = DvPAgos[0]["NumTarjeta"].ToString();  
                            //strAutorizacion = arrSolicitaRedencion[1].strNoAutorizacion.ToString().Trim();
                            dblImporte = double.Parse(row["Importe"].ToString());
                        }
                        else
                        {
                            if (row["FPago"].ToString().Trim().ToUpper() == "PAR 9")
                            {
                                DvPAgos.RowFilter = "TipoPago='PAR 9'"; 
                                strAutorizacion = DvPAgos[0]["NumTarjeta"].ToString(); 

                                //strAutorizacion = arrSolicitaRedencionP9[1].strNoAutorizacion.ToString().Trim();
                                dblImporte = 0;
                            }
                            else
                            {
                                if (row["FPago"].ToString().Trim().ToUpper() == "BOLSA 5")
                                {
                                    DvPAgos.RowFilter = "TipoPago='BOLSA 5'";
                                    strAutorizacion = DvPAgos[0]["NumTarjeta"].ToString(); 
                                    dblImporte = 0;
                                }
                                else
                                {
                                    if (row["FPago"].ToString().Trim().ToUpper() == "CERTIFICADO")
                                    {
                                        strAutorizacion = "";
                                        dblImporte = Int16.Parse(row["Importe"].ToString());
                                    }
                                }
                            }
                        }

                        WS_FPagos += lngFPKey.ToString().Trim() + ";" + Math.Round(dblImporte) + ";" + strAutorizacion.ToString().Trim() + "&";

                    }



                }


                Ds.Dispose();

                if (WS_FPagos.ToString().Trim() == "") { WS_FPagos = "1;0;&"; }

                if (WS_FPagos.ToString().Trim() != "") { WS_FPagos = WS_FPagos.Substring(0, (WS_FPagos.Length - 1)); }

                functionReturnValue = WS_FPagos;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            
            return functionReturnValue;

        }
        public string GetTarjetaClienteDevo(long lngClienteID, DateTime fecha)
        {
            string functionReturnValue = "";

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                if (lngClienteID != -1)
                {
                    strSQL = "SELECT tarj.NumTarjeta FROM N_CLIENTES_GENERAL cg WITH (NOLOCK) INNER JOIN N_CLIENTES_TARJETAS_FIDELIDAD tarj WITH (NOLOCK) ";
                    strSQL += " ON cg.Id_cliente=tarj.IdCliente WHERE cg.Id_Cliente=" + lngClienteID;
                    strSQL += " AND tarj.FechaCaducidad >= CONVERT(DATETIME,'" +fecha.ToShortDateString() + "',103) AND ISNULL(tarj.IdBaja,0)=0 ORDER BY tarj.IdTarjeta DESC ";

                    Ds = objCapaDatos.GEtSQLDataset(strSQL);


                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        functionReturnValue = Ds.Tables[0].Rows[0]["NumTarjeta"].ToString().Trim();
                    }
                    else
                    {
                        functionReturnValue = "";
                    }

                    Ds.Dispose();
                }
                else
                {
                    functionReturnValue = "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        public string GetTarjetaCliente(long lngClienteID)
        {
            string functionReturnValue = "";

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                if (lngClienteID != -1)
                {
                    strSQL = "SELECT tarj.NumTarjeta FROM N_CLIENTES_GENERAL cg WITH (NOLOCK) INNER JOIN N_CLIENTES_TARJETAS_FIDELIDAD tarj WITH (NOLOCK) ";
                    strSQL += " ON cg.Id_cliente=tarj.IdCliente WHERE cg.Id_Cliente=" + lngClienteID;
                    strSQL += " AND tarj.FechaCaducidad >= CONVERT(DATETIME,'" + _v.Fecha.ToShortDateString() + "',103) AND ISNULL(tarj.IdBaja,0)=0 ORDER BY tarj.IdTarjeta DESC ";

                    Ds = objCapaDatos.GEtSQLDataset(strSQL);


                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        functionReturnValue = Ds.Tables[0].Rows[0]["NumTarjeta"].ToString().Trim();
                    }
                    else
                    {
                        functionReturnValue = "";
                    }

                    Ds.Dispose();
                }
                else
                {
                    functionReturnValue = "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        public long Get_WS_Num_Art(string strIdTicket)
        {
            long functionReturnValue = 0;

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                strSQL = " SELECT SUM(td.Estado) as Cantidad FROM N_TICKETS t WITH (NOLOCK) INNER JOIN N_TICKETS_DETALLES td WITH (NOLOCK) ";
                strSQL += " ON t.Id_Auto=td.Id_Auto WHERE t.Id_Ticket='" + strIdTicket + "'";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    functionReturnValue = Int16.Parse(Ds.Tables[0].Rows[0]["Cantidad"].ToString().Trim()) * -1;
                }
                else
                {
                    functionReturnValue = 0;
                }

                Ds.Dispose();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }
        public string Get_WS_DatosRefDevo(string strIdTicket)
        {
            string functionReturnValue = "";

            try
            {
                string str_DatosRef = "";
                double dbl_DatosRef_Dto;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                strSQL = " SELECT art.CodigoALfa,(td.Estado*-1) as CantidadUnd,td.Pvp_Vig,td.ImporteEuros,td.DtoEuroArticulo,td.Id_Articulo, td.IdPosicion FROM N_TICKETS t INNER JOIN N_TICKETS_DETALLES td WITH (NOLOCK) ON td.Id_Auto=t.Id_Auto ";
                strSQL += " INNER JOIN ARTICULOS art WITH (NOLOCK) ON td.Id_Articulo =art.IdArticulo ";
                strSQL += " WHERE t.Id_Ticket='" + strIdTicket + "'";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        dbl_DatosRef_Dto = 0;
                        //BUSCAMOS EL DESCUENTO DE PUNTOS NINE DE LA REFERENCIA 
                        dbl_DatosRef_Dto = Get_WS_DatosRef_Dto(strIdTicket, Int64.Parse(row["Id_Articulo"].ToString().Trim()), Int64.Parse(row["IdPosicion"].ToString().Trim()));

                        str_DatosRef += row["CodigoALfa"].ToString().Trim() + ";" + row["CantidadUnd"].ToString().Trim() + ";";
                        str_DatosRef += row["Pvp_Vig"].ToString().Trim() + ";" + (float.Parse(row["ImporteEuros"].ToString()) + dbl_DatosRef_Dto).ToString() + ";0&";
                        //str_DatosRef += row["DtoEuroArticulo"].ToString().Trim() + ";";
                    }
                }
                else
                {
                    str_DatosRef = "";
                }

                Ds.Dispose();

                functionReturnValue = str_DatosRef.ToString().Trim();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }


        public string Get_WS_DatosRef(string strIdTicket)
        {
            string functionReturnValue = "";

            try
            {
                string str_DatosRef = "";
                double dbl_DatosRef_Dto;
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                strSQL = " SELECT art.CodigoALfa,(td.Estado*-1) as CantidadUnd,td.Pvp_Vig,td.ImporteEuros,td.DtoEuroArticulo,td.Id_Articulo, td.IdPosicion FROM N_TICKETS t INNER JOIN N_TICKETS_DETALLES td WITH (NOLOCK) ON td.Id_Auto=t.Id_Auto ";
                strSQL += " INNER JOIN ARTICULOS art WITH (NOLOCK) ON td.Id_Articulo =art.IdArticulo ";
                strSQL += " WHERE t.Id_Ticket='" + strIdTicket + "'";

                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in Ds.Tables[0].Rows)
                    {
                        dbl_DatosRef_Dto = 0;
                        //BUSCAMOS EL DESCUENTO DE PUNTOS NINE DE LA REFERENCIA 
                        dbl_DatosRef_Dto = Get_WS_DatosRef_Dto(strIdTicket, Int64.Parse(row["Id_Articulo"].ToString().Trim()), Int64.Parse(row["IdPosicion"].ToString().Trim()));

                        str_DatosRef += row["CodigoALfa"].ToString().Trim() + ";" + row["CantidadUnd"].ToString().Trim() + ";";
                        str_DatosRef += row["Pvp_Vig"].ToString().Trim() + ";" + (float.Parse(row["ImporteEuros"].ToString()) + dbl_DatosRef_Dto).ToString() + ";1&";
                        //str_DatosRef += row["DtoEuroArticulo"].ToString().Trim() + ";";
                    }
                }
                else
                {
                    str_DatosRef = "";
                }

                Ds.Dispose();

                functionReturnValue = str_DatosRef.ToString().Trim();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        public double Get_WS_DatosRef_Dto(string strIdTicket, long lngIdArticulo, long idPosicion)
        {
            double functionReturnValue = 0;

            try
            {
                objCapaDatos = new ClsCapaDatos9();
                objCapaDatos.ConexString = strConnection;
                DataSet Ds;

                string strSQL = null;

                if (lngIdArticulo == -1)
                {
                    strSQL = " select isnull(SUM(ImporteDto),0) as TotalDto from N_TICKETS_DESCUENTOS WITH (NOLOCK)  where id_ticket='" + strIdTicket.ToString().Trim() + "' AND FPago='PUNTOS NINE'";
                }
                else
                {
                    strSQL = " select isnull(SUM(ImporteDto),0) as TotalDto from N_TICKETS_DESCUENTOS WITH (NOLOCK)  where id_ticket='" + strIdTicket.ToString().Trim() + "' AND FPago='PUNTOS NINE' AND id_articulo=" + lngIdArticulo + " and idPosicion = " + idPosicion;
                }
                Ds = objCapaDatos.GEtSQLDataset(strSQL);


                if (Ds.Tables[0].Rows.Count > 0)
                {
                    functionReturnValue = Int64.Parse(Ds.Tables[0].Rows[0]["TotalDto"].ToString());
                }
                else
                {
                    functionReturnValue = 0;
                }

                Ds.Dispose();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return functionReturnValue;
        }

        #endregion

    }
}
