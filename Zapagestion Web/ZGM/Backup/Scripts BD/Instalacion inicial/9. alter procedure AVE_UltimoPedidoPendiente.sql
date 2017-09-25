/****** Object:  StoredProcedure [dbo].[AVE_UltimoPedidoPendiente]    Script Date: 11/14/2013 20:17:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AVE_UltimoPedidoPendiente]
	
AS
BEGIN
	select top 1 isnull(Fecha_modificacion,Fecha_Creacion) as Fecha_Creacion ,idTienda
    from AVE_PEDIDOS where idEstadoSolicitud=1
    order by isnull(Fecha_modificacion,Fecha_Creacion) desc
			
END
