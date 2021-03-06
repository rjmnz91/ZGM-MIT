/****** Object:  StoredProcedure [dbo].[AVE_ArticuloDetalleObtener]    Script Date: 11/21/2013 16:32:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  <Author,,Name>
-- alter date: <alter Date,,>
-- Description: <Description,,>
-- =============================================
alter PROCEDURE [dbo].[AVE_ArticuloDetalleObtener] 
 @IdArticulo int ,
 @IdTienda varchar (10) 
AS
BEGIN
 SELECT 
   dbo.PROVEEDORES.Nombre AS Proveedor,
   dbo.ARTICULOS.CodigoAlfa as Referencia,
   dbo.ARTICULOS.ModeloProveedor + ' (' + dbo.ARTICULOS.DescripcionFabricante + ')' AS Modelo,
   dbo.ARTICULOS.ModeloProveedor,
   dbo.ARTICULOS.Descripcion AS Descripcion,
   dbo.COLORES.Color AS Color,
   dbo.ARTICULOS.Observaciones,
    ( SELECT TOP 1 PrecioEuro FROM
   dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
   ORDER BY Fecha DESC) AS Precio,
CASE 
                  WHEN ArticulosCS.Observaciones <>'Obs:'
                     THEN ArticulosCS.Observaciones
                  ELSE '' 
             ENd AS CS_OBS     
 FROM
   dbo.ARTICULOS LEFT JOIN 
   dbo.PROVEEDORES ON dbo.ARTICULOS.IdProveedor = dbo.PROVEEDORES.IdProveedor LEFT JOIN 
   dbo.COLORES on dbo.COLORES.IdColor = dbo.ARTICULOS.IdColor
 left JOIN
             dbo.ArticulosCS ON 
     CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + 'ý' + 
     CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + 'ý' + 
     CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50)) = 
     dbo.ArticulosCS.Articulo2
 
 WHERE dbo.ARTICULOS.IdArticulo = @IdArticulo

END

GO
