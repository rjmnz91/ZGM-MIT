

/****** Object:  StoredProcedure [dbo].[AVE_PedidosCrear]    Script Date: 10/02/2013 00:47:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosCambiarTienda]
	@idPedido int,
	@IdTienda varchar(10)
AS
BEGIN
	UPDATE [AVE_PEDIDOS]
   SET [IdTienda] = @IdTienda
 WHERE IdPedido = @idPedido

	RETURN @@IDENTITY
END


GO


