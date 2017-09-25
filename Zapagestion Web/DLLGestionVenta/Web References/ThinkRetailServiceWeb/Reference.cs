﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.18444.
// 
#pragma warning disable 1591

namespace DLLGestionVenta.ThinkRetailServiceWeb {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsThinkRetailSoap", Namespace="http://mypiagui.com:1080/")]
    public partial class wsThinkRetail : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreaReservaTROperationCompleted;
        
        private System.Threading.SendOrPostCallback CancelaReservaTROperationCompleted;
        
        private System.Threading.SendOrPostCallback getInventarioACOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsThinkRetail() {
            this.Url = global::DLLGestionVenta.Properties.Settings.Default.DLLGestionVenta_ThinkRetailServiceWeb_wsThinkRetail;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event CreaReservaTRCompletedEventHandler CreaReservaTRCompleted;
        
        /// <remarks/>
        public event CancelaReservaTRCompletedEventHandler CancelaReservaTRCompleted;
        
        /// <remarks/>
        public event getInventarioACCompletedEventHandler getInventarioACCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mypiagui.com:1080/Login", RequestNamespace="http://mypiagui.com:1080/", ResponseNamespace="http://mypiagui.com:1080/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Login(string User, string Pwd) {
            object[] results = this.Invoke("Login", new object[] {
                        User,
                        Pwd});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string User, string Pwd) {
            this.LoginAsync(User, Pwd, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string User, string Pwd, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        User,
                        Pwd}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mypiagui.com:1080/CreaReservaTR", RequestNamespace="http://mypiagui.com:1080/", ResponseNamespace="http://mypiagui.com:1080/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Salida CreaReservaTR(string NoPedido, string Almacen, string Almacen_Salida, string NumVendedor, [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)] Row_Input[] Posiciones) {
            object[] results = this.Invoke("CreaReservaTR", new object[] {
                        NoPedido,
                        Almacen,
                        Almacen_Salida,
                        NumVendedor,
                        Posiciones});
            return ((Salida)(results[0]));
        }
        
        /// <remarks/>
        public void CreaReservaTRAsync(string NoPedido, string Almacen, string Almacen_Salida, string NumVendedor, Row_Input[] Posiciones) {
            this.CreaReservaTRAsync(NoPedido, Almacen, Almacen_Salida, NumVendedor, Posiciones, null);
        }
        
        /// <remarks/>
        public void CreaReservaTRAsync(string NoPedido, string Almacen, string Almacen_Salida, string NumVendedor, Row_Input[] Posiciones, object userState) {
            if ((this.CreaReservaTROperationCompleted == null)) {
                this.CreaReservaTROperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreaReservaTROperationCompleted);
            }
            this.InvokeAsync("CreaReservaTR", new object[] {
                        NoPedido,
                        Almacen,
                        Almacen_Salida,
                        NumVendedor,
                        Posiciones}, this.CreaReservaTROperationCompleted, userState);
        }
        
        private void OnCreaReservaTROperationCompleted(object arg) {
            if ((this.CreaReservaTRCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreaReservaTRCompleted(this, new CreaReservaTRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mypiagui.com:1080/CancelaReservaTR", RequestNamespace="http://mypiagui.com:1080/", ResponseNamespace="http://mypiagui.com:1080/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SalidaMsg CancelaReservaTR(string NoPedido, string Almacen) {
            object[] results = this.Invoke("CancelaReservaTR", new object[] {
                        NoPedido,
                        Almacen});
            return ((SalidaMsg)(results[0]));
        }
        
        /// <remarks/>
        public void CancelaReservaTRAsync(string NoPedido, string Almacen) {
            this.CancelaReservaTRAsync(NoPedido, Almacen, null);
        }
        
        /// <remarks/>
        public void CancelaReservaTRAsync(string NoPedido, string Almacen, object userState) {
            if ((this.CancelaReservaTROperationCompleted == null)) {
                this.CancelaReservaTROperationCompleted = new System.Threading.SendOrPostCallback(this.OnCancelaReservaTROperationCompleted);
            }
            this.InvokeAsync("CancelaReservaTR", new object[] {
                        NoPedido,
                        Almacen}, this.CancelaReservaTROperationCompleted, userState);
        }
        
        private void OnCancelaReservaTROperationCompleted(object arg) {
            if ((this.CancelaReservaTRCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CancelaReservaTRCompleted(this, new CancelaReservaTRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mypiagui.com:1080/getInventarioAC", RequestNamespace="http://mypiagui.com:1080/", ResponseNamespace="http://mypiagui.com:1080/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode getInventarioAC() {
            object[] results = this.Invoke("getInventarioAC", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void getInventarioACAsync() {
            this.getInventarioACAsync(null);
        }
        
        /// <remarks/>
        public void getInventarioACAsync(object userState) {
            if ((this.getInventarioACOperationCompleted == null)) {
                this.getInventarioACOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetInventarioACOperationCompleted);
            }
            this.InvokeAsync("getInventarioAC", new object[0], this.getInventarioACOperationCompleted, userState);
        }
        
        private void OngetInventarioACOperationCompleted(object arg) {
            if ((this.getInventarioACCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getInventarioACCompleted(this, new getInventarioACCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mypiagui.com:1080/")]
    public partial class Row_Input {
        
        private int posicionField;
        
        private string numMaterialField;
        
        private string tallaField;
        
        private int cantidadField;
        
        /// <comentarios/>
        public int Posicion {
            get {
                return this.posicionField;
            }
            set {
                this.posicionField = value;
            }
        }
        
        /// <comentarios/>
        public string NumMaterial {
            get {
                return this.numMaterialField;
            }
            set {
                this.numMaterialField = value;
            }
        }
        
        /// <comentarios/>
        public string Talla {
            get {
                return this.tallaField;
            }
            set {
                this.tallaField = value;
            }
        }
        
        /// <comentarios/>
        public int Cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mypiagui.com:1080/")]
    public partial class SalidaMsg {
        
        private string stsField;
        
        private string descripcionField;
        
        private Mensaje[] mensajesField;
        
        /// <comentarios/>
        public string Sts {
            get {
                return this.stsField;
            }
            set {
                this.stsField = value;
            }
        }
        
        /// <comentarios/>
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Mensaje[] Mensajes {
            get {
                return this.mensajesField;
            }
            set {
                this.mensajesField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mypiagui.com:1080/")]
    public partial class Mensaje {
        
        private string tipoField;
        
        private string descripcionField;
        
        /// <comentarios/>
        public string Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
            }
        }
        
        /// <comentarios/>
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mypiagui.com:1080/")]
    public partial class Row_Output {
        
        private int posicionField;
        
        private string numMaterialField;
        
        private string tallaField;
        
        private int cantidadField;
        
        private string stsField;
        
        private Mensaje[] mensajesField;
        
        /// <comentarios/>
        public int Posicion {
            get {
                return this.posicionField;
            }
            set {
                this.posicionField = value;
            }
        }
        
        /// <comentarios/>
        public string NumMaterial {
            get {
                return this.numMaterialField;
            }
            set {
                this.numMaterialField = value;
            }
        }
        
        /// <comentarios/>
        public string Talla {
            get {
                return this.tallaField;
            }
            set {
                this.tallaField = value;
            }
        }
        
        /// <comentarios/>
        public int Cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
        
        /// <comentarios/>
        public string Sts {
            get {
                return this.stsField;
            }
            set {
                this.stsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Mensaje[] Mensajes {
            get {
                return this.mensajesField;
            }
            set {
                this.mensajesField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mypiagui.com:1080/")]
    public partial class Salida {
        
        private string stsField;
        
        private string descripcionField;
        
        private Mensaje[] mensajesField;
        
        private Row_Output[] rowsField;
        
        /// <comentarios/>
        public string Sts {
            get {
                return this.stsField;
            }
            set {
                this.stsField = value;
            }
        }
        
        /// <comentarios/>
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Mensaje[] Mensajes {
            get {
                return this.mensajesField;
            }
            set {
                this.mensajesField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Row_Output[] Rows {
            get {
                return this.rowsField;
            }
            set {
                this.rowsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void CreaReservaTRCompletedEventHandler(object sender, CreaReservaTRCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreaReservaTRCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreaReservaTRCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Salida Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Salida)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void CancelaReservaTRCompletedEventHandler(object sender, CancelaReservaTRCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CancelaReservaTRCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CancelaReservaTRCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SalidaMsg Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SalidaMsg)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void getInventarioACCompletedEventHandler(object sender, getInventarioACCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getInventarioACCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getInventarioACCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591