USE [BDMARIO]
GO

/****** Object:  StoredProcedure [dbo].[AVE_AniadeArticuloCarrito]    Script Date: 10/09/2014 12:29:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ACL
-- Create date: 08/10/2014
-- Description:	Añadimos el articulo al carrito, desde la pantalla de detalle del carrito.
-- =============================================
CREATE PROCEDURE [dbo].[AVE_AniadeArticuloCarrito]
   @IdCarrito INT,
   @Referencia nvarchar(256),
	@IdTienda varchar(256),
   @IdEmpleado varchar(256),
   @strError varchar(2000) output
   
AS
BEGIN

DECLARE @IdArticulo int
declare @IdCabDetalle int
declare @Pedido int
declare @PVPACT float
declare @Talla varchar(100)
declare @PVPORI float
declare @Stock int
declare @Descripcion varchar(100)

DECLARE @tipoCorte varchar(50)
DECLARE @ErrorMessage varchar(4000)
 set @ErrorMessage=''
set @IdArticulo=0

 
select @IdArticulo=IdArticulo, @IdCabDetalle=IdCabeceroDetalle from EAN WHERE EAN=@Referencia
if @IdArticulo > 0
begin
	select @Stock= Cantidad from N_EXISTENCIAS where IdArticulo=@IdArticulo and
		Id_Cabecero_Detalle=@IdCabDetalle and IdTienda=@IdTienda

	select @Descripcion=art.Descripcion ,@PVPORI=art.PrecioVentaEuro , @PVPACT= COALESCE(PA.PrecioEuro,art.PrecioVentaEuro) ,@Talla=cab.Nombre_Talla from articulos art 
	inner join cabeceros_detalles cab on cab.Id_Cabecero= art.Id_Cabecero
	LEFT Join Precios_Articulos PA ON PA.IdArticulo = art.IdArticulo and Activo = 1
	where art.IdArticulo= @IdArticulo and cab.Id_Cabecero_Detalle=@IdCabDetalle
  
	SELECT @tipocorte=co.Corte FROM ARTICULOS art WITH (NOLOCK) INNER JOIN CORTE co 
        WITH (NOLOCK) ON art.IdCorte=co.IdCorte WHERE art.IdArticulo= @IdArticulo 
 

	if @@ROWCOUNT>0
	begin
		if CHARINDEX('FO',@tipoCorte)>0
		begin 
			set @tipoCorte='FO'
		end
		else
			if CHARINDEX('HB',@tipoCorte)>0
			begin 
				set @tipoCorte='HB'
			end
			else
			begin 
				set @tipoCorte=''
			end 
	end


	BEGIN TRANSACTION
 
	BEGIN TRY

 
		INSERT INTO AVE_PEDIDOS(IdArticulo,Talla,Unidades,Precio,IdTienda,Stock,IdEstadoPedido,IdEstadoSolicitud,
		IdEmpleado,Fecha_Creacion)VALUES(@IdArticulo,@Talla,1,@PVPACT,@IdTienda,@Stock,1,6,@IdEmpleado,GETDATE())
		
		set @Pedido= @@IDENTITY
 
		INSERT INTO AVE_CARRITO_LINEA (Id_Carrito, Idarticulo, Id_cabecero_detalle, cantidad
		,PVPORI, PVPACT, DTOArticulo, idPromocion, idPedido,TIPOARTICULO)
		values(@IdCarrito,@IdArticulo,@IdCabDetalle,1,@PVPORI,@PVPACT,0,0,@Pedido,@tipoCorte)
	COMMIT TRANSACTION
	set @strError=@Descripcion
	END TRY
	BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
        SELECT @ErrorMessage = ERROR_MESSAGE();
	RAISERROR (@ErrorMessage, 10, 1);
	set @strError='NOK.' + @ErrorMessage
	END CATCH
end
else
	set @strError='NOK.NO EXISTE EL ARTICULO CON EL EAN INTRODUCIDO'
END


GO

