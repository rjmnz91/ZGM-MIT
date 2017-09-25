/****** Object:  StoredProcedure [dbo].[AVE_GuardaDetalleCarrito]    Script Date: 04/28/2014 11:20:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AVE_GuardaDetalleCarrito]
   @IdArticulo INT,
   @IdCarrito INT,
   @IdPedido INT,
   @Talla nvarchar(250),
   @Cantidad INT   
AS
BEGIN

DECLARE @tipoCorte varchar(50)

 SELECT @tipocorte=co.Corte FROM ARTICULOS art WITH (NOLOCK) INNER JOIN CORTE co 
        WITH (NOLOCK) ON art.IdCorte=co.IdCorte WHERE art.IdArticulo= @IdArticulo 
 

if @@ROWCOUNT>0
begin
  if CHARINDEX('FO',@tipoCorte)>0
  begin 
     set @tipoCorte='FO'
  end
  else
      if CHARINDEX('HB',@tipoCorte)>0
      begin 
       set @tipoCorte='HB'
      end
      else
      begin 
        set @tipoCorte=''
      end 
end


-- MJM 28/04/2014 INICIO
-- Se cambia PA.PrecioEuro por COALESCE(PA.PrecioEuro,A.PrecioVentaEuro)
-- para evitar que falle al cargar artículos que no tienen precio en la 
-- tabla PRECIOS_ARTICULOS

--INSERT INTO AVE_CARRITO_LINEA (Id_Carrito, Idarticulo, Id_cabecero_detalle, cantidad
--	,PVPORI, PVPACT, DTOArticulo, idPromocion, idPedido,TIPOARTICULO)
--SELECT  @IdCarrito IdCarrito, A.IdArticulo, CD.Id_Cabecero_detalle, @Cantidad Cantidad, 
--	A.PrecioVentaEuro as PVPORI, PA.PrecioEuro as PVPACT, 0 DTOArticulo,0 idPromocion,
--	@IdPedido idPedido,@tipoCorte  
--FROM Articulos A
--	Inner Join Cabeceros_Detalles CD ON A.Id_Cabecero = CD.Id_Cabecero	
--	Inner Join Precios_Articulos PA ON PA.IdArticulo = A.IdArticulo and Activo = 1
--WHERE A.IdArticulo = @IdArticulo
--	AND CD.Nombre_Talla = @Talla

INSERT INTO AVE_CARRITO_LINEA (Id_Carrito, Idarticulo, Id_cabecero_detalle, cantidad
	,PVPORI, PVPACT, DTOArticulo, idPromocion, idPedido,TIPOARTICULO)
SELECT  @IdCarrito IdCarrito, A.IdArticulo, CD.Id_Cabecero_detalle, @Cantidad Cantidad, 
	A.PrecioVentaEuro as PVPORI, COALESCE(PA.PrecioEuro,A.PrecioVentaEuro) as PVPACT, 0 DTOArticulo,0 idPromocion,
	@IdPedido idPedido,@tipoCorte  
FROM Articulos A
	Inner Join Cabeceros_Detalles CD ON A.Id_Cabecero = CD.Id_Cabecero	
	LEFT Join Precios_Articulos PA ON PA.IdArticulo = A.IdArticulo and Activo = 1
WHERE A.IdArticulo = @IdArticulo
	AND CD.Nombre_Talla = @Talla

-- MJM 28/04/2014 FIN

RETURN @@IDENTITY
	
END
GO


