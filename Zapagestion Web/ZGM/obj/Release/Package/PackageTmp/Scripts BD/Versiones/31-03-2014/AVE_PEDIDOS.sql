/*
   lunes, 31 de marzo de 201412:42:02
   Usuario: sa
   Servidor: DESARROLLO98\TPVGESTION2008R2
   Base de datos: BDMARIO
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AVE_PEDIDOS ADD
	IdTerminal int NULL
GO
COMMIT
