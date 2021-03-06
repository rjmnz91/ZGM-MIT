/****** Object:  StoredProcedure [dbo].[AVE_StockEnTiendaCSObtener]    Script Date: 10/11/2013 15:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[AVE_StockEnTiendaCSObtener]
@IdArticulo int , 
@IdTienda varchar(10) ,
@Tipo char(1) 
AS
begin
SELECT    distinct ARTICULOS_1.Descripcion AS Descripcion, 
            ARTICULOS_1.IdArticulo
            --,dbo.COLORES.Color, 
		    ,( SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS 
			WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS_1.IdArticulo 
			AND PRECIOS_ARTICULOS.IdTienda = @IdTienda 
			AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC) AS Precio
			
FROM         dbo.ARTICULOS
 inner JOIN
             dbo.ArticulosCS ON 
					CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + 'ý' + 
					CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + 'ý' + 
					CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50))	= 
					dbo.ArticulosCS.Articulo1 INNER JOIN
             dbo.ARTICULOS AS ARTICULOS_1 ON 
					CAST(ARTICULOS_1.IdTemporada AS VARCHAR(5)) + 'ý' + 
					CAST(ARTICULOS_1.IdProveedor AS VARCHAR(5)) + 'ý' + 
					CAST(ARTICULOS_1.ModeloProveedor AS VARCHAR(50)) = 
					dbo.ArticulosCS.Articulo2 
					INNER JOIN
             dbo.CABECEROS_DETALLES ON 
					ARTICULOS_1.Id_Cabecero = 
					dbo.CABECEROS_DETALLES.Id_Cabecero 
					INNER JOIN
             (Select * from dbo.N_EXISTENCIAS where N_EXISTENCIAS.cantidad > 0) as EXISTENCIAS
              ON 
					ARTICULOS_1.IdArticulo = 
					EXISTENCIAS.IdArticulo AND
				    dbo.CABECEROS_DETALLES.Id_Cabecero_Detalle = 
					EXISTENCIAS.Id_Cabecero_Detalle 
					--AND dbo.N_EXISTENCIAS.IdTienda = @IdTienda 
     LEFT OUTER JOIN
             dbo.COLORES ON dbo.COLORES.IdColor = ARTICULOS_1.IdColor
WHERE     (dbo.ARTICULOS.IdArticulo = @IdArticulo) AND (dbo.ArticulosCS.TipoRela = @Tipo)
END
