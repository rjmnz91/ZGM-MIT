SET ANSI_PADDING OFF
GO
DELETE AVE_ESTADOSSOLICITUDES WHERE IdEstado in (5,6,7,8,9)
GO
SET IDENTITY_INSERT AVE_ESTADOSSOLICITUDES ON
GO
INSERT [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado], [Estado], [Resource]) VALUES (5, N'Regresado', N'SolicitudEstadoRegresado')
INSERT [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado], [Estado], [Resource]) VALUES (6, N'Vendido', N'SolicitudEstadoVendido')
INSERT [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado], [Estado], [Resource]) VALUES (7, N'Solicitado', N'SolicitudEstadoSolicitado')
INSERT [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado], [Estado], [Resource]) VALUES (8, N'Confirmado', N'SolicitudEstadoConfirmado')
INSERT [dbo].[AVE_ESTADOSSOLICITUDES] ([IdEstado], [Estado], [Resource]) VALUES (9, N'Cancelado', N'SolicitudEstadoCancelado')
GO
SET IDENTITY_INSERT AVE_ESTADOSSOLICITUDES OFF