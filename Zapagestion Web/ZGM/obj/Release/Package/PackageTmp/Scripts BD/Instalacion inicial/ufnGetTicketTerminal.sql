ALTER FUNCTION [dbo].[ufnGetTicketTerminal](@myTerminal int)
RETURNS nvarchar (10)
AS 
BEGIN
      declare @IdTicket nvarchar(10)
      declare @IdTerminal     nvarchar(10)
      Declare @IDs TABLE
      (
            ID    int
      )
      Declare @sVd      nvarchar(15)
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
