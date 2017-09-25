

/****** Object:  StoredProcedure [dbo].[AVE_EstadosSolicitudesObtener]    Script Date: 10/01/2013 23:44:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[AVE_EstadosSolicitudesAlmacenObtener]
AS
BEGIN
	SELECT	min(IdEstado) IdEstado,
			[Resource]
	FROM	AVE_ESTADOSSolicitudes
	where estado ='Pendiente' or estado ='Asignado' or estado ='Regresado' or estado ='Vendido' 
	group by [resource]
	order by idestado
END
go
create PROCEDURE [dbo].[AVE_EstadosSolicitudesOtrasObtener]
AS
BEGIN
	SELECT	min(IdEstado) IdEstado,
			[Resource]
	FROM	AVE_ESTADOSSolicitudes
	where estado ='Solicitado' or estado ='Confirmado' or estado ='Cancelado' 
	group by [resource]
	order by idestado
END

GO


