/****** Object:  StoredProcedure [dbo].[AVE_TicketsPendientesImprimir]    Script Date: 03/18/2014 18:01:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MJM/
-- Create date: 13/03/2014
-- Description:	Verifica si existe la tabla AVE_CARRITO_IMPRESION
-- Si existe devuelve una consulta con los filtros proporcionados
-- sino devuelve una copia vacía de la tabla.
-- =============================================
ALTER PROCEDURE [dbo].[AVE_TicketsPendientesImprimir]
AS
BEGIN
	SET NOCOUNT ON;

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARRITO_IMPRESION]') AND type in (N'U'))
	BEGIN
		
		SELECT		IdCarrito, idTicket, FechaSesion, Estado, FechaImpresion
		FROM		AVE_CARRITO_IMPRESION
		WHERE		Estado = 0

	END
	ELSE  --  No existe la tabla, TPV sin AVE.
	BEGIN

		DECLARE @TABLA TABLE (IdCarrito int, idTicket varchar(20), FechaSesion datetime, 
							  Estado bit, FechaImpresion datetime)

		SELECT * FROM @TABLA
		
	END

END

GO

