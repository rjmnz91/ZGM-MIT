--- =============================================
-- Author:		VVM
-- Create date: 22/01/2014
-- Description:	Creamos la entrada en el carrito
-- =============================================

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARRITO]') AND type in (N'U'))
drop table ave_carrito
go
CREATE TABLE [dbo].[AVE_CARRITO](
	[IdCarrito] [int] IDENTITY(1,1) NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[id_Cliente] [int] NULL,
	[Usuario] [nvarchar](256) NULL,
	[Maquina] [varchar](250) NULL,
	[Fecha_Modificacion] [datetime] NULL,
	[EstadoCarrito] [int] NULL,
 CONSTRAINT [PK_AVE_CARRITO] PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[AVE_CARRITO_LINEA](
	[id_Carrito_Detalle] [int] IDENTITY(1,1) NOT NULL,
	[id_Carrito] [int] NULL,
	[idArticulo] [int] NULL,
	[Id_cabecero_detalle] [int] NULL,
	[Cantidad] [int] NULL,
	[PVPORI] [float] NULL,
	[PVPACT] [float] NULL,
	[DTOArticulo] [float] NULL,
	[idPromocion] [int] NULL,
	[idPedido] [int] NULL,
 CONSTRAINT [PK_AVE_CARRITO_LINEA] PRIMARY KEY CLUSTERED 
(
	[id_Carrito_Detalle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARRITO_LINEA_AVE_CARRITO_LINEA] FOREIGN KEY([id_Carrito_Detalle])
REFERENCES [dbo].[AVE_CARRITO_LINEA] ([id_Carrito_Detalle])
GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA] CHECK CONSTRAINT [FK_AVE_CARRITO_LINEA_AVE_CARRITO_LINEA]
GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARRITO_LINEA_PEDIDO] FOREIGN KEY([idPedido])
REFERENCES [dbo].[AVE_PEDIDOS] ([IdPedido])
GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA] CHECK CONSTRAINT [FK_AVE_CARRITO_LINEA_PEDIDO]
GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA]  WITH CHECK ADD  CONSTRAINT [FK_AVECARRITOLINEA] FOREIGN KEY([id_Carrito])
REFERENCES [dbo].[AVE_CARRITO] ([IdCarrito])
GO

ALTER TABLE [dbo].[AVE_CARRITO_LINEA] CHECK CONSTRAINT [FK_AVECARRITOLINEA]
GO

create PROCEDURE [dbo].[AVE_InsertaCarrito]   
   @Usuario nvarchar(256),
   @IdCliente INT,
   @Maquina nvarchar(250),
   @EstadoCarrito int
AS
BEGIN
	DECLARE @fecha AS DATETIME
	SET @fecha = GETDATE()

	INSERT INTO dbo.AVE_CARRITO
           ([FechaCreacion]
           ,[Id_cliente]
           ,Usuario
           ,Maquina
           ,EstadoCarrito
           )
     VALUES
           (@fecha
           ,@IdCliente           
           ,@Usuario
           ,@Maquina
           ,@EstadoCarrito
           )
           
		RETURN @@IDENTITY
END
go
/***************************************************************/

go
/*******************************************************************/

-- =============================================
-- Author:		VVM
-- Create date: 22/01/2014
-- Description:	Creamos la entrada en el detalle del carrito
-- =============================================
Create PROCEDURE [dbo].[AVE_GuardaDetalleCarrito]
   @IdArticulo INT,
   @IdCarrito INT,
   @IdPedido INT,
   @Talla nvarchar(250),
   @Cantidad INT   
AS
BEGIN

INSERT INTO AVE_CARRITO_LINEA (Id_Carrito, Idarticulo, Id_cabecero_detalle, cantidad
	,PVPORI, PVPACT, DTOArticulo, idPromocion, idPedido)
SELECT  @IdCarrito IdCarrito, A.IdArticulo, CD.Id_Cabecero_detalle, @Cantidad Cantidad, 
	A.PrecioVentaEuro as PVPORI, PA.PrecioEuro as PVPACT, 0 DTOArticulo,0 idPromocion,
	@IdPedido idPedido 
FROM Articulos A
	Inner Join Cabeceros_Detalles CD ON A.Id_Cabecero = CD.Id_Cabecero	
	Inner Join Precios_Articulos PA ON PA.IdArticulo = A.IdArticulo and Activo = 1
WHERE A.IdArticulo = @IdArticulo
	AND CD.Nombre_Talla = @Talla

RETURN @@IDENTITY
	
END
go

/****************************************************************/

-- =============================================
-- Author:		VVM
-- Create date: 23/01/2014
-- Description:	Devolvemos el detalle del carrito
-- =============================================
Create PROCEDURE [dbo].[AVE_dameDetalleCarrito]
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
	,CD.DTOArticulo
	,CD.IdPromocion
	,CD.IdPedido	
	,CA.Nombre_Talla
	,Art.Descripcion
	,Colo.Color
	,Art.ModeloProveedor
	,Art.IdTemporada
	,Art.IdProveedor
	,PE.IdTienda
from AVE_Carrito_Linea CD
Inner Join Cabeceros_Detalles CA ON CD.Id_Cabecero_detalle = CA.Id_Cabecero_detalle
Inner Join Articulos Art ON CD.idArticulo = Art.IdArticulo
Inner Join Colores Colo ON Art.IdColor = Colo.IdColor And Colo.Colores_Activo = 1
Inner Join Ave_Pedidos PE on CD.IdPedido = PE.IdPedido
WHERE CD.id_carrito = @IdCarrito

END

go
/*****************************************************************/

-- =============================================
-- Author:		VVM
-- Create date: 23/01/2014
-- Description:	Borra una linea del carrito y su pedido
-- =============================================
Create PROCEDURE [dbo].[AVE_eliminaLineaCarrito]
   @IdLineaCarrito INT
AS
BEGIN
Declare @IdPedido INT

delete from ave_carrito_linea where id_Carrito_Detalle = @IdLineaCarrito

--delete from ave_pedidos where idPedido = @IdPedido

END

go

/***********************************************************************/

IF OBJECT_ID (N'dbo.ufnGetTicketTerminal', N'FN') IS NOT NULL
    DROP FUNCTION ufnGetTicketTerminal;
GO
Create FUNCTION dbo.ufnGetTicketTerminal(@myTerminal int)
RETURNS nvarchar (10)
AS 
BEGIN
	declare @IdTicket	nvarchar(10)
	declare @IdTerminal	nvarchar(10)
	Declare @IDs TABLE
	(
		ID	int
	)
	Declare @sVd	nvarchar(15)
	INSERT INTO @IDs (ID) VALUES (1)
	INSERT INTO @IDs (ID) VALUES (2)
	INSERT INTO @IDs (ID) VALUES (3)
	INSERT INTO @IDs (ID) VALUES (4)
	INSERT INTO @IDs (ID) VALUES (5)

	Select Top(1) @IdTicket = TBL.ID, @IdTerminal = TBL.Terminal
	From
	(Select TMP_ID.ID, Ter.Terminal
	From @IDs TMP_ID 
	, Terminal_Concurrentes Ter) as TBL
	Left Join N_Tickets_espera TE ON substring(TE.Id_Ticket,1,1) = Tbl.ID and TE.Id_Terminal= Tbl.Terminal
	Where TE.id_terminal IS NULL and TBL.Terminal <> @myTerminal
	
	if (@IdTicket IS null) BEGIN
		set @sVd=''
	end
	else BEGIN
		set @sVd =  @IdTicket + '/' + @IdTerminal
	END
	
	return @sVd
END
/***********************************************************************/
go
-- =============================================
-- Author:		VVM
-- Create date: 24/01/2014
-- Description:	Pone el pedido en la tabla de Ticket espera
-- =============================================
CREATE PROCEDURE [dbo].[AVE_EnviaPOS]
	@IdTerminal		nvarchar(15),
	@IdTienda		nvarchar(15),
	@IdCarrito		int,
	@IdUsuario		nvarchar(15),
	@dPrecio		float --Precio a cobrar	
AS
BEGIN	
	declare @NewTicket nvarchar(10)
	declare @IdTicketEspera	int
	DECLARE @FECHAACTIVA as datetime
	
	select @FECHAACTIVA =CONVERT(datetime,FEchaActiva,103) from CONFIGURACIONES_TPV 
	
	--Busca el primer ticket libre en un terminal
	Select @NewTicket = dbo.ufnGetTicketTerminal(@IdTerminal)	
	if (@NewTicket<>'') BEGIN
		/*Insertamos la cabecera del ticket en espera*/
		Insert Into N_Tickets_espera (Id_Ticket,Id_Tienda, Id_Empleado, Fecha,
			TotalEuro,Id_Cliente_N, ID_Terminal)
		VALUES (@NewTicket,@IdTienda,@IdUsuario
				,@FECHAACTIVA,@dPrecio,0,substring(@newticket,3,1))

		--Guardamos el nuevo id del ticket en espera
		Select @IdTicketEspera = @@Identity

		/*Insertamos los detalles del ticket en espera*/
		Insert into N_TICKETS_ESPERA_DETALLES (Id_Auto,Id_Articulo, id_cabecero_detalle,id_tienda,
			ImporteEuros, Estado, DtoEuroArticulo,PVP_vig,PVP_Or, EstadoArticulo)
		Select @IdTicketEspera, ACL.idArticulo, ACL.Id_cabecero_detalle,@IdTienda,
				ACL.PVPACT, ACL.Cantidad*(-1), ACL.DTOArticulo, ACL.PVPACT, ACL.PVPORI,'N'
		From Ave_Carrito_linea ACL
		Where id_carrito = @IdCarrito
		
		UPDATE AVE_CARRITO Set EstadoCarrito = 1 WHERE IdCarrito = @IdCarrito
	END
	
	Select @NewTicket

END
GO


create PROCEDURE [dbo].[AVE_LeeDatosIniciales]   
   @Usuario nvarchar(256)   
AS
BEGIN
      select EMP.IdEmpleado, Nombre,Apellidos, TBLCONF.Tienda as IdTienda, T.Observaciones as NombreTienda, TC.Terminal, 
      fechaactiva
      from empleados EMP
      ,(select * from configuraciones_tpv) as TBLCONF
      inner Join Tiendas T on Tblconf.Tienda = T.IdTienda
      inner Join Terminal_concurrentes TC on TC.IdTienda = T.idTienda and NOMBREMAQUINA='AVE'
      Where idEmpleado = @Usuario
END
go

alter table CONFIGURACIONES_MIT add [Semilla] [varchar](50) NULL
GO 
alter table AVE_CARRITO add [TarjetaCliente] [varchar](50) NULL
GO
alter table AVE_CARRITO_LINEA add [TIPOARTICULO] [varchar](50) NULL
GO
CREATE TABLE [dbo].[AVE_CARRITO_PAGOS](
	[IdCarritoPago] [int] IDENTITY(1,1) NOT NULL,
	[IdCarrito] [int] NOT NULL,
	[TipoPago] [varchar](100) NULL,
	[TipoPagoDetalle] [varchar](100) NULL,
	[NumTarjeta] [varchar](50) NULL,
	[Importe] [float] NULL,
 CONSTRAINT [PK_AVE_CARRITO_PAGOS] PRIMARY KEY CLUSTERED 
(
	[IdCarritoPago] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AVE_CARRITO_PAGOS]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARRITO_PAGOS] FOREIGN KEY([IdCarrito])
REFERENCES [dbo].[AVE_CARRITO] ([IdCarrito])
GO

ALTER TABLE [dbo].[AVE_CARRITO_PAGOS] CHECK CONSTRAINT [FK_AVE_CARRITO_PAGOS]
GO
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
	,(PVPORI-PVPACT) + DTOArticulo as DTOArticulo
	,CD.IdPromocion
	,CD.IdPedido	
	,CA.Nombre_Talla
	,Art.Descripcion
	,Colo.Color
	,Art.ModeloProveedor
	,Art.IdTemporada
	,Art.IdProveedor
	,PE.IdTienda
from AVE_Carrito_Linea CD
Inner Join Cabeceros_Detalles CA ON CD.Id_Cabecero_detalle = CA.Id_Cabecero_detalle
Inner Join Articulos Art ON CD.idArticulo = Art.IdArticulo
Inner Join Colores Colo ON Art.IdColor = Colo.IdColor And Colo.Colores_Activo = 1
Inner Join Ave_Pedidos PE on CD.IdPedido = PE.IdPedido
WHERE CD.id_carrito = @IdCarrito

END

GO

ALTER PROCEDURE [dbo].[AVE_EnviaPOS]
	@IdTerminal		nvarchar(15),
	@IdTienda		nvarchar(15),
	@IdCarrito		int,
	@IdUsuario		nvarchar(15),
	@dPrecio		float --Precio a cobrar	
AS
BEGIN	
	declare @NewTicket nvarchar(10)
	declare @IdTicketEspera	int
	DECLARE @FECHAACTIVA as datetime
	Declare  @id_cliente as int
	
	select @FECHAACTIVA =CONVERT(datetime,FEchaActiva,103) from CONFIGURACIONES_TPV 
	
	select @id_cliente=id_Cliente from  AVE_CARRITO where IdCarrito=@IdCarrito
	
	--Busca el primer ticket libre en un terminal
	Select @NewTicket = dbo.ufnGetTicketTerminal(@IdTerminal)	
	if (@NewTicket<>'') BEGIN
		/*Insertamos la cabecera del ticket en espera*/
		Insert Into N_Tickets_espera (Id_Ticket,Id_Tienda, Id_Empleado, Fecha,
			TotalEuro,Id_Cliente_N,Id_Cliente, ID_Terminal)
		VALUES (@NewTicket,@IdTienda,@IdUsuario
				,@FECHAACTIVA,@dPrecio,@id_cliente,0,substring(@newticket,3,1))

		--Guardamos el nuevo id del ticket en espera
		Select @IdTicketEspera = @@Identity

		/*Insertamos los detalles del ticket en espera*/
		Insert into N_TICKETS_ESPERA_DETALLES (Id_Auto,Id_Articulo, id_cabecero_detalle,id_tienda,
			ImporteEuros, Estado, DtoEuroArticulo,PVP_vig,PVP_Or, EstadoArticulo)
		Select @IdTicketEspera, ACL.idArticulo, ACL.Id_cabecero_detalle,@IdTienda,
				ACL.PVPACT, ACL.Cantidad*(-1), ACL.DTOArticulo, ACL.PVPACT, ACL.PVPORI,'N'
		From Ave_Carrito_linea ACL
		Where id_carrito = @IdCarrito
		
		UPDATE AVE_CARRITO Set EstadoCarrito = 1 WHERE IdCarrito = @IdCarrito
	END
	
	Select @NewTicket

END

go
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


INSERT INTO AVE_CARRITO_LINEA (Id_Carrito, Idarticulo, Id_cabecero_detalle, cantidad
	,PVPORI, PVPACT, DTOArticulo, idPromocion, idPedido,TIPOARTICULO)
SELECT  @IdCarrito IdCarrito, A.IdArticulo, CD.Id_Cabecero_detalle, @Cantidad Cantidad, 
	A.PrecioVentaEuro as PVPORI, PA.PrecioEuro as PVPACT, 0 DTOArticulo,0 idPromocion,
	@IdPedido idPedido,@tipoCorte  
FROM Articulos A
	Inner Join Cabeceros_Detalles CD ON A.Id_Cabecero = CD.Id_Cabecero	
	Inner Join Precios_Articulos PA ON PA.IdArticulo = A.IdArticulo and Activo = 1
WHERE A.IdArticulo = @IdArticulo
	AND CD.Nombre_Talla = @Talla

RETURN @@IDENTITY
	
END

GO

CREATE TABLE [dbo].[AVE_CARRITO_ENTREGAS](
 [IdCarritoEntrega] [int] IDENTITY(1,1) NOT NULL,
 [IdCarrito] [int] NOT NULL,
 [Nombre] [varchar](50) NOT NULL,
 [Apellidos] [varchar](100) NOT NULL,
 [Email] [varchar](255) NOT NULL,
 [Direccion] [varchar](100) NOT NULL,
 [NoInterior] [varchar](20) NULL,
 [NoExterior] [varchar](20) NULL,
 [Estado] [varchar](75) NULL,
 [Ciudad] [varchar](100) NULL,
 [Colonia] [varchar](50) NULL,
 [CodigoPostal] [varchar](10) NULL,
 [TelefonoCelular] [varchar](20) NULL,
 [TelefonoFijo] [varchar](20) NULL,
 [ReferenciaLlegada] [varchar](100) NULL,
 	[id_Carrito_Detalle] [int] NULL,
 CONSTRAINT [PK_AVE_CARRITO_ENTREGAS] PRIMARY KEY CLUSTERED 
(
 [IdCarritoEntrega] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AVE_CARRITO_ENTREGAS] UNIQUE NONCLUSTERED 
(
 [IdCarritoEntrega] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO

CREATE TABLE [dbo].[AVE_RESPUESTA_PAGO](
	[idLogRespuesta] [bigint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[XML] [nvarchar](max) NOT NULL,
	[Metodo] [varchar](100) NULL,
	[IdTienda] [varchar](20) NULL,
	[IdTerminal] [varchar](20) NULL,
	[IdEmpleado] [varchar](20) NULL,
	[IdCarritoPago] [int] NULL,
 CONSTRAINT [PK_AVE_RESPUESTA_PAGO] PRIMARY KEY CLUSTERED 
(
	[idLogRespuesta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AVE_RESPUESTA_PAGO] ADD  CONSTRAINT [DF_AVE_RESPUESTA_PAGO_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO

GO
drop table AVE_CARRITO_PAGOS

CREATE TABLE [dbo].[AVE_CARRITO_PAGOS](
	[IdCarritoPago] [int] IDENTITY(1,1) NOT NULL,
	[IdCarrito] [int] NOT NULL,
	[TipoPago] [varchar](100) NULL,
	[TipoPagoDetalle] [varchar](100) NULL,
	[NumTarjeta] [varchar](50) NULL,
	[Importe] [float] NULL,
	[PagadoOK] [bit] NULL,
	[Nombre] [varchar](100) NULL,
	[Foliocpagos] [varchar](20) NULL,
 CONSTRAINT [PK_AVE_CARRITO_PAGOS] PRIMARY KEY CLUSTERED 
(
	[IdCarritoPago] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AVE_CARRITO_PAGOS]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARRITO_PAGOS] FOREIGN KEY([IdCarrito])
REFERENCES [dbo].[AVE_CARRITO] ([IdCarrito])
GO

ALTER TABLE [dbo].[AVE_CARRITO_PAGOS] CHECK CONSTRAINT [FK_AVE_CARRITO_PAGOS]
GO

ALTER TABLE [dbo].[AVE_CARRITO_PAGOS] ADD  CONSTRAINT [DF_AVE_CARRITO_PAGOS_PagadoOK]  DEFAULT ((0)) FOR [PagadoOK]
GO

GO

CREATE PROCEDURE [dbo].[AVE_RegistrarPagoCarrito]
@IdCarrito int,
@TipoPago varchar(100),
@TipoPagoDetalle varchar(100),
@NumTarjeta varchar(50),
@Importe float
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [AVE_CARRITO_PAGOS] ([IdCarrito],[TipoPago],[TipoPagoDetalle],[NumTarjeta],[Importe])
	values
	(@IdCarrito, @TipoPago, @TipoPagoDetalle, @NumTarjeta, @Importe)


	-- Se devuelve una select porque el método ExecuteScalar lo recoge directamente.
	SELECT @@IDENTITY

END

GO

ALTER FUNCTION [dbo].[ufnGetTicketTerminal](@myTerminal int)
RETURNS nvarchar (10)
AS 
BEGIN
	declare @IdTicket	nvarchar(10)
	declare @IdTerminal	nvarchar(10)
	Declare @IDs TABLE
	(
		ID	int
	)
	Declare @sVd	nvarchar(15)
	INSERT INTO @IDs (ID) VALUES (1)
	INSERT INTO @IDs (ID) VALUES (2)
	INSERT INTO @IDs (ID) VALUES (3)
	INSERT INTO @IDs (ID) VALUES (4)
	INSERT INTO @IDs (ID) VALUES (5)

	Select Top(1) @IdTicket = TBL.ID, @IdTerminal = TBL.Terminal
	From
	(Select TMP_ID.ID, Ter.Terminal
	From @IDs TMP_ID 
	, Terminal_Concurrentes Ter where isnull(APPAVE,0)=0) as TBL
	Left Join N_Tickets_espera TE ON substring(TE.Id_Ticket,1,1) = Tbl.ID 
	and TE.Id_Terminal= Tbl.Terminal
	Where TE.id_terminal IS NULL 
	
	if (@IdTicket IS null) 
	BEGIN
		set @sVd=''
	end
	else BEGIN
		set @sVd =  @IdTicket + '/' + @IdTerminal
	END
	
	return @sVd
END

go

CREATE TABLE [dbo].[AVE_CARRITO_LINEA_PROMO](
	[id_carrito_Promocion] [int] IDENTITY(1,1) NOT NULL,
	[id_linea_Carrito] [int] NULL,
	[idPromo] [int] NULL,
	[DtoPromo] [float] NULL,
	[DescriPromo] [varchar](300) NULL,
	[DescriAmpliaPromo] [varchar](500) NULL,
 CONSTRAINT [PK_AVE_CARRITO_LINEA_PROMO] PRIMARY KEY CLUSTERED 
(
	[id_carrito_Promocion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

go

ALTER PROCEDURE [dbo].[AVE_LeeDatosIniciales]   
   @Usuario nvarchar(256),
   @Maquina varchar(40)   
AS
BEGIN
      select EMP.IdEmpleado, Nombre,Apellidos, TBLCONF.Tienda as IdTienda, T.Observaciones as NombreTienda, TC.Terminal, 
      fechaactiva,
      (select top 1 isnull(Fecha_modificacion,Fecha_Creacion)  from AVE_PEDIDOS where idEstadoSolicitud=1
             order by isnull(Fecha_modificacion,Fecha_Creacion) desc) as FechaPedido 
      from empleados EMP
      ,(select * from configuraciones_tpv) as TBLCONF
      inner Join Tiendas T on Tblconf.Tienda = T.IdTienda
      inner Join Terminal_concurrentes TC on TC.IdTienda = T.idTienda and NOMBREMAQUINA=@Maquina
      Where idEmpleado = @Usuario
END


Go

ALTER TABLE dbo.AVE_CARRITO_PAGOS ADD
                Auth varchar(20) NULL
GO
ALTER TABLE dbo.AVE_CARRITO_PAGOS SET (LOCK_ESCALATION = TABLE)
go

alter table CONFIGURACIONES_MIT add 
[MERCHANTAMEX] [varchar](50) NULL

go

create PROCEDURE [dbo].[AVE_ActualizarExistencias] @IdArticulo INT
	,@IdTienda VARCHAR(10)
	,@Id_Cabecero_Detalle INT
	,@Cantidad INT
AS
SELECT idarticulo
FROM N_Existencias
WHERE idarticulo = @IdArticulo
	AND IdTienda = @IdTienda
	AND Id_Cabecero_Detalle = @Id_Cabecero_Detalle

IF @@rowcount = 0
BEGIN
	INSERT INTO [N_EXISTENCIAS] (
		[IdArticulo]
		,[IdTienda]
		,[Id_Cabecero_Detalle]
		,[Cantidad]
		)
	VALUES (
		@IdArticulo
		,@IdTienda
		,@Id_Cabecero_Detalle
		,@Cantidad
		)
END
ELSE
BEGIN
	UPDATE [N_EXISTENCIAS]
	SET [Cantidad] =Cantidad-@Cantidad
	WHERE idarticulo = @IdArticulo
		AND IdTienda = @IdTienda
		AND Id_Cabecero_Detalle = @Id_Cabecero_Detalle
END

go

CREATE TABLE [dbo].[AVE_CARRITO_IMPRESION](
	[IdCarrito] [int] NOT NULL,
	[IdTicket] [varchar](20) NOT NULL,
	[FechaSesion] [datetime] NOT NULL,
	[Estado] [bit] NOT NULL,
	[FechaImpresion] [datetime] NULL,
 CONSTRAINT [PK_AVE_CARRITO_IMPRESION] PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER PROCEDURE AVE_TicketsPendientesImprimir
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
ALTER PROCEDURE [dbo].[AVE_EnviaPOS]
	@IdTerminal		nvarchar(15),
	@IdTienda		nvarchar(15),
	@IdCarrito		int,
	@IdUsuario		nvarchar(15),
	@dPrecio		float --Precio a cobrar	
AS
BEGIN	
	declare @NewTicket nvarchar(10)
	declare @IdTicketEspera	int
	DECLARE @FECHAACTIVA as datetime
	
	select @FECHAACTIVA =CONVERT(datetime,FEchaActiva,103) from CONFIGURACIONES_TPV 
	
	--Busca el primer ticket libre en un terminal
	Select @NewTicket = dbo.ufnGetTicketTerminal(@IdTerminal)	
	if (@NewTicket<>'') BEGIN
		/*Insertamos la cabecera del ticket en espera*/
		Insert Into N_Tickets_espera (Id_Ticket,Id_Tienda, Id_Empleado, Fecha,
			TotalEuro,Id_Cliente_N, ID_Terminal)
		VALUES (@NewTicket,@IdTienda,@IdUsuario
				,@FECHAACTIVA,@dPrecio,0,substring(@newticket,3,1))

		--Guardamos el nuevo id del ticket en espera
		Select @IdTicketEspera = @@Identity

		/*Insertamos los detalles del ticket en espera*/
		Insert into N_TICKETS_ESPERA_DETALLES (Id_Auto,Id_Articulo, id_cabecero_detalle,id_tienda,
			ImporteEuros, Estado, DtoEuroArticulo,PVP_vig,PVP_Or, EstadoArticulo,MotivoCambioPrecio)
		Select @IdTicketEspera, ACL.idArticulo, ACL.Id_cabecero_detalle,@IdTienda,
				ACL.PVPACT-ACL.DTOArticulo, ACL.Cantidad*(-1),
				Case isnull(ACL.DTOArticulo,0) 
				when 0 then 0
				else round(100 * (ACL.DTOArticulo) / ACL.PVPACT,0) end 
				, ACL.PVPACT, ACL.PVPORI,'N',
				Case isnull(ACL.idPromocion,0) 
				when 0 then ''
				else 'Promociones - ' + P.promocion end 
		From Ave_Carrito_linea ACL
		left join tbPR_promociones P on
		ACL.idPromocion=P.idPromocion 
		Where id_carrito = @IdCarrito
		
		UPDATE AVE_CARRITO Set EstadoCarrito = 1 WHERE IdCarrito = @IdCarrito
	END
	
	Select @NewTicket




END


go

GO
ALTER FUNCTION [dbo].[ufnGetTicketTerminal](@myTerminal int)
RETURNS nvarchar (10)
AS 
BEGIN
	declare @IdTicket	nvarchar(10)
	declare @IdTerminal	nvarchar(10)
	Declare @IDs TABLE
	(
		ID	int
	)
	Declare @sVd	nvarchar(15)
	INSERT INTO @IDs (ID) VALUES (1)
	INSERT INTO @IDs (ID) VALUES (2)
	INSERT INTO @IDs (ID) VALUES (3)
	INSERT INTO @IDs (ID) VALUES (4)
	INSERT INTO @IDs (ID) VALUES (5)

	Select Top(1) @IdTicket = TBL.ID, @IdTerminal = TBL.Terminal
	From
	(Select TMP_ID.ID, Ter.Terminal
	From @IDs TMP_ID 
	, Terminal_Concurrentes Ter where isnull(APPAVE,0)=0) as TBL
	Left Join N_Tickets_espera TE ON substring(TE.Id_Ticket,1,1) = Tbl.ID 
	and TE.Id_Terminal= Tbl.Terminal
	Where TE.id_terminal IS NULL 
	
	if (@IdTicket IS null) 
	BEGIN
		set @sVd=''
	end
	else BEGIN
		set @sVd =  @IdTicket + '/' + @IdTerminal
	END
	
	return @sVd
END
/***********************************************************************/


