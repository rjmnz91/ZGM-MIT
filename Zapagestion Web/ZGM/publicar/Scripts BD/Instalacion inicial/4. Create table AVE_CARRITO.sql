
/****** Object:  Table [dbo].[AVE_PEDIDOS]    Script Date: 09/16/2013 09:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AVE_CARRITO](
	[IdCarrito] [int] IDENTITY(1,1) NOT NULL,
	[IdPedido] [int] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioCreacion] [nvarchar](256) NULL,
	[UsuarioModificacion] [nvarchar](256) NULL,
	[Fecha_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_AVE_CARRITO] PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


