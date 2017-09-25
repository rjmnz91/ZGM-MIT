USE [JUEVES_PIAGUI]
GO

/****** Object:  UserDefinedFunction [dbo].[AVE_GenerarIdTicketDevo]    Script Date: 01/13/2015 12:48:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ACL
-- Create date: 13-01-2015
-- Description:	Crea un TICKET NUEVO
-- =============================================
CREATE FUNCTION [dbo].[AVE_GenerarIdTicketDevo]
(
	@IdTienda varchar(10),
	@IdTerminal VARCHAR(10)
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
          set @Idticket='000001/' + @idTienda
      end   
    else
      begin 

         set @numTicket= substring(@idTicketAnt,1,charindex('/',@idTicketAnt)-1)    
         set @numTicket= @numTicket+1
         set @Idticket=cast(@numTicket as varchar)  
         
         if len(@Idticket)<6
           begin          
               set @Idticket=replicate('0',6-len(@Idticket)) +  @Idticket + '/'+@IdTerminal + '/'+ @idTienda 
           end  
         else
           begin 
              set @Idticket=@Idticket +'/'+@IdTerminal+ '/' +  @idTienda 
           end   
      end 
	
      RETURN @Idticket

END

GO

