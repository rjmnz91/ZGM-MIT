go
create PROCEDURE [dbo].[GetComprobarLogin] 
@Usuario VARCHAR(20)

AS

select  E.IdEmpleado,ISNULL(Clave,'') as ControlP,
        ISNULL(SS,'') as Usuarios from 
        EMPLEADOS E left join 
        EANEMPLEADOS EA on
        E.IdEmpleado=EA.IdEmpleado 
        where E.IdEmpleado=@Usuario
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
go