USE [JUEVES_PIAGUI]
GO

/****** Object:  StoredProcedure [dbo].[AVE_InsertaDevolucion]    Script Date: 01/13/2015 12:48:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[AVE_InsertaDevolucion] @IdTicket varchar(20),@IdTienda varchar(10), @IdArticulo int, @Talla varchar(30), @Respuesta varchar(2048)OUTPUT
 AS 
 BEGIN
 
 DECLARE  @Id_ticketV VARCHAR(20)='', @id_tiendaV VARCHAR(10)='',  
		@fechaV DATETIME, @id_terminalV VARCHAR(10), @Id_ClienteV int, @Total_EuroV float,
		@Id_EmpleadoV int, @ComentariosV varchar(100),@Cantidad int,@PrecioEuro float
 DECLARE @ImporteBase float, @DtoEuroArticulo float, @IdCabeceroDetalle int, @iva float,@pvpVig float, @posicion int
 DECLARE @Id_Auto int, @Id_Ticket_Nuevo varchar(20)
 DECLARE @IdTransaccion int
 DECLARE @NTransSesion int
 declare @FechaT datetime
 
 
 
 SELECT @Id_ticketV=T.Id_Ticket,@id_tiendaV=T.Id_Tienda, @Id_EmpleadoV=t.Id_Empleado, @ComentariosV=T.Comentarios,
                 @IdCabeceroDetalle=TD.Id_cabecero_detalle,@Cantidad=(TD.Estado * -1),@fechaV=T.Fecha ,
                  @PrecioEuro=PA.PrecioEuro, @Total_EuroV=(TD.ImporteEuros)*-1,@DtoEuroArticulo=TD.DtoEuroArticulo,
                  @Id_ClienteV=T.Id_Cliente_N, @id_terminalV= T.ID_TERMINAL, @ImporteBase=(TD.ImporteBase *-1), 
                  @iva= TD.IVA, @pvpVig=(TD.Pvp_Vig*-1), @posicion= TD.IdPosicion
                 FROM N_TICKETS T WITH (NOLOCK) 
                 INNER JOIN N_TICKETS_DETALLES TD WITH (NOLOCK) ON T.Id_Auto  = TD.Id_Auto 
                 INNER JOIN CABECEROS_DETALLES CABD WITH (NOLOCK) ON CABD.Id_Cabecero_Detalle=TD.Id_cabecero_detalle 
                 INNER JOIN ARTICULOS ART WITH (NOLOCK) ON ART.IdArticulo=TD.Id_Articulo 
                 INNER JOIN PRECIOS_ARTICULOS PA WITH (NOLOCK) ON PA.Idarticulo= TD.Id_Articulo  AND PA.IdTienda= T.Id_Tienda 
                 AND PA.Activo =1 
                 WHERE T.Id_Ticket=@IdTicket AND T.Id_Tienda=@IdTienda and CABD.NOMBRE_TALLA=@Talla

 if @@ROWCOUNT > 0
 BEGIN
 BEGIN TRAN 
 DECLARE @ErrorMessage varchar(2000)
 BEGIN TRY
 --GENERAMOS UN Nº DE TICKET NUEVO
 SET @Id_Ticket_Nuevo = dbo.AVE_GenerarIdTicketDevo(@IdTienda,@id_terminalV)
 -- CREAMOS EL TICKET DE DEVOLUCION
 INSERT INTO N_TICKETS(Id_Ticket,Id_Tienda,Fecha,TotalEuro,Id_Cliente,Id_Empleado,Id_Abonado_Empleado,Id_Pago,
	NombreTarjeta,cEfectivoEuro,cValeEuro,cTarjetaEuro,cCuentaEuro,TotalParaValeEuro,cRecibidoEuro,DescuentoEuro,
	ComisionVenta,Id_Cliente_N,ID_TERMINAL,Id_SubPago,IdCajero,NumFactura,Comentarios,CPostal,IdMonedaCliente,
	IdMonedaTienda,CambioTienda,CIFSociedad,TipoFactura,PrintFact,NCF,[Hash],Agente)
	VALUES(@Id_Ticket_Nuevo, @id_tiendaV,GETDATE(),@Total_EuroV,@Id_ClienteV,@Id_EmpleadoV,0,3,
	'',0,@Total_EuroV,0,0,0,@Total_EuroV,0,
	0,@Id_ClienteV,@id_terminalV,3,@Id_EmpleadoV,'',@ComentariosV,'','',
	'',0,'' ,'AUT',0,null,null,'1')
   
  SET @Id_Auto =@@IDENTITY
  
  --CREAMOS EL DETALLE DEL TICKET DE DEVOLUCION
  
  DECLARE @Coste_Articulo float
DECLARE @Pvp_Or float

SELECT @Coste_Articulo=PrecioCostoReal,
       @Pvp_Or=precioVentaEuro 
	from articulos where idarticulo=@IdArticulo
  
  INSERT INTO N_TICKETS_DETALLES(Id_Auto,Id_Articulo,Id_cabecero_detalle,Id_Tienda,ImporteEuros,Estado,MotivoCambioPrecio,
  DtoEuroArticulo,ComisionPremio,IdAlmacen,ImporteBase,motivo_devolucion,Cancelado,Comentarios,IVA,Pvp_Vig,IdPosicion,
  Asesor,ComisionAsesor,IdConcesionTienda,IdCompostura,Coste_Articulo,IdAutorizadorDto,Pvp_Or)
  VALUES(@Id_Auto,@IdArticulo,@IdCabeceroDetalle,@id_tiendaV,@Total_EuroV,@Cantidad,'',
  0,0,0,@ImporteBase,'','','',@iva,@pvpVig,@posicion,
  @Id_EmpleadoV,0,0 ,null,@Coste_Articulo,null,@Pvp_Or)
  
  --EN ESTADOS SE ASOCIA EL TICKET ANTIGUO CON EL NUEVO DE DEVOLUCION
  INSERT INTO N_TICKETS_ESTADOS(Id_Tienda,Id_Terminal,Id_Ticket,Fecha,Operacion,NumFactura,Id_Articulo,Id_Cabecero_detalle,Cantidad,
  Importe,DtoEuroArticulo,Id_Tienda_Venta,Id_Terminal_Venta,Id_Ticket_Venta,Fecha_Venta,Fecha_Modificacion,
  IdEmpleado,CIFSociedad,MotivoCambioPrecio,Asesor,IdConcesionTienda,TipoFactura,ReferenciaP)
  VALUES(@id_tiendaV,@id_terminalV,@Id_Ticket_Nuevo,GETDATE(),'DEVOLUCION','',@IdArticulo,@IdCabeceroDetalle,@Cantidad,
  @Total_EuroV,0,@id_tiendaV,0,@Id_ticketV,GETDATE(),GETDATE(),
  @Id_EmpleadoV,'','','',0,'AUT','')
  
  --ACTUALIZAMOS EXISTENCIAS
  UPDATE  ex SET ex.Cantidad=ex.Cantidad + @cantidad 
        FROM N_EXISTENCIAS ex WHERE ex.IdArticulo=@IdArticulo
		AND ex.Id_cabecero_detalle=@IdCabeceroDetalle
		AND ex.IdTienda=@id_tiendaV

	--Insertar el Stock de Referencia Talla NO existente		
  INSERT INTO N_EXISTENCIAS(IdArticulo,IdTienda,Id_Cabecero_Detalle,Cantidad)
	SELECT art.IdArticulo,t.IdTienda,cd.Id_cabecero_detalle,@Cantidad 
     FROM TIENDAS t WITH (NOLOCK)  INNER JOIN ARTICULOS art WITH (NOLOCK)  ON 1=1 
     INNER JOIN CABECEROS_DETALLES cd WITH (NOLOCK)  ON art.Id_cabecero=cd.Id_cabecero 
     WHERE art.IdArticulo=@IdArticulo
     AND t.IdTienda=@id_tiendaV AND cd.Id_cabecero_detalle=@IdCabeceroDetalle
     AND (select count(ex.Cantidad) FROM N_EXISTENCIAS ex WITH (NOLOCK)  WHERE ex.IdArticulo=art.IdArticulo AND 
     ex.Id_Cabecero_Detalle=cd.Id_Cabecero_Detalle AND ex.IdTienda=t.IdTienda)=0  GROUP BY art.IdArticulo,t.IdTienda,cd.Id_cabecero_Detalle 
            
   
   --insertamos en N_HISTORICO
   INSERT INTO N_HISTORICO(IdArticulo,Concepto,Origen,Destino,IdEmpleado,Fecha,Id_Cabecero_Detalle,Cantidad,Numero,
   IdTienda,sEtiqueta,Fecha_Modificacion,RefFP)
   VALUES(@IdArticulo,'Devolucion',@id_tiendaV,'Cliente',@Id_EmpleadoV,GETDATE(),@IdCabeceroDetalle,@Cantidad,@Id_Ticket_Nuevo,
   @id_tiendaV,'C',GETDATE(),'')
   
   --insertamos en N_TICKETS_FPAGOS
   INSERT INTO N_TICKETS_FPAGOS(Id_Ticket,IdOrden,Id_Tienda,Id_Terminal,Fecha,FPago,FPagoDetalle,Tipo,
   TipoOrigen,Importe,Divisa,OtraDivisa,OtraDivisaImporte,OtraDivisaCambio,NumTarjeta,NumTarjetaAutoriza,NumTarjetaOperacion,
   ValeId,ValeTienda,Visto_Pago,IdFP,IdConcesionTienda,CuotaIVA,Id_Empleado,NomTitular,NomEntidad,RefFP,Id_Cliente,Fecha_Modificacion)
   VALUES(@Id_Ticket_Nuevo,1,@id_tiendaV,@id_terminalV,GETDATE(),'INTERNET','','OTRAS/OTHERS',
   'Devolucion',@Total_EuroV,'MXN','',0,0,'','','',
   0,'','',0,0,0,@Id_EmpleadoV,'','','',@Id_ClienteV,GETDATE())
   
   --INSERTAMOS TICKET TABLA (HISTORICO_NTRANS) DE DEVOLUCIONES
   SELECT @IdTransaccion=isnull(MAX(IdTransaccion),0)+1 FROM HISTORICO_NTRANS WITH (NOLOCK)  
   WHERE IdTienda=@id_tiendaV 
   
   set @FechaT= GETDATE()
	SELECT @NTransSesion=isnull(MAX(IdTransaccion),0)+1 FROM HISTORICO_NTRANS WITH (NOLOCK)  
   WHERE IdTienda=@id_tiendaV  and Fecha between @FechaT and  DATEADD(DAY,1,@FechaT) 

   
   INSERT INTO HISTORICO_NTRANS(IdTransaccion,IdTienda,Id_Terminal,Fecha,NTransSesion,Concepto,NTicket,Importe,Visto_Trans)
   VALUES(@IdTransaccion,@Id_ticketV,NULL,GETDATE(),@NTransSesion,'Devolucion',@Id_Ticket_Nuevo,@Total_EuroV,'')
   IF @@TRANCOUNT > 0
         COMMIT TRANSACTION;
    set @Respuesta='OK.' + @Id_Ticket_Nuevo
 

 END TRY
 BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    SELECT @ErrorMessage = ERROR_MESSAGE();
	RAISERROR (@ErrorMessage, 10, 1);
	set @Respuesta='NOK.' + @ErrorMessage
 END CATCH
 END
 
 END