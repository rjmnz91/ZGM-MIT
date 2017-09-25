SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbPR_clientesBeneficiariosTipo](
	[IdCliBeneficiarioTipo] [int] IDENTITY(1,1) NOT NULL,
	[fkPromocion] [int] NOT NULL,
	[fkId_Tipo] [int] NOT NULL,
	[NumBono] [int] NULL,
 CONSTRAINT [PK_tbPR_clientesBeneficiariosTipo] PRIMARY KEY CLUSTERED 
(
	[IdCliBeneficiarioTipo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

