ALTER PROCEDURE [dbo].[AVE_dameDetalleCarrito]
   @IdCarrito INT
AS
BEGIN

Select CD.id_carrito_detalle
      ,CD.id_Carrito
      ,CD.idArticulo
      ,CD.Id_cabecero_detalle
      ,CD.Cantidad
      ,CD.PVPORI
      ,CD.PVPACT
      ,(PVPORI-PVPACT) as DTOArticulo
      ,CD.IdPromocion
      ,CD.IdPedido      
      ,CA.Nombre_Talla
      ,Art.Descripcion
      ,Colo.Color
      ,Art.ModeloProveedor
      ,Art.IdTemporada
      ,Art.IdProveedor
      ,PE.IdTienda
      ,DTOArticulo as DtoPromo
      ,P.promocion
      ,mat.Material 
      
from 

AVE_Carrito_Linea CD 
Inner Join Cabeceros_Detalles CA ON CD.Id_Cabecero_detalle = CA.Id_Cabecero_detalle
Inner Join Articulos Art ON CD.idArticulo = Art.IdArticulo
inner join MATERIALES mat on Art.IdMaterial = mat.IdMaterial 
Inner Join Colores Colo ON Art.IdColor = Colo.IdColor And Colo.Colores_Activo = 1
Inner Join Ave_Pedidos PE on CD.IdPedido = PE.IdPedido
Left join tbPR_promociones  P on 
CD.IdPromocion=P.idPromocion 
WHERE CD.id_carrito = @IdCarrito 

END


GO