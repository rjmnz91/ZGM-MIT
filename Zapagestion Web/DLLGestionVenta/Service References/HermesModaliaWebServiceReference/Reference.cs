﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLLGestionVenta.HermesModaliaWebServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com", ConfigurationName="HermesModaliaWebServiceReference.HermesImpl")]
    public interface HermesImpl {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento login del espacio de nombres http://server.hermes.ecommerce.hermes.com no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderResponse insertOrder(DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento login del espacio de nombres http://server.hermes.ecommerce.hermes.com no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        DLLGestionVenta.HermesModaliaWebServiceReference.updateStockResponse updateStock(DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento login del espacio de nombres http://server.hermes.ecommerce.hermes.com no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        DLLGestionVenta.HermesModaliaWebServiceReference.getStockResponse getStock(DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento login del espacio de nombres http://server.hermes.ecommerce.hermes.com no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionResponse insertInfoIntegracion(DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertOrderRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertOrder", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequestBody Body;
        
        public insertOrderRequest() {
        }
        
        public insertOrderRequest(DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class insertOrderRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string idSite;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string in0;
        
        public insertOrderRequestBody() {
        }
        
        public insertOrderRequestBody(string login, string password, string idSite, string in0) {
            this.login = login;
            this.password = password;
            this.idSite = idSite;
            this.in0 = in0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertOrderResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertOrderResponse", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderResponseBody Body;
        
        public insertOrderResponse() {
        }
        
        public insertOrderResponse(DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class insertOrderResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string insertOrderReturn;
        
        public insertOrderResponseBody() {
        }
        
        public insertOrderResponseBody(string insertOrderReturn) {
            this.insertOrderReturn = insertOrderReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class updateStockRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="updateStock", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequestBody Body;
        
        public updateStockRequest() {
        }
        
        public updateStockRequest(DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class updateStockRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string idSite;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string idTienda;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string codigoAlfa;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int stock;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string tallaOriginalCliente;
        
        public updateStockRequestBody() {
        }
        
        public updateStockRequestBody(string login, string password, string idSite, string idTienda, string codigoAlfa, int stock, string tallaOriginalCliente) {
            this.login = login;
            this.password = password;
            this.idSite = idSite;
            this.idTienda = idTienda;
            this.codigoAlfa = codigoAlfa;
            this.stock = stock;
            this.tallaOriginalCliente = tallaOriginalCliente;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class updateStockResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="updateStockResponse", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.updateStockResponseBody Body;
        
        public updateStockResponse() {
        }
        
        public updateStockResponse(DLLGestionVenta.HermesModaliaWebServiceReference.updateStockResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class updateStockResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string updateStockReturn;
        
        public updateStockResponseBody() {
        }
        
        public updateStockResponseBody(string updateStockReturn) {
            this.updateStockReturn = updateStockReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getStockRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getStock", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequestBody Body;
        
        public getStockRequest() {
        }
        
        public getStockRequest(DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class getStockRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string idSite;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string codigoAlfa;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string tallaOriginalCliente;
        
        public getStockRequestBody() {
        }
        
        public getStockRequestBody(string login, string password, string idSite, string codigoAlfa, string tallaOriginalCliente) {
            this.login = login;
            this.password = password;
            this.idSite = idSite;
            this.codigoAlfa = codigoAlfa;
            this.tallaOriginalCliente = tallaOriginalCliente;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getStockResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getStockResponse", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.getStockResponseBody Body;
        
        public getStockResponse() {
        }
        
        public getStockResponse(DLLGestionVenta.HermesModaliaWebServiceReference.getStockResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class getStockResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string getStockReturn;
        
        public getStockResponseBody() {
        }
        
        public getStockResponseBody(string getStockReturn) {
            this.getStockReturn = getStockReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertInfoIntegracionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertInfoIntegracion", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequestBody Body;
        
        public insertInfoIntegracionRequest() {
        }
        
        public insertInfoIntegracionRequest(DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class insertInfoIntegracionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int idSite;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int idFase;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public System.DateTime fecha;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int productosEnviados;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public int productosIntegrados;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public int productosConFoto;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public int productosConStock;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public int productosPublicados;
        
        public insertInfoIntegracionRequestBody() {
        }
        
        public insertInfoIntegracionRequestBody(string login, string password, int idSite, int idFase, System.DateTime fecha, int productosEnviados, int productosIntegrados, int productosConFoto, int productosConStock, int productosPublicados) {
            this.login = login;
            this.password = password;
            this.idSite = idSite;
            this.idFase = idFase;
            this.fecha = fecha;
            this.productosEnviados = productosEnviados;
            this.productosIntegrados = productosIntegrados;
            this.productosConFoto = productosConFoto;
            this.productosConStock = productosConStock;
            this.productosPublicados = productosPublicados;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertInfoIntegracionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertInfoIntegracionResponse", Namespace="http://server.hermes.ecommerce.hermes.com", Order=0)]
        public DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionResponseBody Body;
        
        public insertInfoIntegracionResponse() {
        }
        
        public insertInfoIntegracionResponse(DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://server.hermes.ecommerce.hermes.com")]
    public partial class insertInfoIntegracionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string insertInfoIntegracionReturn;
        
        public insertInfoIntegracionResponseBody() {
        }
        
        public insertInfoIntegracionResponseBody(string insertInfoIntegracionReturn) {
            this.insertInfoIntegracionReturn = insertInfoIntegracionReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HermesImplChannel : DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HermesImplClient : System.ServiceModel.ClientBase<DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl>, DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl {
        
        public HermesImplClient() {
        }
        
        public HermesImplClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HermesImplClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HermesImplClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HermesImplClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderResponse DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl.insertOrder(DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequest request) {
            return base.Channel.insertOrder(request);
        }
        
        public string insertOrder(string login, string password, string idSite, string in0) {
            DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequest inValue = new DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequest();
            inValue.Body = new DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.idSite = idSite;
            inValue.Body.in0 = in0;
            DLLGestionVenta.HermesModaliaWebServiceReference.insertOrderResponse retVal = ((DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl)(this)).insertOrder(inValue);
            return retVal.Body.insertOrderReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DLLGestionVenta.HermesModaliaWebServiceReference.updateStockResponse DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl.updateStock(DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequest request) {
            return base.Channel.updateStock(request);
        }
        
        public string updateStock(string login, string password, string idSite, string idTienda, string codigoAlfa, int stock, string tallaOriginalCliente) {
            DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequest inValue = new DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequest();
            inValue.Body = new DLLGestionVenta.HermesModaliaWebServiceReference.updateStockRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.idSite = idSite;
            inValue.Body.idTienda = idTienda;
            inValue.Body.codigoAlfa = codigoAlfa;
            inValue.Body.stock = stock;
            inValue.Body.tallaOriginalCliente = tallaOriginalCliente;
            DLLGestionVenta.HermesModaliaWebServiceReference.updateStockResponse retVal = ((DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl)(this)).updateStock(inValue);
            return retVal.Body.updateStockReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DLLGestionVenta.HermesModaliaWebServiceReference.getStockResponse DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl.getStock(DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequest request) {
            return base.Channel.getStock(request);
        }
        
        public string getStock(string login, string password, string idSite, string codigoAlfa, string tallaOriginalCliente) {
            DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequest inValue = new DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequest();
            inValue.Body = new DLLGestionVenta.HermesModaliaWebServiceReference.getStockRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.idSite = idSite;
            inValue.Body.codigoAlfa = codigoAlfa;
            inValue.Body.tallaOriginalCliente = tallaOriginalCliente;
            DLLGestionVenta.HermesModaliaWebServiceReference.getStockResponse retVal = ((DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl)(this)).getStock(inValue);
            return retVal.Body.getStockReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionResponse DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl.insertInfoIntegracion(DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequest request) {
            return base.Channel.insertInfoIntegracion(request);
        }
        
        public string insertInfoIntegracion(string login, string password, int idSite, int idFase, System.DateTime fecha, int productosEnviados, int productosIntegrados, int productosConFoto, int productosConStock, int productosPublicados) {
            DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequest inValue = new DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequest();
            inValue.Body = new DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.idSite = idSite;
            inValue.Body.idFase = idFase;
            inValue.Body.fecha = fecha;
            inValue.Body.productosEnviados = productosEnviados;
            inValue.Body.productosIntegrados = productosIntegrados;
            inValue.Body.productosConFoto = productosConFoto;
            inValue.Body.productosConStock = productosConStock;
            inValue.Body.productosPublicados = productosPublicados;
            DLLGestionVenta.HermesModaliaWebServiceReference.insertInfoIntegracionResponse retVal = ((DLLGestionVenta.HermesModaliaWebServiceReference.HermesImpl)(this)).insertInfoIntegracion(inValue);
            return retVal.Body.insertInfoIntegracionReturn;
        }
    }
}