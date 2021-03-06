
/****** Object:  StoredProcedure [dbo].[AVE_PedidosEntradaCrear]    Script Date: 09/16/2013 09:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dessen
-- Create date: 16/09/2013
-- Description:	Creamos la entrada en el carrito
-- =============================================
CREATE PROCEDURE [dbo].[AVE_AnadirCarrito]
   @IdPedido INT,
   @Usuario nvarchar(256)
AS
BEGIN
	DECLARE @fecha AS DATETIME
	SET @fecha = GETDATE()

	INSERT INTO dbo.AVE_CARRITO
           ([IdPedido]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
     VALUES
           (@IdPedido
           ,@fecha
           ,@Usuario)
END

