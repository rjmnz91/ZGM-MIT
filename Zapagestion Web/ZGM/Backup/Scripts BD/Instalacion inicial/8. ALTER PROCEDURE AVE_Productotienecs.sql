/****** Object:  StoredProcedure [dbo].[AVE_ProductoTieneCS]    Script Date: 10/11/2013 16:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AVE_ProductoTieneCS]
	@IdArticulo int
AS
BEGIN
	select [C],[S] FROM
	(
	select count(Articulo1 ) AS Cuenta, TipoRela 
	FROM         dbo.ARTICULOS INNER JOIN
				 dbo.ArticulosCS ON 
						CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + 'ý' + 
						CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + 'ý' + 
						CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50))	= 
						dbo.ArticulosCS.Articulo1 
				INNER JOIN
             dbo.ARTICULOS AS ARTICULOS_1 ON 
					CAST(ARTICULOS_1.IdTemporada AS VARCHAR(5)) + 'ý' + 
					CAST(ARTICULOS_1.IdProveedor AS VARCHAR(5)) + 'ý' + 
					CAST(ARTICULOS_1.ModeloProveedor AS VARCHAR(50)) = 
					dbo.ArticulosCS.Articulo2 
					
					INNER JOIN
             (Select * from dbo.N_EXISTENCIAS where N_EXISTENCIAS.cantidad > 0) as EXISTENCIAS
              ON 
					ARTICULOS_1.IdArticulo = EXISTENCIAS.IdArticulo 
				    
			where ARTICULOS.idarticulo=@IdArticulo 
	group by TipoRela
	) AS Source
	PIVOT
	(
	SUM(Cuenta)
	FOR TipoRela IN ([C],[S])
	) AS PivotTable
END
