/****** Object:  StoredProcedure [dbo].[AVE_PedidosCrear]    Script Date: 03/31/2014 12:36:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AVE_PedidosCrear]
	@IdArticulo int,
	@Talla varchar(6),
	@Unidades smallint,
	@Precio money,
	@IdTienda varchar(10),
	@IdEmpleado int,
	@Usuario nvarchar(256),
	@Stock int,
	@IdTerminal int
AS
BEGIN
	INSERT INTO dbo.AVE_Pedidos
		(
		IdArticulo,
		Talla,
		Unidades,
		Precio,
		IdTienda,
		Stock,
		Fecha_Creacion,
		IdEmpleado,
		UsuarioCreacion,
		IdTerminal
		)
	VALUES
		(
		@IdArticulo,
		@Talla,
		@Unidades,
		@Precio,
		@IdTienda,
		@Stock,
		getdate(),
		@IdEmpleado,
		@Usuario,
		@IdTerminal
		)
	RETURN @@IDENTITY
END

