USE [BDMARIO]
GO

/****** Object:  StoredProcedure [dbo].[AVE_UltimoCarritoPendiente]    Script Date: 08/06/2014 09:40:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[AVE_UltimoCarritoPendiente]
    @IdUsuario nvarchar(256)
	--@IdMaquina nvarchar(256)
	
AS
BEGIN

	SELECT  MAX(carr.IdCarrito) from dbo.[AVE_CARRITO] AS CARR
	INNER JOIN AVE_CARRITO_LINEA AS DET on DET.ID_CARRITO= CARR.IdCarrito
	WHERE CARR.EstadoCarrito= 0  and carr.Usuario=@idUsuario
	--AND Maquina=@IdMaquina;
END
GO

