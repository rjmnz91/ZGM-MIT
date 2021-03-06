/****** Object:  StoredProcedure [dbo].[AVE_SolicitudesAlmacenObtener]    Script Date: 03/31/2014 12:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AVE_SolicitudesAlmacenObtener] 
	@IdEstado int = NULL,
	@IdPedido int = NULL,
	@IdTienda varchar(10),
    @Fecha datetime=null,
    @IdTerminal int  

AS
BEGIN
    
	SELECT  P.IdPedido,
			PRO.IdProveedor,
			PRO.Nombre AS Proveedor,
			T.Observaciones AS Tienda,
			P.IdArticulo,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor + ' (' + A.DescripcionFabricante + ')' AS Modelo,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			E.Nombre + ' ' + E.Apellidos as Vendedor,
			ES.[Resource] AS EstadoSolicitudResource,
			P.Fecha_Creacion AS FechaPedido,
			ISNULL(P.Fecha_Modificacion,P.Fecha_Creacion) AS FechaCambio,
            ES.IdEstado,T.IdTienda   
	FROM	AVE_PEDIDOS P INNER JOIN
			EMPLEADOS E ON P.IdEmpleado = E.IdEmpleado INNER JOIN
			AVE_ESTADOSSolicitudes ES ON ES.IdEstado = P.IdEstadoSolicitud INNER JOIN
			ARTICULOS A ON A.IdArticulo= P.IdArticulo INNER JOIN
			PROVEEDORES PRO ON PRO.IdProveedor = A.IdProveedor INNER JOIN 
			COLORES C ON C.IdColor = A.IdColor INNER JOIN 
			TIENDAS T ON T.IdTienda = P.IdTienda
	WHERE	(P.IdEstadoSolicitud = ISNULL(@IdEstado,P.IdEstadoSolicitud) AND
			P.IdPedido = ISNULL(@IdPedido,P.IdPedido)) AND
           (@FECHA IS NULL OR (P.Fecha_Creacion between @FECHA 
            and dateadd(day,1,@FECHA)))
            and P.IdTienda = @IdTienda
            and P.IdTerminal = @IdTerminal
            order by P.Fecha_Creacion desc
END