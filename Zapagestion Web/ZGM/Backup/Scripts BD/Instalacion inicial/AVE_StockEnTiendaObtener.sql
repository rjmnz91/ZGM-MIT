/****** Object:  StoredProcedure [dbo].[AVE_StockEnTiendaObtener]    Script Date: 03/21/2014 23:22:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AVE_StockEnTiendaObtener]
	@IdArticulo int,
	@IdTienda varchar(10)
AS
BEGIN


Select  * into  #Stock    
FROM 
(SELECT		PRO.Nombre + ' ' + ARTICULOS_1.ModeloProveedor + ' (' + ARTICULOS_1.DescripcionFabricante + ')' AS Descripcion,
			dbo.CABECEROS_DETALLES.Nombre_Talla AS Talla,
			dbo.N_EXISTENCIAS.Cantidad, 
			dbo.TIENDAS.IdTienda, 
			dbo.TIENDAS.Observaciones AS Tienda,
			COLORES.Color,
		     isnull((SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = @IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC),PrecioVentaEuro) AS Precio,articulos_1.idArticulo,1 as Orden
FROM         dbo.TIENDAS INNER JOIN
                      dbo.GRUPOS_DETALLES INNER JOIN
                      dbo.GRUPOS_DETALLES AS GRUPOS_DETALLES_1 ON dbo.GRUPOS_DETALLES.Id_Grupo = GRUPOS_DETALLES_1.Id_Grupo INNER JOIN
                      dbo.N_EXISTENCIAS ON dbo.GRUPOS_DETALLES.Id_Tienda = dbo.N_EXISTENCIAS.IdTienda ON 
                      dbo.TIENDAS.IdTienda = dbo.GRUPOS_DETALLES.Id_Tienda INNER JOIN
                      dbo.ARTICULOS AS ARTICULOS_1 INNER JOIN
                      dbo.PROVEEDORES PRO on PRO.IDPROVEEDOR=ARTICULOS_1.idPRoveedor inner join
                      dbo.CABECEROS_DETALLES ON ARTICULOS_1.Id_Cabecero = dbo.CABECEROS_DETALLES.Id_Cabecero ON 
                      dbo.N_EXISTENCIAS.IdArticulo = ARTICULOS_1.IdArticulo AND 
                      dbo.N_EXISTENCIAS.Id_Cabecero_Detalle = dbo.CABECEROS_DETALLES.Id_Cabecero_Detalle LEFT JOIN
                      dbo.COLORES ON dbo.COLORES.IdColor = ARTICULOS_1.IdColor
WHERE     (ARTICULOS_1.IdArticulo = @IdArticulo) AND (GRUPOS_DETALLES_1.Id_Tienda = @IdTienda) AND (GRUPOS_DETALLES.Id_Tienda <> @IdTienda)
GROUP BY ARTICULOS_1.Descripcion, dbo.CABECEROS_DETALLES.Nombre_Talla, dbo.N_EXISTENCIAS.Cantidad, dbo.TIENDAS.IdTienda, 
                      dbo.TIENDAS.Observaciones, COLORES.Color, ARTICULOS_1.IdArticulo,PRO.Nombre,ARTICULOS_1.ModeloProveedor,ARTICULOS_1.DescripcionFabricante,PrecioVentaEuro
union 
SELECT		PRO.Nombre + ' ' + ARTICULOS.ModeloProveedor + ' (' + dbo.ARTICULOS.DescripcionFabricante + ')' AS Descripcion ,
			dbo.CABECEROS_DETALLES.Nombre_Talla AS Talla,
			dbo.N_EXISTENCIAS.Cantidad,
            dbo.TIENDAS.IdTienda, 
			dbo.TIENDAS.Observaciones AS Tienda,
			COLORES.Color,
		    isnull((SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC),ARTICULOS.PrecioVentaEuro) AS Precio,ARTICULOS.IdArticulo,0 as orden
FROM         dbo.ARTICULOS INNER JOIN
             dbo.CABECEROS_DETALLES ON ARTICULOS.Id_Cabecero = dbo.CABECEROS_DETALLES.Id_Cabecero INNER JOIN
                      dbo.N_EXISTENCIAS ON ARTICULOS.IdArticulo = dbo.N_EXISTENCIAS.IdArticulo AND 
                      dbo.CABECEROS_DETALLES.Id_Cabecero_Detalle = dbo.N_EXISTENCIAS.Id_Cabecero_Detalle AND dbo.N_EXISTENCIAS.IdTienda = @IdTienda inner JOIN
					  Tiendas on dbo.N_EXISTENCIAS.IdTienda=Tiendas.idTienda inner join 
                      dbo.COLORES ON dbo.COLORES.IdColor = ARTICULOS.IdColor INNER JOIN
					  dbo.PROVEEDORES PRO ON PRO.IdProveedor = ARTICULOS.IdProveedor
                     WHERE  dbo.ARTICULOS.IdArticulo = @IdArticulo
                      GROUP BY	PRO.Nombre,
			          ARTICULOS.ModeloProveedor,
			           dbo.CABECEROS_DETALLES.Nombre_Talla ,
			    dbo.N_EXISTENCIAS.Cantidad, 
			    ARTICULOS.IdArticulo,ARTICULOS.PrecioVentaEuro, 
			   COLORES.Color,ARTICULOS.DescripcionFabricante, dbo.TIENDAS.IdTienda, 
			  dbo.TIENDAS.Observaciones) STOCK
order by orden,IdTienda   


if  (SELECT COUNT (DISTINCT IdArticulo) FROM #Stock where idTienda=@IdTienda and cantidad<>0)>0
begin
   
   select  * from #Stock

end
else
begin   

select * from (
select  * from #Stock
union
SELECT		PRO.Nombre + ' ' + ARTICULOS.ModeloProveedor + ' (' + dbo.ARTICULOS.DescripcionFabricante + ')' AS Descripcion ,
			dbo.CABECEROS_DETALLES.Nombre_Talla AS Talla,
			null as  Cantidad,
            @idTienda as IdTienda, 
			(select Observaciones  from Tiendas where idTienda=@idTienda) AS Tienda,
			COLORES.Color,
		    isnull((SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC),ARTICULOS.PrecioVentaEuro) AS Precio,ARTICULOS.IdArticulo,0 as orden
            FROM ARTICULOS inner join 
            PROVEEDORES PRO ON PRO.IdProveedor = ARTICULOS.IdProveedor
            inner join CABECEROS_DETALLES on CABECEROS_DETALLES.id_cabecero=Articulos.id_cabecero 
            inner join Colores on Colores.idColor=Articulos.idColor
            where articulos.idArticulo=@idarticulo
            and visualizacion_tpv=1) ST order by   orden    
 
end


END