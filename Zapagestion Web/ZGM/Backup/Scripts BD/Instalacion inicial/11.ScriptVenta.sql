create PROCEDURE [dbo].[AVE_InsertarTicket] 
	@Id_Ticket varchar(20),
    @Id_Tienda varchar(10),
    @Id_Empleado int,
    @Id_Abonado_Empleado int,
    @Id_Pago int,
    @Fecha datetime,
    @NombreTarjeta varchar(50),
    @TotalEuro float,
    @cEfectivoEuro float,
    @cValeEuro float,
    @cTarjetaEuro float,
    @cCuentaEuro float,
    @TotalParaValeEuro float,
    @cRecibidoEuro float,
    @DescuentoEuro float,
    @ComisionVenta float,
    @Id_Cliente_N int,
    @ID_TERMINAL varchar(10),
    @Id_SubPago varchar(10),
    @IdCajero int,
    @NumFactura varchar(50),
    @Comentarios varchar(100),
    @CPostal varchar(10),
    @IdMonedaCliente varchar(10),
    @IdMonedaTienda varchar(10),
    @CambioTienda numeric(16,4),
    @CIFSociedad varchar(50),
    @IDAuto int out
  
AS
BEGIN
	INSERT INTO [N_TICKETS]
           ([Id_Ticket]
           ,[Id_Tienda]
           ,[Id_Empleado]
           ,[Id_Cliente]
           ,[Id_Abonado_Empleado]
           ,[Id_Pago]
           ,[Fecha]
           ,[NombreTarjeta]
           ,[TotalEuro]
           ,[cEfectivoEuro]
           ,[cValeEuro]
           ,[cTarjetaEuro]
           ,[cCuentaEuro]
           ,[TotalParaValeEuro]
           ,[cRecibidoEuro]
           ,[DescuentoEuro]
           ,[ComisionVenta]
           ,[Id_Cliente_N]
           ,[ID_TERMINAL]
           ,[Id_SubPago]
           ,[IdCajero]
           ,[NumFactura]
           ,[Comentarios]
           ,[CPostal]
           ,[IdMonedaCliente]
           ,[IdMonedaTienda]
           ,[CambioTienda]
           ,[CIFSociedad]
           ,[TipoFactura]
           ,[PrintFact]
           ,[NCF]
           ,[Hash]
           ,[Agente] )
     VALUES
           (@Id_Ticket 
           ,@Id_Tienda 
           ,@Id_Empleado 
           ,0
           ,@Id_Abonado_Empleado
           ,@Id_Pago
           ,@Fecha
           ,@NombreTarjeta
           ,@TotalEuro 
           ,@cEfectivoEuro 
           ,@cValeEuro 
           ,@cTarjetaEuro
           ,@cCuentaEuro
           ,@TotalParaValeEuro
           ,@cRecibidoEuro
           ,@DescuentoEuro
           ,@ComisionVenta
           ,@Id_Cliente_N 
           ,@ID_TERMINAL 
           ,@Id_SubPago
           ,@IdCajero 
           ,@NumFactura 
           ,@Comentarios 
           ,@CPostal
           ,@IdMonedaCliente
           ,@IdMonedaTienda
           ,@CambioTienda
           ,@CIFSociedad
           ,'AUT'
           ,0
           ,null
           ,null
           ,'1')
           
          select @IDAuto=@@IDENTITY  
           
END

GO 


CREATE PROCEDURE AVE_TICKETDETALLE

@Id_Auto int,
@Id_Articulo int,
@Id_cabecero_detalle int,
@Id_Tienda varchar(10),
@ImporteEuros float,
@Estado smallint,
@MotivoCambioPrecio varchar(100),
@DtoEuroArticulo float,
@ComisionPremio float,
@IdAlmacen int,
@ImporteBase float,
@motivo_devolucion varchar(100),
@Cancelado varchar(1),
@Comentarios varchar(100),
@IVA float,
@Pvp_Vig float,
@IdPosicion int,
@Asesor varchar(100),
@ComisionAsesor float,
@IdConcesionTienda int



AS

DECLARE @Coste_Articulo float
DECLARE @Pvp_Or float

SELECT @Coste_Articulo=PrecioCostoReal,
       @Pvp_Or=precioVentaEuro 
from articulos where idarticulo=@id_articulo

INSERT INTO [N_TICKETS_DETALLES]
           ([Id_Auto]
           ,[Id_Articulo]
           ,[Id_cabecero_detalle]
           ,[Id_Tienda]
           ,[ImporteEuros]
           ,[Estado]
           ,[MotivoCambioPrecio]
           ,[DtoEuroArticulo]
           ,[ComisionPremio]
           ,[Disco]
           ,[EstadoArticulo]
           ,[IdAlmacen]
           ,[ImporteBase]
           ,[motivo_devolucion]
           ,[Cancelado]
           ,[Comentarios]
           ,[IVA]
           ,[Pvp_Vig]
           ,[Pvp_Or]
           ,[IdPosicion]
           ,[Asesor]
           ,[ComisionAsesor]
           ,[IdConcesionTienda]
           ,[IdCompostura]
           ,[Coste_Articulo]
           ,[IdAutorizadorDto])
     VALUES
           (@id_Auto
           ,@Id_Articulo 
           ,@Id_cabecero_detalle 
           ,@Id_Tienda 
           ,@ImporteEuros
           ,@Estado   
           ,@MotivoCambioPrecio
           ,@DtoEuroArticulo
           ,@ComisionPremio
           ,0
           ,'N'
           ,@IdAlmacen
           ,@ImporteBase
           ,@motivo_devolucion
           ,@Cancelado
           ,@Comentarios
           ,@IVA
           ,@Pvp_Vig
           ,@Pvp_Or
           ,@IdPosicion 
           ,@Asesor 
           ,@ComisionAsesor
           ,@IdConcesionTienda
           ,null
           ,@Coste_Articulo
           ,null)

GO

CREATE PROCEDURE [dbo].[AVE_TICKETESTADOS]
@Id_ticket varchar(20)
as

INSERT INTO N_TICKETS_ESTADOS(Id_Tienda,
                              Id_Terminal,
                              Id_Ticket,
                              Id_Cliente,
                              IdEmpleado,
                              Fecha,
                              Operacion,
                              Id_Articulo,
                              Id_cabecero_detalle,
                              Cantidad,
                              Importe,
                              MotivoCambioPrecio,
                              DtoEuroArticulo,
                              Fecha_Modificacion,
                              Id_Tienda_Venta,
                              Id_Terminal_Venta,
                              Id_Ticket_Venta,
                              Fecha_Venta,
                              Asesor,
                              IdConcesionTienda,
                              NumFactura,
                              IdAutorizadorDto)
      Select N.id_tienda,N.ID_TERMINAL,@id_ticket,Id_Cliente_N,
             Id_Empleado,Fecha,
             CASE WHEN  Estado<0 THEN 'VENTA'
             ELSE 'DEVOLUCION' end,Id_Articulo,Id_cabecero_detalle,Estado,
             ImporteEuros,MotivoCambioPrecio,DtoEuroArticulo,GETDATE(),
             null,null,null,null,Asesor,IdConcesionTienda,NumFactura,IdAutorizadorDto                
            from N_TICKETS N inner join 
           N_TICKETS_DETALLES D on 
           N.id_Auto=D.id_auto
           where id_ticket=@Id_TICKET
    
    
    
    
GO

GO
create PROCEDURE [dbo].[AVE_HISTORICOTRANS]

@Id_Tienda varchar(50),
@Id_Terminal int,
@Fecha Datetime,
@Concepto varchar(100),
@NTicket varchar(20),
@Importe float,
@Visto_Trans varchar(1)

AS

DECLARE @IdTransaccion int
DECLARE @NTransSesion int


SELECT @IdTransaccion=isnull(MAX(IdTransaccion),0)+1 FROM HISTORICO_NTRANS WITH (NOLOCK)  
   WHERE IdTienda=@Id_Tienda 
   
   
SELECT @NTransSesion=isnull(MAX(IdTransaccion),0)+1 FROM HISTORICO_NTRANS WITH (NOLOCK)  
   WHERE IdTienda=@Id_Tienda  and Fecha between @Fecha and  DATEADD(DAY,1,@Fecha) 


INSERT INTO [HISTORICO_NTRANS]
           ([IdTransaccion]
           ,[IdTienda]
           ,[Id_Terminal]
           ,[Fecha]
           ,[NTransSesion]
           ,[Concepto]
           ,[NTicket]
           ,[Importe]
           ,[Visto_Trans])
     VALUES
           (@IdTransaccion
           ,@Id_Tienda 
           ,@Id_Terminal 
           ,@Fecha
           ,@NTransSesion
           ,@Concepto
           ,@NTicket
           ,@Importe
           ,@Visto_Trans)
    
    