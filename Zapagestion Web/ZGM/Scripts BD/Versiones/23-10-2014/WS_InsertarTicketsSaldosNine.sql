USE [MC_SQL]
GO

/****** Object:  StoredProcedure [dbo].[WS_InsertarTicketsSaldosNine]    Script Date: 10/23/2014 11:57:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 ALTER PROCEDURE [dbo].[WS_InsertarTicketsSaldosNine] 
  @Id_Ticket varchar(20), 
  @Id_Tienda varchar(10), 
  @Id_Terminal varchar(10), 
  @Fecha datetime,  
  @Id_Cliente int, 
  @Cliente varchar(100), 
  @NumTarjeta varchar(30), 
  @SaldoPuntosAnt float,  
  @PuntosRedimidos float,
  @PuntosObtenidos float, 
  @SaldoPuntosAct float,
  @dblSaldoPares9 float,  
  @ParesAcumuladosAnt float, 
  @ParesRedimidos float, 
  @ParesAcumuladosAct float, 
  @dblSaldoBolsa5 float,  
  @BolsasAcumuladasAnt float, 
  @BolsasRedimidas float, 
  @BolsasAcumuladasAct float, 
  @Aniversario varchar(10),  
  @Cumpleaños varchar(10), 
  @NivelActual varchar(30), 
  @CandidataShoeLover varchar(2),  
  @NumConfirmaPuntos9 int,  
  @NumConfirmaPar9 int, 
  @NumConfirmaBolsa5 int,  
  @NumTarjetaNew varchar(30), 
  @BenC9 varchar(1),  
  @Fecha_Modificacion datetime,
  @CandidataBasico varchar(2),
  @CandidataFirstShoeLover varchar(2)  
  
  
  AS  
  
  select Id_Ticket from N_TICKETS_SALDOS_NINE  
  where Id_Ticket=@Id_Ticket and  Id_Tienda=@Id_Tienda  and   Fecha =@Fecha   
  if @@ROWCOUNT =0  
  begin  
  INSERT INTO  [N_TICKETS_SALDOS_NINE] ([Id_Ticket],[Id_Tienda],[Id_Terminal],[Fecha]  ,[Id_Cliente],[Cliente],[NumTarjeta],[SaldoPuntosAnt],[PuntosRedimidos],[PuntosObtenidos],[SaldoPuntosAct]  ,[dblSaldoPares9] ,[ParesAcumuladosAnt],[ParesRedimidos],[ParesAcumuladosAct],[dblSaldoBolsa5],[BolsasAcumuladasAnt]  ,[BolsasRedimidas],[BolsasAcumuladasAct],[Aniversario],[Cumpleaños],[NivelActual],[CandidataShoeLover],[NumConfirmaPuntos9]  ,[NumConfirmaPar9],[NumConfirmaBolsa5],[NumTarjetaNew],[BenC9],[Fecha_Modificacion],[CandidataBasico],[CandidataFirstShoeLover])  
  Values  (@Id_Ticket,@Id_Tienda  ,@Id_Terminal,@Fecha,@Id_Cliente,@Cliente,@NumTarjeta,@SaldoPuntosAnt,@PuntosRedimidos ,@PuntosObtenidos,@SaldoPuntosAct  ,@dblSaldoPares9,@ParesAcumuladosAnt,@ParesRedimidos,@ParesAcumuladosAct,@dblSaldoBolsa5,@BolsasAcumuladasAnt,@BolsasRedimidas  ,@BolsasAcumuladasAct,@Aniversario,@Cumpleaños,@NivelActual,@CandidataShoeLover,@NumConfirmaPuntos9,@NumConfirmaPar9  ,@NumConfirmaBolsa5,@NumTarjetaNew,@BenC9,@Fecha_Modificacion,@CandidataBasico,@CandidataFirstShoeLover) 
  End 
GO

