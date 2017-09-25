ALTER PROCEDURE [dbo].[AVE_EnviaPOS]
	@IdTerminal		nvarchar(15),
	@IdTienda		nvarchar(15),
	@IdCarrito		int,
	@IdUsuario		nvarchar(15),
	@dPrecio		float --Precio a cobrar	
AS
BEGIN	
	declare @NewTicket nvarchar(10)
	declare @IdTicketEspera	int
	DECLARE @FECHAACTIVA as datetime
	Declare  @id_cliente as int
	
	select @FECHAACTIVA =CONVERT(datetime,FEchaActiva,103) from CONFIGURACIONES_TPV 
	
	select @id_cliente=id_Cliente from  AVE_CARRITO where IdCarrito=@IdCarrito
	
	--Busca el primer ticket libre en un terminal
	Select @NewTicket = dbo.ufnGetTicketTerminal(@IdTerminal)	
	if (@NewTicket<>'') BEGIN
		/*Insertamos la cabecera del ticket en espera*/
		Insert Into N_Tickets_espera (Id_Ticket,Id_Tienda, Id_Empleado, Fecha,
			TotalEuro,Id_Cliente_N,Id_Cliente, ID_Terminal)
		VALUES (@NewTicket,@IdTienda,@IdUsuario
				,@FECHAACTIVA,@dPrecio,@id_cliente,0,substring(@newticket,3,1))

		--Guardamos el nuevo id del ticket en espera
		Select @IdTicketEspera = @@Identity

        /*Insertamos los detalles del ticket en espera*/
        Insert into N_TICKETS_ESPERA_DETALLES (Id_Auto,Id_Articulo, id_cabecero_detalle,id_tienda,
              ImporteEuros, Estado, DtoEuroArticulo,PVP_vig,PVP_Or, EstadoArticulo,MotivoCambioPrecio)
        Select @IdTicketEspera, ACL.idArticulo, ACL.Id_cabecero_detalle,@IdTienda,
                    ACL.PVPACT-ACL.DTOArticulo, ACL.Cantidad*(-1),
                    Case isnull(ACL.DTOArticulo,0) 
                    when 0 then 0
                    else round(100 * (ACL.DTOArticulo) / ACL.PVPACT,0) end 
                    , ACL.PVPACT, ACL.PVPORI,'N',
                    Case isnull(ACL.idPromocion,0) 
                    when 0 then ''
                    else 'Promociones - ' + P.promocion end 
        From Ave_Carrito_linea ACL
        left join tbPR_promociones P on
        ACL.idPromocion=P.idPromocion 
        Where id_carrito = @IdCarrito
		
		UPDATE AVE_CARRITO Set EstadoCarrito = 1 WHERE IdCarrito = @IdCarrito
	END
	
	Select @NewTicket

END

go