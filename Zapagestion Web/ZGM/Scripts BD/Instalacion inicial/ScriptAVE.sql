SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARGOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CARGOS](
	[IdCargo] [int] NOT NULL,
	[IdTiendaOrigen] [varchar](10) NOT NULL,
	[IdTiendaDestino] [varchar](10) NOT NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTerminal] [nvarchar](50) NULL,
	[Finalizado] [bit] NOT NULL CONSTRAINT [DF_AVE_CARGOS_Finalizado]  DEFAULT ((0)),
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_CARGOS_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_CARGOS] PRIMARY KEY CLUSTERED 
(
	[IdCargo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_CONFIGURACIONES]    Fecha de la secuencia de comandos: 06/28/2013 10:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CONFIGURACIONES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CONFIGURACIONES](
	[IdTienda] [varchar](10) NOT NULL,
	[IdTipoVista] [int] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_CONFIGURACIONES] PRIMARY KEY CLUSTERED 
(
	[IdTienda] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_CONVERSIONMONEDA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CONVERSIONMONEDA]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CONVERSIONMONEDA](
	[Codigo] [char](3) NOT NULL,
	[Simbolo] [nchar](10) NULL,
	[FactorConversion] [decimal](9, 4) NULL,
 CONSTRAINT [PK_AVE_CONVERSIONMONEDA] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_ESTADISTICAS]    Fecha de la secuencia de comandos: 06/28/2013 10:40:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ESTADISTICAS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_ESTADISTICAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Accion] [varchar](100) NULL,
	[Articulo] [varchar](100) NULL,
	[Talla] [varchar](20) NULL,
	[Usuario] [nvarchar](256) NULL,
	[IdTerminal] [varchar](50) NULL,
	[FechaCreacion] [datetime] NULL,
	[Seccion] [varchar](100) NULL,
	[Marca] [varchar](100) NULL,
	[Corte] [varchar](100) NULL,
	[Material] [varchar](100) NULL,
	[Color] [varchar](100) NULL,
	[Comentario] [varchar](250) NULL,
 CONSTRAINT [PK_AVE_ESTADISTICAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_ESTADOSPEDIDOS]    Fecha de la secuencia de comandos: 06/28/2013 10:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ESTADOSPEDIDOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_ESTADOSPEDIDOS](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[Resource] [varchar](50) NULL,
 CONSTRAINT [PK_AVE_ENVIOSESTADOS] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_ESTADOSSOLICITUDES]    Fecha de la secuencia de comandos: 06/28/2013 10:41:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ESTADOSSOLICITUDES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_ESTADOSSOLICITUDES](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[Resource] [varchar](50) NULL,
 CONSTRAINT [PK_AVE_ESTADOS_SOLICITUDES] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_INVENTARIOSHISTORICOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSHISTORICOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_INVENTARIOSHISTORICOS](
	[IdInventario] [int] NOT NULL,
	[IdTipoInventario] [int] NOT NULL,
	[IdTipoVista] [int] NOT NULL,
	[Empresa] [nvarchar](50) NULL,
	[IdTienda] [varchar](10) NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTerminal] [nvarchar](50) NULL,
	[IdOrdenInventario] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_INVENTARIOSHISTORICOS_Fecha_Creacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_INVENTARIOSHISTORICOS] PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_CARGOSENTRADA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARGOSENTRADA]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CARGOSENTRADA](
	[IdEntrada] [int] IDENTITY(1,1) NOT NULL,
	[IdCargo] [int] NOT NULL,
	[FechaEntrada] [datetime] NOT NULL,
	[IdTienda] [varchar](10) NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTerminal] [nvarchar](50) NULL,
	[Finalizado] [bit] NOT NULL CONSTRAINT [DF_AVE_CARGOSENTRADA_Finalizado]  DEFAULT ((0)),
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_CARGOSENTRADA_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_CARGOSENTRADA] PRIMARY KEY CLUSTERED 
(
	[IdEntrada] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_INVENTARIOSPENDIENTES]    Fecha de la secuencia de comandos: 06/28/2013 10:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSPENDIENTES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_INVENTARIOSPENDIENTES](
	[IdInventario] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoInventario] [int] NOT NULL,
	[IdTipoVista] [int] NOT NULL,
	[Empresa] [nvarchar](50) NULL,
	[IdTienda] [varchar](10) NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTerminal] [nvarchar](50) NULL,
	[IdOrdenInventario] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_Table_1_Fecha_Creacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_INVENTARIOSPENDIENTES] PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_PEDIDOSENTRADA]    Fecha de la secuencia de comandos: 06/28/2013 10:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOSENTRADA]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_PEDIDOSENTRADA](
	[IdEntrada] [int] IDENTITY(1,1) NOT NULL,
	[IdEntradaPedido] [int] NOT NULL,
	[IdPedido] [int] NOT NULL,
	[FechaEntrada] [datetime] NOT NULL,
	[IdTienda] [varchar](10) NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTerminal] [nvarchar](50) NULL,
	[Finalizado] [bit] NOT NULL CONSTRAINT [DF_AVE_PEDIDOSENTRADA_Finalizado]  DEFAULT ((0)),
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_PEDIDOSENTRADA_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_PEDIDOSENTRADA] PRIMARY KEY CLUSTERED 
(
	[IdEntrada] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_USUARIOSEMPLEADOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_USUARIOSEMPLEADOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_USUARIOSEMPLEADOS](
	[UserId] [uniqueidentifier] NOT NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTienda] [varchar](10) NULL,
 CONSTRAINT [PK_AVE_UsuariosEmpleados] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_PRODUCTOS_NEGADOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PRODUCTOS_NEGADOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_PRODUCTOS_NEGADOS](
	[IdNegado] [int] IDENTITY(1,1) NOT NULL,
	[IdTienda] [varchar](50) NOT NULL,
	[IdArticulo] [int] NOT NULL CONSTRAINT [DF_AVE_PRODUCTOS_NEGADOS_IdArticulo]  DEFAULT ((-1)),
	[Id_Cabecero_Detalle] [int] NOT NULL CONSTRAINT [DF_AVE_PRODUCTOS_NEGADOS_Id_Cabeceros_Detalle]  DEFAULT ((-1)),
	[Fecha] [datetime] NOT NULL CONSTRAINT [DF_AVE_PRODUCTOS_NEGADOS_Fecha]  DEFAULT (getdate()),
	[Descripcion] [varchar](500) NOT NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_AVE_PRODUCTOS_NEGADOS] PRIMARY KEY CLUSTERED 
(
	[IdNegado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[aspnet_Applications]    Fecha de la secuencia de comandos: 06/28/2013 10:39:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[aspnet_SchemaVersions]    Fecha de la secuencia de comandos: 06/28/2013 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[aspnet_UsersInRoles]    Fecha de la secuencia de comandos: 06/28/2013 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[AVE_CARGOSDETALLE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARGOSDETALLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CARGOSDETALLE](
	[IdCargo] [int] NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Id_Cabecero_Detalle] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_CARGOSDETALLE_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_CARGOSDETALLE] PRIMARY KEY CLUSTERED 
(
	[IdCargo] ASC,
	[IdArticulo] ASC,
	[Id_Cabecero_Detalle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[AVE_CARGOSENTRADADETALLE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CARGOSENTRADADETALLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_CARGOSENTRADADETALLE](
	[IdEntrada] [int] NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Id_Cabecero_Detalle] [nchar](10) NOT NULL,
	[Cantidad] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_CARGOSENTRADADETALLE_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_CARGOSENTRADADETALLE] PRIMARY KEY CLUSTERED 
(
	[IdEntrada] ASC,
	[IdArticulo] ASC,
	[Id_Cabecero_Detalle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[AVE_PEDIDOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_PEDIDOS](
	[IdPedido] [int] IDENTITY(1,1) NOT NULL,
	[id_Cliente] [int] NULL,
	[Cif_Cliente] [nvarchar](50) NULL,
	[Nombre_Cliente] [nvarchar](60) NULL,
	[Apellidos_Cliente] [nvarchar](100) NULL,
	[Direccion_Cliente] [nvarchar](150) NULL,
	[CodPostal_Cliente] [nvarchar](50) NULL,
	[Poblacion_Cliente] [nvarchar](100) NULL,
	[Provincia_Cliente] [nvarchar](100) NULL,
	[Pais_Cliente] [nvarchar](100) NULL,
	[Telefono_Cliente] [nvarchar](50) NULL,
	[Movil_Cliente] [nvarchar](50) NULL,
	[email_Cliente] [nvarchar](50) NULL,
	[Observaciones_Cliente] [nvarchar](1000) NULL,
	[Nombre_Destinatario] [nvarchar](60) NULL,
	[Apellidos_Destinatario] [nvarchar](100) NULL,
	[Direccion_Destinatario] [nvarchar](150) NULL,
	[CodPostal_Destinatario] [nvarchar](50) NULL,
	[Poblacion_Destinatario] [nvarchar](100) NULL,
	[Provincia_Destinatario] [nvarchar](100) NULL,
	[Pais_Destinatario] [nvarchar](100) NULL,
	[Telefono_Destinatario] [nvarchar](50) NULL,
	[Movil_Destinatario] [nvarchar](50) NULL,
	[email_Destinatario] [nvarchar](50) NULL,
	[Observaciones_Destinatario] [nvarchar](1000) NULL,
	[IdArticulo] [int] NULL,
	[Talla] [varchar](6) NULL,
	[Unidades] [smallint] NULL,
	[Precio] [money] NULL,
	[Costes_Envio] [money] NULL,
	[IdTienda] [varchar](10) NULL,
	[Stock] [int] NULL,
	[IdEstadoPedido] [int] NULL CONSTRAINT [DF_AVE_ENVIOS_IdEstado]  DEFAULT ((1)),
	[IdEstadoSolicitud] [int] NULL CONSTRAINT [DF_AVE_PEDIDOS_IdEstadoMensaje]  DEFAULT ((1)),
	[IdEmpleado] [int] NULL,
	[Fecha_Creacion] [datetime] NULL CONSTRAINT [DF_AVE_ENVIOS_Fecha_Creacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
	[Fecha_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_AVE_ENVIOS] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Objeto:  Table [dbo].[AVE_INVENTARIOSHISTORICOSDETALLE]    Fecha de la secuencia de comandos: 06/28/2013 10:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSHISTORICOSDETALLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_INVENTARIOSHISTORICOSDETALLE](
	[IdInventario] [int] NOT NULL,
	[Bloque] [nvarchar](50) NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Id_Cabecero_Detalle] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_INVENTARIOSHISTORICOSDETALLE_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_INVENTARIOSHISTORICOSDETALLE] PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC,
	[IdArticulo] ASC,
	[Id_Cabecero_Detalle] ASC,
	[Bloque] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[AVE_INVENTARIOSPENDIENTESDETALLE]    Fecha de la secuencia de comandos: 06/28/2013 10:41:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSPENDIENTESDETALLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_INVENTARIOSPENDIENTESDETALLE](
	[IdInventario] [int] NOT NULL,
	[Bloque] [nvarchar](50) NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Id_Cabecero_Detalle] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_INVENTARIOSPENDIENTESDETALLE_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_INVENTARIOSPENDIENTESDETALLE] PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC,
	[IdArticulo] ASC,
	[Id_Cabecero_Detalle] ASC,
	[Bloque] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[AVE_PEDIDOSENTRADADETALLE]    Fecha de la secuencia de comandos: 06/28/2013 10:41:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOSENTRADADETALLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AVE_PEDIDOSENTRADADETALLE](
	[IdEntrada] [int] NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Id_Cabecero_Detalle] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_AVE_PEDIDOSENTRADADETALLE_FechaCreacion]  DEFAULT (getdate()),
	[UsuarioCreacion] [nvarchar](256) NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
 CONSTRAINT [PK_AVE_PEDIDOSENTRADADETALLE] PRIMARY KEY CLUSTERED 
(
	[IdEntrada] ASC,
	[IdArticulo] ASC,
	[Id_Cabecero_Detalle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[aspnet_Membership]    Fecha de la secuencia de comandos: 06/28/2013 10:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL DEFAULT ((0)),
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Objeto:  Table [dbo].[aspnet_Roles]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0089B3F9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0089B3F9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__00E1E710]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__00E1E710]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0198DDEF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0198DDEF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__02543115]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__02543115]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__025FB79A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__025FB79A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__02E07135]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__02E07135]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__03B7C739]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__03B7C739]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0408D441]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0408D441]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__04243D92]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__04243D92]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__05B1F0E8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__05B1F0E8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__06193B74]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__06193B74]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__064BB1CF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__064BB1CF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__06828471]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__06828471]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0711C023]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0711C023]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__074EBBB3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__074EBBB3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__07B5D475]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__07B5D475]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__07D66993]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__07D66993]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__07E61A95]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__07E61A95]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__08126802]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__08126802]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0816FA42]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0816FA42]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__092BE563]    Fecha de la secuencia de comandos: 06/28/2013 10:39:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__092BE563]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__097D55FF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__097D55FF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__09AC0571]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__09AC0571]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0AAC7B86]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0AAC7B86]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0B11FD9A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0B11FD9A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0B8A2C42]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0B8A2C42]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0BF2DBB2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0BF2DBB2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0CA1479E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0CA1479E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0D852148]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0D852148]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0DCD09D0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0DCD09D0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0E26A1CB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0E26A1CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0E453878]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0E453878]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0E72EAC9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0E72EAC9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0EE1F4F1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0EE1F4F1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0F071C4F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0F071C4F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0F627CC2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0F627CC2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__0FA56F76]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0FA56F76]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__105E9261]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__105E9261]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1063EFF8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1063EFF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1097953E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1097953E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__119D68EA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__119D68EA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__11CBB099]    Fecha de la secuencia de comandos: 06/28/2013 10:39:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__11CBB099]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1305298B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1305298B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__13186FAC]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__13186FAC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__134AB00E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__134AB00E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__135A2F46]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__135A2F46]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__141E0D5F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__141E0D5F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1425CCFB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1425CCFB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__15B38051]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__15B38051]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__16A9D4C5]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__16A9D4C5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__182808E3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__182808E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__18DF6356]    Fecha de la secuencia de comandos: 06/28/2013 10:39:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__18DF6356]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1923ECB8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1923ECB8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1969A505]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1969A505]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1A6EDF24]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1A6EDF24]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1BAABA1B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1BAABA1B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1C0124BA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1C0124BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1C0D409D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1C0D409D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1D2CB522]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1D2CB522]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1DFB8033]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1DFB8033]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__1F4F6555]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__1F4F6555]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__20CD9973]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__20CD9973]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__21DAC4F8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__21DAC4F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2254BC18]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2254BC18]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__22CB2248]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__22CB2248]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2397598A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2397598A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__23BDE19D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__23BDE19D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__23F1EEA6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__23F1EEA6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__243C0769]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__243C0769]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__24D7F88B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__24D7F88B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__258C25A2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__258C25A2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__25B34742]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__25B34742]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__264F3864]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__264F3864]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__26BAE366]    Fecha de la secuencia de comandos: 06/28/2013 10:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__26BAE366]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__26DF7137]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__26DF7137]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__271C9E91]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__271C9E91]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2733AB9B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2733AB9B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2773D487]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2773D487]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__27AE6E12]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__27AE6E12]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2820DB8F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2820DB8F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__28AB1D3E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__28AB1D3E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2980DC94]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2980DC94]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__298A0114]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__298A0114]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__29ECEF59]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__29ECEF59]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2A7665B1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2A7665B1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2AA6ABD1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2AA6ABD1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2B485E1E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2B485E1E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2C185C49]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2C185C49]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2C1E8537]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2C1E8537]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2C358E12]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2C358E12]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2C611028]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2C611028]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2CABC278]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2CABC278]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2CAF219E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2CAF219E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2D782B84]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2D782B84]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2D80B677]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2D80B677]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2E87EAD8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2E87EAD8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2F9811EF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2F9811EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__2FAA24F6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2FAA24F6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__302E73B0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__302E73B0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__30ACFD10]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__30ACFD10]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__30BE44C0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__30BE44C0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__30DA479E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__30DA479E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3136DB2B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3136DB2B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__31DB5740]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__31DB5740]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__32B443F2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__32B443F2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__32E7E938]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__32E7E938]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3310A186]    Fecha de la secuencia de comandos: 06/28/2013 10:39:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3310A186]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__33938B5C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__33938B5C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__34E1E11D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__34E1E11D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__34F0F892]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__34F0F892]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__34FCB0E1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__34FCB0E1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__36FE6862]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__36FE6862]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__373D942D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__373D942D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__39145EF6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__39145EF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3953272D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3953272D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__39B942CE]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__39B942CE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__39D70E24]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__39D70E24]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3A729783]    Fecha de la secuencia de comandos: 06/28/2013 10:39:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3A729783]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3B18787C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3B18787C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3B73D8EF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3B73D8EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3BC021ED]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3BC021ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3BEF9CB6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3BEF9CB6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3BFDB70A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3BFDB70A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3C217984]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3C217984]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3C2C667C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3C2C667C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3CC4F449]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3CC4F449]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3CF3A3BB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3CF3A3BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3DCAF9BF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3DCAF9BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3E05FB0D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3E05FB0D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3F17510F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3F17510F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__3F574831]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__3F574831]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__405C8250]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__405C8250]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__40C696A4]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__40C696A4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__40DF9DF0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__40DF9DF0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__40E69235]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__40E69235]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4169ADD5]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4169ADD5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__41952FEB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__41952FEB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__425810E3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__425810E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__425CA323]    Fecha de la secuencia de comandos: 06/28/2013 10:39:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__425CA323]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4266C4C4]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4266C4C4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__42CC46D8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__42CC46D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4331FAB6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4331FAB6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__440E7C87]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__440E7C87]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__441FC437]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__441FC437]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__44785F11]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__44785F11]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__44DAB3C9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__44DAB3C9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4509C6CF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4509C6CF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4584577C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4584577C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__466C919C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__466C919C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__488C463D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__488C463D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__49DE94B1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__49DE94B1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4A76255D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4A76255D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4B272518]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4B272518]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4B4BE4B3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4B4BE4B3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4B85811D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4B85811D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4BFA8269]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4BFA8269]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4D0DD6DC]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4D0DD6DC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4D13663D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4D13663D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4D13FFCA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4D13FFCA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4E3EC4DB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4E3EC4DB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__4FA5885B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4FA5885B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__50656DC1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__50656DC1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__50DCD112]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__50DCD112]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__51169F46]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__51169F46]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__517CBAE7]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__517CBAE7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5278052F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5278052F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__52D595DD]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__52D595DD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__52F42C8A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__52F42C8A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__53391DAF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__53391DAF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__538530B4]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__538530B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__53E1C441]    Fecha de la secuencia de comandos: 06/28/2013 10:39:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__53E1C441]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__54C1A538]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__54C1A538]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__54DD0E89]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__54DD0E89]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__54E6FE60]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__54E6FE60]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5598C972]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5598C972]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__55C0B669]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__55C0B669]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__56047474]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__56047474]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__56C7B900]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__56C7B900]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__56D8CEE6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__56D8CEE6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__574CD311]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__574CD311]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5825BFC3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5825BFC3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__58A0E9FD]    Fecha de la secuencia de comandos: 06/28/2013 10:39:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__58A0E9FD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__59108DB2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__59108DB2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5A91EF2C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5A91EF2C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5AB813AB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5AB813AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5C524AA7]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5C524AA7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5C6F4AA6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5C6F4AA6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5C77D599]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5C77D599]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5D3EE10E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5D3EE10E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5DA06A6F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5DA06A6F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5DE26002]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5DE26002]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5DF2DC5B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5DF2DC5B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5EBEABDA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5EBEABDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5ED05B4D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5ED05B4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__5FD0699F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__5FD0699F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__60CAB6C6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__60CAB6C6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__60D43EDA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__60D43EDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__63B047F1]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__63B047F1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__653A345E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__653A345E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__65726BE4]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__65726BE4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__65EACC56]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__65EACC56]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__662698FB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__662698FB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__66AF43FC]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__66AF43FC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__66FF85AD]    Fecha de la secuencia de comandos: 06/28/2013 10:39:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__66FF85AD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__67571F37]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__67571F37]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__677486F9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__677486F9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__67B24DE0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__67B24DE0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6833077B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6833077B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__695E344F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__695E344F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6A6E8D30]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6A6E8D30]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6BFE08FE]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6BFE08FE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6C1B70C0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6C1B70C0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6CCD6D9C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6CCD6D9C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6CD0692E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6CD0692E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6CF9217C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:53 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6CF9217C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6D16ECD2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6D16ECD2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6DF10C9E]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6DF10C9E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6ED8AD31]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6ED8AD31]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6EDE6E5C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6EDE6E5C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6EED223D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6EED223D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__6FADD2FA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__6FADD2FA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__706C21B2]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__706C21B2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__70770EAA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__70770EAA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__707D3798]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__707D3798]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__710D706B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__710D706B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7135F6EF]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7135F6EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__72D2C1BA]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__72D2C1BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__72EC6293]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__72EC6293]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7347F4D0]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7347F4D0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__734EE915]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__734EE915]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__73B80048]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__73B80048]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__73E87832]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__73E87832]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__74447232]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__74447232]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__744EC59D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__744EC59D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__745E130B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__745E130B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7485CE38]    Fecha de la secuencia de comandos: 06/28/2013 10:39:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7485CE38]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7539FB4F]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7539FB4F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__776E24FC]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__776E24FC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__779E9CE6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__779E9CE6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7874F5C9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7874F5C9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__79062BBD]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__79062BBD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7A210E02]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7A210E02]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7A48FAF9]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7A48FAF9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7A82C92D]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7A82C92D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7AEB1509]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7AEB1509]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7BD91054]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7BD91054]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7C16D73B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7C16D73B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7D8A1E61]    Fecha de la secuencia de comandos: 06/28/2013 10:39:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7D8A1E61]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7E18C086]    Fecha de la secuencia de comandos: 06/28/2013 10:39:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7E18C086]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Me__Appli__7ECA8B98]    Fecha de la secuencia de comandos: 06/28/2013 10:39:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7ECA8B98]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__000108F8]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__000108F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__00B2D40A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__00B2D40A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0271FC6B]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0271FC6B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__02CA2F82]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__02CA2F82]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__03812661]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__03812661]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__043C7987]    Fecha de la secuencia de comandos: 06/28/2013 10:39:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__043C7987]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0448000C]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0448000C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__04C8B9A7]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__04C8B9A7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__05A00FAB]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__05A00FAB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__05F11CB3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__05F11CB3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__060C8604]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__060C8604]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__079A395A]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__079A395A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__080183E6]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__080183E6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0833FA41]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0833FA41]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__086ACCE3]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__086ACCE3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__08FA0895]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__08FA0895]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__09370425]    Fecha de la secuencia de comandos: 06/28/2013 10:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__09370425]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__099E1CE7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__099E1CE7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__09BEB205]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__09BEB205]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__09CE6307]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__09CE6307]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__09FAB074]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__09FAB074]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__09FF42B4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__09FF42B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0B142DD5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0B142DD5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0B659E71]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0B659E71]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0B944DE3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0B944DE3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0C94C3F8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0C94C3F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0CFA460C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0CFA460C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0D7274B4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0D7274B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0DDB2424]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0DDB2424]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0E899010]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0E899010]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0F6D69BA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0F6D69BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__0FB55242]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__0FB55242]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__100EEA3D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__100EEA3D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__102D80EA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__102D80EA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__105B333B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__105B333B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__10CA3D63]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__10CA3D63]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__10EF64C1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__10EF64C1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__114AC534]    Fecha de la secuencia de comandos: 06/28/2013 10:40:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__114AC534]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__118DB7E8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__118DB7E8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1246DAD3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1246DAD3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__124C386A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__124C386A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__127FDDB0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__127FDDB0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1385B15C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1385B15C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__13B3F90B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__13B3F90B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__14ED71FD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__14ED71FD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1500B81E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1500B81E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1532F880]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1532F880]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__154277B8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__154277B8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__160655D1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__160655D1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__160E156D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__160E156D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__179BC8C3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__179BC8C3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__18921D37]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__18921D37]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1A105155]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1A105155]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1AC7ABC8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1AC7ABC8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1B0C352A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1B0C352A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1B51ED77]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1B51ED77]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1C572796]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1C572796]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1D93028D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1D93028D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1DE96D2C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1DE96D2C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1DF5890F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1DF5890F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1F14FD94]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1F14FD94]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__1FE3C8A5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__1FE3C8A5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2137ADC7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2137ADC7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__22B5E1E5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__22B5E1E5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__23C30D6A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__23C30D6A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__243D048A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__243D048A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__24B36ABA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__24B36ABA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__257FA1FC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__257FA1FC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__25A62A0F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__25A62A0F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__25DA3718]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__25DA3718]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__26244FDB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__26244FDB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__26C040FD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__26C040FD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__27746E14]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__27746E14]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__279B8FB4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__279B8FB4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__283780D6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__283780D6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__28A32BD8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__28A32BD8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__28C7B9A9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__28C7B9A9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2904E703]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2904E703]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__291BF40D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:05 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__291BF40D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__295C1CF9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__295C1CF9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2996B684]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2996B684]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2A092401]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2A092401]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2A9365B0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2A9365B0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2B692506]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2B692506]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2B724986]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2B724986]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2BD537CB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2BD537CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2C5EAE23]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2C5EAE23]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2C8EF443]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2C8EF443]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2D30A690]    Fecha de la secuencia de comandos: 06/28/2013 10:40:06 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2D30A690]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E00A4BB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E00A4BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E06CDA9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E06CDA9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E1DD684]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E1DD684]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E49589A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E49589A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E940AEA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E940AEA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2E976A10]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2E976A10]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2F6073F6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2F6073F6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__2F68FEE9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__2F68FEE9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3070334A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3070334A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__31805A61]    Fecha de la secuencia de comandos: 06/28/2013 10:40:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__31805A61]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__31926D68]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__31926D68]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3216BC22]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3216BC22]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__32954582]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__32954582]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__32A68D32]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__32A68D32]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__32C29010]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__32C29010]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__331F239D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__331F239D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__33C39FB2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__33C39FB2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__349C8C64]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__349C8C64]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__34D031AA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__34D031AA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__34F8E9F8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__34F8E9F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__357BD3CE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__357BD3CE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__36CA298F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__36CA298F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__36D94104]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__36D94104]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__36E4F953]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__36E4F953]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__38E6B0D4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__38E6B0D4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3925DC9F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3925DC9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3AFCA768]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3AFCA768]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3B3B6F9F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3B3B6F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3BA18B40]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3BA18B40]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3BBF5696]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3BBF5696]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3C5ADFF5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3C5ADFF5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3D00C0EE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3D00C0EE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3D5C2161]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3D5C2161]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3DA86A5F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3DA86A5F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3DD7E528]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3DD7E528]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3DE5FF7C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3DE5FF7C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3E09C1F6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3E09C1F6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3E14AEEE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3E14AEEE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3EAD3CBB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3EAD3CBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3EDBEC2D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3EDBEC2D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3FB34231]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3FB34231]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__3FEE437F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__3FEE437F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__40FF9981]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__40FF9981]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__413F90A3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__413F90A3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4244CAC2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4244CAC2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__42AEDF16]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__42AEDF16]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__42C7E662]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__42C7E662]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__42CEDAA7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__42CEDAA7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4351F647]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4351F647]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__437D785D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__437D785D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__44405955]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__44405955]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4444EB95]    Fecha de la secuencia de comandos: 06/28/2013 10:40:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4444EB95]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__444F0D36]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__444F0D36]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__44B48F4A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__44B48F4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__451A4328]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__451A4328]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__45F6C4F9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__45F6C4F9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__46080CA9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__46080CA9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4660A783]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4660A783]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__46C2FC3B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__46C2FC3B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__46F20F41]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__46F20F41]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__476C9FEE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__476C9FEE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4854DA0E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4854DA0E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4A748EAF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4A748EAF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4BC6DD23]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4BC6DD23]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4C5E6DCF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4C5E6DCF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4D0F6D8A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4D0F6D8A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4D342D25]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4D342D25]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4D6DC98F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4D6DC98F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4DE2CADB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4DE2CADB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4EF61F4E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4EF61F4E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4EFBAEAF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4EFBAEAF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__4EFC483C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4EFC483C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__50270D4D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__50270D4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__518DD0CD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__518DD0CD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__524DB633]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__524DB633]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__52C51984]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__52C51984]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__52FEE7B8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__52FEE7B8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__53650359]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__53650359]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__54604DA1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__54604DA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__54BDDE4F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__54BDDE4F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__54DC74FC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__54DC74FC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__55216621]    Fecha de la secuencia de comandos: 06/28/2013 10:40:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__55216621]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__556D7926]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__556D7926]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__55CA0CB3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__55CA0CB3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__56A9EDAA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__56A9EDAA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__56C556FB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__56C556FB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__56CF46D2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__56CF46D2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__578111E4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__578111E4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__57A8FEDB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__57A8FEDB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__57ECBCE6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__57ECBCE6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__58B00172]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__58B00172]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__58C11758]    Fecha de la secuencia de comandos: 06/28/2013 10:40:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__58C11758]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__59351B83]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__59351B83]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5A0E0835]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5A0E0835]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5A89326F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5A89326F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5AF8D624]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5AF8D624]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5C7A379E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5C7A379E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5CA05C1D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5CA05C1D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5E3A9319]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5E3A9319]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5E579318]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5E579318]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5E601E0B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5E601E0B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5F272980]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5F272980]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5F88B2E1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5F88B2E1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5FCAA874]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5FCAA874]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__5FDB24CD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5FDB24CD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__60A6F44C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__60A6F44C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__60B8A3BF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__60B8A3BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__61B8B211]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__61B8B211]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__62B2FF38]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__62B2FF38]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__62BC874C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__62BC874C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__65989063]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__65989063]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__67227CD0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__67227CD0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__675AB456]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__675AB456]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__67D314C8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:17 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__67D314C8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__680EE16D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__680EE16D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__68978C6E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__68978C6E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__68E7CE1F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__68E7CE1F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__693F67A9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__693F67A9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__695CCF6B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__695CCF6B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__699A9652]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__699A9652]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6A1B4FED]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6A1B4FED]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6B467CC1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6B467CC1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6C56D5A2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6C56D5A2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6DE65170]    Fecha de la secuencia de comandos: 06/28/2013 10:40:18 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6DE65170]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6E03B932]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6E03B932]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6EB5B60E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6EB5B60E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6EB8B1A0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6EB8B1A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6EE169EE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6EE169EE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6EFF3544]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6EFF3544]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__6FD95510]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__6FD95510]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__70C0F5A3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__70C0F5A3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__70C6B6CE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__70C6B6CE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__70D56AAF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__70D56AAF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__71961B6C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__71961B6C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__72546A24]    Fecha de la secuencia de comandos: 06/28/2013 10:40:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__72546A24]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__725F571C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__725F571C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7265800A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7265800A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__72F5B8DD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__72F5B8DD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__731E3F61]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__731E3F61]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__74BB0A2C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__74BB0A2C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__74D4AB05]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__74D4AB05]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__75303D42]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__75303D42]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__75373187]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__75373187]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__75A048BA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__75A048BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__75D0C0A4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__75D0C0A4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__762CBAA4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:20 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__762CBAA4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__76370E0F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__76370E0F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__76465B7D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__76465B7D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__766E16AA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__766E16AA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__772243C1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__772243C1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__79566D6E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__79566D6E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7986E558]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7986E558]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7A5D3E3B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7A5D3E3B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7AEE742F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7AEE742F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7C095674]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7C095674]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7C31436B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:21 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7C31436B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7C6B119F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7C6B119F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7CD35D7B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7CD35D7B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7DC158C6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7DC158C6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7DFF1FAD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7DFF1FAD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Ro__Appli__7F7266D3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__7F7266D3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__00668B0C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:23 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__00668B0C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__00F52D31]    Fecha de la secuencia de comandos: 06/28/2013 10:40:23 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__00F52D31]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__01A6F843]    Fecha de la secuencia de comandos: 06/28/2013 10:40:23 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__01A6F843]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__036620A4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__036620A4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__03BE53BB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__03BE53BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__04754A9A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__04754A9A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__05309DC0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__05309DC0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__053C2445]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__053C2445]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__05BCDDE0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__05BCDDE0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__069433E4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__069433E4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__06E540EC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__06E540EC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0700AA3D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0700AA3D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__088E5D93]    Fecha de la secuencia de comandos: 06/28/2013 10:40:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__088E5D93]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__08F5A81F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__08F5A81F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__09281E7A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__09281E7A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__095EF11C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__095EF11C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__09EE2CCE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__09EE2CCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0A2B285E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0A2B285E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0A924120]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0A924120]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0AB2D63E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0AB2D63E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0AC28740]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0AC28740]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0AEED4AD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0AEED4AD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0AF366ED]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0AF366ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0C08520E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0C08520E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0C59C2AA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0C59C2AA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0C88721C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0C88721C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0D88E831]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0D88E831]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0DEE6A45]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0DEE6A45]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0E6698ED]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0E6698ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0ECF485D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0ECF485D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__0F7DB449]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__0F7DB449]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__10618DF3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__10618DF3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__10A9767B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__10A9767B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__11030E76]    Fecha de la secuencia de comandos: 06/28/2013 10:40:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__11030E76]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1121A523]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1121A523]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__114F5774]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__114F5774]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__11BE619C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__11BE619C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__11E388FA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__11E388FA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__123EE96D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__123EE96D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1281DC21]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1281DC21]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__133AFF0C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__133AFF0C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__13405CA3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__13405CA3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__137401E9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__137401E9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1479D595]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1479D595]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__14A81D44]    Fecha de la secuencia de comandos: 06/28/2013 10:40:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__14A81D44]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__15E19636]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__15E19636]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__15F4DC57]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__15F4DC57]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__16271CB9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__16271CB9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__16369BF1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__16369BF1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__16FA7A0A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__16FA7A0A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__170239A6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__170239A6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__188FECFC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__188FECFC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__19864170]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__19864170]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1B04758E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1B04758E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1BBBD001]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1BBBD001]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1C005963]    Fecha de la secuencia de comandos: 06/28/2013 10:40:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1C005963]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1C4611B0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1C4611B0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1D4B4BCF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1D4B4BCF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1E8726C6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1E8726C6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1EDD9165]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1EDD9165]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__1EE9AD48]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1EE9AD48]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__200921CD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__200921CD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__20D7ECDE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__20D7ECDE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__222BD200]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__222BD200]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__23AA061E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__23AA061E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__24B731A3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__24B731A3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__253128C3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__253128C3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__25A78EF3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__25A78EF3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2673C635]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2673C635]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__269A4E48]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__269A4E48]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__26CE5B51]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__26CE5B51]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__27187414]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__27187414]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__27B46536]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__27B46536]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2868924D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2868924D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__288FB3ED]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__288FB3ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__292BA50F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__292BA50F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__29975011]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__29975011]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__29BBDDE2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__29BBDDE2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__29F90B3C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__29F90B3C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2A101846]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2A101846]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2A504132]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2A504132]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2A8ADABD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2A8ADABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2AFD483A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2AFD483A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2B8789E9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2B8789E9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2C5D493F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2C5D493F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2C666DBF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2C666DBF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2CC95C04]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2CC95C04]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2D52D25C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2D52D25C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2D83187C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2D83187C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2E24CAC9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2E24CAC9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2EF4C8F4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2EF4C8F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2EFAF1E2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2EFAF1E2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2F11FABD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2F11FABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2F3D7CD3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2F3D7CD3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2F882F23]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2F882F23]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__2F8B8E49]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__2F8B8E49]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3054982F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3054982F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__305D2322]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__305D2322]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__31645783]    Fecha de la secuencia de comandos: 06/28/2013 10:40:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__31645783]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__32747E9A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__32747E9A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__328691A1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__328691A1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__330AE05B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__330AE05B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__338969BB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__338969BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__339AB16B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__339AB16B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__33B6B449]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__33B6B449]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__341347D6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__341347D6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__34B7C3EB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__34B7C3EB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3590B09D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3590B09D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__35C455E3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__35C455E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__35ED0E31]    Fecha de la secuencia de comandos: 06/28/2013 10:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__35ED0E31]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__366FF807]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__366FF807]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__37BE4DC8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__37BE4DC8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__37CD653D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__37CD653D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__37D91D8C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__37D91D8C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__39DAD50D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__39DAD50D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3A1A00D8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3A1A00D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3BF0CBA1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3BF0CBA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3C2F93D8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3C2F93D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3C95AF79]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3C95AF79]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3CB37ACF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3CB37ACF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3D4F042E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3D4F042E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3DF4E527]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3DF4E527]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3E50459A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3E50459A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3E9C8E98]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3E9C8E98]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3ECC0961]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3ECC0961]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3EDA23B5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3EDA23B5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3EFDE62F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3EFDE62F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3F08D327]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3F08D327]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3FA160F4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3FA160F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__3FD01066]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__3FD01066]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__40A7666A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:35 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__40A7666A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__40E267B8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__40E267B8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__41F3BDBA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__41F3BDBA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4233B4DC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4233B4DC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4338EEFB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4338EEFB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__43A3034F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__43A3034F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__43BC0A9B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__43BC0A9B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__43C2FEE0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__43C2FEE0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__44461A80]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__44461A80]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__44719C96]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__44719C96]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__45347D8E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:36 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__45347D8E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__45390FCE]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__45390FCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4543316F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4543316F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__45A8B383]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__45A8B383]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__460E6761]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__460E6761]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__46EAE932]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__46EAE932]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__46FC30E2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__46FC30E2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4754CBBC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4754CBBC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__47B72074]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__47B72074]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__47E6337A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__47E6337A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4860C427]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4860C427]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4948FE47]    Fecha de la secuencia de comandos: 06/28/2013 10:40:37 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4948FE47]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4B68B2E8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4B68B2E8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4CBB015C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4CBB015C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4D529208]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4D529208]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4E0391C3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4E0391C3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4E28515E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4E28515E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4E61EDC8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4E61EDC8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4ED6EF14]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4ED6EF14]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4FEA4387]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4FEA4387]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4FEFD2E8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4FEFD2E8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__4FF06C75]    Fecha de la secuencia de comandos: 06/28/2013 10:40:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__4FF06C75]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__511B3186]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__511B3186]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5281F506]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5281F506]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5341DA6C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5341DA6C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__53B93DBD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__53B93DBD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__53F30BF1]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__53F30BF1]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__54592792]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__54592792]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__555471DA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__555471DA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__55B20288]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__55B20288]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__55D09935]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__55D09935]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__56158A5A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__56158A5A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__56619D5F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__56619D5F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__56BE30EC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__56BE30EC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__579E11E3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__579E11E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__57B97B34]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__57B97B34]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__57C36B0B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__57C36B0B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5875361D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5875361D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__589D2314]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__589D2314]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__58E0E11F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__58E0E11F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__59A425AB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__59A425AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__59B53B91]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__59B53B91]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5A293FBC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5A293FBC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5B022C6E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:40 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5B022C6E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5B7D56A8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5B7D56A8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5BECFA5D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5BECFA5D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5D6E5BD7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5D6E5BD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5D948056]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5D948056]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5F2EB752]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5F2EB752]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5F4BB751]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5F4BB751]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__5F544244]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5F544244]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__601B4DB9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__601B4DB9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__607CD71A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__607CD71A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__60BECCAD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:41 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__60BECCAD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__60CF4906]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__60CF4906]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__619B1885]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__619B1885]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__61ACC7F8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__61ACC7F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__62ACD64A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__62ACD64A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__63A72371]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__63A72371]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__63B0AB85]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__63B0AB85]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__668CB49C]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__668CB49C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6816A109]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6816A109]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__684ED88F]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__684ED88F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__68C73901]    Fecha de la secuencia de comandos: 06/28/2013 10:40:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__68C73901]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__690305A6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__690305A6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__698BB0A7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__698BB0A7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__69DBF258]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__69DBF258]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6A338BE2]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6A338BE2]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6A50F3A4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6A50F3A4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6A8EBA8B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6A8EBA8B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6B0F7426]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6B0F7426]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6C3AA0FA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6C3AA0FA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6D4AF9DB]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6D4AF9DB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6EDA75A9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6EDA75A9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6EF7DD6B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6EF7DD6B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6FA9DA47]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6FA9DA47]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6FACD5D9]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6FACD5D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6FD58E27]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6FD58E27]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__6FF3597D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__6FF3597D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__70CD7949]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__70CD7949]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__71B519DC]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__71B519DC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__71BADB07]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__71BADB07]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__71C98EE8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__71C98EE8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__728A3FA5]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__728A3FA5]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__73488E5D]    Fecha de la secuencia de comandos: 06/28/2013 10:40:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__73488E5D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__73537B55]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__73537B55]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7359A443]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7359A443]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__73E9DD16]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__73E9DD16]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7412639A]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7412639A]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__75AF2E65]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__75AF2E65]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__75C8CF3E]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__75C8CF3E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7624617B]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7624617B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__762B55C0]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__762B55C0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__76946CF3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__76946CF3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__76C4E4DD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__76C4E4DD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7720DEDD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7720DEDD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__772B3248]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__772B3248]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__773A7FB6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__773A7FB6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__77623AE3]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__77623AE3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__781667FA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__781667FA]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7A4A91A7]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7A4A91A7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7A7B0991]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7A7B0991]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7B516274]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7B516274]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7BE29868]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7BE29868]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7CFD7AAD]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7CFD7AAD]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7D2567A4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7D2567A4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7D5F35D8]    Fecha de la secuencia de comandos: 06/28/2013 10:40:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7D5F35D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7DC781B4]    Fecha de la secuencia de comandos: 06/28/2013 10:40:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7DC781B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7EB57CFF]    Fecha de la secuencia de comandos: 06/28/2013 10:40:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7EB57CFF]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK__aspnet_Us__RoleI__7EF343E6]    Fecha de la secuencia de comandos: 06/28/2013 10:40:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__7EF343E6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Objeto:  ForeignKey [FK_AVE_CARGOSDETALLE_AVE_CARGOS]    Fecha de la secuencia de comandos: 06/28/2013 10:40:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_CARGOSDETALLE_AVE_CARGOS]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_CARGOSDETALLE]'))
ALTER TABLE [dbo].[AVE_CARGOSDETALLE]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARGOSDETALLE_AVE_CARGOS] FOREIGN KEY([IdCargo])
REFERENCES [dbo].[AVE_CARGOS] ([IdCargo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AVE_CARGOSDETALLE] CHECK CONSTRAINT [FK_AVE_CARGOSDETALLE_AVE_CARGOS]
GO
/****** Objeto:  ForeignKey [FK_AVE_CARGOSENTRADADETALLE_AVE_CARGOSENTRADA]    Fecha de la secuencia de comandos: 06/28/2013 10:40:55 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_CARGOSENTRADADETALLE_AVE_CARGOSENTRADA]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_CARGOSENTRADADETALLE]'))
ALTER TABLE [dbo].[AVE_CARGOSENTRADADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_AVE_CARGOSENTRADADETALLE_AVE_CARGOSENTRADA] FOREIGN KEY([IdEntrada])
REFERENCES [dbo].[AVE_CARGOSENTRADA] ([IdEntrada])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AVE_CARGOSENTRADADETALLE] CHECK CONSTRAINT [FK_AVE_CARGOSENTRADADETALLE_AVE_CARGOSENTRADA]
GO
/****** Objeto:  ForeignKey [FK_AVE_INVENTARIOSHISTORICOSDETALLE_AVE_INVENTARIOSHISTORICOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_INVENTARIOSHISTORICOSDETALLE_AVE_INVENTARIOSHISTORICOS]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSHISTORICOSDETALLE]'))
ALTER TABLE [dbo].[AVE_INVENTARIOSHISTORICOSDETALLE]  WITH CHECK ADD  CONSTRAINT [FK_AVE_INVENTARIOSHISTORICOSDETALLE_AVE_INVENTARIOSHISTORICOS] FOREIGN KEY([IdInventario])
REFERENCES [dbo].[AVE_INVENTARIOSHISTORICOS] ([IdInventario])
GO
ALTER TABLE [dbo].[AVE_INVENTARIOSHISTORICOSDETALLE] CHECK CONSTRAINT [FK_AVE_INVENTARIOSHISTORICOSDETALLE_AVE_INVENTARIOSHISTORICOS]
GO
/****** Objeto:  ForeignKey [FK_AVE_INVENTARIOSPENDIENTESDETALLE_AVE_INVENTARIOSPENDIENTES]    Fecha de la secuencia de comandos: 06/28/2013 10:41:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_INVENTARIOSPENDIENTESDETALLE_AVE_INVENTARIOSPENDIENTES]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_INVENTARIOSPENDIENTESDETALLE]'))
ALTER TABLE [dbo].[AVE_INVENTARIOSPENDIENTESDETALLE]  WITH CHECK ADD  CONSTRAINT [FK_AVE_INVENTARIOSPENDIENTESDETALLE_AVE_INVENTARIOSPENDIENTES] FOREIGN KEY([IdInventario])
REFERENCES [dbo].[AVE_INVENTARIOSPENDIENTES] ([IdInventario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AVE_INVENTARIOSPENDIENTESDETALLE] CHECK CONSTRAINT [FK_AVE_INVENTARIOSPENDIENTESDETALLE_AVE_INVENTARIOSPENDIENTES]
GO
/****** Objeto:  ForeignKey [FK_AVE_PEDIDOS_AVE_ESTADOSMENSAJES]    Fecha de la secuencia de comandos: 06/28/2013 10:41:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_PEDIDOS_AVE_ESTADOSMENSAJES]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOS]'))
ALTER TABLE [dbo].[AVE_PEDIDOS]  WITH CHECK ADD  CONSTRAINT [FK_AVE_PEDIDOS_AVE_ESTADOSMENSAJES] FOREIGN KEY([IdEstadoSolicitud])
REFERENCES [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado])
GO
ALTER TABLE [dbo].[AVE_PEDIDOS] CHECK CONSTRAINT [FK_AVE_PEDIDOS_AVE_ESTADOSMENSAJES]
GO
/****** Objeto:  ForeignKey [FK_AVE_PEDIDOS_AVE_ESTADOSPEDIDOS]    Fecha de la secuencia de comandos: 06/28/2013 10:41:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_PEDIDOS_AVE_ESTADOSPEDIDOS]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOS]'))
ALTER TABLE [dbo].[AVE_PEDIDOS]  WITH CHECK ADD  CONSTRAINT [FK_AVE_PEDIDOS_AVE_ESTADOSPEDIDOS] FOREIGN KEY([IdEstadoPedido])
REFERENCES [dbo].[AVE_ESTADOSPEDIDOS] ([IdEstado])
GO
ALTER TABLE [dbo].[AVE_PEDIDOS] CHECK CONSTRAINT [FK_AVE_PEDIDOS_AVE_ESTADOSPEDIDOS]
GO
/****** Objeto:  ForeignKey [FK_AVE_PEDIDOSENTRADADETALLE_AVE_PEDIDOSENTRADA]    Fecha de la secuencia de comandos: 06/28/2013 10:41:19 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AVE_PEDIDOSENTRADADETALLE_AVE_PEDIDOSENTRADA]') AND parent_object_id = OBJECT_ID(N'[dbo].[AVE_PEDIDOSENTRADADETALLE]'))
ALTER TABLE [dbo].[AVE_PEDIDOSENTRADADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_AVE_PEDIDOSENTRADADETALLE_AVE_PEDIDOSENTRADA] FOREIGN KEY([IdEntrada])
REFERENCES [dbo].[AVE_PEDIDOSENTRADA] ([IdEntrada])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AVE_PEDIDOSENTRADADETALLE] CHECK CONSTRAINT [FK_AVE_PEDIDOSENTRADADETALLE_AVE_PEDIDOSENTRADA]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Objeto:  View [dbo].[VINFINVENTARIO]    Fecha de la secuencia de comandos: 06/28/2013 10:44:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VINFINVENTARIO]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[VINFINVENTARIO] as SELECT art.IdArticulo, art.CodigoAlfa AS Referencia, art.Descripcion,'''' as Talla , isnull(ex.Stock,0) as Stock, isnull(vta.Venta,0) as Venta,(CASE WHEN (isnull(vta.Venta,0)+isnull(ex.Stock,0))=0 THEN 0 ELSE (CONVERT(float,isnull(vta.Venta,0))*100 /(isnull(vta.Venta,0)+isnull(ex.Stock,0)))  END) as VR,  col.Color, (CASE WHEN pr.NombreComercial<>'''' THEN pr.NombreComercial ELSE pr.Nombre END)+''(''+LTrim(RTrim(pr.IdProveedor))+'')''  as Proveedor,  art.ModeloProveedor AS [ModProveedor],  (CASE WHEN art.DescripcionFabricante=''#'' Or art.DescripcionFabricante=''x'' Or art.DescripcionFabricante=''x#'' THEN ''''  WHEN charindex(''#'',art.DescripcionFabricante)>0 THEN LEFT(art.DescripcionFabricante,charindex(''#'',art.DescripcionFabricante)-1) ELSE art.DescripcionFabricante END) AS [Desc Proveedor],  (CASE WHEN art.DescripcionFabricante=''#'' Or art.DescripcionFabricante=''x'' Or art.DescripcionFabricante=''x#'' THEN ''''  WHEN charindex(''#'',art.DescripcionFabricante)>0 THEN RIGHT(art.DescripcionFabricante,len(art.DescripcionFabricante)-charindex(''#'',art.DescripcionFabricante)) ELSE '''' END) AS [Color Proveedor],  tempo.Nombre+''(''+LTrim(RTrim(tempo.IdTemporada))+'')'' AS Temporada, sc.Nombre+''(''+LTrim(RTrim(sc.IdSeccion))+'')'' AS [Sección],fam.Nombre+''(''+LTrim(RTrim(fam.IdFamilia))+'')'' as Familia,sc.IdSeccion  FROM ARTICULOS AS art  With (NOLOCK)  LEFT JOIN COLORES AS col  With (NOLOCK)  ON art.idColor = col.IdColor  INNER JOIN PROVEEDORES AS pr  With (NOLOCK)  ON art.IdProveedor = pr.IdProveedor  INNER JOIN TEMPORADA AS tempo  With (NOLOCK)  ON art.IdTemporada = tempo.IdTemporada  INNER JOIN SECCIONES AS sc  With (NOLOCK)  ON art.IdSeccion = sc.IdSeccion  INNER JOIN FAMILIAS AS fam  With (NOLOCK)  ON art.IdFamilia = fam.IdFamilia  LEFT JOIN (SELECT isnull(SUM(Cantidad),0) as Stock,IdArticulo FROM N_EXISTENCIAS  With (NOLOCK)  WHERE IdTienda=''T-0430'' GROUP BY IdArticulo) ex  ON ex.IdArticulo=art.IdArticulo  LEFT JOIN (SELECT SUM(Estado*-1) as Venta,Id_Articulo FROM N_TICKETS_DETALLES  With (NOLOCK)  WHERE Id_Tienda=''T-0430'' GROUP BY Id_Articulo) vta  ON vta.Id_Articulo=art.IdArticulo  WHERE art.IdArticulo IN(32,181,182,183,184,185,186,187,217,221,222,608,609,610,611,612,613,614,615,616,617,618,619,620,621,622,623,624,625,626,627,628,629,783,787,813,1805,1806,1807,1808,1809,1810,1811,1953,1954,1955,1959,1960,1967,1968,1973,1974,1975,1976,1977,1978,1979,1980,1981,1982,1983,1984,1985,1986,1987,2050,2051,2052,2064,2065,2066,3173,3174,3175,3176,3191,3192,3193,3194,3224,3225,3226,3313,3314,3315,3345,3346,3347,3357,3369,3370,3371,3372,3496,3497,3498,3504,3505,3506,4818,4819,4820,4821,4827,4828,4829,4931,4962,4963,4964,4965,4966,4967,4991,4992,5023,5024,5025,18686,18687,18688,18689,18690,18691,18692,18693,18694,18924,18925,18926,18927,18928,18929,18930,18931,18932,18933,18934,18935,18936,18937,18938,26836,26837,26838,26839,26840,26841,26842,26843,26844,26845,26846,26847,26848,26849,26850,26851,26852,27168,27169,27170,27181,27182,27183,27184,27233,27234,27235,27236,27240,27241,27248,27249,27879,27880,27881,27882,27883,7903,7904,8166,8167,8168,8169,8193,8634,8635,8636,8637,8638,8639,8640,8641,8642,8643,8644,8645,8646,8647,8648,8649,8650,8651,8652,8776,9699,9700,9701,9737,9738,9739,9740,9741,9742,9743,9744,9855,9856,9860,9861,9862,9901,9902,9903,11318,11319,11320,11321,11399,11400,11401,11402,11403,11406,11407,11408,11412,11413,11414,11446,11447,11528,11529,11530,11531,11546,11547,11548,11588,11589,11590,11591,11592,11593,11595,11596,11597,11598,11599,12564,12565,12566,12699,12851,12852,12961,12962,12963,12964,14428,14429,14430,14431,23929,23930,23931,23932,23933,23934,23935,23936,23937,23938,23939,23940,23941,23991,23992,23993,23994,23995,23996,23997,23998,24028,24029,24030,24031,24106,24107,24108,24109,24110,24111,24112,24113,24142,24143,24144,24156,24157,24158,25828,25829)  AND art.CodigoAlfa<>''ARGLO'' AND art.CodigoAlfa<>''Arreglos'' '
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VINFINVENTARIO_GROUP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[VINFINVENTARIO_GROUP] as  Select Proveedor as Orden , '''' as talla , sum(stock) as stock , sum(venta) as venta From dbo.vinfinventario Group by Proveedor'
GO
/****** Objeto:  View [dbo].[VW_AVE_PEDIDOSPENDIENTES]    Fecha de la secuencia de comandos: 06/28/2013 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_AVE_PEDIDOSPENDIENTES]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[VW_AVE_PEDIDOSPENDIENTES]
AS
SELECT     PD.IdPedido, SD.Id_Tienda AS IdTienda, PD.IdArticulo, SUM(SD.Cantidad) AS Cantidad, SD.Id_Cabecero_detalle
FROM         N_PEDIDOS_DETALLES PD INNER JOIN
                      N_SURTIDOS_DETALLES SD ON PD.IdSurtido = SD.id_surtido INNER JOIN
                      N_PEDIDOS_PENDIENTES PP ON SD.Id_Tienda = PP.IdTienda AND PD.IdArticulo = PP.IdArticulo AND PD.IdPedido = PP.IdPedidoPendiente
GROUP BY PD.IdPedido, SD.Id_Tienda, SD.Id_Cabecero_detalle, PD.IdArticulo
UNION
SELECT     [PI].Id_Pedido, [PI].IdTienda, PID.IdArticulo, SUM(PID.Cantidad) AS Cantidad, PID.Id_Cabecero_detalle
FROM         N_PEDIDOS_INCOMPLETOS_DETALLES PID INNER JOIN
                      N_PEDIDOS_INCOMPLETOS[PI] ON PID.Id_Incompleto = [PI].id_Incompleto
GROUP BY [PI].Id_Pedido, [PI].IdTienda, PID.Id_Cabecero_detalle, PID.IdArticulo
'
GO
/****** Objeto:  View [dbo].[V_EVOLUCION_VENTA]    Fecha de la secuencia de comandos: 06/28/2013 10:44:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_EVOLUCION_VENTA]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[V_EVOLUCION_VENTA] AS SELECT (CASE WHEN emp.Apellidos='''' or emp.Apellidos is null THEN emp.Nombre ELSE emp.Apellidos+'', ''+emp.Nombre END)+'' (''+LTrim(RTrim(emp.IdEmpleado))+'')'' as Empleado, art.CodigoAlfa, art.ModeloProveedor, art.DescripcionFabricante,pr.Nombre+'' (''+LTrim(RTrim(art.IdProveedor))+'')'' as Proveedor, sc.Nombre+'' (''+LTrim(RTrim(art.Idseccion))+'')'' as Seccion, SUM(td.Estado*-1) as Ventas, SUM(td.ImporteEuros*abs(td.Estado)) as VentasEuros, (SELECT SUM(Cantidad) FROM N_EXISTENCIAS ex WHERE ex.IdArticulo=art.IdArticulo AND ex.IdTienda=''T-0430'') as Stock  FROM N_TICKETS AS t WITH (NOLOCK) INNER JOIN N_TICKETS_DETALLES AS td WITH (NOLOCK) ON t.Id_Auto = td.Id_Auto AND t.Id_Tienda = td.Id_Tienda  INNER JOIN ARTICULOS AS art WITH (NOLOCK) ON td.Id_Articulo = art.IdArticulo  INNER JOIN SECCIONES AS sc WITH (NOLOCK) ON sc.IdSeccion = art.IdSeccion  INNER JOIN PROVEEDORES AS pr WITH (NOLOCK) ON pr.IdProveedor = art.IdProveedor  INNER JOIN EMPLEADOS AS emp WITH (NOLOCK) ON emp.IdEmpleado = t.Id_Empleado  WHERE t.Fecha between CONVERT(DATETIME,''20/03/2012 00:00:00'') AND CONVERT(DATETIME,''20/03/2012 23:59:59'') AND  td.Id_Tienda=''T-0430'' GROUP BY emp.Nombre,emp.Apellidos,emp.IdEmpleado,art.CodigoAlfa, art.IdArticulo, art.IdProveedor,pr.Nombre, sc.Nombre, art.IdSeccion, art.ModeloProveedor, art.DescripcionFabricante '
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarEnvio]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarEnvio]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Jose Luis
-- Create date: 26/06/2010
-- Description: Generación automática de un iedntificador de envio
-- =============================================
CREATE FUNCTION [dbo].[GenerarEnvio] 
(	
		@IdTienda varchar(10),
		@TerminalD varchar(10)
)
RETURNS varchar(256)
AS
BEGIN
		DECLARE @Aleatorio varchar(6),
				@Unico bit,
				@Resul varchar(256)
		
		SET @Aleatorio=0
		SET @Unico=0
				
		WHILE (@Unico)=0
			BEGIN
				WHILE (Convert(int,@Aleatorio))<100000
					BEGIN
						SELECT @Aleatorio=convert(varchar,DATEPART(Ms, getdate())) + convert(varchar,(DATEPART(Ms, getdate()))/2 + 1)
					END
					
				SET @Resul=''S/''+@IdTienda+@TerminalD+Convert(varchar,Year(getDate()))+@Aleatorio	
				
				IF NOT Exists (Select 1 from COMP_Composturas where Compostura=@Resul)
					SET @Unico=1
			END
		
		RETURN @Resul
END
' 
END
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarCompostura]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarCompostura]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Jose Luis
-- Create date: 26/06/2010
-- Description: Generación automática de la compostura
-- =============================================
CREATE FUNCTION [dbo].[GenerarCompostura] 
(
		
		@IdTienda varchar(10),
		@TerminalD varchar(10)
)
RETURNS varchar(256)
AS
BEGIN
		DECLARE @Aleatorio varchar(6),
				@Unico bit,
				@Resul varchar(256)
		
		SET @Aleatorio=0
		SET @Unico=0
				
		WHILE (@Unico)=0
			BEGIN
				WHILE (Convert(int,@Aleatorio))<100000
					BEGIN
						SELECT @Aleatorio=convert(varchar,DATEPART(Ms, getdate())) + convert(varchar,(DATEPART(Ms, getdate()))/2 + 1)
					END
					
				SET @Resul=@IdTienda+@TerminalD+Convert(varchar,Year(getDate()))+@Aleatorio	
				
				IF NOT Exists (Select 1 from COMP_Composturas where Compostura=@Resul)
					SET @Unico=1
				
			END
		
		RETURN @Resul
END
' 
END
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarIdCliente]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarIdCliente]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GenerarIdCliente] 
(
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @IdLibre float
	DECLARE @Encontrado bit
	SET @Encontrado = 0

    --''Obtiene Numero Virtual
SELECT @IdLibre = CAST(RIGHT(DATEPART(Yy, getdate()),2) AS CHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mm, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Dd, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)

select	@IdLibre = CAST(
		CAST(RIGHT(DATEPART(Hh, getdate()),2) AS VARCHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mi, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Ss, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)
		AS float)+@IdLibre

select @IdLibre = ROUND(@IdLibre/2,0)

WHILE (@Encontrado = 0)
BEGIN
	IF (EXISTS (SELECT id_Cliente FROM N_CLIENTES_GENERAL WHERE id_Cliente=@IdLibre))
		SELECT @IdLibre = @IdLibre + 1
	ELSE
		SELECT @Encontrado = 1
END
	-- Return the result of the function
	RETURN @IdLibre

END
' 
END
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarIdCargo]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarIdCargo]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		OLG
-- Create date: 28-05-2010
-- Description:	Crea un IdCargo libre según formato de Moddo
-- =============================================
CREATE FUNCTION [dbo].[GenerarIdCargo]
(
	@IdTienda varchar(10)
)
RETURNS int
AS
BEGIN
	
	DECLARE @Serie int
	DECLARE @IdLibre int
	DECLARE @Encontrado bit
	SET @Encontrado = 0
  
	SET  @Serie =  RIGHT(@IdTienda, LEN(@IdTienda) - CHARINDEX(''-'', @IdTienda))
    IF (@Serie > 100) 
    BEGIN
		SET @Serie = @Serie / 10
	END

    --''Obtiene Numero Virtual
	SELECT @IdLibre = CAST(RIGHT(DATEPART(Yy, getdate()),2) AS CHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mm, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Dd, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)

	SELECT	@IdLibre = CAST(
		CAST(RIGHT(DATEPART(Hh, getdate()),2) AS VARCHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mi, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Ss, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)
		AS FLOAT)+@IdLibre

    IF LEN(@IdLibre) <= 7 
		--Ponemos los 0 que falten delante de IdLibre hasta hacer 7 dígitos. Luego ponemos delante la serie.
		SET @IdLibre = CAST(CAST(@Serie AS VARCHAR(MAX)) + RIGHT(''0000000'' + CAST(@IdLibre AS VARCHAR(MAX)),7)AS FLOAT)
    ELSE
		--Del idLibre tomamos los 7 últimos caracteres (mid) y los ponemos tras la serie
		SET @IdLibre = CAST(CAST(@Serie AS VARCHAR(MAX)) + RTRIM(LTRIM(RIGHT(@IdLibre,7))) AS FLOAT)

	WHILE (@Encontrado = 0)
	BEGIN
		IF (EXISTS (SELECT IdCargo FROM AVE_CARGOS WHERE IdCargo=@IdLibre) OR
			EXISTS (SELECT Id_Cargo FROM N_CARGOS WHERE Id_Cargo=@IdLibre))
			SELECT @IdLibre = @IdLibre + 1
		ELSE
			SELECT @Encontrado = 1
	END
		-- Return the result of the function
		RETURN @IdLibre

END
' 
END
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarIdEntradaPedido]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarIdEntradaPedido]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28-05-2010
-- Description:	Crea un IdEntradaPedido libre
-- =============================================
CREATE FUNCTION [dbo].[GenerarIdEntradaPedido]
(
	@IdTienda varchar(10)
)
RETURNS int
AS
BEGIN
	
	DECLARE @Serie int
	DECLARE @IdLibre int
	DECLARE @Encontrado bit
	SET @Encontrado = 0
  
	SET  @Serie =  RIGHT(@IdTienda, LEN(@IdTienda) - CHARINDEX(''-'', @IdTienda))
    IF (@Serie > 100) 
    BEGIN
		SET @Serie = @Serie / 10
	END

    --''Obtiene Numero Virtual
	SELECT @IdLibre = CAST(RIGHT(DATEPART(Yy, getdate()),2) AS CHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mm, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Dd, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)

	SELECT	@IdLibre = CAST(
		CAST(RIGHT(DATEPART(Hh, getdate()),2) AS VARCHAR(2)) + RIGHT(''0''+CAST(DATEPART(Mi, getdate()) AS VARCHAR(2)),2)
		+ RIGHT(''0''+CAST(DATEPART(Ss, getdate()) AS VARCHAR(2)),2) + RIGHT(''00''+CAST(DATEPART(Ms, getdate()) AS VARCHAR(3)),3)
		AS int)+@IdLibre

    IF LEN(@IdLibre) <= 7 
		--Ponemos los 0 que falten delante de IdLibre hasta hacer 7 dígitos. Luego ponemos delante la serie.
		SET @IdLibre = CAST(CAST(@Serie AS VARCHAR(MAX)) + RIGHT(''0000000'' + CAST(@IdLibre AS VARCHAR(MAX)),7)AS int)
    ELSE
		--Del idLibre tomamos los 7 últimos caracteres (mid) y los ponemos tras la serie
		SET @IdLibre = CAST(CAST(@Serie AS VARCHAR(MAX)) + RTRIM(LTRIM(RIGHT(@IdLibre,7))) AS int)

	WHILE (@Encontrado = 0)
	BEGIN
		IF (EXISTS (SELECT IdEntradaPedido FROM AVE_PEDIDOSENTRADA WHERE IdEntradaPedido=@IdLibre))
			SELECT @IdLibre = @IdLibre + 1
		ELSE
			SELECT @Encontrado = 1
	END
		-- Return the result of the function
		RETURN @IdLibre

END
' 
END
GO
/****** Objeto:  UserDefinedFunction [dbo].[GenerarIdTicket]    Fecha de la secuencia de comandos: 06/28/2013 10:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerarIdTicket]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28-05-2010
-- Description:	Crea un IdCargo libre según formato de Moddo
-- =============================================
CREATE FUNCTION [dbo].[GenerarIdTicket]
(
	@IdTienda varchar(10)
)
RETURNS varchar(20)
AS
BEGIN
	
	DECLARE @idTicketAnt varchar(20)
	DECLARE @Idticket varchar(20)
    DECLARE @numTicket int
	
    Select @idTicketAnt=MAX(Id_Ticket)
           from N_Tickets where Id_Tienda=@IdTienda
  
    if   @idTicketAnt is null
	  begin
          set @Idticket=''000001/'' + @idTienda
      end   
    else
      begin 

         set @numTicket= substring(@idTicketAnt,1,charindex(''/'',@idTicketAnt)-1)    
         set @numTicket= @numTicket+1
         set @Idticket=cast(@numTicket as varchar)  
         
         if len(@Idticket)<6
           begin          
               set @Idticket=replicate(''0'',6-len(@Idticket)) +  @Idticket + ''/'' +  @idTienda 
           end  
         else
           begin 
              set @Idticket=@Idticket + ''/'' +  @idTienda 
           end   
      end 
	
      RETURN @Idticket

END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	Busca un pedido del TPV para la funcionalidad de Entrada de pedidos.
--	El pedido debe serlo de la tienda del usuario. 
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosEntradaBuscar]
	@IdPedido int,
	@IdTienda varchar(10)
AS
BEGIN
	SELECT	P.*,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor AS Modelo,
			CD.Nombre_Talla AS Talla
	
	FROM	VW_AVE_PEDIDOSPENDIENTES P INNER JOIN	
			ARTICULOS A ON P.IdArticulo = A.IdArticulo INNER JOIN
			CABECEROS_DETALLES CD ON CD.Id_Cabecero_detalle = P.Id_Cabecero_detalle
		
	WHERE	P.IdPedido = @IdPedido AND
			P.IdTienda = @IdTienda
	
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaCrear]    Fecha de la secuencia de comandos: 06/28/2013 10:54:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaCrear]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	Creamos la entrada y sus detalles. Los detalles son los artículos y 
--	cabeceros del pedido, sin cantidades.
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosEntradaCrear]
   @IdPedido INT
   ,@FechaEntrada DATETIME
   ,@IdTienda VARCHAR(10)
   ,@IdEmpleado INT
   ,@IdTerminal NVARCHAR(50)
   ,@Usuario NVARCHAR(256)
   ,@IdEntrada INT OUTPUT
AS
BEGIN
	DECLARE @fecha AS DATETIME
	SET @fecha = GETDATE()
--INSERCIÓN DE LA ENTRADA
	INSERT INTO AVE_PEDIDOSENTRADA
           ([IdEntradaPedido]
           ,[IdPedido]
           ,[FechaEntrada]
           ,[IdTienda]
           ,[IdEmpleado]
           ,[IdTerminal]
           ,[Finalizado]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
     VALUES
           (dbo.GenerarIdEntradaPedido(@IdTienda)
           ,@IdPedido
           ,@FechaEntrada
           ,@IdTienda
           ,@IdEmpleado
           ,@IdTerminal
           ,0
           ,@fecha
           ,@Usuario)
			
	SET @IdEntrada = @@IDENTITY		

--BUSCAMOS EL DETALLE EN EL PEDIDO Y LO INSERTAMOS COMO DETALLE DE LA ENTRADA, SIN CANTIDAD	
	INSERT INTO AVE_PEDIDOSENTRADADETALLE 
			(IdEntrada
			,IdArticulo
			,Id_Cabecero_Detalle
			,FechaCreacion
			,UsuarioCreacion)
			
	SELECT	@IdEntrada
			,P.IdArticulo
			,P.Id_Cabecero_Detalle
			,@fecha
			,@Usuario
		
	FROM	VW_AVE_PEDIDOSPENDIENTES P
		
	WHERE	P.IdPedido = @IdPedido AND
			P.IdTienda = @IdTienda	
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	Devuelve los datos de una entrada de pedido dado el IdEntrada. Incluye también los
--		datos originales del pedido al que corresponde
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosEntradaObtener]
	@IdEntrada int
AS
BEGIN

	-- Obtenemos la entrada solicitada, también el pedido pendiente que le corresponde a dicha entrada y
	-- la información de detalle de la entrada la fusionamos por artículo y cabecero con el pedido pendiente
	-- para obtener la cantidad pedida y recibida en una sola línea
	SELECT  P.IdArticulo,
			P.Id_Cabecero_Detalle,
			P.Cantidad AS CantidadPedido,
			PED.Cantidad AS CantidadEntrada,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor AS Modelo,
			CD.Nombre_Talla AS Talla
						
	FROM	VW_AVE_PEDIDOSPENDIENTES P INNER JOIN
			AVE_PEDIDOSENTRADA PE 
				ON P.IdPedido = PE.IdPedido AND
				PE.IdEntrada = @IdEntrada LEFT JOIN 
			AVE_PEDIDOSENTRADADETALLE PED 
				ON PED.IdEntrada = PE.IdEntrada AND
				PED.IdArticulo = P.IdArticulo AND
				PED.Id_Cabecero_Detalle	 = P.Id_Cabecero_Detalle INNER JOIN
			ARTICULOS A 
				ON P.IdArticulo = A.IdArticulo INNER JOIN
			CABECEROS_DETALLES CD 
				ON CD.Id_Cabecero_detalle = P.Id_Cabecero_detalle
				
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CambiaCompatibilidadBBDD]    Fecha de la secuencia de comandos: 06/28/2013 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CambiaCompatibilidadBBDD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
CREATE PROCEDURE  [dbo].[AVE_CambiaCompatibilidadBBDD] 
	-- Add the parameters for the stored procedure here
	 @level as  int out,
     @DDBB  as Varchar(50) out
as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

    
  
    select @DDBB=DB_NAME() 

    select @level=compatibility_level 
    from sys.databases where name=@DDBB
   	
 END' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosIdCargoGenerar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosIdCargoGenerar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 8-06-2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosIdCargoGenerar]
	@IdTienda varchar(10)
AS
BEGIN
	SELECT dbo.GenerarIdCargo(@IdTienda)
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Color]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Color]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_Color] 
	
AS
BEGIN
     select 0 as idColor,'''' as Color
     union
	 select  idColor,Color 
     from colores
     where idColor>0
     order by IdColor
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ArticuloDetalleObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ArticuloDetalleObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ArticuloDetalleObtener] 
	@IdArticulo int,
	@IdTienda varchar (10)
AS
BEGIN
	SELECT	
			dbo.PROVEEDORES.Nombre AS Proveedor,
			dbo.ARTICULOS.CodigoAlfa as Referencia,
			dbo.ARTICULOS.ModeloProveedor + '' ('' + dbo.ARTICULOS.DescripcionFabricante + '')'' AS Modelo,
			dbo.ARTICULOS.ModeloProveedor,
			dbo.ARTICULOS.Descripcion AS Descripcion,
			dbo.COLORES.Color AS Color,
			dbo.ARTICULOS.Observaciones,
		  ( SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC) AS Precio
	
	FROM
			dbo.ARTICULOS LEFT JOIN 
			dbo.PROVEEDORES ON dbo.ARTICULOS.IdProveedor = dbo.PROVEEDORES.IdProveedor LEFT JOIN 
			dbo.COLORES on dbo.COLORES.IdColor = dbo.ARTICULOS.IdColor
	
	WHERE	dbo.ARTICULOS.IdArticulo = @IdArticulo

END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ColorModFabricante]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ColorModFabricante]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ColorModFabricante]
	@IdArticulo int
	
AS
BEGIN

    select  art.codigoalfa,art.idarticulo,art.modeloproveedor,Color from
            articulos art inner join 
            articulos  art1 on
            art.ModeloProveedor=art1.ModeloProveedor
            inner join Colores Co on
            Art.idColor=Co.idColor
            where art1.idarticulo=@IdArticulo


END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_StockOtrasTiendasObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_StockOtrasTiendasObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
 CREATE PROCEDURE [dbo].[AVE_StockOtrasTiendasObtener]
	@IdArticulo int,
	@IdTienda varchar(10)
AS
BEGIN

SELECT		ARTICULOS_1.Descripcion AS Descripcion,
			dbo.CABECEROS_DETALLES.Nombre_Talla AS Talla,
			dbo.N_EXISTENCIAS.Cantidad, 
			dbo.TIENDAS.IdTienda, 
			dbo.TIENDAS.Observaciones AS Tienda,
			COLORES.Color,
		  ( SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS_1.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC) AS Precio,articulos_1.idArticulo
FROM         dbo.TIENDAS INNER JOIN
                      dbo.GRUPOS_DETALLES INNER JOIN
                      dbo.GRUPOS_DETALLES AS GRUPOS_DETALLES_1 ON dbo.GRUPOS_DETALLES.Id_Grupo = GRUPOS_DETALLES_1.Id_Grupo INNER JOIN
                      dbo.N_EXISTENCIAS ON dbo.GRUPOS_DETALLES.Id_Tienda = dbo.N_EXISTENCIAS.IdTienda ON 
                      dbo.TIENDAS.IdTienda = dbo.GRUPOS_DETALLES.Id_Tienda INNER JOIN
                      dbo.ARTICULOS AS ARTICULOS_1 INNER JOIN
                      dbo.CABECEROS_DETALLES ON ARTICULOS_1.Id_Cabecero = dbo.CABECEROS_DETALLES.Id_Cabecero ON 
                      dbo.N_EXISTENCIAS.IdArticulo = ARTICULOS_1.IdArticulo AND 
                      dbo.N_EXISTENCIAS.Id_Cabecero_Detalle = dbo.CABECEROS_DETALLES.Id_Cabecero_Detalle LEFT JOIN
                      dbo.COLORES ON dbo.COLORES.IdColor = ARTICULOS_1.IdColor
WHERE     (ARTICULOS_1.IdArticulo = @IdArticulo) AND (GRUPOS_DETALLES_1.Id_Tienda = @IdTienda) AND (GRUPOS_DETALLES.Id_Tienda <> @IdTienda)
GROUP BY ARTICULOS_1.Descripcion, dbo.CABECEROS_DETALLES.Nombre_Talla, dbo.N_EXISTENCIAS.Cantidad, dbo.TIENDAS.IdTienda, 
                      dbo.TIENDAS.Observaciones, COLORES.Color, ARTICULOS_1.IdArticulo
ORDER BY dbo.TIENDAS.IdTienda
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_StockEnTiendaCSObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_StockEnTiendaCSObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[AVE_StockEnTiendaCSObtener]
@IdArticulo int,
@IdTienda varchar(10),
@Tipo char(1)
AS
begin
SELECT     ARTICULOS_1.Descripcion AS Descripcion, dbo.CABECEROS_DETALLES.Nombre_Talla AS Talla, dbo.N_EXISTENCIAS.Cantidad, 
            ARTICULOS_1.IdArticulo, dbo.COLORES.Color, 
		  ( SELECT TOP 1 PrecioEuro FROM
			dbo.PRECIOS_ARTICULOS WHERE PRECIOS_ARTICULOS.IdArticulo = ARTICULOS_1.IdArticulo AND PRECIOS_ARTICULOS.IdTienda = @IdTienda AND PRECIOS_ARTICULOS.Activo = 1
			ORDER BY Fecha DESC) AS Precio
			
FROM         dbo.ARTICULOS INNER JOIN
             dbo.ArticulosCS ON 
					CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + ''ý'' + 
					CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + ''ý'' + 
					CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50))	= 
					dbo.ArticulosCS.Articulo1 INNER JOIN
             dbo.ARTICULOS AS ARTICULOS_1 ON 
					CAST(ARTICULOS_1.IdTemporada AS VARCHAR(5)) + ''ý'' + 
					CAST(ARTICULOS_1.IdProveedor AS VARCHAR(5)) + ''ý'' + 
					CAST(ARTICULOS_1.ModeloProveedor AS VARCHAR(50)) = 
					dbo.ArticulosCS.Articulo2 INNER JOIN
             dbo.CABECEROS_DETALLES ON 
					ARTICULOS_1.Id_Cabecero = 
					dbo.CABECEROS_DETALLES.Id_Cabecero INNER JOIN
             dbo.N_EXISTENCIAS ON 
					ARTICULOS_1.IdArticulo = 
					dbo.N_EXISTENCIAS.IdArticulo AND 
                    dbo.CABECEROS_DETALLES.Id_Cabecero_Detalle = 
					dbo.N_EXISTENCIAS.Id_Cabecero_Detalle AND 
                    dbo.N_EXISTENCIAS.IdTienda = @IdTienda LEFT OUTER JOIN
             dbo.COLORES ON dbo.COLORES.IdColor = ARTICULOS_1.IdColor
WHERE     (dbo.ARTICULOS.IdArticulo = @IdArticulo) AND (dbo.ArticulosCS.TipoRela = @Tipo)
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_StockEnTiendaObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_StockEnTiendaObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_StockEnTiendaObtener]
	@IdArticulo int,
	@IdTienda varchar(10)
AS
BEGIN


Select  * into  #Stock    
FROM 
(SELECT		PRO.Nombre + '' '' + ARTICULOS_1.ModeloProveedor + '' ('' + ARTICULOS_1.DescripcionFabricante + '')'' AS Descripcion,
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
SELECT		PRO.Nombre + '' '' + ARTICULOS.ModeloProveedor + '' ('' + dbo.ARTICULOS.DescripcionFabricante + '')'' AS Descripcion ,
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
SELECT		PRO.Nombre + '' '' + ARTICULOS.ModeloProveedor + '' ('' + dbo.ARTICULOS.DescripcionFabricante + '')'' AS Descripcion ,
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


END' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_SolicitudesAlmacenObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_SolicitudesAlmacenObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_SolicitudesAlmacenObtener] 
	@IdEstado int = NULL,
	@IdPedido int = NULL,
	@IdTienda varchar(10),
    @Fecha datetime=null  

AS
BEGIN
    
	SELECT  P.IdPedido,
			PRO.IdProveedor,
			PRO.Nombre AS Proveedor,
			T.Observaciones AS Tienda,
			P.IdArticulo,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor + '' ('' + A.DescripcionFabricante + '')'' AS Modelo,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			E.Nombre + '' '' + E.Apellidos as Vendedor,
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
            --and P.IdTienda = @IdTienda
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosBuscarAvanzado]    Fecha de la secuencia de comandos: 06/28/2013 10:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosBuscarAvanzado]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[AVE_PedidosBuscarAvanzado]
	@IdPedido int = NULL,
	@Proveedor varchar(50) = NULL,
	@Producto varchar(50)= NULL,
	@Vendedor varchar(50)= NULL,
	@IdEstado int = NULL
AS
BEGIN
	SELECT  P.IdPedido,
			PR.Nombre AS Proveedor,
			P.IdArticulo,
			A.CodigoAlfa AS Referencia, 
			A.ModeloProveedor AS Modelo,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			EMP.Nombre + '' '' + EMP.Apellidos AS Vendedor,
			EP.[Resource] AS EstadosPedidosResource,
			P.Fecha_Creacion AS Fecha
	
	FROM	AVE_PEDIDOS P INNER JOIN
			ARTICULOS A ON A.IdArticulo = P.IdArticulo INNER JOIN
            EAN ON A.IdArticulo = EAN.IdArticulo INNER JOIN 
            EMPLEADOS EMP ON EMP.IdEmpleado = P.IdEmpleado INNER JOIN
            PROVEEDORES PR ON PR.IdProveedor = A.IdProveedor INNER JOIN 
            COLORES C ON C.IdColor = A.IdColor INNER JOIN 
            AVE_ESTADOSPEDIDOS EP ON EP.IdEstado = P.IdEstadoPedido
	
	WHERE	P.IdPedido = ISNULL(@IdPedido,P.IdPedido) 
			AND
			(CAST(PR.IdProveedor AS VARCHAR(10)) = ISNULL(@Proveedor,PR.IdProveedor) OR
			 PR.Nombre like CAST(''%'' + @Proveedor  + ''%'' AS VARCHAR(52))) 
			AND
			(CAST(A.IdArticulo AS VARCHAR(10)) = ISNULL(@Producto,A.IdArticulo) OR		--Para la búsqueda de @Producto, si el param es nulo basta con igualarlo a uno de los campos (aquí IdARticulo) para que la condición completa del WHERE se anule para este parametro
			 A.CodigoAlfa like CAST(''%'' + @Producto  + ''%'' AS VARCHAR(52)) OR
			 EAN.EAN = @Producto OR 
			 A.Descripcion like CAST(''%'' + @Producto  + ''%'' AS VARCHAR(52))) 
			AND
			(CAST(P.IdEmpleado AS VARCHAR(10)) = ISNULL(@Vendedor,P.IdEmpleado) OR		--Para la búsqueda de @Vendedor, si el param es nulo basta con igualarlo a uno de los campos (aquí IdEmpleado) para que la condición completa del WHERE se anule para este parametro
			 EMP.Nombre like CAST(''%'' + @Vendedor  + ''%'' AS VARCHAR(52)) OR 
			 EMP.Apellidos like CAST(''%'' + @Vendedor  + ''%'' AS VARCHAR(52))) 
			 AND
 			 P.IdEstadoPedido = ISNULL(@IdEstado, P.IdEstadoPedido)
			
	GROUP BY 
			P.IdPedido,
			PR.Nombre,
			P.IdArticulo,
			A.CodigoAlfa, 
			A.ModeloProveedor,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			EMP.Nombre,
			EMP.Apellidos,
			EP.[Resource],
			P.Fecha_Creacion
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosBuscar]
	@Filtro varchar(50) = NULL
AS
BEGIN
	SELECT  P.IdPedido,
			PR.Nombre AS Proveedor,
			P.IdArticulo,
			A.CodigoAlfa AS Referencia, 
			A.ModeloProveedor AS Modelo,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			EMP.Nombre + '' '' + EMP.Apellidos AS Vendedor,
			EP.[Resource] AS EstadosPedidosResource,
			P.Fecha_Creacion AS Fecha
	
	FROM	AVE_PEDIDOS P INNER JOIN
			ARTICULOS A ON A.IdArticulo = P.IdArticulo INNER JOIN
            EAN ON A.IdArticulo = EAN.IdArticulo INNER JOIN 
            EMPLEADOS EMP ON EMP.IdEmpleado = P.IdEmpleado INNER JOIN
            PROVEEDORES PR ON PR.IdProveedor = A.IdProveedor INNER JOIN 
            COLORES C ON C.IdColor = A.IdColor INNER JOIN 
            AVE_ESTADOSPEDIDOS EP ON EP.IdEstado = P.IdEstadoPedido
	
	WHERE	CAST(P.IdPedido AS VARCHAR(10)) = @Filtro OR
			CAST(PR.IdProveedor AS VARCHAR(10)) = @Filtro OR
			PR.Nombre like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR
			CAST(A.IdArticulo AS VARCHAR(10)) = @Filtro OR		--Para la búsqueda de @Producto, si el param es nulo basta con igualarlo a uno de los campos (aquí IdARticulo) para que la condición completa del WHERE se anule para este parametro
			A.CodigoAlfa like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR
			EAN.EAN = @Filtro OR 
			A.Descripcion like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR
			CAST(P.IdEmpleado AS VARCHAR(10)) = ISNULL(@Filtro,P.IdEmpleado) OR		--Para la búsqueda de @Vendedor, si el param es nulo basta con igualarlo a uno de los campos (aquí IdEmpleado) para que la condición completa del WHERE se anule para este parametro
			EMP.Nombre like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR 
			EMP.Apellidos like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52))
			
	GROUP BY 
			P.IdPedido,
			PR.Nombre,
			P.IdArticulo,
			A.CodigoAlfa, 
			A.ModeloProveedor,
			A.Descripcion,
			C.Color,
			P.Talla,
			P.Unidades,
			EMP.Nombre,
			EMP.Apellidos,
			EP.[Resource],
			P.Fecha_Creacion
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_OrdenInventarioEsArticuloEnPerimetro]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_OrdenInventarioEsArticuloEnPerimetro]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 9-7-2010
-- Description:	Para una orden de inventario, indica si un artículo se encuentra en el perímetro de la OI.
--	 El artículo debe cumplir todas las condiciones que se dan para una OI. Por ejemplo, si no se ha definido ninguna familia en la OI,
--	 el artículo puede pertenecer a cualquier familia sin embargo, si se definen familias para la OI, el articulo debe pertenecer a alguna de ellas
--	 del mismo modo tendrá que cumplir con el resto de características del perímetro: temporadas, proveedores, secciones, etc. En el momento en que no 
--	 cumpla con una de las condiciones del perímetro ya no se dará por válido. El propio IdArticulo puede ser una parte del perímetro.
--	 Se devuelve 1 si el artículo es válido y 0 si no lo es.
-- =============================================
CREATE PROCEDURE [dbo].[AVE_OrdenInventarioEsArticuloEnPerimetro] 
	@IdArticulo int,
	@IdOrdenInventario int
AS
BEGIN
	SELECT CAST(COUNT(OI.IdOrdenInventario) AS BIT)
	FROM	ORDENES_INVENTARIO OI LEFT JOIN
			ARTICULOS_INVENTARIO AI ON AI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			TEMPORADAS_INVENTARIO TI ON TI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			PROVEEDORES_INVENTARIO PRI ON PRI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			FAMILIAS_INVENTARIO FI ON FI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			SECCIONES_INVENTARIO SI ON SI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			COLORES_INVENTARIO CI ON CI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			MATERIALES_INVENTARIO MI ON MI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			TACON_INVENTARIO TAI ON TAI.IdOrdenInventario = OI.IdOrdenInventario LEFT JOIN
			CORTE_INVENTARIO COI ON COI.IdOrdenInventario = OI.IdOrdenInventario INNER JOIN
			ARTICULOS A ON	A.IdArticulo = ISNULL(AI.IdArticulo,A.IdArticulo) AND
							A.IdTemporada = ISNULL(TI.IdTemporada,A.IdTemporada) AND
							A.IdProveedor = ISNULL(PRI.IdProveedor,A.IdProveedor) AND
							A.IdFamilia = ISNULL(FI.IdFamilia,A.IdFamilia) AND
							A.IdSeccion = ISNULL(SI.IdSeccion,A.IdSeccion) AND
							A.IdColor = ISNULL(CI.IdColor,A.IdColor) AND
							A.IdMaterial = ISNULL(MI.IdMaterial,A.IdMaterial) AND
							A.IdTacon = ISNULL(TAI.IdTacon,A.IdTacon) AND
							A.IdCorte = ISNULL(COI.IdCorte,A.IdCorte)
							
	WHERE	OI.IdOrdenInventario = @IdOrdenInventario AND
			A.IdArticulo = @idarticulo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ArticuloBuscarLike]    Fecha de la secuencia de comandos: 06/28/2013 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ArticuloBuscarLike]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_ArticuloBuscarLike] 
	@Filtro VARCHAR(50),
    @StrTalla VARCHAR(50)=''*'',
	@IdCabeceroDetalle int = NULL OUT ,
	@IdArticulo int = NULL OUT 
AS
BEGIN
   DECLARE @CRow int
   DECLARE @idTienda varchar(10)  
  


   --obtenemos tienda actual
   select @idTienda=Tienda 
   from CONFIGURACIONES_TPV 

	SELECT
		A.IdArticulo,
		A.CodigoAlfa,
		MAX(EAN.EAN) AS EAN,						--No nos interesan los distintos EANs (tallas) para un mismo idarticulo
		A.ModeloProveedor AS Modelo,
		A.DescripcionFabricante AS Descripcion,
        A.id_Cabecero, 0 as StockTienda,
        0 as StockOtras,isnull(S.Nombre,'''') as Seccion 
	INTO #TEMP	
	FROM	
		ARTICULOS A INNER JOIN
		EAN ON A.IdArticulo = EAN.IdArticulo
        LEFT JOIN SECCIONES S on
        A.idSeccion=S.idSeccion       

	WHERE
		CAST(A.IdArticulo AS VARCHAR(10)) like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52))  OR		
		A.CodigoAlfa like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR
		EAN.EAN like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR 
		A.ModeloProveedor like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR 
		A.Descripcion like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR
		A.DescripcionFabricante like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52))
		
	GROUP BY
		A.IdArticulo,
		A.CodigoAlfa,
		A.ModeloProveedor,
		A.DescripcionFabricante,
        A.id_Cabecero,S.Nombre

       --obtenemos tiendas del grupo tiendas sin incluir la actual
        select id_tienda into #Tiendas from grupos_detalles where id_grupo in (
        select  id_grupo from grupos_detalles where id_tienda=@idTienda)
        and id_Tienda not in (@idTienda)  group by id_tienda  

       
    if @StrTalla<>''*''  
       begin
        --por talla
        select  @IdArticulo=IdArticulo from 
               #TEMP where CAST(IdArticulo AS VARCHAR(10))= CAST(@Filtro AS VARCHAR(52))  
       
        set @CRow=@@rowcount 
       
        if  @CRow>0 
           begin   
            -- es articulo 
            select  TP.*  into #TEMP1    
               from #TEMP TP inner join 
                    Cabeceros_detalles CB on
                    TP.id_Cabecero=CB.id_Cabecero            
               where CAST(IdArticulo AS VARCHAR(10))=CAST( @Filtro  AS VARCHAR(52))
                     AND  UPPER(LTRIM(RTRIM(Nombre_Talla)))=UPPER(LTRIM(RTRIM(@StrTalla)))

               DELETE From #TEMP
               INSERT INTO #TEMP select * from #TEMP1  
               DROP TABLE #TEMP1 
          end
       else
            begin
              -- codigo alfa 0 modelo proveedor ean
              select TP.*,id_cabecero_detalle into #TEMP11     
               from #TEMP TP inner join 
                    Cabeceros_detalles CB on
                    TP.id_Cabecero=CB.id_Cabecero            
               where UPPER(LTRIM(RTRIM(Nombre_Talla)))=UPPER(LTRIM(RTRIM(@StrTalla)))

              --Actualizamos Stock Tienda
              Update #TEMP11     
               set StockTienda=cantidad
               from  #TEMP11 TP, N_Existencias Exist
               where TP.idarticulo=Exist.idarticulo and                                      
                     TP.id_cabecero_Detalle=Exist.id_cabecero_Detalle and     
                     Exist.idTienda=@idTienda and cantidad>=0

           
               
--               --Actualizamos Stock otras Tiendas  
                Update #TEMP11     
                set StockOtras=Exist.cantidad
                from  #TEMP11 TP, (select sum(cantidad) as cantidad,
                                   idarticulo,id_cabecero_Detalle from
                                   N_existencias Where  
                                   idTienda in (Select id_tienda from #Tiendas) and cantidad>=0
                                   group by idarticulo,id_cabecero_Detalle) Exist
                where TP.idarticulo=Exist.idarticulo and                                      
                      TP.id_cabecero_Detalle=Exist.id_cabecero_Detalle 

 
              DELETE From #TEMP
              INSERT INTO #TEMP select IdArticulo,
		      CodigoAlfa, EAN, Modelo,
		      Descripcion,id_Cabecero, StockTienda,
              StockOtras from #TEMP11
              DROP TABLE #TEMP11 
         
           end 
       end 
    else
       begin
           -- sin talla    
          Select idarticulo,idTienda,
                sum(cantidad) as cantidad into #ExistSinTalla
                from N_Existencias
                where cantidad>=0           
                group by idarticulo,idTienda    
   
            -- Stock Tienda
           update #TEMP
                set StockTienda=cantidad   
                from #ExistSinTalla Exist,#TEMP TMP
                where Exist.idarticulo=TMP.idArticulo 
                and Exist.idTienda=@idTienda
             -- Stock Otras
            update #TEMP
                set StockOtras=cantidad
                from 
                (select sum(cantidad) as cantidad,
                        idarticulo from
                        #ExistSinTalla  Where idTienda in ((Select id_tienda from #Tiendas)) and cantidad>=0
                        group by idarticulo) Exist,#TEMP TMP
                where Exist.idarticulo=TMP.idArticulo 
                                       
         end    
    
       
	--Obtenemos el IdCabeceroDetalle si hay un EAN=filtro si no, será NULL
	SELECT  @IdCabeceroDetalle = IdCabeceroDetalle
	FROM	EAN 
	WHERE	EAN = @Filtro
	
	IF (1 = ( SELECT COUNT (DISTINCT IdArticulo) FROM #TEMP Where (StockTienda>0 or StockOtras>0))
        or 1=(SELECT COUNT (DISTINCT Modelo) FROM #TEMP Where (StockTienda>0 or StockOtras>0)))
	BEGIN
		SELECT @IdArticulo = IdArticulo FROM #TEMP Where (StockTienda>0 or StockOtras>0)
        -- si ponemos talla y no tiene stock en talla 

      
	END	
    else		
     begin


           SELECT * FROM #TEMP
           Where (StockTienda>0 or StockOtras>0) 
 
	end	
	DROP TABLE #TEMP
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Corte]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Corte]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_Corte] 
	
AS
BEGIN
     select 0 as idCorte, '''' as Corte
     union
	 Select  idCorte,Corte from Corte
     where idCorte>1
     order by idCorte
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_OrdenesInventarioBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_OrdenesInventarioBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 5-07-2010
-- Description:	Obtiene las OI para una tienda dada que pueden verse a día de hoy
-- =============================================
CREATE PROCEDURE [dbo].[AVE_OrdenesInventarioBuscar]
	@IdTienda varchar(10)	
AS
BEGIN
	SELECT  OI.IdOrdenInventario
			,OI.Nombre
			,OI.FechaInventarioIni
			,OI.FechaInventarioFin
			,CAST(TI.OIRealizada AS bit) AS OIRealizada
	
	FROM	ORDENES_INVENTARIO OI INNER JOIN
			TIENDAS_INVENTARIO TI ON OI.IdOrdenInventario = TI.IdOrdenInventario 
	
	WHERE	TI.IdTienda = @IdTienda AND
			DATEADD(DAY, 0, DATEDIFF(DAY,0,CURRENT_TIMESTAMP)) BETWEEN OI.FechaAvisoIni AND OI.FechaAvisoFin	--DATEADD(DAY, 0, DATEDIFF(DAY,0,CURRENT_TIMESTAMP)) ES LA FECHA ACTUAL SIN HORA
	
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_OrdenInventarioObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_OrdenInventarioObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 6-07-2010
-- Description:	Obtiene los datos de una OI dada, verificando que el empleado tiene acceso
-- =============================================
CREATE PROCEDURE [dbo].[AVE_OrdenInventarioObtener]
	@IdOrdenInventario int,
	@IdTienda varchar(10)			--Para verificar que puede ver esta OI
AS
BEGIN
	SELECT  OI.Nombre
			,OI.FechaInventarioIni
			,OI.FechaInventarioFin
			,OI.IdTipoInventario
			,TPI.Nombre AS TipoInventario
			,OI.IdTipoVista
			,TVI.Nombre AS TipoVista
			,OI.Observaciones
			,CAST(TI.OIRealizada AS bit) AS Realizada
			,CAST(TI.OIEnviada AS bit) AS Enviada
			,TI.Observaciones AS ObservacionesTienda
	
	FROM	ORDENES_INVENTARIO OI INNER JOIN
			TIENDAS_INVENTARIO TI ON OI.IdOrdenInventario = TI.IdOrdenInventario INNER JOIN 
			TIPOS_INVENTARIO TPI ON TPI.IdTipoInventario = OI.IdTipoInventario INNER JOIN
			TIPOS_VISTA_INVENTARIO TVI ON TVI.IdTipoVista = OI.IdTipoVista
	
	WHERE	OI.IdOrdenInventario = @IdOrdenInventario AND
			TI.IdTienda = @IdTienda AND
			DATEADD(DAY, 0, DATEDIFF(DAY,0,CURRENT_TIMESTAMP)) BETWEEN OI.FechaAvisoIni AND OI.FechaAvisoFin	--DATEADD(DAY, 0, DATEDIFF(DAY,0,CURRENT_TIMESTAMP)) ES LA FECHA ACTUAL SIN HORA
	
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ClienteBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ClienteBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ClienteBuscar]
	@Filtro varchar(50)
AS
BEGIN
	SELECT  id_Cliente,
			Nombre,
			Apellidos,
			Cif,
			Direccion,
			CodPostal,
			Poblacion,
			Provincia,
			Pais
			
	FROM dbo.N_CLIENTES_GENERAL

	WHERE	Nombre like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			Apellidos like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			Cif like CAST(''%'' + @Filtro  + ''%'' AS VARCHAR(52)) OR 
			Direccion like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			CodPostal like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			Poblacion like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			Provincia like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) OR 
			Pais like CAST(''%'' + @Filtro  + ''%'' AS NVARCHAR(52)) 
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ClienteGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ClienteGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ClienteGuardar] 
	@id_Cliente int OUTPUT,
	@Cif varchar(50),
	@Nombre nvarchar(60),
	@Apellidos nvarchar(100),
	@Direccion nvarchar(150),
	@CodPostal nvarchar(10),
	@Poblacion nvarchar(100),
	@Provincia nvarchar(100),
	@Telefono varchar(50),
	@Movil varchar(14),
	@email nvarchar(50),
	@Pais nvarchar(100)
	
AS
BEGIN
	IF (EXISTS (SELECT id_Cliente FROM dbo.N_CLIENTES_GENERAL WHERE id_Cliente = @id_Cliente))
		BEGIN
			UPDATE dbo.N_CLIENTES_GENERAL
			SET
				Nombre =	@Nombre,
				Apellidos = @Apellidos,  
				Cif		  =	@Cif, 
				Direccion =	@Direccion, 
				CodPostal =	@CodPostal, 
				Poblacion =	@Poblacion, 
				Provincia =	@Provincia, 
				Telefono =	@Telefono, 
				Movil =		@Movil, 
				email =		@email,
				Pais =		@Pais,
				Fecha_Modificacion = getdate()
			WHERE
				id_Cliente = @id_Cliente
		END
	ELSE
		BEGIN
			SELECT @id_Cliente = dbo.GenerarIdCliente()
			INSERT INTO dbo.N_CLIENTES_GENERAL (
				id_Cliente,
				Nombre,	
				Apellidos, 
				Cif,
				Direccion,
				CodPostal,
				Poblacion,
				Provincia,
				Telefono,
				Movil,
				email,
				Pais,
				Fecha_Modificacion 
				)
			VALUES (
				@id_Cliente,
				@Nombre,
				@Apellidos,
				@Cif,
				@Direccion, 
				@CodPostal, 
				@Poblacion, 
				@Provincia, 
				@Telefono, 
				@Movil, 
				@email,
				@Pais,
				getdate()
				)
		END

		SELECT @id_Cliente
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ClienteObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ClienteObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ClienteObtener]
	@id_Cliente int
AS
BEGIN
	SELECT	Nombre,
			Apellidos,
			Cif,
			Direccion,
			CodPostal,
			Poblacion,
			Provincia,
			Pais,
			email,
			Telefono,
			Movil
	
	FROM	dbo.N_CLIENTES_GENERAL
		
	WHERE	id_Cliente = @id_Cliente
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesBuscar]
	@IdTienda varchar(10),
	@IdEmpleado int,
	@IdTerminal nvarchar(50)
AS
BEGIN
	SELECT
		 IP.IdInventario
		,TI.Nombre AS TipoInventario
		,TVI.Nombre AS TipoVista
		,TVI.IdTipoVista
		,Empresa
		,IP.IdOrdenInventario
		,OI.Nombre AS OrdenInventario
		,T.Observaciones AS Tienda
		,IP.FechaCreacion
		,IP.FechaModificacion
		,SUM(IPD.Cantidad) AS Cantidad
	FROM 
		AVE_INVENTARIOSPENDIENTES IP LEFT JOIN 
		AVE_INVENTARIOSPENDIENTESDETALLE IPD ON IP.IdInventario = IPD.IdInventario INNER JOIN
		TIPOS_INVENTARIO TI ON IP.IdTipoInventario = TI.IdTipoInventario INNER JOIN
		TIPOS_VISTA_INVENTARIO TVI ON IP.IdTipoVista = TVI.IdTipoVista INNER JOIN
		TIENDAS T ON T.IdTienda = IP.IdTienda LEFT JOIN
		ORDENES_INVENTARIO OI ON OI.IdOrdenInventario = IP.IdOrdenInventario
	WHERE
		@IdTienda = IP.IdTienda AND
		@IdEmpleado = IP.IdEmpleado AND
		@IdTerminal = IP.IdTerminal

	GROUP BY 
		 IP.IdInventario
		,TI.Nombre 
		,TVI.Nombre
		,TVI.IdTipoVista
		,Empresa
		,IP.IdOrdenInventario
		,OI.Nombre
		,T.Observaciones
		,IP.FechaCreacion
		,IP.FechaModificacion
		
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosDetalleGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosDetalleGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 23/06/2010
-- Description:	Guarda una línea de detalle de un cargo dado
--			También actualiza la última modificación en Cargos
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosDetalleGuardar]
	@IdCargo INT,
	@IdArticulo INT,
	@Id_Cabecero_Detalle INT,
	@Cantidad INT,
	@Usuario NVARCHAR(256)
AS
BEGIN
	DECLARE @Fecha AS DATETIME
	SET @Fecha = GETDATE()
	IF EXISTS(SELECT IdCargo FROM AVE_CARGOSDETALLE
				WHERE IdCargo = @IdCargo AND IdArticulo = @IdArticulo AND 
					Id_Cabecero_Detalle = @Id_Cabecero_Detalle)
	BEGIN
		UPDATE AVE_CARGOSDETALLE
		   SET
			  Cantidad = @Cantidad
			  ,FechaModificacion = @Fecha
			  ,UsuarioModificacion = @Usuario
      
      		WHERE 
			IdCargo = @IdCargo AND
			IdArticulo = @IdArticulo AND
			Id_Cabecero_Detalle = @Id_Cabecero_Detalle
	END
	ELSE
	BEGIN
		INSERT INTO AVE_CARGOSDETALLE
           ([IdCargo]
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Cantidad]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
        VALUES 
           (@IdCargo
           ,@IdArticulo
           ,@Id_Cabecero_Detalle
           ,@Cantidad
           ,@Fecha
           ,@Usuario)
	END

	--En cualquier caso actualizamos los datos de última modificación de Cargos
	UPDATE 	AVE_CARGOS
	SET
		FechaModificacion = @Fecha,
		UsuarioModificacion = @Usuario
	WHERE
		IdCargo = @IdCargo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 24/06/2010
-- Description:	Los datos del cargo se van guardando al moverse por el carrusel, pero si el 
--	 usuario no lo finaliza sino que cancela, tendrá que borrarlo
-- =============================================
create PROCEDURE [dbo].[AVE_CargosBorrar]
	@IdCargo int
AS
BEGIN
	DELETE AVE_CARGOS WHERE IdCargo = @IdCargo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosCrear]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosCrear]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 8-06-2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosCrear]
	@IdCargo int,
	@IdTiendaOrigen varchar(10),
	@IdTiendaDestino varchar(10),
	@IdEmpleado int,
	@IdTerminal nvarchar(50),
	@Usuario nvarchar(256)
	
AS
BEGIN
	INSERT INTO AVE_CARGOS
           ([IdCargo]
           ,[IdTiendaOrigen]
           ,[IdTiendaDestino]
           ,[IdEmpleado]
           ,[IdTerminal]
           ,[Finalizado]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
     VALUES
           (@IdCargo
           ,@IdTiendaOrigen
           ,@IdTiendaDestino
           ,@IdEmpleado
           ,@IdTerminal
           ,0
           ,GETDATE()
           ,@Usuario)
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 23/06/2010
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosObtener]
	@IdCargo INT
AS
BEGIN
	SELECT	IdTiendaDestino
			,IdArticulo
			,Id_Cabecero_Detalle
			,Cantidad
	
	FROM	AVE_CARGOS C INNER JOIN
			AVE_CARGOSDETALLE CD ON C.IdCargo = CD.IdCargo
			
	WHERE	C.IdCargo = @IdCargo
			
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosFinalizar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosFinalizar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28/06/2010
-- Description:	Marca que el usuario ha finalizado el cargo y ya no lo va a tocar.
--	Se diferencia así de los datos guardados durante el proceso de creación o de 
--	cargos que se han quedado "zombis" porque se ha salido de la aplicación sin finalizarlos 
--	ni cancelarlos.
--	También se copia el cargo a las tablas del TPV (N_CARGOS y N_CARGOS_DETALLE)
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosFinalizar]
	@IdCargo int,
	@Usuario nvarchar(256)
AS
BEGIN

	BEGIN TRAN

	--INSERTAMOS EN N_CARGOS
	INSERT INTO N_CARGOS
		(Id_Cargo
		,Id_TiendaOrigen
		,Id_TiendaDestino
		,Id_Empleado
		,FechaCargo)
	SELECT 
		IdCargo
		,IdTiendaOrigen
		,IdTiendaDestino
		,IdEmpleado
		,GETDATE()
	FROM AVE_CARGOS	
	WHERE IdCargo = @IdCargo
	
    IF (@@ERROR <> 0) BEGIN
        ROLLBACK TRAN
        RETURN 1
    END

--INSERTAMOS N_CARGOS_DETALLES
	INSERT INTO N_CARGOS_DETALLES
		(Id_Cargo
		,IdArticulo
		,Id_cabecero_detalle
		,Cantidad)
	SELECT
		IdCargo
		,IdArticulo
		,Id_Cabecero_Detalle
		,Cantidad
	FROM AVE_CARGOSDETALLE
	WHERE IdCargo = @IdCargo
	
    IF (@@ERROR <> 0) BEGIN
        ROLLBACK TRAN
        RETURN 1
    END
	
	--ACTUALIZAMOS EL ESTADO
	UPDATE AVE_CARGOS
	SET Finalizado = 1
		,FechaModificacion = GETDATE()
		,UsuarioModificacion = @Usuario
	WHERE IdCargo = @IdCargo
		
    IF (@@ERROR <> 0)
        ROLLBACK TRAN
	ELSE
		COMMIT TRAN
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosDetalleBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosDetalleBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	Borra todos detalles de un cargo para el idCargo e idArticulo dados. 
--	Es independiente del Id_Cabecero_Detalle porque en la aplicación se borra
--	por artículo. 
-- =============================================
create PROCEDURE [dbo].[AVE_CargosDetalleBorrar] 
	@IdCargo int,	
	@IdArticulo int	
AS
BEGIN
	DELETE AVE_CARGOSDETALLE
	WHERE 
		IdCargo = @IdCargo AND
		IdArticulo = @IdArticulo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_TiendasObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_TiendasObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_TiendasObtener]
        @idTienda varchar(10)
AS
BEGIN
	SELECT	IdTienda, IdTienda + '' - '' + Observaciones AS Tienda
	FROM	TIENDAS where IdTienda not in (@IdTienda)
    order by idTienda
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ObtenerDatosLogin]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ObtenerDatosLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_ObtenerDatosLogin] 
	@Usuario uniqueidentifier
AS
BEGIN
	SELECT UE.IdEmpleado,UE.IdTienda, T.Observaciones AS NombreTienda,
	       (select top 1 isnull(Fecha_modificacion,Fecha_Creacion)  from AVE_PEDIDOS where idEstadoSolicitud=1
             order by isnull(Fecha_modificacion,Fecha_Creacion) desc) as FechaPedido 
    FROM	
           AVE_USUARIOSEMPLEADOS UE LEFT JOIN
			TIENDAS T ON T.IdTienda = UE.IdTienda
	WHERE UserId = @Usuario
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaFinalizar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaFinalizar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 26/06/2010
-- Description:	Marca que el usuario ha finalizado la entrada  y ya no la va a tocar.
--	Se diferencia así de los datos guardados durante el proceso de creación de la entrada o de 
--	entradas que se han quedado "zombis" porque se ha salido de la aplicación sin finalizarlas 
--	ni cancelarlas
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosEntradaFinalizar]
	@IdEntrada int,
	@Usuario nvarchar(256)
AS
BEGIN

	UPDATE AVE_CARGOSENTRADA
	
	SET Finalizado = 1
		,FechaModificacion = GETDATE()
		,UsuarioModificacion = @Usuario
	WHERE IdEntrada = @IdEntrada
				
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28/06/2010
-- Description:	Devuelve los datos de una entrada dado el IdEntrada. Incluye también los
--		datos originales del cargo al que corresponde
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosEntradaObtener]
	@IdEntrada int
AS
BEGIN

	-- Obtenemos la entrada solicitada, también el cargo que le corresponde a dicha entrada y
	-- la información de detalle de la entrada la fusionamos por artículo y cabecero con el cargo
	-- para obtener la cantidad pedida y recibida en una sola línea
	SELECT  CD.IdArticulo,
			CD.Id_Cabecero_Detalle,
			CD.Cantidad AS CantidadCargo,
			CED.Cantidad AS CantidadEntrada,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor AS Modelo,
			CBD.Nombre_Talla AS Talla,
			Id_TiendaOrigen AS IdTiendaOrigen,
			Id_TiendaDestino AS IdTiendaDestino
	
	FROM	N_CARGOS C INNER JOIN
			N_CARGOS_DETALLES CD 
				ON C.Id_Cargo = CD.Id_Cargo INNER JOIN
			AVE_CARGOSENTRADA CE 
				ON CE.IdEntrada = @IdEntrada AND 
				CE.IdCargo = C.Id_Cargo INNER JOIN
			AVE_CARGOSENTRADADETALLE CED
				ON CE.IdEntrada = CED.IdEntrada AND
				CED.IdArticulo = CD.IdArticulo AND
				CED.Id_Cabecero_Detalle	 = CD.Id_Cabecero_Detalle INNER JOIN
			ARTICULOS A 
				ON CD.IdArticulo = A.IdArticulo INNER JOIN
			CABECEROS_DETALLES CBD 
				ON CBD.Id_Cabecero_detalle = CD.Id_Cabecero_detalle
				
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaCrear]    Fecha de la secuencia de comandos: 06/28/2013 10:54:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaCrear]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	Creamos la entrada y sus detalles. Los detalles son los artículos y 
--	cabeceros del cargo, sin cantidades.
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CargosEntradaCrear]
   @IdCargo INT
   ,@FechaEntrada DATETIME
   ,@IdTienda VARCHAR(10)
   ,@IdEmpleado INT
   ,@IdTerminal NVARCHAR(50)
   ,@Usuario NVARCHAR(256)
   ,@IdEntrada INT OUTPUT
AS
BEGIN
	DECLARE @fecha AS DATETIME
	SET @fecha = GETDATE()
--INSERCIÓN DE LA ENTRADA
	INSERT INTO AVE_CARGOSENTRADA
           ([IdCargo]
           ,[FechaEntrada]
           ,[IdTienda]
           ,[IdEmpleado]
           ,[IdTerminal]
           ,[Finalizado]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
     VALUES
           (@IdCargo
           ,@FechaEntrada
           ,@IdTienda
           ,@IdEmpleado
           ,@IdTerminal
           ,0
           ,@fecha
           ,@Usuario)
			
	SET @IdEntrada = @@IDENTITY		

--BUSCAMOS EL DETALLE EN EL CARGO Y LO INSERTAMOS COMO DETALLE DE LA ENTRADA, SIN CANTIDAD	
	INSERT INTO AVE_CARGOSENTRADADETALLE 
			(IdEntrada
			,IdArticulo
			,Id_Cabecero_Detalle
			,FechaCreacion
			,UsuarioCreacion)
			
	SELECT	@IdEntrada
			,CD.IdArticulo
			,CD.Id_Cabecero_Detalle
			,@fecha
			,@Usuario
		
	FROM	N_CARGOS_DETALLES CD
		
	WHERE	CD.Id_Cargo = @IdCargo 
		
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaDetalleGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaDetalleGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28/06/2010
-- Description:	
-- =============================================
create PROCEDURE [dbo].[AVE_CargosEntradaDetalleGuardar]
	@IdEntrada int,
	@IdArticulo int,
	@Id_Cabecero_Detalle int,
	@Cantidad int,
	@Usuario nvarchar(256)
AS
BEGIN
	DECLARE @fecha as datetime
	SET @fecha = GETDATE()
	IF EXISTS(SELECT IdEntrada FROM AVE_CARGOSENTRADADETALLE
				WHERE IdEntrada = @IdEntrada AND IdArticulo = @IdArticulo AND 
				Id_Cabecero_Detalle = @Id_Cabecero_Detalle)
	BEGIN
		UPDATE AVE_CARGOSENTRADADETALLE
		   SET
			  Cantidad = @Cantidad
			  ,FechaModificacion = @fecha
			  ,UsuarioModificacion = @Usuario
      
      		WHERE 
			IdEntrada = @IdEntrada AND
			IdArticulo = @IdArticulo AND
			Id_Cabecero_Detalle = @Id_Cabecero_Detalle
	END
	ELSE
	BEGIN
		INSERT INTO AVE_CARGOSENTRADADETALLE
           (IdEntrada
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Cantidad]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
        VALUES 
           (@IdEntrada
           ,@IdArticulo
           ,@Id_Cabecero_Detalle
           ,@Cantidad
           ,@fecha
           ,@Usuario)
	END

	--En cualquier caso actualizamos los datos de última modificación de CargosEntrada
	UPDATE AVE_CARGOSENTRADA
	SET
		FechaModificacion = @fecha,
		UsuarioModificacion = @Usuario
	WHERE
		IdEntrada = @IdEntrada
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28/06/2010
-- Description:	Los datos de la entrada se van guardando al moverse por el carrusel, pero si el 
--	 usuario no finaliza la entrada sino que cancela, tendrá que borrar la entrada
-- =============================================
create PROCEDURE [dbo].[AVE_CargosEntradaBorrar]
	@IdEntrada int
AS
BEGIN
	DELETE AVE_CARGOSENTRADA WHERE IdEntrada = @IdEntrada
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ConfiguracionesGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ConfiguracionesGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 15/06/2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ConfiguracionesGuardar]
	@IdTienda varchar(10),
	@IdTipoVista int,
	@Usuario nvarchar(256)
AS
BEGIN
	UPDATE	AVE_CONFIGURACIONES
	
	SET		IdTipoVista = @IdTipoVista,
			FechaModificacion = GETDATE(),
			UsuarioModificacion = @Usuario
		
	WHERE	IdTienda = @IdTienda
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ConfiguracionesObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ConfiguracionesObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 15/06/2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ConfiguracionesObtener]
	@IdTienda varchar(10)
AS
BEGIN
	SELECT  IdTienda,
			IdTipoVista
			
	FROM	AVE_CONFIGURACIONES
	
	WHERE	IdTienda = @IdTienda
			
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ConvertirImporte]    Fecha de la secuencia de comandos: 06/28/2013 10:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ConvertirImporte]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ConvertirImporte]
	@Importe decimal(9,2)
AS
BEGIN
	SELECT	ROUND(@Importe*FactorConversion,2) as Importe,
			Simbolo AS Moneda
	FROM dbo.AVE_CONVERSIONMONEDA
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_EstadisticasGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_EstadisticasGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[AVE_EstadisticasGuardar]
	@Accion varchar(100),
	@Articulo varchar(100),
	@Talla varchar(20),
	@Usuario nvarchar(256),
	@IdTerminal varchar(50),
    @Seccion varchar(100)=null,
    @Marca varchar(100)=null,
    @Corte varchar(100)=null,
    @Material varchar(100)=null,
    @Color varchar(100)=null,
    @Comentario varchar(250)=null 
as
INSERT INTO [AVE_ESTADISTICAS]
           ([Accion]
           ,[Articulo]
           ,[Talla]
           ,Usuario
           ,IdTerminal
           ,[FechaCreacion]
           ,Seccion
           ,Marca
           ,Corte
           ,Material
           ,Color
           ,Comentario)
     VALUES
           (@Accion, 
           @Articulo, 
           @Talla, 
           @Usuario,
           @IdTerminal,
           getdate(),
           @Seccion,
           @Marca,
           @Corte,
           @Material,
           @Color,
           @Comentario)

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_EstadosPedidosObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_EstadosPedidosObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_EstadosPedidosObtener]
AS
BEGIN
	SELECT	IdEstado,
			[Resource]
	FROM	AVE_ESTADOSPEDIDOS
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_EstadosSolicitudesObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_EstadosSolicitudesObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_EstadosSolicitudesObtener]
AS
BEGIN
	SELECT	IdEstado,
			[Resource]
	FROM	AVE_ESTADOSSolicitudes
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_RegistrarParNegado]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_RegistrarParNegado]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_RegistrarParNegado]
	@IdTienda varchar(50),
	@idArticulo int,
	@Talla varchar(10),
	@Fecha datetime,
	@Descripcion varchar(500),
    @TipoNegado int out
	
AS
BEGIN

DECLARE @Id_Cabecero_Detalle int	

set @Id_Cabecero_Detalle=-1
set @tipoNegado=1


 select @id_Cabecero_Detalle=id_cabecero_Detalle from Articulos Ar
        inner join Cabeceros_Detalles on
        Ar.id_Cabecero=Cabeceros_detalles.id_Cabecero
        where ar.idarticulo=@idArticulo and nombre_talla=@Talla 



INSERT INTO [AVE_PRODUCTOS_NEGADOS]
           ([IdTienda]
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Fecha]
           ,[Descripcion])
     VALUES
           (@idtienda
           ,@IdArticulo
           ,@Id_Cabecero_Detalle 
           ,Getdate()
           ,@Descripcion)

   
  select id_tienda into #Tiendas from grupos_detalles where id_grupo in (
        select  id_grupo from grupos_detalles where id_tienda=@idTienda)
       and id_Tienda not in (@idTienda)  group by id_tienda  

	select  idarticulo from n_existencias ex inner join
				   #Tiendas T on ex.Idtienda=T.id_Tienda
	where idarticulo=@idarticulo and id_cabecero_Detalle=@Id_Cabecero_Detalle
	and cantidad>0 


    if @@rowCount=0 
    begin
        INSERT INTO [AVE_PRODUCTOS_NEGADOS]
           ([IdTienda]
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Fecha]
           ,[Descripcion])
           VALUES
           (@idtienda
           ,@IdArticulo
           ,@Id_Cabecero_Detalle 
           ,Getdate()
           ,''PAR NEGADO EN TODAS'')
 
           set @TipoNegado=2
    end



END




' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosObtener]
	@IdPedido int
AS
BEGIN
	SELECT  P.IdPedido,
			A.DescripcionFabricante AS Descripcion,
			P.Talla,
			P.Unidades,
			P.Precio	
			,P.id_cliente			
			,[Nombre_Cliente]
			,[Cif_Cliente]
			,[Apellidos_Cliente]
			,[Direccion_Cliente]
			,[CodPostal_Cliente]
			,[Poblacion_Cliente]
			,[Provincia_Cliente]
			,[Pais_Cliente] 
			,[Telefono_Cliente] 
			,[Movil_Cliente]
			,[email_Cliente]
			,[Observaciones_Cliente]
			,[Nombre_Destinatario] 
			,[Apellidos_Destinatario]
			,[Direccion_Destinatario]
			,[CodPostal_Destinatario]
			,[Poblacion_Destinatario]
			,[Provincia_Destinatario]
			,[Pais_Destinatario] 
			,[Telefono_Destinatario] 
			,[Movil_Destinatario]
			,[email_Destinatario]
			,[Observaciones_Destinatario]
			
	FROM	AVE_PEDIDOS P INNER JOIN
			ARTICULOS A ON A.IdArticulo = P.IdArticulo INNER JOIN
            EAN ON A.IdArticulo = EAN.IdArticulo 
	
	WHERE	P.IdPedido = @IdPedido
			
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	Borra todo un inventario pendiente. Los detalles se borran en cascada por 
--	la relación entre tablas
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesBorrar] 
	@IdInventario int	
AS
BEGIN
	DELETE AVE_INVENTARIOSPENDIENTES
	WHERE IdInventario = @IdInventario
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesCrear]    Fecha de la secuencia de comandos: 06/28/2013 10:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesCrear]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	Guarda un inventario nuevo
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesCrear]
	@IdTipoInventario int,
	@IdTipoVista int,
	@Empresa nvarchar(50),
	@IdTienda varchar(10),
	@IdEmpleado int,
	@IdTerminal nvarchar(50),
	@IdOrdenInventario int = NULL,
	@Usuario nvarchar(256)
AS
BEGIN
	INSERT INTO dbo.AVE_INVENTARIOSPENDIENTES(
		IdTipoInventario,
		IdTipoVista,
		Empresa,
		IdTienda,
		IdEmpleado,
		IdTerminal,
		IdOrdenInventario,
		FechaCreacion,
		UsuarioCreacion)
	VALUES (
		@IdTipoInventario,
		@IdTipoVista,
		@Empresa,
		@IdTienda,
		@IdEmpleado,
		@IdTerminal,
		@IdOrdenInventario,
		getdate(),
		@Usuario
	)
	RETURN @@IDENTITY
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	Obtiene todos los datos (detalles incluidos) de un inventario dado
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesObtener]
	@IdInventario int
AS
BEGIN
	SELECT
		Bloque,
		IdArticulo,
		Id_Cabecero_Detalle,
		Cantidad,
		Bloque
	FROM
		AVE_INVENTARIOSPENDIENTES IP INNER JOIN 
		AVE_INVENTARIOSPENDIENTESDETALLE IPD ON IP.IdInventario = IPD.IdInventario
		
	WHERE 
		IP.IdInventario = @IdInventario 
		
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesDetalleGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesDetalleGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-5-2010
-- Description:	Guarda una línea de detalle de un inventario pendiente dado
--			También actualiza la última modificación en InventariosPendiente
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesDetalleGuardar]
	@IdInventario int,
	@Bloque nvarchar(50),
	@IdArticulo int,
	@Id_Cabecero_Detalle int,
	@Cantidad int,
	@Usuario nvarchar(256)
AS
BEGIN
	DECLARE @fecha as datetime
	SET @fecha = GETDATE()
	IF EXISTS(SELECT IdInventario FROM AVE_INVENTARIOSPENDIENTESDETALLE 
				WHERE IdInventario = @IdInventario AND Bloque = @Bloque AND 
					  IdArticulo = @IdArticulo AND Id_Cabecero_Detalle = @Id_Cabecero_Detalle)
	BEGIN
		UPDATE AVE_INVENTARIOSPENDIENTESDETALLE
		   SET
			  Cantidad = @Cantidad
			  ,FechaModificacion = @fecha
			  ,UsuarioModificacion = @Usuario
      
      		WHERE 
			IdInventario = @IdInventario AND
			Bloque = @Bloque AND
			IdArticulo = @IdArticulo AND
			Id_Cabecero_Detalle = @Id_Cabecero_Detalle
	END
	ELSE
	BEGIN
		INSERT INTO AVE_INVENTARIOSPENDIENTESDETALLE
           ([IdInventario]
           ,[Bloque]
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Cantidad]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
        VALUES 
           (@IdInventario
           ,@Bloque
           ,@IdArticulo
           ,@Id_Cabecero_Detalle
           ,@Cantidad
           ,@fecha
           ,@Usuario)
	END

	--En cualquier caso actualizamos los datos de última modificación de InventariosPendientes
	UPDATE 	AVE_INVENTARIOSPENDIENTES
	SET
		FechaModificacion = @fecha,
		UsuarioModificacion = @Usuario
	WHERE
		IdInventario = @IdInventario
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Material]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Material]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_Material] 
	
AS
BEGIN
     Select 0 as IdMaterial,'''' as Material
     union 
	 select  IdMaterial,Material 
     from Materiales
     where idMaterial>1
     order by idMaterial
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesDetalleBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesDetalleBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-05-2010
-- Description:	Borra todos detalles de un inventario pendiente para el idInventario, bloque e 
--	idArticulo dados. Es independiente del Id_Cabecero_Detalle porque en la aplicación se borra
-- por artículo. 
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesDetalleBorrar] 
	@IdInventario int,	
	@Bloque nvarchar(50),
	@IdArticulo int	
AS
BEGIN
	DELETE AVE_INVENTARIOSPENDIENTESDETALLE
	WHERE 
		IdInventario = @IdInventario AND
		Bloque = @Bloque AND
		IdArticulo = @IdArticulo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesExisteArticulo]    Fecha de la secuencia de comandos: 06/28/2013 10:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesExisteArticulo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 16/06/2010
-- Description:	Indica si un artículo existe ya en un inventario pendiente
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesExisteArticulo]
	@IdInventario int,
	@Bloque nvarchar(50),
	@IdArticulo int
AS
BEGIN
	SELECT CAST (COUNT(*) AS Bit)
	FROM	AVE_INVENTARIOSPENDIENTESDETALLE
	WHERE   IdInventario = @IdInventario AND
			Bloque = @Bloque AND
			IdArticulo = @IdArticulo
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_UltimoPedidoPendiente]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_UltimoPedidoPendiente]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_UltimoPedidoPendiente]
	
AS
BEGIN
	select top 1 isnull(Fecha_modificacion,Fecha_Creacion) as Fecha_Creacion 
    from AVE_PEDIDOS where idEstadoSolicitud=1
    order by isnull(Fecha_modificacion,Fecha_Creacion) desc
			
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidoActualizarCliente]    Fecha de la secuencia de comandos: 06/28/2013 10:54:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidoActualizarCliente]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidoActualizarCliente]
	@IdPedido int = 0,
	@id_Cliente int = NULL,
	@Cif_Cliente nvarchar(60),
	@Nombre_Cliente nvarchar(60),
	@Apellidos_Cliente nvarchar(100),
	@Direccion_Cliente nvarchar(150),
	@CodPostal_Cliente nvarchar(10),
	@Poblacion_Cliente nvarchar(100),
	@Provincia_Cliente nvarchar(100),
	@Pais_Cliente nvarchar(100),
	@Telefono_Cliente nvarchar(50),
	@Movil_Cliente nvarchar(50),
	@email_Cliente nvarchar(50),
	@Observaciones_Cliente nvarchar(1000),
	@Nombre_Destinatario nvarchar(60),
	@Apellidos_Destinatario nvarchar(100),
	@Direccion_Destinatario nvarchar(150),
	@CodPostal_Destinatario nvarchar(10),
	@Poblacion_Destinatario nvarchar(100),
	@Provincia_Destinatario nvarchar(100),
	@Pais_Destinatario nvarchar(100),
	@Telefono_Destinatario nvarchar(50),
	@Movil_Destinatario nvarchar(50),
	@email_Destinatario nvarchar(50),
	@Observaciones_Destinatario nvarchar(1000),
	@Usuario nvarchar(256)
AS
BEGIN
	UPDATE [AVE_PEDIDOS]
	
	SET		id_Cliente = @id_Cliente
		   ,[Cif_Cliente] = @Cif_Cliente
		   ,[Nombre_Cliente] = @Nombre_Cliente
           ,[Apellidos_Cliente] = @Apellidos_Cliente
           ,[Direccion_Cliente] = @Direccion_Cliente
           ,[CodPostal_Cliente] = @CodPostal_Cliente
           ,[Poblacion_Cliente] = @Poblacion_Cliente
           ,[Provincia_Cliente] = @Provincia_Cliente
           ,[Pais_Cliente] = @Pais_Cliente
           ,[Telefono_Cliente] = @Telefono_Cliente
           ,[Movil_Cliente] = @Movil_Cliente
           ,[email_Cliente] = @email_Cliente
           ,[Nombre_Destinatario] = @Nombre_Destinatario
           ,[Apellidos_Destinatario] = @Apellidos_Destinatario
           ,[Direccion_Destinatario] = @Direccion_Destinatario
           ,[CodPostal_Destinatario] = @CodPostal_Destinatario
           ,[Poblacion_Destinatario] = @Poblacion_Destinatario
           ,[Provincia_Destinatario] = @Provincia_Destinatario
           ,[Pais_Destinatario] = @Pais_Destinatario
           ,[Telefono_Destinatario] = @Telefono_Destinatario
           ,[Movil_Destinatario] = @Movil_Destinatario
           ,[email_Destinatario] = @email_Destinatario
           ,[Observaciones_Cliente] = @Observaciones_Cliente
           ,[Observaciones_Destinatario] = @Observaciones_Destinatario
           ,[UsuarioModificacion] = @Usuario
           ,[Fecha_Modificacion] = GETDATE()
		   ,IdEstadoPedido = 2
	WHERE	IdPedido = @IdPedido

END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosCambiarEstadoSolicitud]    Fecha de la secuencia de comandos: 06/28/2013 10:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosCambiarEstadoSolicitud]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosCambiarEstadoSolicitud]
	@IdPedido int,
	@IdEstado int
AS
BEGIN
	UPDATE dbo.AVE_PEDIDOS
	SET IdEstadoSolicitud = @IdEstado
	WHERE IdPedido = @IdPedido
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosCrear]    Fecha de la secuencia de comandos: 06/28/2013 10:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosCrear]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosCrear]
	@IdArticulo int,
	@Talla varchar(6),
	@Unidades smallint,
	@Precio money,
	@IdTienda varchar(10),
	@IdEmpleado int,
	@Usuario nvarchar(256),
	@Stock int
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
		UsuarioCreacion
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
		@Usuario
		)
	RETURN @@IDENTITY
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaFinalizar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaFinalizar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 26/06/2010
-- Description:	Marca que el usuario ha finalizado la entrada del pedido y ya no la va a tocar.
--	Se diferencia así de los datos guardados durante el proceso de creación de la entrada o de 
--	entradas que se han quedado "zombis" porque se ha salido de la aplicación sin finalizarlas 
--	ni cancelarlas
-- =============================================
CREATE PROCEDURE [dbo].[AVE_PedidosEntradaFinalizar]
	@IdEntrada int,
	@Usuario nvarchar(256)
AS
BEGIN

	UPDATE AVE_PEDIDOSENTRADA
	
	SET Finalizado = 1
		,FechaModificacion = GETDATE()
		,UsuarioModificacion = @Usuario
	WHERE IdEntrada = @IdEntrada
				
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaBorrar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaBorrar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	Los datos de la entrada se van guardando al moverse por el carrusel, pero si el 
--	 usuario no finaliza la entrada sino que cancela, tendrá que borrar la entrada
-- =============================================
create PROCEDURE [dbo].[AVE_PedidosEntradaBorrar]
	@IdEntrada int
AS
BEGIN
	DELETE AVE_PEDIDOSENTRADA WHERE IdEntrada = @IdEntrada
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_PedidosEntradaDetalleGuardar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_PedidosEntradaDetalleGuardar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 22/06/2010
-- Description:	
-- =============================================
create PROCEDURE [dbo].[AVE_PedidosEntradaDetalleGuardar]
	@IdEntrada int,
	@IdArticulo int,
	@Id_Cabecero_Detalle int,
	@Cantidad int,
	@Usuario nvarchar(256)
AS
BEGIN
	DECLARE @fecha as datetime
	SET @fecha = GETDATE()
	IF EXISTS(SELECT IdEntrada FROM AVE_PEDIDOSENTRADADETALLE
				WHERE IdEntrada = @IdEntrada AND IdArticulo = @IdArticulo AND 
				Id_Cabecero_Detalle = @Id_Cabecero_Detalle)
	BEGIN
		UPDATE AVE_PEDIDOSENTRADADETALLE
		   SET
			  Cantidad = @Cantidad
			  ,FechaModificacion = @fecha
			  ,UsuarioModificacion = @Usuario
      
      		WHERE 
			IdEntrada = @IdEntrada AND
			IdArticulo = @IdArticulo AND
			Id_Cabecero_Detalle = @Id_Cabecero_Detalle
	END
	ELSE
	BEGIN
		INSERT INTO AVE_PEDIDOSENTRADADETALLE
           (IdEntrada
           ,[IdArticulo]
           ,[Id_Cabecero_Detalle]
           ,[Cantidad]
           ,[FechaCreacion]
           ,[UsuarioCreacion])
        VALUES 
           (@IdEntrada
           ,@IdArticulo
           ,@Id_Cabecero_Detalle
           ,@Cantidad
           ,@fecha
           ,@Usuario)
	END

	--En cualquier caso actualizamos los datos de última modificación de PedidosEntrada
	UPDATE 	AVE_PEDIDOSENTRADA
	SET
		FechaModificacion = @fecha,
		UsuarioModificacion = @Usuario
	WHERE
		IdEntrada = @IdEntrada
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CargosEntradaBuscar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CargosEntradaBuscar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 28/06/2010
-- Description:	Busca un cargo del TPV para la funcionalidad de Entrada de cargos.
--	El cargo debe serlo con destino la tienda del usuario. 
-- =============================================
create PROCEDURE [dbo].[AVE_CargosEntradaBuscar]
	@IdCargo int,
	@IdTienda varchar(10)
AS
BEGIN
	SELECT	C.Id_Cargo,
			C.Id_TiendaOrigen,
			C.Id_TiendaDestino,
			CD.IdArticulo,
			CD.Id_cabecero_detalle,
			CD.Cantidad,
			A.CodigoAlfa AS Referencia,
			A.ModeloProveedor AS Modelo,
			CBD.Nombre_Talla AS Talla
	
	FROM	N_CARGOS C INNER JOIN
			N_CARGOS_DETALLES CD ON C.Id_Cargo = CD.Id_Cargo INNER JOIN
			ARTICULOS A ON CD.IdArticulo = A.IdArticulo INNER JOIN
			CABECEROS_DETALLES CBD ON CBD.Id_Cabecero_detalle = CD.Id_Cabecero_detalle
		
	WHERE	C.Id_Cargo = @IdCargo AND
			C.Id_TiendaDestino = @IdTienda
	
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_MARCASPROVEEDOR]    Fecha de la secuencia de comandos: 06/28/2013 10:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_MARCASPROVEEDOR]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_MARCASPROVEEDOR] 
	
AS
BEGIN
    select * from ( 
    select 0 as idProveedor,'''' as NombreComercial
    union
	select idproveedor,NombreComercial
        from proveedores 
        where idproveedor  > 3
    union
    Select 99999  idProveedor,''OTRA'' NombreComercial) PROV 
    order by idproveedor
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Secciones]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Secciones]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_Secciones] 
	
AS
BEGIN
     select 0 as idSeccion , '''' as Seccion 
     union
	 select idSeccion,Nombre as Seccion from Secciones
     where idSeccion>3
      order by idseccion
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_TallajeArticulo]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_TallajeArticulo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_TallajeArticulo]
	@IdArticulo int
	
AS
BEGIN
   
   select  id_Cabecero_Detalle,Nombre_Talla as Talla 
   from cabeceros_Detalles Cb inner join Articulos ar on
   Cb.id_Cabecero=Ar.id_cabecero
   where idarticulo=@IdArticulo and visualizacion_tpv=1 
   order by  orden_tpv,Nombre_Talla


END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_CabecerosDetallesObtenerPorArticulo]    Fecha de la secuencia de comandos: 06/28/2013 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_CabecerosDetallesObtenerPorArticulo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 32/06/2010
-- Description:	Obtiene las tallas de un producto dado
-- =============================================
CREATE PROCEDURE [dbo].[AVE_CabecerosDetallesObtenerPorArticulo] 
	@IdArticulo INT
AS
BEGIN

	SELECT	Id_Cabecero_Detalle,
			Nombre_Talla AS Talla
			
	FROM	ARTICULOS A INNER JOIN
            CABECEROS_DETALLES CD ON A.Id_Cabecero = CD.Id_Cabecero
    
    WHERE	IdArticulo = @IdArticulo

END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_TallasZG]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_TallasZG]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_TallasZG] 
	
AS
BEGIN
  Select '''' as Nombre_Talla
  union 	 
  (select  Nombre_Talla from Cabeceros_detalles where visualizacion_tpv=1
  group by Nombre_talla)
  
END
' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_StockInventarioObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_StockInventarioObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 31-5-2010
-- =============================================
CREATE PROCEDURE [dbo].[AVE_StockInventarioObtener]
	@IdArticulo int,
	@IdTienda varchar(10)
AS
BEGIN

SELECT		A.IdArticulo, 
			A.CodigoAlfa AS Referencia,
			CD.Id_Cabecero_Detalle,
			CD.Nombre_Talla AS Talla,
			N_EXISTENCIAS.Cantidad 
FROM        ARTICULOS A INNER JOIN
            CABECEROS_DETALLES CD ON A.Id_Cabecero = CD.Id_Cabecero LEFT JOIN
            N_EXISTENCIAS ON A.IdArticulo = N_EXISTENCIAS.IdArticulo AND 
						CD.Id_Cabecero_Detalle = N_EXISTENCIAS.Id_Cabecero_Detalle AND 
						N_EXISTENCIAS.IdTienda = @IdTienda 

WHERE     A.IdArticulo = @IdArticulo

END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Tipos_InventarioObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Tipos_InventarioObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 5-7-2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_Tipos_InventarioObtener]
AS
BEGIN
	SELECT	IdTipoInventario,
			Nombre
	FROM	dbo.TIPOS_INVENTARIO
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_Tipos_Vista_InventarioObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_Tipos_Vista_InventarioObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Create date: 5-7-2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AVE_Tipos_Vista_InventarioObtener]
AS
BEGIN
	SELECT	IdTipoVista,
			Nombre
	FROM	dbo.TIPOS_VISTA_INVENTARIO
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ProductoTieneCS]    Fecha de la secuencia de comandos: 06/28/2013 10:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ProductoTieneCS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AVE_ProductoTieneCS]
	@IdArticulo int
AS
BEGIN

	select [C],[S] FROM
	(
	select count(Articulo1 ) AS Cuenta, TipoRela 
	FROM         dbo.ARTICULOS INNER JOIN
				 dbo.ArticulosCS ON 
						CAST(dbo.ARTICULOS.IdTemporada AS VARCHAR(5)) + ''ý'' + 
						CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(5)) + ''ý'' + 
						CAST(dbo.ARTICULOS.ModeloProveedor AS VARCHAR(50))	= 
						dbo.ArticulosCS.Articulo1 
						
						where ARTICULOS.idarticulo=@IdArticulo 
	group by TipoRela
	) AS Source
	PIVOT
	(
	SUM(Cuenta)
	FOR TipoRela IN ([C],[S])
	) AS PivotTable
END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_ArticuloFotoObtener]    Fecha de la secuencia de comandos: 06/28/2013 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_ArticuloFotoObtener]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		OLG
-- Description:	Para la temporada, si la longitud es 1 se añade un cero al principio en el resto de casos se deja con la longitud normal
-- =============================================
CREATE PROCEDURE [dbo].[AVE_ArticuloFotoObtener] 
	@IdArticulo int
AS
BEGIN
	SELECT	CASE LEN(dbo.ARTICULOS.idTemporada)
				WHEN 1 THEN ''0'' + CAST(dbo.ARTICULOS.idTemporada AS VARCHAR(1))
				ELSE CAST(dbo.ARTICULOS.idTemporada AS VARCHAR(5))
			END AS idTemporada,
			RIGHT(''000'' + CAST(dbo.ARTICULOS.IdProveedor AS VARCHAR(3)),3) AS IdProveedor,
			dbo.ARTICULOS.ModeloProveedor	
	FROM    dbo.ARTICULOS 
	WHERE	dbo.ARTICULOS.IdArticulo = @IdArticulo

END

' 
END
GO
/****** Objeto:  StoredProcedure [dbo].[AVE_InventariosPendientesFinalizar]    Fecha de la secuencia de comandos: 06/28/2013 10:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVE_InventariosPendientesFinalizar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Transfiere un inventario de pendiente a histórico. Para ello mueve la información
--		general y todos sus detalles.
--		La operación es transaccional
--	En el caso de ser un inventario de una OI, se insertará la información adicional de la tienda (es decir, que la OI ha sido 
--	realizada para la tienda y sus observaciones
-- =============================================
CREATE PROCEDURE [dbo].[AVE_InventariosPendientesFinalizar] 
	@IdInventario int,
	@Usuario nvarchar(256),
	@IdOrdenInventario int = NULL,
	@IdTienda varchar(10) = NULL,
	@ObservacionesTienda varchar(255) = NULL
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		
		DECLARE @Fecha DATETIME
		SET @Fecha = GETDATE()
		
		--Insertamos el maestro
		INSERT INTO AVE_INVENTARIOSHISTORICOS
		   ([IdInventario]
		   ,[IdTipoInventario]
		   ,[IdTipoVista]
		   ,[Empresa]
		   ,[IdTienda]
		   ,[IdEmpleado]
		   ,[IdTerminal]
		   ,[IdOrdenInventario]
		   ,[FechaCreacion]
		   ,[UsuarioCreacion]
		   ,[FechaModificacion]
		   ,[UsuarioModificacion])
			SELECT
				[IdInventario]
			   ,[IdTipoInventario]
			   ,[IdTipoVista]
			   ,[Empresa]
			   ,[IdTienda]
			   ,[IdEmpleado]
			   ,[IdTerminal]
			   ,[IdOrdenInventario]
			   ,[FechaCreacion]
			   ,[UsuarioCreacion]
			   ,@Fecha
			   ,@Usuario
			FROM AVE_INVENTARIOSPENDIENTES
			WHERE IdInventario = @IdInventario
			
		--Insertamos los detalles
		INSERT INTO [AVE_INVENTARIOSHISTORICOSDETALLE]
			([IdInventario]
			,[Bloque]
			,[IdArticulo]
			,[Id_Cabecero_Detalle]
			,[Cantidad]
			,[FechaCreacion]
			,[UsuarioCreacion]
			,[FechaModificacion]
			,[UsuarioModificacion])
			SELECT
				[IdInventario]
			   ,[Bloque]
			   ,[IdArticulo]
			   ,[Id_Cabecero_Detalle]
			   ,[Cantidad]
			   ,[FechaCreacion]
			   ,[UsuarioCreacion]
			   ,@Fecha
			   ,@Usuario
			FROM AVE_INVENTARIOSPENDIENTESDETALLE
			WHERE IdInventario = @IdInventario
			
		--Borramos los datos de pendientes. Los detalles se borran en cascada
		EXEC AVE_InventariosPendientesBorrar @IdInventario

		-- Actualizamos la OI si existe
		UPDATE	TIENDAS_INVENTARIO
		SET		OIRealizada = 1,
				Observaciones = @ObservacionesTienda
		WHERE	IdOrdenInventario = @IdOrdenInventario AND
				IdTienda = @IdTienda
		
		COMMIT TRAN
		
    END TRY

    BEGIN CATCH
		DECLARE @ErrMsg INT, @ErrSev INT, @ErrSt INT

		SELECT @ErrMsg = ERROR_MESSAGE()
		SELECT @ErrSev = ERROR_SEVERITY()
		SELECT @ErrSt = ERROR_STATE()
        
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN 

		RAISERROR(@ErrMsg,@ErrSev,@ErrSt)

    END CATCH
END

' 
END
GO
INSERT INTO [AVE_CONFIGURACIONES]
           ([IdTienda]
           ,[IdTipoVista]
           ,[FechaModificacion]
           ,[UsuarioModificacion])
 select  Tienda,1,GetDate(),
           'Administrador'
from configuraciones_tpv

GO
INSERT INTO [AVE_ESTADOSPEDIDOS]
           ([Estado]
           ,[Resource])
     VALUES
           ('Sin Asignar',
           'PedidoEstadoSinAsignar')

GO
INSERT INTO [AVE_ESTADOSPEDIDOS]
           ([Estado]
           ,[Resource])
     VALUES
           ('Asignado',
           'PedidoEstadoAsignado')

GO
INSERT INTO [AVE_ESTADOSSOLICITUDES]
           ([Estado]
           ,[Resource])
     VALUES
           ('Pendiente'
            ,'SolicitudEstadoPendiente')
GO
INSERT INTO [AVE_ESTADOSSOLICITUDES]
           ([Estado]
           ,[Resource])
     VALUES
           ('Asignado'
            ,'SolicitudEstadoAsignado')
GO
INSERT INTO [AVE_ESTADOSSOLICITUDES]
           ([Estado]
           ,[Resource])
     VALUES
           ('Aparcado'
            ,'SolicitudEstadoAparcado')
GO
INSERT INTO [AVE_ESTADOSSOLICITUDES]
           ([Estado]
           ,[Resource])
     VALUES
           ('Completado'
            ,'SolicitudEstadoCompletado')
GO
INSERT INTO [AVE_USUARIOSEMPLEADOS]
           ([UserId]
           ,[IdEmpleado]
           ,[IdTienda])
Select '62984540-17f0-4bca-a99d-0c3d4a60fcd7',
(select min(idempleado) as idEmpleado from empleados) as Idempleado,   
(select  Tienda from configuraciones_tpv) as tienda
