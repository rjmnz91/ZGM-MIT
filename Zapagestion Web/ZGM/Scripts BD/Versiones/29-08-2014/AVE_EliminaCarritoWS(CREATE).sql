CREATE PROCEDURE [dbo].[AVE_EliminaCarritoWS]
   @IdCarrito INT   
AS
BEGIN

DECLARE @idPedido int

	-- Creamos tabla temporal donde se guardaran los identificadores de carrito,lineas de carrito y pedidos
	DECLARE @Temp TABLE (
		id int identity(1,1),
		IdCarrito int,
		IdCarritoDetalle int,
		IdPedido int);
	
	-- Insertamos en la tabla temporal los identificadores de ese carrito
	INSERT INTO @Temp(IdCarrito,IdCarritoDetalle,IdPedido)
	(SELECT id_Carrito,id_Carrito_Detalle,idPedido 
	FROM AVE_CARRITO_LINEA 
	WHERE id_Carrito = @IdCarrito);
	
	
	DECLARE @Contador INT           -- Variable contador
	SET @Contador = 1   
	DECLARE @Regs INT               -- Variable para el Numero de Registros a procesar
	SET @Regs = (SELECT COUNT(*) FROM @Temp)
 
	-- Eliminamos los registros por el identificador del carrito
	DELETE FROM AVE_CARRITO_PAGOS WHERE IdCarrito = @IdCarrito 
	DELETE FROM AVE_CARRITO_LINEA WHERE id_Carrito = @IdCarrito 
	DELETE FROM AVE_CARRITO WHERE IdCarrito = @IdCarrito 
 
	-- Hacemos el Loop para eliminar todos los pedidos relacionados con el carrito
	WHILE @Contador <= @Regs
	BEGIN
		SELECT @idPedido = t.IdPedido  
			FROM @Temp t
			WHERE t.id = @Contador

		 DELETE FROM AVE_PEDIDOS WHERE IdPedido = @idPedido 
 
		SET @Contador = @Contador + 1
 
	END

END