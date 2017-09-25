Imports System.IO
Imports AVEGestorPromociones.GestorPromociones

Public Class clsModPromociones
    Public lngSellosRumbo As Long
    Public blnArtBeneficiario As Boolean
    Public blnArtDonante As Boolean
    Public blnRespuestaRestricciones As Boolean
    Public blnRestriccionesApertura As Boolean
    Public strCodigoBono As String
    Public blnPrecioFinal As Boolean
    Public blnPromoCascada As Boolean
    Public strDescPromo As String
    Public strTallasValidas As String
    Public gblstrTipoPromocion As String
    Public gdteFechaSesion As String
    Public gstrTiendaSesion As String
    Public gblLngTipoDescuento As Long
    Public gblLngTipoPromocion As Long
    Public gblLngIdPromocion As Long
    Public gblfkRedondeo As Long
    Public gblfkAplicacionRedondeo As Long
    Public gblLngIdPromocionTodos As Long
    Public gdblTotalVenta As Double
    Public strDIRFTP As String

    Public Function ValidoArticuloPromo(ByVal txtCI As Long, ByVal lngClienteID As Long, ByVal txtIdTalla As Long, _
             ByVal strTipo As String, ByVal lngRegRecalculo As Long, ByVal dblPVPVig As Double, _
             ByVal dblPVPORG As Double, ByVal lngUnidades As Long, ByRef objPromo As IEnumerable(Of Promocion), ByRef StrMensaje As String) As Double

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim dblDto As Double
        Dim lngIdPromo As Long
        Dim blnClientes As Boolean
        Dim blnPromo2x1 As Boolean
        Dim lngBuscaArticulos2Unidad As Long
        Dim lngBuscaArtBeneficiario, lngBuscaArtDonante As Long
        Dim blnDescuentoMenor As Boolean
        Dim blnRestricciones As Boolean
        Dim dblImporteRestriccion As Double
        Dim lngFkIdPromo As Long
        Dim blnFromPromo As Boolean
        Dim blnSigueEscalonada As Boolean
        Dim strPromoTodosArticulos As String
        Dim lngExistenciaArt, lngExistenciaTda, lngExistenciaRejilla As Long
        Dim dblDtoMaxEscalonada As Double
        Dim dblDtoIR_Cascada As Double
        Dim dblDtoArticulo As Double
        Dim dblDiferencia As Double
        Dim lngClientesEncontrados As Long
        Dim lngClientesEnLaPromocion As Long
        Dim strBusquedaClientesEnPromo As String
        Dim strBusquedaSiPromoTieneClientes As String
        Dim IdTipoUltimoPar As Long
        Dim dblIncrementoDto As Double
        Dim gblLngIdPromocionTodos As Long
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)
        Dim intI, intF As Long
        Dim blnPromoSeleccionada As Boolean
        Dim lngClienteTipo As Long
        Dim strHoraMinutoActual As String


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            strSQL = "select dirftp from parametrosgenerales "
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
            If dtrsADO.Tables(0).Rows.Count = 0 Then
                strDIRFTP = ""
            Else
                strDIRFTP = clsMU.Null_Vac(dtrsADO.Tables(0).Rows(0).Item("dirftp"))
            End If
            dtrsADO.Dispose()

            blnClientes = False : blnPromo2x1 = False : lngIdPromo = 0 : lngFkIdPromo = 0 : lngBuscaArticulos2Unidad = 0 : lngBuscaArtBeneficiario = 0 : lngBuscaArtDonante = 0 : blnDescuentoMenor = False
            blnRestricciones = False : dblImporteRestriccion = 0 : blnPrecioFinal = False : blnFromPromo = False : strPromoTodosArticulos = "" : lngExistenciaArt = 0 : lngExistenciaTda = 0 : lngExistenciaRejilla = 0
            strDescPromo = "" : strTallasValidas = "" : blnSigueEscalonada = False : ValidoArticuloPromo = 0

            '***** TIPO DE PROMOCION - (NORMAL/RECALCULO) *****
            gblstrTipoPromocion = strTipo


            strSQL = "Select count(*) from tbPR_clientesBeneficiarios CLI WITH (NOLOCK) " & _
                " Inner join tbPR_promociones P WITH (NOLOCK) ON CLI.fkPromocion=P.IdPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) " & _
                " and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.fkTipoPromocion in(2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) "


            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnClientes = True
            Else
                blnClientes = False
            End If

            dtrsADO.Dispose()

            '***** TIPO DE CLIENTE - (OBTENEMOS EL TIPO DE CLIENTE ) *****
            lngClienteTipo = GetTipoClientePromo(lngClienteID)

            '****** BUSCAMOS LA HORA Y MINUTO ACTUAL ******
            'strHoraMinutoActual = Format(Now, "hh") & ":" & Format(Now, "mm")
            strHoraMinutoActual = Now.Hour.ToString("D2") & ":" & Now.Minute.ToString("D2")


            '****** BUSCAMOS PROMOCIONES CON TODOS LOAS ARTICULOS ACTIVOS ******
            '****** 28/01/2014 AQUI HAY QUE VERIFICAR 
            strPromoTodosArticulos = GetPromosTodosArticulos("PROMOCIONES")

            ' 15/04/2013 codigo modificado añadiendo campo p.IdTipoUltimoPar
            ' 04/09/2013 añadir campo descuentocascada
            strSQL = "Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
                " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax,RDPI.BuscaFlyers, p.fkRedondeo, p.fkAplicacionRedondeo,ARTB.Id_Cabecero_Detalle, " & _
                " p.IdTipoUltimoPar,RDPI.fkTipoDescuentoPorcentajeImporte,RDPI.descuentocascada,RDPI.descuentoCombinado,RDPI.fkPrimeraCompra from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " INNER join (select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkArticulo=" & clsMU.EsNuloN(txtCI) & _
                " And (Id_Cabecero_Detalle like'%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
                " LEFT Join (select * from tbPR_articulosDonantes WITH (NOLOCK) where fkArticulo=" & clsMU.EsNuloN(txtCI) & _
                " And (Id_Cabecero_Detalle like'%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTD ON P.IdPromocion= ARTD.fkPromocion " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & _
                "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.fkTipoPromocion in (2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) "


            strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & clsMU.EsNuloN(lngClienteID) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "

            strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK) where fkId_Tipo=" & clsMU.EsNuloN(lngClienteTipo) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK))) "

            strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK) where CONVERT(datetime, HoraInicio)<='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And " & _
                " CONVERT(datetime, HoraFin)>='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And ComunicacionValidezEmisionCanje=2 ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK))) "

            strSQL = strSQL & _
                " UNION " & _
                " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
                " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax,RDPI.BuscaFlyers, p.fkRedondeo, p.fkAplicacionRedondeo,ARTB.Id_Cabecero_Detalle," & _
                " p.IdTipoUltimoPar,RDPI.fkTipoDescuentoPorcentajeImporte,RDPI.descuentocascada,RDPI.descuentoCombinado,RDPI.fkPrimeraCompra from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " LEFT join (select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkArticulo=" & clsMU.EsNuloN(txtCI) & _
                " And (Id_Cabecero_Detalle like'%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
                " INNER Join (select * from tbPR_articulosDonantes WITH (NOLOCK) where fkArticulo=" & clsMU.EsNuloN(txtCI) & _
                " And (Id_Cabecero_Detalle like'%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTD ON P.IdPromocion= ARTD.fkPromocion " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & _
                "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.fkTipoPromocion in (2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) " & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) Where fkCliente=" & clsMU.EsNuloN(lngClienteID) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "

            strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK) where fkId_Tipo=" & clsMU.EsNuloN(lngClienteTipo) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK))) "

            strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK) where CONVERT(datetime, HoraInicio)<='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And " & _
                " CONVERT(datetime, HoraFin)>='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And ComunicacionValidezEmisionCanje=2 ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK))) "

            If strPromoTodosArticulos <> "" Then

                ' 15/04/2013 codigo modificado añadiendo campo p.IdTipoUltimoPar
                ' 04/09/2013 añadir campo descuentocascada
                strSQL = strSQL & " UNION " & _
                " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
                " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax,RDPI.BuscaFlyers, p.fkRedondeo, p.fkAplicacionRedondeo," & _
                " '' as Id_Cabecero_Detalle, p.IdTipoUltimoPar,RDPI.fkTipoDescuentoPorcentajeImporte,RDPI.descuentocascada,RDPI.descuentoCombinado,RDPI.fkPrimeraCompra from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & _
                "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion in " & clsMU.EsNuloT(strPromoTodosArticulos) & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & clsMU.EsNuloN(lngClienteID) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK)))"

                strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK) where fkId_Tipo=" & clsMU.EsNuloN(lngClienteTipo) & " ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiariosTipo WITH (NOLOCK))) "

                strSQL = strSQL & _
                " And (" & _
                " P.IdPromocion in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK) where CONVERT(datetime, HoraInicio)<='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And " & _
                " CONVERT(datetime, HoraFin)>='" & clsMU.EsNuloT(strHoraMinutoActual) & "' And ComunicacionValidezEmisionCanje=2 ) " & _
                " Or P.IdPromocion not in (select fkPromocion from tbPR_fechasAvanzada  WITH (NOLOCK))) "

            End If

            strSQL = strSQL & " ORDER By P.FechaValidezInicio"


            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                If dtrsADO.Tables(0).Rows.Count > 1 Then
                    intI = 0 : blnPromoSeleccionada = False
                    'Encuentra la referencia buscada -- más de una promoción por el articulo
                    '**** AQUI TENEMOS QUE CARGAR EL OBJETO CON LAS PROMOCIONES VALIDAS ****
                    For Each objpro In objPromo
                        If intI = lngRegRecalculo Then
                            If objpro.Promo IsNot Nothing Then
                                '***** BUSCAMOS ENTRE LAS PROMOCIONES SI ESTA ALGUNA SELECCIONADA DEL AVE *****
                                For Each objproD As DetallePromo In objpro.Promo
                                    If objproD.PromocionSelecionada Then
                                        blnPromoSeleccionada = True : lngFkIdPromo = objproD.Idpromo : Exit For
                                    End If
                                Next

                                If blnPromoSeleccionada Then
                                    '***** NOS POSICIONAMOS EN LA PROMO SELECCIONADA *****
                                    Dim dvAux As DataView

                                    ''dtrsADO.Tables(0).DefaultView.RowFilter = "IdPromocion=" & lngFkIdPromo
                                    'DML 100314
                                    dvAux = dtrsADO.Tables(0).DefaultView
                                    dvAux.RowFilter = "IdPromocion=" & lngFkIdPromo
                                    dtrsADO = New DataSet()
                                    dtrsADO.Tables.Add(dvAux.ToTable().Copy())
                                    '-----------
                                    '***** CARGAMOS LOS DATOS DE LA PROMO SELECCIONADA *****
                                    dblDto = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Descuento"))
                                    lngIdPromo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdTipoPromocion"))
                                    dblIncrementoDto = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("descuentocascada"))
                                    IdTipoUltimoPar = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdTipoUltimoPar"))
                                    blnRestricciones = dtrsADO.Tables(0).Rows(0).Item("Restricciones")
                                    dblImporteRestriccion = clsMU.EsNuloN(gdblTotalVenta)
                                    strTallasValidas = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item("Id_Cabecero_Detalle"))
                                    '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                    gblLngTipoDescuento = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("fkTipoDescuentoPorcentajeImporte"))
                                Else
                                    '***** BORRAMOS LOS ANTERIORES REGISTROS DEL OBJETO DE PROMOCIONES *****
                                    objpro.Promo = Nothing : intF = 0
                                    For Each FilaI In dtrsADO.Tables(0).Rows
                                        If intF > 0 Then objPD = New DetallePromo
                                        objPD.DescriPromo = FilaI.item("Promocion").ToString
                                        objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                        objPD.DtoPromo = clsMU.EsNuloN(FilaI.item("Descuento"))
                                        objPD.Idpromo = clsMU.EsNuloN(FilaI.item("IdPromocion"))
                                        objPD.PromocionSelecionada = False

                                        ppD.Add(objPD)
                                        intF += 1
                                    Next
                                    objpro.Promo = ppD.AsEnumerable()
                                    Exit Function
                                End If


                                '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                            ElseIf objpro.Promo Is Nothing Then
                                intF = 0
                                For Each FilaI In dtrsADO.Tables(0).Rows
                                    If intF > 0 Then objPD = New DetallePromo
                                    objPD.DescriPromo = FilaI.item("Promocion").ToString
                                    objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                    objPD.DtoPromo = clsMU.EsNuloN(FilaI.item("Descuento"))
                                    objPD.Idpromo = clsMU.EsNuloN(FilaI.item("IdPromocion"))
                                    objPD.PromocionSelecionada = False

                                    ppD.Add(objPD)
                                    intF += 1
                                Next
                                objpro.Promo = ppD.AsEnumerable()
                                Exit Function
                            End If
                        End If
                        intI += 1
                    Next

                Else
                    'Encuentra la referencia buscada -- Solo una promocion por IdArticulo
                    dblDto = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Descuento"))
                    lngIdPromo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdTipoPromocion"))
                    dblIncrementoDto = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("descuentocascada"))
                    IdTipoUltimoPar = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdTipoUltimoPar"))
                    blnRestricciones = dtrsADO.Tables(0).Rows(0).Item("Restricciones")
                    dblImporteRestriccion = clsMU.EsNuloN(gdblTotalVenta)
                    strTallasValidas = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item("Id_Cabecero_Detalle"))
                    '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                    gblLngTipoDescuento = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("fkTipoDescuentoPorcentajeImporte"))

                End If

                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                If strTallasValidas <> "" Then
                    If Not ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(txtIdTalla)) Then
                        ValidoArticuloPromo = 0
                        'If blnFromPromo Then UnloadForm(frmOpcionesPromociones.Name)
                        Exit Function
                    End If
                End If

                gblLngTipoPromocion = lngIdPromo
                gblLngIdPromocion = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion"))

                '**** LMGN - VALIDAMOS SI LA PROMO SELECCIONADA - ES DE TODOS LOS ARTICULOS ****
                If strPromoTodosArticulos <> "" Then
                    gblLngIdPromocionTodos = GetValidaPromosTodosArticulos(strPromoTodosArticulos, dtrsADO.Tables(0).Rows(0).Item("IdPromocion"))
                Else
                    gblLngIdPromocionTodos = False
                End If

                gblfkRedondeo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("fkRedondeo"))
                gblfkAplicacionRedondeo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("fkAplicacionRedondeo"))

                strBusquedaSiPromoTieneClientes = "Select COUNT(*) from tbPR_clientesBeneficiarios CLI WITH (NOLOCK) " & _
                    " Inner join tbPR_promociones P WITH (NOLOCK) ON CLI.fkPromocion=P.IdPromocion " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & _
                    "' And P.fkTipoPromocion in(2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) " & _
                    " And cli.fkpromocion = " & clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion"))


                strBusquedaClientesEnPromo = strBusquedaSiPromoTieneClientes & " And cli.fkcliente = " & clsMU.EsNuloN(lngClienteID)

                lngClientesEnLaPromocion = clsMU.GetCountTable(strBusquedaSiPromoTieneClientes)
                lngClientesEncontrados = clsMU.GetCountTable(strBusquedaClientesEnPromo)

                If blnClientes And lngClientesEncontrados = 0 And lngClientesEnLaPromocion > 0 Then
                    ValidoArticuloPromo = 0
                    'If blnFromPromo Then UnloadForm(frmOpcionesPromociones.Name)
                    Exit Function
                End If

                '**** BUSCAMOS RESTRICCIONES DE PRIMERAS COMPRAS 17/06/2014 ****
                If blnRestricciones Then
                    ' 11/06/2014
                    '*** comprobar aqui la PRIMERAS COMPRAS ***
                    If Not ValidaFiltroPrimerasCompras(lngClienteID, clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("fkPrimeraCompra"))) Then
                        Exit Function
                    End If
                End If

                '*****=> VALIDAMOS LAS PROMOCIONES <=*****

                If lngIdPromo = 2 And Not blnRestricciones Then
                    '****** PROMO (Descuento por criterio de selección) ******
                    If CBool(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("BuscaFlyers"))) Then
                        'Restricción de busqueda FLYERS o CUPONES
                        'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.blnFlyers = True : frmOpcionesPromocionesRestricciones.lngClienteID = EsNuloN(lngClienteID) : frmOpcionesPromocionesRestricciones.Show(vbModal)
                        'If blnRespuestaRestricciones And strCodigoBono <> "" Then
                        '    Call InsertoArtBeneficiarioDescuentoRestriccionesTG(EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto)
                        '    If strTipo = "VENTA" Then
                        '        frmTickets.strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        '    ElseIf strTipo = "RECALCULO" Then
                        '        strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        '    End If
                        '    ValidoArticuloPromo = EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion"))))
                        'End If
                    Else
                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                            ValidoArticuloPromo = dblDto
                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                            dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                            ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                        End If

                        'frmTickets.strDescPromo = frmOpcionesPromociones.strDto
                        If strTipo = "VENTA" Then
                            strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        ElseIf strTipo = "RECALCULO" Then
                            strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        End If
                    End If
                ElseIf lngIdPromo = 2 And blnRestricciones And clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa")) = 0 Then
                    '****** PROMO (Descuento por criterio de selección - CUPONES ) ******

                    If UCase(strDIRFTP) = "TIENDASKIOTO" Then
                        'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.Show(vbModal)
                        If blnRespuestaRestricciones Then
                            Call InsertoArtBeneficiarioDescuentoRestricciones(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), dblDto, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), objPromo)

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        End If
                    ElseIf UCase(strDIRFTP) = "TINOGONZALEZ" Or UCase(strDIRFTP) = "PRUEBASTINO" Then
                        'Verifico si existen promocion de FLYER TINO GONZALEZ
                        ' If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.Show(vbModal)
                        If blnRespuestaRestricciones And strCodigoBono <> "" Then
                            Call InsertoArtBeneficiarioDescuentoRestriccionesTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto, objPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), objPromo))
                        End If
                        'ElseIf clsLANG.GetIdiomaID = 8 Then
                        '    'Verifico si existen promocion de FLYER
                        '    If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.Show(vbModal)
                        '    If blnRespuestaRestricciones And strCodigoBono <> "" Then
                        '        Call InsertoArtBeneficiarioDescuentoRestriccionesTG(EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto)
                        '        If strTipo = "VENTA" Then
                        '            frmTickets.strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        '        ElseIf strTipo = "RECALCULO" Then
                        '            strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        '        End If
                        '        ValidoArticuloPromo = EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion"))))
                        '    End If
                    ElseIf CBool(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("BuscaFlyers"))) Then
                        'Restricción de busqueda FLYERS o CUPONES
                        'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.blnFlyers = True : frmOpcionesPromocionesRestricciones.lngClienteID = EsNuloN(lngClienteID) : frmOpcionesPromocionesRestricciones.Show(vbModal)
                        If blnRespuestaRestricciones And strCodigoBono <> "" Then
                            Call InsertoArtBeneficiarioDescuentoRestriccionesTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto, objPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), objPromo))
                        End If
                    Else
                        Call InsertoArtBeneficiarioDescuentoRestricciones(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), dblDto, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), objPromo)
                        If strTipo = "VENTA" Then
                            strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        ElseIf strTipo = "RECALCULO" Then
                            strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                        End If
                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                            ValidoArticuloPromo = dblDto
                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                            dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                            ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                        End If
                    End If

                ElseIf lngIdPromo = 2 And blnRestricciones Then
                    If IIf(UCase(strDIRFTP) = "TINOGONZALEZ", BuscoImporteFlyersTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), objPromo) + (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa")), clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If UCase(strDIRFTP) = "TINOGONZALEZ" Or UCase(strDIRFTP) = "PRUEBASTINO" Then
                            'Verifico si existen promocion de FLYER TINO GONZALEZ
                            'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.Show(vbModal)
                            If blnRespuestaRestricciones And strCodigoBono <> "" Then
                                Call InsertoArtBeneficiarioDescuentoRestriccionesTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto, objPromo)
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), objPromo))
                            End If
                        ElseIf CBool(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("BuscaFlyers"))) Then
                            'Restricción de busqueda FLYERS o CUPONES
                            'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.blnFlyers = True : frmOpcionesPromocionesRestricciones.lngClienteID = EsNuloN(lngClienteID) : frmOpcionesPromocionesRestricciones.Show(vbModal)
                            If blnRespuestaRestricciones And strCodigoBono <> "" Then
                                Call InsertoArtBeneficiarioDescuentoRestriccionesTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto, objPromo)
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), objPromo))
                            End If
                        Else
                            Call InsertoArtBeneficiarioDescuentoRestricciones(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), dblDto, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), objPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        End If
                    End If
                ElseIf lngIdPromo = 3 Then
                    '****** PROMO (Promoción regalo) ******
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        blnArtBeneficiario = False : blnArtDonante = False
                        lngBuscaArtBeneficiario = BuscaArticulosBeneficiario(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                        lngBuscaArtDonante = BuscaArticulosDonante(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                        'articulo beneficiario
                        If blnArtBeneficiario Then
                            lngBuscaArtBeneficiario = IIf(strTipo = "VENTA", lngBuscaArtBeneficiario + 1, lngBuscaArtBeneficiario)
                            If lngBuscaArtDonante >= lngBuscaArtBeneficiario Then
                                InsertoArtDonantesDescuento(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), 3, objPromo)
                            Else
                                If strTipo = "VENTA" Then
                                    strDescPromo = 0
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = 0
                                End If
                                ValidoArticuloPromo = 0
                            End If
                        End If
                        'articulo Donante
                        If blnArtDonante Then
                            '*** HE DESCOMENTADO ESTA LINEA YA QUE ES CORRECTO QUE SE INCREMENTE ESTE CONTADOR  ***
                            '*** DE NO HACERLO FALLA LA PROMOCION - NO SE QUIEN LA HA COMENTADO ASI QUE OS DEJO ***
                            '*** ESTA ACLARACION LMGN 21/03/13 ***
                            lngBuscaArtDonante = IIf(strTipo = "VENTA", lngBuscaArtDonante + 1, lngBuscaArtDonante)
                            If lngBuscaArtBeneficiario >= lngBuscaArtDonante Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = 100
                            Else
                                If strTipo = "VENTA" Then
                                    strDescPromo = 0
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = 0
                                End If
                                ValidoArticuloPromo = 0
                            End If
                        End If
                    End If
                ElseIf lngIdPromo = 10 Or lngIdPromo = 15 Or lngIdPromo = 17 Or lngIdPromo = 19 And Not blnRestricciones Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 10 And BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento por criterio de selección 2da Unidad) ******
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        ElseIf lngIdPromo = 15 And BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento por criterio de selección 3ra Unidad) ******
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        ElseIf lngIdPromo = 17 And BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento por criterio de selección 4ta Unidad) ******
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        ElseIf lngIdPromo = 19 And BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento por criterio de selección 5ta Unidad) ******
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ValidoArticuloPromo = dblDto
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If
                ElseIf lngIdPromo = 11 Or lngIdPromo = 16 Or lngIdPromo = 18 Or lngIdPromo = 20 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 11 And BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento 2da Unidad Menor Valor) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                    ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                End If
                            End If
                        ElseIf lngIdPromo = 16 And BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento 3ra Unidad Menor Valor) ******
                            If (UCase(strDIRFTP) = "SPRINGSTEP") Then
                                blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor_SPRING(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            Else
                                blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            End If
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                    ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                End If
                            End If
                        ElseIf lngIdPromo = 18 And BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento 4ta Unidad Menor Valor) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                    ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                End If
                            End If
                        ElseIf lngIdPromo = 20 And BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento 5ta Unidad Menor Valor) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                    ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                End If
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If
                ElseIf lngIdPromo = 12 Then
                    blnArtBeneficiario = False : blnArtDonante = False
                    lngBuscaArtBeneficiario = BuscaArticulosBeneficiario(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                    lngBuscaArtDonante = BuscaArticulosDonante(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                    'articulo beneficiario
                    If blnArtBeneficiario Then
                        If lngBuscaArtBeneficiario <= lngBuscaArtDonante Then
                            InsertoArtDonantesDescuento(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), 12, objPromo)
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If
                    'articulo Donante
                    If blnArtDonante Then
                        If lngBuscaArtBeneficiario >= lngBuscaArtDonante Then
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If
                ElseIf lngIdPromo = 13 Or lngIdPromo = 22 Or lngIdPromo = 23 Or lngIdPromo = 24 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 13 And BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Articulo más barato por Compra de 2ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 22 And BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Articulo más barato por Compra de 3ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 23 And BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Articulo más barato por Compra de 4ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 24 And BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Articulo más barato por Compra de 5ª unidad) ******                            
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 27 Then
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        ' ABM 25/10/2012 aqui valida si hay 2 articulo con el resto de dividir por 2
                        If lngIdPromo = 27 Then
                            '****** PROMO (Precio Reducido en Articulo ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 28 And Not blnRestricciones Then
                    '****** PROMO (Descuento Maximo) ******
                    dblDtoArticulo = clsMU.GetPorcentajeDtoArticulo(clsMU.EsNuloN(txtCI), gstrTiendaSesion)

                    If dblDtoArticulo > dblDto Then

                        ' MJM 18/03/2014 INICIO
                        Dim szSqlNombreArticulo As String = String.Empty
                        Dim dsNombreArticulo As New DataSet
                        Dim strSqlNombreArticulo As String = String.Format("Select DESCRIPCION from articulos where idarticulo = {0}", txtCI)
                        Dim strNombreArticulo As String = String.Empty

                        clsAD.CargarDataset(dsNombreArticulo, strSqlNombreArticulo, "NombreArticuloPromocion")

                        If dsNombreArticulo.Tables(0).Rows.Count > 0 Then strNombreArticulo = dsNombreArticulo.Tables(0).Rows(0)(0).ToString()

                        StrMensaje = String.Format("El artículo introducido [{0}] YA tiene un descuento del {1}% superior al de la promocion a aplicar que es del {2}%.", strNombreArticulo, clsMU.FormatoEuros(dblDtoArticulo, True, True, True), dblDto)
                        'StrMensaje = "El artículo introducido YA tiene un descuento del " & clsMU.FormatoEuros(dblDtoArticulo, True, True, True) & "% superior al de la promocion a aplicar que es del " & dblDto & "%."
                        ' MJM 18/03/2014 FIN

                        dblDto = 0 : gblLngTipoPromocion = 0
                End If


                ValidoArticuloPromo = dblDto
                'frmTickets.strDescPromo = frmOpcionesPromociones.strDto
                If strTipo = "VENTA" Then
                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                ElseIf strTipo = "RECALCULO" Then
                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                End If

                ' 17/01/2013
                dblDiferencia = clsMU.EsNuloN(dblPVPORG) - (clsMU.EsNuloN(dblPVPORG) * dblDto / 100)
                dblDiferencia = clsMU.EsNuloN(dblPVPVig) - dblDiferencia
                ValidoArticuloPromo = dblDiferencia * 100 / clsMU.EsNuloN(dblPVPVig)

                ElseIf lngIdPromo = 28 And blnRestricciones Then
                    If clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa")) Then
                        If UCase(strDIRFTP) = "TINOGONZALEZ" Or UCase(strDIRFTP) = "PRUEBASTINO" Then
                            'Verifico si existen promocion de FLYER TINO GONZALEZ
                            'If Not blnRestriccionesApertura Then frmOpcionesPromocionesRestricciones.Show(vbModal)
                            If blnRespuestaRestricciones And strCodigoBono <> "" Then
                                Call InsertoArtBeneficiarioDescuentoRestriccionesTG(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), strCodigoBono, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), dblDto, objPromo)
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, dtrsADO.Tables(0).Rows(0).Item("IdPromocion"), objPromo))
                            End If
                        Else
                            Call InsertoArtBeneficiarioDescuentoRestricciones(dtrsADO.Tables(0).Rows(0).Item("IdPromocion"), dblDto, clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0)), objPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = dblDto
                        End If
                    End If


                ElseIf lngIdPromo = 21 Then
                    '****** PROMO (Descuento Combinado en selección de conjuntos) ******
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        blnArtBeneficiario = False : blnArtDonante = False
                        lngBuscaArtBeneficiario = BuscaArticulosBeneficiarioConjunto(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, lngRegRecalculo, objPromo, lngIdPromo)
                        lngBuscaArtDonante = BuscaArticulosDonante(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngIdPromo)
                        'articulo beneficiario
                        If blnArtBeneficiario Then
                            If lngBuscaArtBeneficiario <= lngBuscaArtDonante And lngBuscaArtBeneficiario > 0 Then
                                InsertoArtDonantesDescuento(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, objPromo)
                            Else
                                If strTipo = "VENTA" Then
                                    strDescPromo = 0
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = 0
                                End If
                                ValidoArticuloPromo = 0
                            End If
                        End If
                        'articulo Donante
                        If blnArtDonante Then
                            lngBuscaArtDonante = IIf(strTipo = "VENTA", lngBuscaArtDonante + 1, lngBuscaArtDonante)
                            If lngBuscaArtBeneficiario >= lngBuscaArtDonante And lngBuscaArtDonante > 0 Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = False
                            Else
                                If strTipo = "VENTA" Then
                                    strDescPromo = 0
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = 0
                                End If
                                ValidoArticuloPromo = 0
                            End If
                        End If
                    End If
                ElseIf lngIdPromo = 25 Then
                    '****** PROMO (ULTIMO PAR) ******
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then

                        lngExistenciaArt = 0 : lngExistenciaTda = 0 : lngExistenciaRejilla = 0

                        ' 15/04/2013 añadimos nuevo parámetro
                        lngExistenciaArt = BuscaExistenciaUltimoPar(txtCI, txtIdTalla, "EXISTENCIA_ARTICULO", objPromo, IdTipoUltimoPar)

                        lngExistenciaTda = BuscaExistenciaUltimoPar(txtCI, txtIdTalla, "EXISTENCIA_TIENDA", objPromo)
                        lngExistenciaRejilla = BuscaExistenciaUltimoPar(txtCI, txtIdTalla, "EXISTENCIA_REJILLA", objPromo)
                        'Restamos la exitencia actual del Articulo - la la existencia articulo en la rejilla
                        lngExistenciaArt = lngExistenciaArt - lngExistenciaRejilla

                        'Para tiendas Kioto aunque sea el ultimo par si la existencias en otras tiendas es superior a 5 no aplica el descuento
                        ' FTP_KIOTO
                        If (UCase(strDIRFTP) <> "SPRINGSTEP" And lngExistenciaArt = 1) Or _
                           (UCase(strDIRFTP) = "SPRINGSTEP" And lngExistenciaArt = 1 And lngExistenciaTda <= 5) Then
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = dblDto : blnPrecioFinal = False
                        End If
                    End If

                ElseIf lngIdPromo = 29 Or lngIdPromo = 30 Or lngIdPromo = 31 Or lngIdPromo = 32 Or lngIdPromo = 33 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 29 Then
                            '****** PROMO (Descuento Cascada por criterio de selección) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                Else

                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad)
                                    End If

                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If dblIncrementoDto <> 0 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else

                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = ValidoArticuloPromo
                                    Else
                                        ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad)
                                    End If

                                End If
                            End If


                            ' 05/09/2013
                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            Call InsertoDescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, objPromo)
                            ' 05/09/2013 cambiado elseif
                        ElseIf lngIdPromo = 30 And ((BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Cascada por criterio de selección 2da Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then

                                If lngBuscaArticulos2Unidad > 2 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ' 05/09/2013 codigo anterior estaba comentado
                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            Call InsertoDescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, objPromo)
                            ' 05/09/2013
                        ElseIf lngIdPromo = 31 And ((BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Cascada por criterio de selección 3ra Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 3 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 3 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 3 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ' 05/09/2013 codigo anterior comentado
                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            Call InsertoDescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, objPromo)
                        ElseIf lngIdPromo = 32 And ((BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Cascada por criterio de selección 4ta Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then

                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 4 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 4 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 4 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ' 05/09/2013
                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            Call InsertoDescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, objPromo)
                        ElseIf lngIdPromo = 33 And ((BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Cascada por criterio de selección 5ta Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 5 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 5 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 5 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            Call InsertoDescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, objPromo)
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 36 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 36 And BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Descuento 2da y 3ra Unidad Menor Valor) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig, True)
                            '****** APLICAMOS 2 VECES EL INSERTAR ARTICULOS MENOS UNIDAD *****
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoMenor(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                    ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                End If
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 37 Or lngIdPromo = 38 Or lngIdPromo = 39 Or lngIdPromo = 40 Or lngIdPromo = 41 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 37 Then
                            '****** PROMO (Descuento Escalonado por criterio de selección) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                Else
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad)
                                    End If

                                End If

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If dblIncrementoDto <> 0 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = ValidoArticuloPromo
                                    Else
                                        ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad)
                                    End If

                                End If
                            End If

                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If ValidoArticuloPromo <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then
                                    blnSigueEscalonada = True
                                Else
                                    dblDtoMaxEscalonada = GetDtoMaxRejilla(objPromo)
                                    If (dblDtoMaxEscalonada < clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax"))) Or (dblDtoMaxEscalonada < (clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) - 3)) Then blnSigueEscalonada = True
                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If ((clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDiferencia) <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then blnSigueEscalonada = True
                            End If

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            If blnSigueEscalonada Then
                                Call InsertoDescuentoEscalonado(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDto, dblIncrementoDto, 1, objPromo, lngRegRecalculo)
                            Else
                                ValidoArticuloPromo = 0
                            End If

                        ElseIf lngIdPromo = 38 And ((BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Escalonado por criterio de selección 2ª unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If lngBuscaArticulos2Unidad > 2 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)


                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If ValidoArticuloPromo <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then
                                    blnSigueEscalonada = True
                                Else
                                    dblDtoMaxEscalonada = GetDtoMaxRejilla(objPromo)
                                    If (dblDtoMaxEscalonada < clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax"))) Or (dblDtoMaxEscalonada < (clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) - 3)) Then blnSigueEscalonada = True
                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If ((clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDiferencia) <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then blnSigueEscalonada = True
                            End If

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            If blnSigueEscalonada Then
                                Call InsertoDescuentoEscalonado(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDto, dblIncrementoDto, 2, objPromo, lngRegRecalculo)
                            Else
                                ValidoArticuloPromo = 0
                            End If

                        ElseIf lngIdPromo = 39 And ((BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Escalonado por criterio de selección 3ª unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If lngBuscaArticulos2Unidad > 3 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 3 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 3 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If ValidoArticuloPromo <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then
                                    blnSigueEscalonada = True
                                Else
                                    dblDtoMaxEscalonada = GetDtoMaxRejilla(objPromo)
                                    If (dblDtoMaxEscalonada < clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax"))) Or (dblDtoMaxEscalonada < (clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) - 3)) Then blnSigueEscalonada = True
                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If ((clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDiferencia) <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then blnSigueEscalonada = True
                            End If

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            If blnSigueEscalonada Then
                                Call InsertoDescuentoEscalonado(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDto, dblIncrementoDto, 3, objPromo, lngRegRecalculo)
                            Else
                                ValidoArticuloPromo = 0
                            End If

                        ElseIf lngIdPromo = 40 And ((BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Escalonado por criterio de selección 4ª unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then

                                If lngBuscaArticulos2Unidad > 4 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 4 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 4 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If ValidoArticuloPromo <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then
                                    blnSigueEscalonada = True
                                Else
                                    dblDtoMaxEscalonada = GetDtoMaxRejilla(objPromo)
                                    If (dblDtoMaxEscalonada < clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax"))) Or (dblDtoMaxEscalonada < (clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) - 3)) Then blnSigueEscalonada = True
                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If ((clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDiferencia) <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then blnSigueEscalonada = True
                            End If

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            If blnSigueEscalonada Then
                                Call InsertoDescuentoEscalonado(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDto, dblIncrementoDto, 4, objPromo, lngRegRecalculo)
                            Else
                                ValidoArticuloPromo = 0
                            End If

                        ElseIf lngIdPromo = 41 And ((BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO ( Descuento Escalonado por criterio de selección 5ª unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then

                                If lngBuscaArticulos2Unidad > 5 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 5 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 5 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If ValidoArticuloPromo <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then
                                    blnSigueEscalonada = True
                                Else
                                    dblDtoMaxEscalonada = GetDtoMaxRejilla(objPromo)
                                    If (dblDtoMaxEscalonada < clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax"))) Or (dblDtoMaxEscalonada < (clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) - 3)) Then blnSigueEscalonada = True
                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If ((clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDiferencia) <= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")) Then blnSigueEscalonada = True
                            End If

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            If blnSigueEscalonada Then
                                Call InsertoDescuentoEscalonado(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDto, dblIncrementoDto, 5, objPromo, lngRegRecalculo)
                            Else
                                ValidoArticuloPromo = 0
                            End If

                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 42 Then
                    blnArtBeneficiario = False : blnArtDonante = False
                    lngBuscaArtBeneficiario = BuscaArticulosBeneficiario(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                    lngBuscaArtDonante = BuscaArticulosDonante(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), txtCI, txtIdTalla, objPromo, lngRegRecalculo, lngIdPromo)
                    'articulo beneficiario
                    If blnArtBeneficiario Then
                        If lngBuscaArtBeneficiario < lngBuscaArtDonante Then
                            InsertoArtDonantesDescuento(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), 42, objPromo)
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If
                    'articulo Donante
                    If blnArtDonante Then
                        If lngBuscaArtBeneficiario > lngBuscaArtDonante Then
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If
                            ValidoArticuloPromo = dblDto : blnPrecioFinal = False
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If
                    End If

                ElseIf lngIdPromo = 43 Or lngIdPromo = 44 Or lngIdPromo = 45 Or lngIdPromo = 46 Or lngIdPromo = 47 Then

                    '****** PROMO (Precio Reducido en Articulo ******
                    If strTipo = "VENTA" Then
                        strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                    ElseIf strTipo = "RECALCULO" Then
                        strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                    End If
                    dblDtoIR_Cascada = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("descuentoCombinado")) : blnPrecioFinal = True


                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 43 Then
                            '****** PROMO (Descuento Importe Reducido + Cascada por criterio de selección) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = dblDto
                                Else
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad)
                                    End If

                                End If
                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                ' 05/09/2013
                                If dblIncrementoDto <> 0 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 1) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)
                                If (lngBuscaArticulos2Unidad - 1) = 0 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ' 05/09/2013
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = ValidoArticuloPromo
                                    Else
                                        ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad)
                                    End If

                                End If
                            End If

                            ' 05/09/2013
                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIR_Cascada), ValidoArticuloPromo), True, True, False)


                        ElseIf lngIdPromo = 44 And ((BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Importe Reducido + Cascada por criterio de selección 2da Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If lngBuscaArticulos2Unidad > 2 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 2) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 2 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)

                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIR_Cascada), ValidoArticuloPromo), True, True, False)

                            ' 05/09/2013
                        ElseIf lngIdPromo = 45 And ((BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Importe Reducido + Cascada por criterio de selección 3ra Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 3 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 3 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 3) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 3 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIR_Cascada), ValidoArticuloPromo), True, True, False)

                        ElseIf lngIdPromo = 46 And ((BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Importe Reducido + Cascada por criterio de selección 4ta Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 4 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 4 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 4) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 4 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIR_Cascada), ValidoArticuloPromo), True, True, False)

                        ElseIf lngIdPromo = 47 And ((BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) And Not blnPromoCascada) Or blnPromoCascada) Then
                            '****** PROMO (Descuento Importe Reducido + Cascada por criterio de selección 5ta Unidad) ******
                            If strTipo = "VENTA" Then
                                lngBuscaArticulos2Unidad = lngBuscaArticulos2Unidad + 1
                            End If
                            blnPromoCascada = True
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                ' 05/09/2013
                                If lngBuscaArticulos2Unidad > 5 Then
                                    If dblIncrementoDto <> 0 Then
                                        ValidoArticuloPromo = dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto)
                                    Else
                                        ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                    End If
                                Else
                                    ValidoArticuloPromo = dblDto * (lngBuscaArticulos2Unidad - 1)
                                End If

                                ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad > 5 Then
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - (dblDto + ((lngBuscaArticulos2Unidad - 5) * dblIncrementoDto))
                                Else
                                    dblDiferencia = (clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades)) - dblDto
                                End If

                                ValidoArticuloPromo = clsMU.GetPcDto(clsMU.EsNuloN(dblPVPVig) * clsMU.EsNuloN(lngUnidades), dblDiferencia)

                                ' 05/09/2013
                                If dblIncrementoDto <> 0 And lngBuscaArticulos2Unidad >= 5 Then
                                    ValidoArticuloPromo = ValidoArticuloPromo
                                Else
                                    ValidoArticuloPromo = ValidoArticuloPromo * (lngBuscaArticulos2Unidad - 1)
                                End If

                            End If

                            ValidoArticuloPromo = IIf(ValidoArticuloPromo > clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("DescuentoMax")), ValidoArticuloPromo)
                            If strTipo = "VENTA" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                            End If

                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIR_Cascada), ValidoArticuloPromo), True, True, False)

                        Else
                            Call InsertoImporteReducido_DescuentoCascada(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), ValidoArticuloPromo, dblDtoIR_Cascada, objPromo)
                            ValidoArticuloPromo = dblDtoIR_Cascada
                        End If
                    End If

                ElseIf lngIdPromo = 48 Or lngIdPromo = 49 Or lngIdPromo = 50 Or lngIdPromo = 51 Then
                    lngBuscaArticulos2Unidad = BuscaArticulos(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngRegRecalculo, objPromo)
                    If Not blnRestricciones Or (blnRestricciones And clsMU.EsNuloN(dblImporteRestriccion) >= clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Valorventa"))) Then
                        If lngIdPromo = 48 And BuscaArticulos2Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Conjunto por Compra de 2ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoConjunto(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 49 And BuscaArticulos3Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Conjunto por Compra de 3ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoConjunto(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 50 And BuscaArticulos4Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Conjunto por Compra de 4ª unidad) ******
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoConjunto(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        ElseIf lngIdPromo = 51 And BuscaArticulos5Unidad(lngBuscaArticulos2Unidad) Then
                            '****** PROMO (Precio Reducido en Conjunto por Compra de 5ª unidad) ******                            
                            blnDescuentoMenor = InsertoArtBeneficiarioDescuentoConjunto(clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("IdPromocion")), lngIdPromo, lngRegRecalculo, objPromo, dblPVPVig)
                            If Not blnDescuentoMenor Then
                                If strTipo = "VENTA" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                ElseIf strTipo = "RECALCULO" Then
                                    strDescPromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item(0))
                                End If
                                ValidoArticuloPromo = dblDto : blnPrecioFinal = True
                            End If
                        Else
                            If strTipo = "VENTA" Then
                                strDescPromo = 0
                            ElseIf strTipo = "RECALCULO" Then
                                strDescPromo = 0
                            End If
                            ValidoArticuloPromo = 0
                        End If

                    End If

                End If

                'If blnFromPromo Then UnloadForm(frmOpcionesPromociones.Name)
                'Me.Refresh: DoEvents

            Else
                ValidoArticuloPromo = 0
            End If

            dtrsADO.Dispose()

            Return ValidoArticuloPromo

        Catch ex As Exception
            Throw New Exception("ModPromociones-ValidoArticuloPromo", ex)
        End Try

    End Function

    '    Public Function GetIdPromo(ByVal strPROMO As String) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        strSQL = "select IdPromocion from tbPR_promociones WITH (NOLOCK) " & _
    '            " where promocion= '" & EC(strPROMO) & "'"
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetIdPromo = rsADO!IdPromocion
    '        Else
    '            GetIdPromo = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GetIdPromo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    '    Public Function GuardoArticulosPromosDescuento(ByVal strIdTicket As String, ByVal cnADO As ADODB.Connection)

    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim intJ, intI As Long

    '        On Error GoTo ReparaErrores

    '        If (Trim(BuscaConfiguraciones("PROMOCIONESTPV"))) = "1" Then
    '            intJ = 1
    '            '*** TIPO OPERACION VENTA - DEVOLUCION ***
    '            For intI = 1 To UBound(arrDto)
    '                With arrDto(intI)
    '                    If .blnActivo And .strFPagoDetalle = "GESTOR PROMOCIONES" Then
    '                        cnADO.Execute("INSERT INTO N_TICKETS_PROMOCIONES(Id_Ticket,IdOrden,Id_Tienda,Id_Promocion,Id_Empleado,Id_Articulo,Id_Cabecero_Detalle,Fecha," & _
    '                            " Pvp_Vig,ImporteDescuento,ImporteDtoPor,ImporteEuros) VALUES(" & _
    '                            "'" & EC(strIdTicket) & "'," & intJ & ",'" & gstrTiendaSesion & "'," & GetIdPromo(.strFpago) & "," & EsNuloN(frmTickets.txtEmpleado.Text) & "," & .lngArticulo & "," & .lngTalla & ",'" & Format(gdteFechaSesion & Space(1) & Time, gLocaleInfo.ShortDateTimeFormat) & "'," & _
    '                            SinComas(.dblPVP_Base) & "," & SinComas(.dblDto_Imp) & "," & SinComas(.dblDto_Por) & "," & SinComas(.dblPVP_Base - .dblDto_Imp) & ")", , adExecuteNoRecords)
    '                        intJ = intJ + 1
    '                    End If
    '                End With
    '            Next
    '        End If


    'ReparaErrores:
    '        CloseRS(rsADO)        
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GuardoArticulosPromosDescuento", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    '    Public Function ValidoPromosValidasImpresion() As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim dblDto As Double

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        ValidoPromosValidasImpresion = 0
    '        'TENEMOS
    '        ' ABM 31/10/2012 añadimos a P.fkTipoPromocion in tipo promocion 28
    '        ' 04/09/2013 añadir el campo descuento cascada
    '        strSQL = "Select P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,P.ImagenAvisoPrevio,TP.TipoPromocion" & _
    '            " from tbPR_promociones P WITH (NOLOCK) " & _
    '            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '            " where P.FechaComunicacionInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaComunicacionFin >= ' " & _
    '            EC(gdteFechaSesion) & "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.MostrarImagenAvisoPrevio=1 And " & _
    '            " P.fkTipoPromocion in (2,3,7,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) order by P.FechaComunicacionInicio"
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                ValidoPromosValidasImpresion = ValidoPromosValidasImpresion + 1
    '                rsADO.MoveNext()
    '            End While
    '        Else
    '            ValidoPromosValidasImpresion = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoPromosValidasImpresion", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    'Public Function BuscaFPpromos(ByVal strpago As String, ByVal strTipoFRM As String, Optional ByVal idFPDetalle As Long, _
    '            Optional ByVal idOrdenFPDetalle As Long, Optional ByVal blnOrdenFPDetalle As Boolean, Optional ByVal strpagoDetalle As String) As Boolean

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim strFP As String
    '        Dim dblDto As Double
    '        Dim intI, intDto As Long
    '        Dim strMensaje As String
    '        Dim blnFormaCorrecta As Boolean
    '        Dim Resp
    '        Dim lngI As Long
    '        Dim strFP_Promos As String
    '        Dim blnTodasFP As Boolean

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        BuscaFPpromos = False : strFP = "" : blnFormaCorrecta = False

    '        If UBound(arrDto) > 0 Then

    '            For intDto = 1 To UBound(arrDto)
    '                If arrDto(intDto).strFPagoDetalle = "GESTOR PROMOCIONES" And arrDto(intDto).blnActivo Then
    '                    strFP = strFP & "'" & arrDto(intDto).strFpago & "',"
    '                End If
    '            Next intDto
    '            If strFP <> "" Then
    '                strFP = Mid(strFP, 1, Len(strFP) - 1)

    '                '****** VERIFICAMOS SI ESTAN TODAS LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******
    '                blnTodasFP = False
    '                strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion   " & _
    '                       " from tbPR_promociones P WITH (NOLOCK) " & _
    '                       " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                       " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                       " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                       " Inner join tbPR_formasPago FP WITH (NOLOCK)  ON P.IdPromocion=  FP.fkPromocion " & _
    '                       " Inner join TIPOS_DE_PAGO TFP WITH (NOLOCK)  ON FP.fkPago=  TFP.IdPago " & _
    '                       " where P.promocion in ( " & strFP & " )"
    '                rsADO = New ADODB.Recordset
    '                rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '                If Not rsADO.EOF Then
    '                    blnTodasFP = False
    '                Else
    '                    blnTodasFP = True
    '                End If

    '                CloseRS(rsADO)


    '                ' ABM 15/10/2012 codigo anterior
    '                ' 15/03/2013 codigo modificado a peticion de Piagui
    '                If gblDirFtp = FTP_NINEWEST_MEX Or gblDirFtp = FTP_BRANTANO Or gblDirFtp = FTP_CHARLY Then
    '                    strSQL = "Select DISTINCT FP.fkPago"
    '                    strSQL = strSQL & " , case fkPago when 999 then 'VALES' else tfp.Descripcion_Pago end as Descripcion_Pago"
    '                    strSQL = strSQL & ",P.Promocion,P.idPromocion   " & _
    '                        " from tbPR_promociones P WITH (NOLOCK) " & _
    '                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                        " Inner join tbPR_formasPago FP WITH (NOLOCK)  ON P.IdPromocion=  FP.fkPromocion " & _
    '                        " left join TIPOS_DE_PAGO TFP WITH (NOLOCK)  ON FP.fkPago=  TFP.IdPago " & _
    '                        " where P.promocion in ( " & strFP & " )"
    '                ElseIf gblDirFtp = FTP_SPRINGSTEP And strTipoFRM = "PAGOS" Then
    '                    strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion,FPD.fkorden   " & _
    '                        " from tbPR_promociones P WITH (NOLOCK) " & _
    '                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                        " Inner join tbPR_formasPago FP WITH (NOLOCK)  ON P.IdPromocion=  FP.fkPromocion " & _
    '                        " LEFT join tbPR_formasPagodetalle FPD WITH (NOLOCK)  ON P.IdPromocion=  FPD.fkPromocion " & _
    '                        " Inner join TIPOS_DE_PAGO TFP WITH (NOLOCK)  ON FP.fkPago=  TFP.IdPago " & _
    '                        " where P.promocion in ( " & strFP & " )  And FP.FKpago=" & EsNuloN(idFPDetalle) & " "                    

    '                    If blnOrdenFPDetalle Then
    '                        strSQL = strSQL & " And FPD.FKOrden=" & EsNuloN(idOrdenFPDetalle)
    '                    End If
    '                Else
    '                    strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion   " & _
    '                        " from tbPR_promociones P WITH (NOLOCK) " & _
    '                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                        " Inner join tbPR_formasPago FP WITH (NOLOCK)  ON P.IdPromocion=  FP.fkPromocion " & _
    '                        " Inner join TIPOS_DE_PAGO TFP WITH (NOLOCK)  ON FP.fkPago=  TFP.IdPago " & _
    '                        " where P.promocion in ( " & strFP & " )"
    '                End If

    '                rsADO = New ADODB.Recordset
    '                rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '                If Not rsADO.EOF Then
    '                    While Not rsADO.EOF
    '                        If strpago = "MIXTA" And EsNuloT(rsADO!Descripcion_pago) <> "EFECTIVO" And EsNuloT(rsADO!Descripcion_pago) <> "TARJETA" Then
    '                            blnFormaCorrecta = True
    '                        ElseIf strpago <> EsNuloT(rsADO!Descripcion_pago) And Not blnOrdenFPDetalle Then                            
    '                            strMensaje = "La forma de Pago seleccionada (" & strpago & ") no es valida para aplicar los descuentos de la promoción: (" & EsNuloT(rsADO!Promocion) & ")." & vbCrLf
    '                        ElseIf strpagoDetalle <> EsNuloT(rsADO!Descripcion_pago) And blnOrdenFPDetalle Then                            
    '                            strMensaje = "La forma de Pago seleccionada (" & strpago & ") no es valida para aplicar los descuentos de la promoción: (" & EsNuloT(rsADO!Promocion) & ")." & vbCrLf
    '                        ElseIf strpago = EsNuloT(rsADO!Descripcion_pago) And Not blnOrdenFPDetalle Then
    '                            blnFormaCorrecta = True
    '                        ElseIf strpagoDetalle = EsNuloT(rsADO!Descripcion_pago) And blnOrdenFPDetalle Then
    '                            blnFormaCorrecta = True
    '                        End If
    '                        rsADO.MoveNext()
    '                    End While

    '                    If blnFormaCorrecta Then                        
    '                        If strpago = "MODDOCARD" And gblDirFtp = FTP_SPRINGSTEP And strTipoFRM = "PAGOS" Then
    '                            strFP_Promos = GetFormasDePagoPromo(EsNuloN(2), EsNuloN(idOrdenFPDetalle), strFP)
    '                            Resp = MsgBox("¿ La tarjeta a realizar el cobro es una tarjeta promocionada entre estas: " & vbCrLf & strFP_Promos & " ?", vbQuestion + vbYesNo + vbDefaultButton2, gstrInforma)
    '                            If Resp = vbYes Then
    '                                BuscaFPpromos = False
    '                            Else
    '                                strMensaje = "La forma de Pago seleccionada (" & strpago & ") no es valida para aplicar los descuentos de la promoción." & vbCrLf
    '                                If strMensaje <> "" Then MsgBox(strMensaje & "Revise los descuentos de la artículos.", vbExclamation + vbOKOnly, gstrInforma)
    '                                BuscaFPpromos = True
    '                            End If
    '                        Else
    '                            BuscaFPpromos = False
    '                        End If

    '                    Else
    '                        BuscaFPpromos = True
    '                        If strMensaje <> "" Then MsgBox(strMensaje & "Revise los descuentos de la artículos.", vbExclamation + vbOKOnly, gstrInforma)
    '                        If strTipoFRM <> "PAGOS" Then
    '                            Resp = MsgBox("¿ Desea continuar con la forma de pago seleccionado y borrar todos los descuentos aplicados a los artículos por esta Promoción ?", vbQuestion + vbYesNo + vbDefaultButton2, gstrInforma)
    '                            If Resp = vbYes Then
    '                                BuscaFPpromos = False
    '                                With frmTickets.mshRejilla
    '                                    lngI = 1
    '                                    Do Until lngI > .Rows - 1
    '                                        '*** BORRA DESCUENTOS ASOCIADOS A TODA LA REJILLA DE ARTICULOS DE LA PROMO ***
    '                                        For intDto = 1 To UBound(arrDto)
    '                                            If arrDto(intDto).intPosicion = lngI And arrDto(intDto).strFPagoDetalle = "GESTOR PROMOCIONES" And arrDto(intDto).blnActivo Then
    '                                                Call InicializarDto(lngI, False, False, False)
    '                                                ' ABM 26/10/2012
    '                                                frmTickets.mshRejilla.TextMatrix(lngI, 6) = FormatoEuros(EsNuloN(frmTickets.mshRejilla.TextMatrix(lngI, 10)), True, True)
    '                                                frmTickets.mshRejilla.TextMatrix(lngI, 9) = FormatoEuros(0)
    '                                                frmTickets.mshRejilla.TextMatrix(lngI, 12) = FormatoEuros(0, True, True)
    '                                                frmTickets.mshRejilla.TextMatrix(lngI, 13) = ""
    '                                                frmTickets.mshRejilla.Refresh()
    '                                                frmTickets.Refresh()
    '                                            End If
    '                                        Next

    '                                        lngI = lngI + 1
    '                                    Loop
    '                                End With


    '                                frmTickets.CalculaTotalVenta(False)

    '                            End If
    '                        End If
    '                    End If
    '                Else
    '                    If gblDirFtp = FTP_SPRINGSTEP And strTipoFRM = "PAGOS" And Not blnTodasFP Then
    '                        strMensaje = "La forma de Pago seleccionada (" & strpago & ") no es valida para aplicar los descuentos de la promoción." & vbCrLf
    '                        If strMensaje <> "" Then MsgBox(strMensaje & "Revise los descuentos de la artículos.", vbExclamation + vbOKOnly, gstrInforma)
    '                        BuscaFPpromos = True
    '                    Else
    '                        BuscaFPpromos = False
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Exit Function
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "BuscaFPpromos", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    '    Public Function GetTarjetaFidelidadRumbo(ByRef cnADO As ADODB.Connection, ByVal lngClienteID As Long) As String

    '        Dim rsADO As New ADODB.Recordset
    '        Dim strSQL As String

    '        GetTarjetaFidelidadRumbo = ""
    '        strSQL = "SELECT NumTarjeta FROM N_CLIENTES_TARJETAS_FIDELIDAD  WITH (NOLOCK)  " & _
    '                 clsBD.SQL_POR_FECHAS(" WHERE ", "ISNULL(FechaCaducidad,'31/12/2999')", gdteFechaSesion, "") & " And Idcliente= " & lngClienteID & _
    '                 " AND ISNULL(IdBaja,0)=0 Order BY FechaEmision"

    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetTarjetaFidelidadRumbo = EsNuloT(rsADO(0))
    '        End If
    '        CloseRS(rsADO)


    '    End Function


    '    Public Function GetSellosTarjetaFidelidadRumbo(ByRef cnADO As ADODB.Connection, ByVal lngClienteID As Long) As Long

    '        Dim rsADO As New ADODB.Recordset
    '        Dim strSQL As String

    '        GetSellosTarjetaFidelidadRumbo = 0
    '        strSQL = "SELECT Sellos FROM N_CLIENTES_TARJETAS_FIDELIDAD WITH (NOLOCK)  " & _
    '                 clsBD.SQL_POR_FECHAS(" WHERE ", "ISNULL(FechaCaducidad,'31/12/2999')", gdteFechaSesion, "") & " And Idcliente= " & lngClienteID & _
    '                 " AND ISNULL(IdBaja,0)=0 Order BY FechaEmision"

    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetSellosTarjetaFidelidadRumbo = EsNuloN(rsADO(0))
    '        End If
    '        CloseRS(rsADO)


    '    End Function


    '    Public Function GuardoArticulosPromosRumbo(ByVal strIdTicket As String, ByVal strNumTarjeta As String, ByVal cnADO As ADODB.Connection)

    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim intJ, intI As Long

    '        On Error GoTo ReparaErrores

    '        'Insertar Articulos que se aplicara descuentos con el Gestor de Promos
    '        If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_RUMBO Then
    '            intJ = 1
    '            cnADO.Execute("INSERT INTO N_TICKETS_PROMOCIONES(Id_Ticket,IdOrden,Id_Tienda,Id_Empleado,Id_Cliente_N,Fecha," & _
    '                " Pvp_Vig,ImporteDescuento,ImporteDtoPor,ImporteEuros,NumTarjeta) " & _
    '                "Select Id_Ticket, 1 as IdOrden,Id_Tienda,Id_Empleado,Id_Cliente_N,Fecha,TotalEuro,DescuentoEuro, 0 as ImporteDtoPor,TotalEuro,'" & strNumTarjeta & "' as NumTarjeta From " & _
    '                " N_TICKETS WHERE Id_Ticket= '" & strIdTicket & "'", , adExecuteNoRecords)
    '        End If


    'ReparaErrores:
    '        CloseRS(rsADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GuardoArticulosPromosRumbo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    '    Public Function GuardoArticulosPromosCupones(ByVal strIdTicket As String, ByVal dblTotalVale As Double, ByVal cnADO As ADODB.Connection, ByVal lngTipoPromocion As Long)

    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim intJ, intI As Long

    '        On Error GoTo ReparaErrores

    '        'Insertar Articulos que se aplicara descuentos con el Gestor de Promos
    '        If (Trim(BuscaConfiguraciones("PROMOCIONESTPV"))) = "1" Then
    '            intJ = 1
    '            cnADO.Execute("INSERT INTO N_TICKETS_PROMOCIONES(Id_Ticket,IdOrden,Id_Tienda,Id_Empleado,Id_Cliente_N,Id_Promocion,Fecha," & _
    '                " Pvp_Vig,ImporteDescuento,ImporteDtoPor,ImporteEuros,NumTarjeta) " & _
    '                "Select Id_Ticket, 1 as IdOrden,Id_Tienda,Id_Empleado,Id_Cliente_N," & EsNuloN(lngTipoPromocion) & ",Fecha,TotalEuro," & SinComas(EsNuloN(dblTotalVale)) & ", 0 as ImporteDtoPor,TotalEuro,'' as NumTarjeta From " & _
    '                " N_TICKETS WHERE Id_Ticket= '" & strIdTicket & "'", , adExecuteNoRecords)
    '        End If


    'ReparaErrores:
    '        CloseRS(rsADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GuardoArticulosPromosCupones", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Function BuscaArticulos(ByVal lngIdpromocion As Long, ByVal lngRegRecalculo As Long, ByRef objPromo As IEnumerable(Of Promocion)) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim lngRegRejilla As Long
        Dim blnTallasC As Boolean
        Dim lngCountFila As Long


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulos = 0 : blnTallasC = False : lngCountFila = 0

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)

            '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
            If gblLngIdPromocionTodos Then
                For Each objpro As Promocion In objPromo
                    BuscaArticulos = BuscaArticulos + 1
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                Next


            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS DE LA PROMO A VERIFICAR activos y validoz
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                        clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' Or ISNULL(ARTB.Id_Cabecero_Detalle,'') ='') " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' " & _
                        " And P.fkTipoPromocion in (10,11,13,15,16,17,18,19,20,22,23,24,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows
                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                BuscaArticulos = BuscaArticulos + 1
                            End If

                        Next
                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                Next
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulos " & ex.Message, ex.InnerException)
        End Try

    End Function

    '    Public Function ValidoPromosValidasImpresionSinImagen(ByVal strTicket As String) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim dblDto As Double
    '        Dim strPromoTodosArticulos As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        ValidoPromosValidasImpresionSinImagen = 0
    '        'TENEMOS
    '        ' ABM 25/10/2012 añado en P.fkTipoPromocion in el tipo de promocion 27
    '        ' ABM 31/10/2012 añado en P.fkTipoPromocion in el tipo de promocion 28

    '        '******BUSCAMOS PROMOCIONES CON TODOS LOAS ARTICULOS ACTIVOS
    '        strPromoTodosArticulos = GetPromosTodosArticulos("IMPRIMIRTEXTOPROMOCIONES")

    '        If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_SPRINGSTEP Then
    '            strSQL = "Select distinct P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion,P.TextoAvisoPrevio,P.FechaValidezInicio " & _
    '                " from tbPR_promociones P WITH (NOLOCK) Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                " Inner join tbPR_tiendasBeneficiarias T ON P.IdPromocion= T.fkPromocion " & _
    '                " Inner join (select distinct ARTB.fkPromocion from tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) inner join " & _
    '                " (SELECT TD.* FROM N_TICKETS_DETALLES TD WITH (NOLOCK) INNER JOIN  N_TICKETS T WITH (NOLOCK) ON TD.Id_Auto = T.Id_Auto AND TD.Id_Tienda=T.Id_Tienda " & _
    '                " WHERE T.Id_Ticket='" & EsNuloT(strTicket) & "') TD " & _
    '                " ON ARTB.fkArticulo=TD.Id_Articulo) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
    '                " where P.fechaComunicacionInicio <= '" & EC(gdteFechaSesion) & "' And P.fechaComunicacionFin >= '" & EC(gdteFechaSesion) & _
    '                "' and T.fkTienda='" & EC(gstrTiendaSesion) & "' And P.MostrarImagenAvisoPrevio=0 And P.comunicacionImpresionTienda=1 And P.fkTipoPromocion in (2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) "

    '        Else
    '            strSQL = "Select distinct P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion,P.TextoAvisoPrevio,P.FechaValidezInicio " & _
    '                " from tbPR_promociones P WITH (NOLOCK) Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                " Inner join (select distinct ARTB.fkPromocion from tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) inner join " & _
    '                " (SELECT TD.* FROM N_TICKETS_DETALLES TD WITH (NOLOCK) INNER JOIN  N_TICKETS T WITH (NOLOCK) ON TD.Id_Auto = T.Id_Auto AND TD.Id_Tienda=T.Id_Tienda " & _
    '                " WHERE T.Id_Ticket='" & EsNuloT(strTicket) & "' And MotivoCambioPrecio like '%GESTOR PROMOCIONES%') TD " & _
    '                " ON ARTB.fkArticulo=TD.Id_Articulo) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
    '                " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '                "' and T.fkTienda='" & EC(gstrTiendaSesion) & "' And P.MostrarImagenAvisoPrevio=0 And P.comunicacionImpresionTienda=1 And P.fkTipoPromocion in (2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) "


    '        End If



    '        If strPromoTodosArticulos <> "" Then

    '            If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_SPRINGSTEP Then
    '                strSQL = strSQL & " UNION " & _
    '                     " Select distinct P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion,P.TextoAvisoPrevio,P.FechaValidezInicio " & _
    '                     " from tbPR_promociones P WITH (NOLOCK) Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                     " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                     " where P.fechaComunicacionInicio <= '" & EC(gdteFechaSesion) & "' And P.fechaComunicacionFin >= '" & EC(gdteFechaSesion) & _
    '                     "' and T.fkTienda='" & EC(gstrTiendaSesion) & "' And P.MostrarImagenAvisoPrevio=0 And P.comunicacionImpresionTienda=1 And P.IdPromocion in " & EsNuloT(strPromoTodosArticulos) & " "
    '            Else
    '                strSQL = strSQL & " UNION " & _
    '                     " Select distinct P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion,P.TextoAvisoPrevio,P.FechaValidezInicio " & _
    '                     " from tbPR_promociones P WITH (NOLOCK) Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                     " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                     " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '                     "' and T.fkTienda='" & EC(gstrTiendaSesion) & "' And P.MostrarImagenAvisoPrevio=0 And P.comunicacionImpresionTienda=1 And P.IdPromocion in " & EsNuloT(strPromoTodosArticulos) & " "

    '            End If

    '            strSQL = strSQL & " order by P.FechaValidezInicio"

    '        Else
    '            strSQL = strSQL & " order by P.FechaValidezInicio"
    '        End If

    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                ValidoPromosValidasImpresionSinImagen = ValidoPromosValidasImpresionSinImagen + 1
    '                rsADO.MoveNext()
    '            End While
    '        Else
    '            ValidoPromosValidasImpresionSinImagen = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoPromosValidasImpresionSinImagen", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Function BuscaArticulosBeneficiario(ByVal lngIdpromocion As Long, ByVal txtCI As Long, ByVal txtIdTalla As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal lngRegRecalculo As Long, Optional ByVal lngTipoPromo As Long = 3) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim blnTallasC As Boolean
        Dim lngRegRejilla As Long        
        Dim lngCountFila As Long

        Try


            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulosBeneficiario = 0 : blnTallasC = False : lngRegRejilla = 0 : lngCountFila = 0

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)


            '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
            If gblLngIdPromocionTodos Then
                For Each objpro As Promocion In objPromo
                    BuscaArticulosBeneficiario = BuscaArticulosBeneficiario + (clsMU.EsNuloN(objpro.Unidades))
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                Next
            Else
                For Each objpro As Promocion In objPromo
                    ' 04/09/2013 añadir campo descuentocascada
                    'TENEMOS LOS ARTICULOS BENEFICIARIOS DE LA PROMO A VERIFICAR activos y validoz
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL( ARTB.Id_Cabecero_Detalle,'')='' )" & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"

                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows
                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                BuscaArticulosBeneficiario = BuscaArticulosBeneficiario + (clsMU.EsNuloN(objpro.Unidades))
                            End If
                        Next
                    End If
                    dtrsADO.Dispose()
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                Next
            End If

            'Verificamos si el artículo es Beneficiario


            '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
            If gblLngIdPromocionTodos Then
                blnArtBeneficiario = True
            Else
                ' 04/09/2013 añadir campo descuentocascada
                strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                    " from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(txtCI) & " And ( ARTB.Id_Cabecero_Detalle Like '%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"

                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    blnTallasC = False
                    '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                    If strTallasValidas <> "" Then
                        If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(txtIdTalla)) Then
                            blnTallasC = True
                        End If
                    End If

                    If strTallasValidas = "" Or blnTallasC Then
                        blnArtBeneficiario = True
                    Else
                        blnArtBeneficiario = False
                    End If
                Else
                    blnArtBeneficiario = False
                End If
            End If


        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulosBeneficiario " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function BuscaArticulosDonante(ByVal lngIdpromocion As Long, ByVal txtCI As Long, ByVal txtIdTalla As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal lngRegRecalculo As Long, Optional ByVal lngTipoPromo As Long = 3) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim blnTallasC As Boolean
        Dim lngRegRejilla As Long
        Dim lngCountFila As Long


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulosDonante = 0 : blnTallasC = False : lngRegRejilla = 0 : lngCountFila = 0

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)

            For Each objpro As Promocion In objPromo
                'TENEMOS LOS ARTICULOS DONANTES DE LA PROMO A VERIFICAR activos y validoz
                ' 04/09/2013 añadir campo descuentocascada
                strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                    " from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " Inner join tbPR_articulosDonantes ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                    clsMU.EsNuloN(objpro.Id_Articulo) & " And (ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'') ='' )" & _
                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"

                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    For Each FilaI In dtrsADO.Tables(0).Rows
                        blnTallasC = False
                        '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                        If strTallasValidas <> "" Then
                            If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                blnTallasC = True
                            End If
                        End If

                        If strTallasValidas = "" Or blnTallasC Then
                            BuscaArticulosDonante = BuscaArticulosDonante + 1
                        End If
                    Next
                End If
                dtrsADO.Dispose()
                If lngCountFila = lngRegRejilla Then Exit For
                lngCountFila += 1
            Next


            'Verificamos si el artículo es donante
            ' 04/09/2013 añadir campo descuentocascada
            strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                " from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " Inner join tbPR_articulosDonantes ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(txtCI) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'') = '' ) " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " Order by P.FechaValidezInicio"
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                If strTallasValidas <> "" Then
                    If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(txtIdTalla)) Then
                        blnTallasC = True
                    End If
                End If

                If strTallasValidas = "" Or blnTallasC Then
                    blnArtDonante = True
                Else
                    blnArtDonante = False
                End If
            Else
                blnArtDonante = False
            End If


        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulosDonante " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Sub InsertoArtDonantesDescuento(ByVal lngIdpromocion As Long, ByVal lngTipoPromocion As Long, ByRef objPromo As IEnumerable(Of Promocion))

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim blnTallasC As Boolean
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            blnTallasC = False

            For Each objpro As Promocion In objPromo
                'TENEMOS LOS ARTICULOS DONANTES
                ' 04/09/2013 añadir campo descuentocascada
                strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                    " from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " Inner join tbPR_articulosDonantes ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                    clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK) ON P.IdPromocion=  RDPI.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")
                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    For Each FilaI In dtrsADO.Tables(0).Rows
                        blnTallasC = False
                        '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                        If strTallasValidas <> "" Then
                            If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                blnTallasC = True
                            End If
                        End If

                        If strTallasValidas = "" Or blnTallasC Then

                            If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                                If lngTipoPromocion = 3 Then
                                    '*** PROMO REGALO DESCUENTO 100%  ***
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(100))
                                ElseIf lngTipoPromocion = 21 Then
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(FilaI.item("Descuento")))
                                ElseIf lngTipoPromocion = 12 Then
                                    objpro.Pvp_Venta = clsMU.EsNuloN(FilaI.item("Descuento"))
                                ElseIf lngTipoPromocion = 42 Then
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(FilaI.item("Descuento")))
                                End If

                                '**** REDONDEO PROMO ****
                                If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                    Select Case gblfkRedondeo
                                        'Case 1, 2 ' redondeo por exceso
                                        ' 15/11/2012
                                        Case 2 ' redondeo por exceso
                                            objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                        Case 3 ' redondeo por defecto                                        
                                            objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                    End Select
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                End If

                                objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                If objpro.Promo Is Nothing Then
                                    objPD.DescriPromo = strDescPromo.ToString
                                    objPD.DescriAmpliaPromo = strDescPromo.ToString
                                    objPD.DtoPromo = objpro.DtoEuro
                                    objPD.Idpromo = lngIdpromocion
                                    objPD.PromocionSelecionada = True

                                    ppD.Add(objPD)
                                    objpro.Promo = ppD.AsEnumerable()

                                End If

                                Exit For
                            End If
                        End If

                    Next
                End If
            Next

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtDonantesDescuento " & ex.Message, ex.InnerException)
        End Try

    End Sub

    Public Function InsertoArtBeneficiarioDescuentoMenor(lngIdpromocion As Long, ByVal lngTipoPromocion As Long, _
                    ByVal lngRegRecalculo As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal dblPVPVig As Double, Optional blnPrimeraB As Boolean = False) As Boolean


        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim dblDescuento As Double
        Dim strPromocion As String
        Dim blnSigueDescuento As Boolean
        Dim lngRegRejilla As Long
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim lngCountFila As Long
        Dim lngCountMenor As Long = 0
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueDescuento = False : blnTallasC = False : lngCountFila = 0
            lngCountMenor = 1

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)


            '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
            If gblLngIdPromocionTodos Then
                For Each objpro As Promocion In objPromo
                    If objpro.DtoEuro = 0 Then
                        If IntMenor = 0 Then IntMenor = lngCountMenor : dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                        If clsMU.EsNuloN(objpro.Pvp_Vig) <= dblPrecioMenor Then
                            IntMenor = lngCountMenor
                            dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                            dblDescuento = GetDctoPromo(lngIdpromocion, lngTipoPromocion)
                            strPromocion = GetNombrePromo(lngIdpromocion, lngTipoPromocion)
                        End If
                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    lngCountMenor += 1
                Next
            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                       clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows

                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                'objpro.Promo = Nothing
                                If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                                    If IntMenor = 0 Then IntMenor = lngCountMenor : dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                                    If clsMU.EsNuloN(objpro.Pvp_Vig) <= dblPrecioMenor Then
                                        IntMenor = lngCountMenor
                                        dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                                        dblDescuento = clsMU.EsNuloN(FilaI.item("Descuento"))
                                        strPromocion = clsMU.EsNuloT(FilaI.item("Promocion"))
                                    End If
                                End If
                            End If

                        Next
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                        lngCountMenor += 1
                    Else
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                        lngCountMenor += 1
                    End If
                Next

            End If


            'valido que si es el que entra menor valor asigno el descuento
            If IntMenor <> 0 And dblPrecioMenor <= clsMU.EsNuloN(dblPVPVig) Then
                blnSigueDescuento = True
            ElseIf IntMenor <> 0 And blnPrimeraB Then
                blnSigueDescuento = True
            ElseIf IntMenor <> 0 And dblPrecioMenor > clsMU.EsNuloN(dblPVPVig) Then
                blnSigueDescuento = False
            End If

            If gblstrTipoPromocion = "RECALCULO" Then
                blnSigueDescuento = True
            End If


            If IntMenor <> 0 And blnSigueDescuento Then
                lngCountFila = 0 : lngCountMenor = 1
                For Each objpro As Promocion In objPromo
                    'objpro.Promo = Nothing
                    If lngCountMenor = IntMenor Then
                        If lngTipoPromocion = 13 Or lngTipoPromocion = 22 Or lngTipoPromocion = 23 Or lngTipoPromocion = 24 Or lngTipoPromocion = 27 Then
                            objpro.Pvp_Venta = dblDescuento
                        ElseIf lngTipoPromocion = 11 Or lngTipoPromocion = 16 Or lngTipoPromocion = 18 Or lngTipoPromocion = 20 Or lngTipoPromocion = 36 Then
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDescuento)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDescuento))
                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                            End If
                        End If

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                        ppD = New List(Of DetallePromo)
                        objpro.Promo = Nothing
                        If objpro.Promo Is Nothing Then
                            objPD.DescriPromo = strPromocion.ToString
                            objPD.DescriAmpliaPromo = strPromocion.ToString
                            objPD.DtoPromo = dblDescuento
                            objPD.Idpromo = lngIdpromocion
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()

                        End If

                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    lngCountMenor += 1
                Next
            End If

            InsertoArtBeneficiarioDescuentoMenor = blnSigueDescuento
            Return InsertoArtBeneficiarioDescuentoMenor

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtBeneficiarioDescuentoMenor " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Sub InsertoArtBeneficiarioDescuentoRestricciones(ByVal lngIdpromocion As Long, ByVal dblDto As Double, _
                            ByVal strPtromocion As String, ByRef objPromo As IEnumerable(Of Promocion))

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try


            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueBusqueda = False : blnTallasC = False

            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()


            If blnArticulosBeneficiarios Then

                '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
                If gblLngIdPromocionTodos Then
                    For Each objpro As Promocion In objPromo
                        If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDto))
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                            End If

                            '**** REDONDEO PROMO ****
                            If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                Select Case gblfkRedondeo
                                    'Case 1, 2 ' redondeo por exceso
                                    ' 15/11/2012
                                    Case 2 ' redondeo por exceso
                                        objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                    Case 3 ' redondeo por defecto                                        
                                        objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                End Select
                                objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                            End If

                            objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)


                            '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                            If objpro.Promo Is Nothing Then
                                objPD.DescriPromo = strPromocion.ToString
                                objPD.DescriAmpliaPromo = strPromocion.ToString
                                objPD.DtoPromo = dblDto
                                objPD.Idpromo = lngIdpromocion
                                objPD.PromocionSelecionada = True

                                ppD.Add(objPD)
                                objpro.Promo = ppD.AsEnumerable()

                            End If


                        End If
                    Next
                Else
                    For Each objpro As Promocion In objPromo
                        If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                            'TENEMOS LOS ARTICULOS BENEFICIARIOS
                            ' 04/09/2013 añadir campo descuentocascada
                            strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                                " from tbPR_promociones P WITH (NOLOCK) " & _
                                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                                " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                                objpro.Id_Articulo & " And ( ARTB.Id_Cabecero_Detalle like '%" & objpro.Id_cabecero_detalle & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='') " & _
                                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=2  and P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                                For Each FilaI In dtrsADO.Tables(0).Rows
                                    blnTallasC = False
                                    '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                                    If strTallasValidas <> "" Then
                                        If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                            blnTallasC = True
                                        End If
                                    End If

                                    If strTallasValidas = "" Or blnTallasC Then

                                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                            objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(FilaI.Item("Descuento")))
                                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                            dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(FilaI.Item("Descuento")))
                                            dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                            objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                                        End If


                                        '**** REDONDEO PROMO ****
                                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                            Select Case gblfkRedondeo
                                                'Case 1, 2 ' redondeo por exceso
                                                ' 15/11/2012
                                                Case 2 ' redondeo por exceso
                                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                                Case 3 ' redondeo por defecto                                        
                                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                            End Select
                                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                        End If

                                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                        If objpro.Promo Is Nothing Then
                                            objPD.DescriPromo = strPromocion.ToString
                                            objPD.DescriAmpliaPromo = strPromocion.ToString
                                            objPD.DtoPromo = dblDto
                                            objPD.Idpromo = lngIdpromocion
                                            objPD.PromocionSelecionada = True

                                            ppD.Add(objPD)
                                            objpro.Promo = ppD.AsEnumerable()

                                        End If

                                    End If

                                Next
                            End If
                            dtrsADO.Dispose()
                        End If
                    Next
                End If
            Else
                For Each objpro As Promocion In objPromo
                    If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                        'TENEMOS LOS ARTICULOS BENEFICIARIOS
                        ' 04/09/2013 añadir campo descuentocascada
                        strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada " & _
                            " from tbPR_promociones P WITH (NOLOCK) " & _
                            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                            " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=2  and P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                        If dtrsADO.Tables(0).Rows.Count <> 0 Then
                            For Each FilaI In dtrsADO.Tables(0).Rows

                                '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(FilaI.Item("Descuento")))
                                ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                    dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(FilaI.Item("Descuento")))
                                    dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                                End If

                                '**** REDONDEO PROMO ****
                                If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                    Select Case gblfkRedondeo
                                        'Case 1, 2 ' redondeo por exceso
                                        ' 15/11/2012
                                        Case 2 ' redondeo por exceso
                                            objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                        Case 3 ' redondeo por defecto                                        
                                            objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                    End Select
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                End If

                                objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                If objpro.Promo Is Nothing Then
                                    objPD.DescriPromo = strPromocion.ToString
                                    objPD.DescriAmpliaPromo = strPromocion.ToString
                                    objPD.DtoPromo = dblDto
                                    objPD.Idpromo = lngIdpromocion
                                    objPD.PromocionSelecionada = True

                                    ppD.Add(objPD)
                                    objpro.Promo = ppD.AsEnumerable()

                                End If
                            Next
                        End If
                        dtrsADO.Dispose()
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtBeneficiarioDescuentoRestricciones " & ex.Message, ex.InnerException)
        End Try

    End Sub


    '    Public Function DesactivarValesPromosCupones()

    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim intJ, intI As Long
    '        Dim FechaDesde, FechaHasta As String
    '        Dim arrFecha
    '        Dim strObservaciones As String
    '        Dim cnADO As ADODB.Connection

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        'Ubicamos los Vales generados por el Gestor de Promociones y Que no esten Cobrados
    '        '***** 1. DESACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE INICIAL MENOR A LA FECHA DE SISTEMA *****
    '        strSQL = "SELECT * FROM N_VALES WITH (NOLOCK) WHERE Observaciones LIKE '%Bono Descuento Canjeable Del%' " & _
    '         "  And Cobrado=0 And isnull(Activo,1)=1 And ISNULL(FechaCobrado,'') ='' And ISNULL(Id_Tienda_Cobro,'')=''  "
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                FechaDesde = "" : FechaHasta = "" : strObservaciones = ""
    '                If EsNuloT(rsADO!Observaciones) <> "" Then
    '                    If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_RUMBO Then
    '                        strObservaciones = Replace(EsNuloT(rsADO!Observaciones), "Club de Clientes Rumbo ", "")
    '                        strObservaciones = Mid(EsNuloT(strObservaciones), 30, Len(rsADO!Observaciones))
    '                    Else
    '                        strObservaciones = Mid(EsNuloT(rsADO!Observaciones), 30, Len(rsADO!Observaciones))
    '                    End If
    '                    arrFecha = Split(strObservaciones, " Al ")
    '                    If UBound(arrFecha) > 0 Then
    '                        FechaDesde = arrFecha(0)
    '                        If InStr(arrFecha(1), "[") > 0 Then
    '                            FechaHasta = Mid(arrFecha(1), 1, InStr(arrFecha(1), "[") - 1)
    '                            FechaHasta = Replace(FechaHasta, ".", "")
    '                        Else
    '                            FechaHasta = Replace(arrFecha(1), ".", "")
    '                        End If
    '                        If gdteFechaSesion < FechaDesde Then
    '                            'DESACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE INICIAL MENOR A LA FECHA DE SISTEMA                            
    '                            strSQL = "UPDATE N_VALES SET Activo=0 WHERE Id_Vale=" & EsNuloN(rsADO!Id_Vale) & " AND Id_Tienda='" & EsNuloT(rsADO!Id_tienda) & "'"
    '                            cnADO.Execute(strSQL, , adExecuteNoRecords)
    '                        End If
    '                    End If
    '                End If
    '                rsADO.MoveNext()
    '            End While
    '        End If

    '        'Ubicamos los Vales generados por el Gestor de Promociones y que no esten Desactivados
    '        '***** 2. ACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE INICIAL MAYOR-IGUAL A LA FECHA DE SISTEMA *****
    '        strSQL = "SELECT * FROM N_VALES WITH (NOLOCK) WHERE Observaciones LIKE '%Bono Descuento Canjeable Del%' " & _
    '         "  And Cobrado=0 And isnull(Activo,1)=0 And ISNULL(FechaCobrado,'') ='' And ISNULL(Id_Tienda_Cobro,'')=''  "
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                FechaDesde = "" : FechaHasta = "" : strObservaciones = ""
    '                If EsNuloT(rsADO!Observaciones) <> "" Then
    '                    If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_RUMBO Then
    '                        strObservaciones = Replace(EsNuloT(rsADO!Observaciones), "Club de Clientes Rumbo ", "")
    '                        strObservaciones = Mid(EsNuloT(strObservaciones), 30, Len(rsADO!Observaciones))
    '                    Else
    '                        strObservaciones = Mid(EsNuloT(rsADO!Observaciones), 30, Len(rsADO!Observaciones))
    '                    End If
    '                    arrFecha = Split(strObservaciones, " Al ")
    '                    If UBound(arrFecha) > 0 Then
    '                        FechaDesde = arrFecha(0)
    '                        If InStr(arrFecha(1), "[") > 0 Then
    '                            FechaHasta = Mid(arrFecha(1), 1, InStr(arrFecha(1), "[") - 1)
    '                            FechaHasta = Replace(FechaHasta, ".", "")
    '                        Else
    '                            FechaHasta = Replace(arrFecha(1), ".", "")
    '                        End If
    '                        If gdteFechaSesion >= FechaDesde And gdteFechaSesion <= FechaHasta Then
    '                            'ACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE INICIAL MAYOR-IGUAL A LA FECHA DE SISTEMA                            
    '                            strSQL = "UPDATE N_VALES SET Activo=1 WHERE Id_Vale=" & EsNuloN(rsADO!Id_Vale) & " AND Id_Tienda='" & EsNuloT(rsADO!Id_tienda) & "'"
    '                            cnADO.Execute(strSQL, , adExecuteNoRecords)
    '                        End If
    '                    End If
    '                End If
    '                rsADO.MoveNext()
    '            End While
    '        End If


    '        'Ubicamos los Vales generados por el Gestor de Promociones y Que no esten Cobrados
    '        '***** 3. DESACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE FINAL SUPERIOR A LA FECHA DE SISTEMA *****
    '        strSQL = "SELECT * FROM N_VALES WITH (NOLOCK) WHERE Observaciones LIKE '%Bono Descuento Canjeable Del%' " & _
    '         "  And Cobrado=0 And isnull(Activo,1)=1 And ISNULL(FechaCobrado,'') ='' And ISNULL(Id_Tienda_Cobro,'')=''  "
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                FechaDesde = "" : FechaHasta = "" : strObservaciones = ""
    '                If EsNuloT(rsADO!Observaciones) <> "" Then
    '                    If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_RUMBO Then
    '                        strObservaciones = Replace(EsNuloT(rsADO!Observaciones), "Club de Clientes Rumbo ", "")
    '                        strObservaciones = Mid(EsNuloT(strObservaciones), 30, Len(rsADO!Observaciones))
    '                    Else
    '                        strObservaciones = Mid(EsNuloT(rsADO!Observaciones), 30, Len(rsADO!Observaciones))
    '                    End If
    '                    arrFecha = Split(strObservaciones, " Al ")
    '                    If UBound(arrFecha) > 0 Then
    '                        FechaDesde = arrFecha(0)
    '                        If InStr(arrFecha(1), "[") > 0 Then
    '                            FechaHasta = Mid(arrFecha(1), 1, InStr(arrFecha(1), "[") - 1)
    '                            FechaHasta = Replace(FechaHasta, ".", "")
    '                        Else
    '                            FechaHasta = Replace(arrFecha(1), ".", "")
    '                        End If
    '                        If gdteFechaSesion > FechaHasta Then
    '                            'DESACTIVAMOS LOS VALES DE PROMOS ACTIVOS CON FECHA DE CANJE FINAL SUPERIOR A LA FECHA DE SISTEMA                            
    '                            strSQL = "UPDATE N_VALES SET Activo=0 WHERE Id_Vale=" & EsNuloN(rsADO!Id_Vale) & " AND Id_Tienda='" & EsNuloT(rsADO!Id_tienda) & "'"
    '                            cnADO.Execute(strSQL, , adExecuteNoRecords)
    '                        End If
    '                    End If
    '                End If
    '                rsADO.MoveNext()
    '            End While
    '        End If


    'ReparaErrores:
    '        CloseRS(rsADO) : CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "DesactivarValesPromosCupones", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Sub InsertoArtBeneficiarioDescuentoRestriccionesTG(ByVal lngIdpromocion As Long, ByVal strCodigoBono As String, _
                ByVal strPtromocion As String, ByVal dblDto As Double, ByRef objPromo As IEnumerable(Of Promocion))


        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim lngDescuentoCalculo As Double
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueBusqueda = False : blnTallasC = False


            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()


            If blnArticulosBeneficiarios Then
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & objpro.Id_Articulo & " And (ARTB.Id_Cabecero_Detalle like '%" & objpro.Id_cabecero_detalle & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=2  and P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows
                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                If strCodigoBono <> "" Then
                                    lngDescuentoCalculo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, lngIdpromocion, objPromo))
                                    objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(lngDescuentoCalculo))
                                Else

                                    '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                    If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                        objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(FilaI.Item("Descuento")))
                                    ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                        dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(FilaI.Item("Descuento")))
                                        dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                        objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                                    End If

                                End If

                                '**** REDONDEO PROMO ****
                                If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                    Select Case gblfkRedondeo
                                        'Case 1, 2 ' redondeo por exceso
                                        ' 15/11/2012
                                        Case 2 ' redondeo por exceso
                                            objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                        Case 3 ' redondeo por defecto                                        
                                            objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                    End Select
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                End If

                                objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                If objpro.Promo Is Nothing Then
                                    objPD.DescriPromo = strPromocion.ToString
                                    objPD.DescriAmpliaPromo = strPromocion.ToString
                                    objPD.DtoPromo = dblDto
                                    objPD.Idpromo = lngIdpromocion
                                    objPD.PromocionSelecionada = True

                                    ppD.Add(objPD)
                                    objpro.Promo = ppD.AsEnumerable()

                                End If

                            End If
                        Next
                    End If
                    dtrsADO.Dispose()
                Next

            Else
                For Each objpro As Promocion In objPromo

                    If strCodigoBono <> "" Then
                        lngDescuentoCalculo = clsMU.EsNuloN(BuscoImporteDescuentoTG(strCodigoBono, lngIdpromocion, objPromo))
                        objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(lngDescuentoCalculo))
                    Else
                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                            objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDto))
                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                            dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDto))
                            dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                            objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                        End If
                    End If

                    '**** REDONDEO PROMO ****
                    If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                        Select Case gblfkRedondeo
                            'Case 1, 2 ' redondeo por exceso
                            ' 15/11/2012
                            Case 2 ' redondeo por exceso
                                objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                            Case 3 ' redondeo por defecto                                        
                                objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                        End Select
                        objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                    End If

                    objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                    '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                    If objpro.Promo Is Nothing Then
                        objPD.DescriPromo = strPromocion.ToString
                        objPD.DescriAmpliaPromo = strPromocion.ToString
                        objPD.DtoPromo = dblDto
                        objPD.Idpromo = lngIdpromocion
                        objPD.PromocionSelecionada = True

                        ppD.Add(objPD)
                        objpro.Promo = ppD.AsEnumerable()

                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtBeneficiarioDescuentoRestriccionesTG " & ex.Message, ex.InnerException)
        End Try

    End Sub


    Public Function BuscoImporteDescuentoTG(ByVal strCodigoBono As String, ByVal lngIdpromocion As Long, ByRef objPromo As IEnumerable(Of Promocion)) As Double


        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblImporteSinDescuento As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim dblImporte As Double
        Dim lngTipo As Long


        Try


            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblImporteSinDescuento = 0 : strPromocion = "" : blnSigueBusqueda = False

            'BUSCAMOS EL BONO PARA APLICAR EL DESCUENTO
            strSQL = "Select cCuponesEuro,Tipo from N_CUPONES_DESCUENTO WITH (NOLOCK) WHERE IdCodigo='" & Trim(strCodigoBono) & "'"
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                dblImporte = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("cCuponesEuro"))
                lngTipo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Tipo"))
            Else
                dblImporte = clsMU.EsNuloN(0)
                lngTipo = clsMU.EsNuloN(0)
            End If

            dtrsADO.Dispose()

            dblImporteSinDescuento = BuscoImporteFlyersTG(lngIdpromocion, objPromo)

            If lngTipo = 0 Then
                BuscoImporteDescuentoTG = (dblImporte * 100) / IIf(clsMU.EsNuloN(dblImporteSinDescuento) <= 0, 1, dblImporteSinDescuento)
            Else
                BuscoImporteDescuentoTG = dblImporte
                blnPrecioFinal = True
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscoImporteDescuentoTG " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function GetPromosTodosArticulos(ByVal strtipoBusqueda As String) As String

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim strIdpromo As String
        Dim blnPromoTodos As Boolean
        Dim intI, I As Long
        Dim intF As Long
        Dim arrPromo

        Try
            Dim clsMU As ModUtilidades = New ModUtilidades

            strIdpromo = "" : blnPromoTodos = True : intI = 0 : I = 0 : intF = 0 : strSQL = ""

            'BUSCAMOS TODAS LA PROMOS ACTIVAS PARA ESTA TIENDA

            If strtipoBusqueda = "PROMOCIONES" Then
                strSQL = "Select * from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' " & _
                    " And P.fkTipoPromocion in(2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) order by P.FechaValidezInicio"
            ElseIf strtipoBusqueda = "IMPRIMIRTEXTOPROMOCIONES" Then
                strSQL = "Select * from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " where P.fechaComunicacionInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.fechaComunicacionFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' " & _
                    " And P.fkTipoPromocion in(2,3,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51) And P.MostrarImagenAvisoPrevio=0 order by P.FechaValidezInicio"
            ElseIf strtipoBusqueda = "CUPONES" Then
                strSQL = "Select * from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' " & _
                    " And P.fkTipoPromocion in(7,34) order by P.FechaValidezInicio"
            ElseIf strtipoBusqueda = "CUPONES_RETENCION" Then
                strSQL = "Select * from tbPR_promociones P WITH (NOLOCK) " & _
                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                    " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' " & _
                    " And P.fkTipoPromocion in(35) order by P.FechaValidezInicio"
            End If

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                For Each filaEan In dtrsADO.Tables(0).Rows
                    strIdpromo = strIdpromo & clsMU.EsNuloN(filaEan.Item("IdPromocion")) & "|"
                    intF = intF + 1
                Next
            Else
                strIdpromo = ""
            End If

            dtrsADO.Dispose()

            '*** VALIDAMOS LAS PROMOS VALIDAS ***

            If clsMU.EsNuloT(strIdpromo) <> "" Then
                If InStrRev(strIdpromo, "|") > 0 Then
                    strIdpromo = Mid(strIdpromo, 1, Len(strIdpromo) - 1)
                    arrPromo = Split(strIdpromo, "|")

                    If UBound(arrPromo) >= 0 Then
                        strIdpromo = ""
                        For I = 0 To UBound(arrPromo)
                            'BUSCAMOS las promo con Activas con todos los articulos en la promo Validos MEGAPROMOCIONES)
                            strSQL = "select COUNT (idarticulobeneficiario) as TOTAL from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkPromocion = " & clsMU.EsNuloN(arrPromo(I))
                            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                                If clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Total")) = 0 Then
                                    blnPromoTodos = True
                                    strIdpromo = strIdpromo & clsMU.EsNuloN(arrPromo(I)) & ","
                                End If
                            Else
                                strIdpromo = ""
                            End If
                            dtrsADO.Dispose()
                        Next I
                        'VAlIDAMOS QUE EXISTA UNA PROMO DE TODOS
                        If blnPromoTodos And clsMU.EsNuloT(strIdpromo) <> "" Then
                            GetPromosTodosArticulos = "(" & Mid(strIdpromo, 1, Len(strIdpromo) - 1) & ")"
                        Else
                            GetPromosTodosArticulos = ""
                        End If
                    Else
                        If intF = 1 Then
                            strIdpromo = ""
                            'BUSCAMOS las promo con Activas con todos los articulos en la promo Validos MEGAPROMOCIONES)
                            strSQL = "select COUNT (idarticulobeneficiario) as TOTAL from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkPromocion = " & clsMU.EsNuloN(arrPromo(I))
                            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                                If clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Total")) = 0 Then
                                    blnPromoTodos = True
                                    strIdpromo = strIdpromo & clsMU.EsNuloN(arrPromo(I)) & ","
                                End If
                            Else
                                strIdpromo = ""
                            End If
                            dtrsADO.Dispose()
                            GetPromosTodosArticulos = "(" & strIdpromo & ")"
                        Else
                            GetPromosTodosArticulos = ""
                        End If
                    End If
                Else
                    GetPromosTodosArticulos = ""
                End If

            Else
                GetPromosTodosArticulos = ""
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-GetPromosTodosArticulos " & ex.Message, ex.InnerException)
        End Try


    End Function

    Public Function BuscaArticulos2Unidad(ByVal lngNumero As Long) As Boolean

        Dim arrValidos2

        Try

            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulos2Unidad = False

            'Relleno arreglo de valores Validos para la segunda Unidades ( Hasta 110 Articulos )

            If gblstrTipoPromocion = "VENTA" Then
                arrValidos2 = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 43, 45, 47, 49, 51, 53, 55, 57, 59, 61, 63, 65, 67, 69, 71, 73, 75}
            ElseIf gblstrTipoPromocion = "RECALCULO" Then
                arrValidos2 = {2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72, 74, 76}
            End If

            For Each x In arrValidos2
                If lngNumero = clsMU.EsNuloN(x) Then
                    BuscaArticulos2Unidad = True
                    Exit For
                End If
            Next

            Return BuscaArticulos2Unidad

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulos2Unidad " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function BuscaArticulos3Unidad(ByVal lngNumero As Long) As Boolean

        Dim arrValidos3

        Try

            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulos3Unidad = False

            'Relleno arreglo de valores Validos para la tercera Unidades ( Hasta 110 Articulos )

            If gblstrTipoPromocion = "VENTA" Then
                arrValidos3 = {2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35, 38, 41, 44, 47, 50, 53, 56, 59, 62, 65, 68, 71, 74, 77, 80, 83, 86, 89, 92, 95, 98, 101, 104, 107, 110}
            ElseIf gblstrTipoPromocion = "RECALCULO" Then
                arrValidos3 = {3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48, 51, 54, 57, 60, 63, 66, 69, 72, 75, 78, 81, 84, 87, 90, 93, 96, 99, 102, 105, 108, 111}
            End If


            For Each x In arrValidos3
                If lngNumero = clsMU.EsNuloN(x) Then
                    BuscaArticulos3Unidad = True
                    Exit For
                End If
            Next

            Return BuscaArticulos3Unidad

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulos3Unidad " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function BuscaArticulos4Unidad(ByVal lngNumero As Long) As Boolean

        Dim arrValidos4

        Try

            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulos4Unidad = False
            'Relleno arreglo de valores Validos para la cuarta Unidades ( Hasta 143 Articulos )
            If gblstrTipoPromocion = "VENTA" Then
                arrValidos4 = {3, 7, 11, 15, 19, 23, 27, 31, 35, 39, 43, 47, 51, 55, 59, 63, 67, 71, 75, 79, 83, 87, 91, 95, 99, 103, 107, 111, 115, 119, 123, 127, 131, 135, 139, 143}
            ElseIf gblstrTipoPromocion = "RECALCULO" Then
                arrValidos4 = {4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96, 100, 104, 108, 112, 116, 120, 124, 128, 132, 136, 140, 144}
            End If


            For Each x In arrValidos4
                If lngNumero = clsMU.EsNuloN(x) Then
                    BuscaArticulos4Unidad = True
                    Exit For
                End If
            Next

            Return BuscaArticulos4Unidad

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulos4Unidad " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function BuscaArticulos5Unidad(ByVal lngNumero As Long) As Boolean

        Dim arrValidos5

        Try

            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaArticulos5Unidad = False
            'Relleno arreglo de valores Validos para la cuarta Unidades ( Hasta 179 Articulos )
            If gblstrTipoPromocion = "VENTA" Then
                arrValidos5 = {4, 9, 14, 19, 24, 29, 34, 39, 44, 49, 54, 59, 64, 69, 74, 79, 84, 89, 94, 99, 104, 109, 114, 119, 124, 129, 134, 139, 144, 149, 154, 159, 164, 169, 174, 179}
            ElseIf gblstrTipoPromocion = "RECALCULO" Then
                arrValidos5 = {5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180}
            End If

            For Each x In arrValidos5
                If lngNumero = clsMU.EsNuloN(x) Then
                    BuscaArticulos5Unidad = True
                    Exit For
                End If
            Next

            Return BuscaArticulos5Unidad

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulos5Unidad " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function BuscaArticulosBeneficiarioConjunto(ByVal lngIdpromocion As Long, ByVal txtCI As Long, ByVal txtIdTalla As Long, _
           ByVal lngRegRecalculo As Long, ByRef objPromo As IEnumerable(Of Promocion), Optional ByVal lngTipoPromo As Long = 3) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim lngContador As Long
        Dim lngContadorMenor As Long
        Dim strContadores As String
        Dim lngRegRejilla As Long
        Dim blnTallasC As Boolean
        Dim arrBeneficiario
        Dim lngCountFila As Long


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            BuscaArticulosBeneficiarioConjunto = 0 : lngContador = 0 : lngContadorMenor = 0 : strContadores = ""
            blnTallasC = False

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)

            'Verificamos si el artículo es Beneficiario
            ' 04/09/2013 añadir campo descuentocascada

            strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                " from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(txtCI) & " And ( ARTB.Id_Cabecero_Detalle Like '%" & clsMU.EsNuloN(txtIdTalla) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                If strTallasValidas <> "" Then
                    If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(txtIdTalla)) Then
                        blnTallasC = True
                    End If
                End If

                If strTallasValidas = "" Or blnTallasC Then
                    blnArtBeneficiario = True
                Else
                    blnArtBeneficiario = False
                End If
            Else
                blnArtBeneficiario = False
            End If

            dtrsADO.Dispose()

            'BUSCAMOS LOS ARTICULOS BENEFICIARIOS DE LA PROMO A VERIFICAR ACTIVOS Y VALIDOS (TODOS LOS DE CONJUTO)
            ' 04/09/2013 añadir campo descuentocascada           
            strSQL = "Select P.Promocion,ARTB.fkArticulo,ARTB.Id_cabecero_detalle,RDPI.Descuento,RDPI.DescuentoCascada " & _
                " from tbPR_promociones P WITH (NOLOCK) " & _
                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion " & _
                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromo & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                For Each FilaI In dtrsADO.Tables(0).Rows
                    lngContador = 0
                    For Each objpro As Promocion In objPromo
                        If clsMU.EsNuloN(FilaI.item("fkArticulo")) = clsMU.EsNuloN(objpro.Id_Articulo) And (ConfirmacionTallasPromo(clsMU.EsNuloT(FilaI.item("Id_Cabecero_Detalle")), clsMU.EsNuloT(objpro.Id_cabecero_detalle)) Or clsMU.EsNuloT(FilaI.item("Id_Cabecero_Detalle")) = "") Then
                            lngContador = lngContador + 1
                        End If
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                    Next
                    'Incluyo el articulo que estoy introduciendo                    
                    If clsMU.EsNuloN(FilaI.item("fkArticulo")) = clsMU.EsNuloN(txtCI) And (ConfirmacionTallasPromo(clsMU.EsNuloT(FilaI.item("Id_Cabecero_Detalle")), clsMU.EsNuloT(txtIdTalla)) Or clsMU.EsNuloT(FilaI.item("Id_Cabecero_Detalle")) = "") And blnArtBeneficiario = True Then
                        lngContador = lngContador + 1
                    End If
                    strContadores = strContadores & lngContador & "|"
                Next
            End If
            dtrsADO.Dispose()

            'Validamos las cantidades de cada Artículo de la promo cargados en la rejilla de pagos
            If InStrRev(strContadores, "|") > 0 Then
                strContadores = Mid(strContadores, 1, Len(strContadores) - 1)
                arrBeneficiario = Split(strContadores, "|")
                BuscaArticulosBeneficiarioConjunto = 0
                lngContadorMenor = arrBeneficiario(0)
                'buscamos el menor de los valores del arreglo
                For Each x In arrBeneficiario
                    If lngContadorMenor > x Then
                        lngContadorMenor = x
                    End If
                Next
                BuscaArticulosBeneficiarioConjunto = lngContadorMenor

            Else
                BuscaArticulosBeneficiarioConjunto = 0
            End If


        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaArticulosBeneficiarioConjunto " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function BuscaExistenciaUltimoPar(txtCI As Long, txtIdTalla As Long, strTipo As String, ByRef objPromo As IEnumerable(Of Promocion), Optional TipoUltimoPar As Long = 0) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            BuscaExistenciaUltimoPar = 0


            If strTipo = "EXISTENCIA_ARTICULO" Then

                ' 15/04/2013 codigo modificado en funcion del tipo de Ultimo Par
                strSQL = "Select sum(cantidad) as cantidad FROM N_EXISTENCIAS  WITH (NOLOCK) "
                Select Case TipoUltimoPar
                    Case 0 'Artículo + Talla (MISMA tienda)
                        strSQL = strSQL & " where IdArticulo= " & clsMU.EsNuloN(txtCI) & " AND Id_Cabecero_Detalle= " & clsMU.EsNuloN(txtIdTalla) & " AND  IdTienda= '" & (gstrTiendaSesion) & "'"
                    Case 1 'Artículo + Talla (TODAS las tiendas)
                        strSQL = strSQL & " where IdArticulo= " & clsMU.EsNuloN(txtCI) & " AND Id_Cabecero_Detalle= " & clsMU.EsNuloN(txtIdTalla)
                    Case 2 'Solo Artículo (MISMA tienda)
                        strSQL = strSQL & " where IdArticulo= " & clsMU.EsNuloN(txtCI) & " AND  IdTienda= '" & (gstrTiendaSesion) & "'"
                    Case 3 'Solo Artículo (TODAS las tiendas)
                        strSQL = strSQL & " where IdArticulo= " & clsMU.EsNuloN(txtCI)
                End Select
                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    BuscaExistenciaUltimoPar = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Cantidad"))
                Else
                    BuscaExistenciaUltimoPar = clsMU.EsNuloN(0)
                End If

            ElseIf strTipo = "EXISTENCIA_TIENDA" Then

                'Verificamos la existencia del articulo y su talla en las otras Tiendas
                strSQL = "Select SUM(CANTIDAD) AS CANTIDAD FROM N_EXISTENCIAS WITH (NOLOCK) " & _
                    " where IdArticulo= " & clsMU.EsNuloN(txtCI) & " AND Id_Cabecero_Detalle= " & clsMU.EsNuloN(txtIdTalla) & " AND  IdTienda<> '" & (gstrTiendaSesion) & "'"
                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    BuscaExistenciaUltimoPar = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Cantidad"))
                Else
                    BuscaExistenciaUltimoPar = clsMU.EsNuloN(0)
                End If

            ElseIf strTipo = "EXISTENCIA_REJILLA" Then

                For Each objpro As Promocion In objPromo
                    'Busco el articulo que estoy introduciendo
                    If clsMU.EsNuloN(txtCI) = clsMU.EsNuloN(objpro.Id_Articulo) And clsMU.EsNuloT(txtIdTalla) = clsMU.EsNuloN(objpro.Id_cabecero_detalle) Then
                        BuscaExistenciaUltimoPar += 1
                    End If
                Next

            End If

            Return BuscaExistenciaUltimoPar


        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscaExistenciaUltimoPar " & ex.Message, ex.InnerException)
        End Try

    End Function

    '    ' 04/01/2013
    '    Public Function ValidoSiArticuloClienteTienePromo(ByVal lngClienteID As Long, ByVal lngIdArticulo As Long) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim strPromoTodosArticulos As String


    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        ValidoSiArticuloClienteTienePromo = 0


    '        '******BUSCAMOS PROMOCIONES CON TODOS LOAS ARTICULOS ACTIVOS
    '        strPromoTodosArticulos = GetPromosTodosArticulos("IMPRIMIRTEXTOPROMOCIONES")


    '        strSQL = "Select P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion" & _
    '            " from tbPR_promociones P WITH (NOLOCK) " & _
    '            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion "
    '        strSQL = strSQL & " Inner join tbPR_articulosBeneficiarios pa WITH (NOLOCK) ON p.idPromocion = pa.fkPromocion "        
    '        strSQL = strSQL & " where P.FechaComunicacionInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaComunicacionFin >= '" & EC(gdteFechaSesion) & "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' "
    '        strSQL = strSQL & " and pa.fkarticulo = " & lngIdArticulo        
    '        strSQL = strSQL & " And (" & _
    '           " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '           " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "

    '        If strPromoTodosArticulos <> "" Then

    '            strSQL = strSQL & " UNION " & _
    '             " Select P.Promocion, P.FechaComunicacionInicio ,P.FechaComunicacionFin,TP.TipoPromocion" & _
    '                 " from tbPR_promociones P WITH (NOLOCK) " & _
    '                 " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                 " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion "
    '            strSQL = strSQL & " Where P.FechaComunicacionInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaComunicacionFin >= '" & EC(gdteFechaSesion) & "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' "
    '            strSQL = strSQL & " And P.IdPromocion in " & EsNuloT(strPromoTodosArticulos) & " "
    '            strSQL = strSQL & " And (" & _
    '            " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '            " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "
    '            strSQL = strSQL & " order by P.FechaComunicacionInicio "

    '        Else
    '            strSQL = strSQL & " order by P.FechaComunicacionInicio "
    '        End If


    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            While Not rsADO.EOF
    '                ValidoSiArticuloClienteTienePromo = ValidoSiArticuloClienteTienePromo + 1
    '                rsADO.MoveNext()
    '            End While
    '        Else
    '            ValidoSiArticuloClienteTienePromo = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoSiArticuloClienteTienePromo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Sub InsertoDescuentoCascada(ByVal lngIdpromocion As Long, ByVal dblDtoCascada As Double, ByRef objPromo As IEnumerable(Of Promocion))

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim blnTallasC As Boolean
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)
        Dim intF As Long = 0

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueBusqueda = False : blnTallasC = False

            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()


            If blnArticulosBeneficiarios Then

                '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
                If gblLngIdPromocionTodos Then
                    For Each objpro As Promocion In objPromo
                        objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoCascada)), True, True, False)

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****   
                        objpro.Promo = Nothing
                        If objpro.Promo Is Nothing Then
                            If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                            objPD.DescriPromo = strDescPromo.ToString
                            objPD.DescriAmpliaPromo = strDescPromo.ToString
                            objPD.DtoPromo = objpro.DtoEuro
                            objPD.Idpromo = lngIdpromocion
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()
                            intF += 1
                        End If
                    Next
                Else
                    For Each objpro As Promocion In objPromo

                        'TENEMOS LOS ARTICULOS BENEFICIARIOS
                        ' 04/09/2013 añadir campo descuentocascada
                        strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                            " from tbPR_promociones P WITH (NOLOCK) " & _
                            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                            " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                            clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='') " & _
                            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                            " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                        If dtrsADO.Tables(0).Rows.Count <> 0 Then
                            For Each FilaI In dtrsADO.Tables(0).Rows
                                blnTallasC = False
                                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                                If strTallasValidas <> "" Then
                                    If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                        blnTallasC = True
                                    End If
                                End If

                                If strTallasValidas = "" Or blnTallasC Then

                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoCascada)), True, True, False)
                                    '**** REDONDEO PROMO ****
                                    If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                        Select Case gblfkRedondeo
                                            'Case 1, 2 ' redondeo por exceso
                                            ' 15/11/2012
                                            Case 2 ' redondeo por exceso
                                                objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                            Case 3 ' redondeo por defecto                                        
                                                objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                        End Select
                                        objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                    End If

                                    objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                    '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                                    objpro.Promo = Nothing
                                    If objpro.Promo Is Nothing Then
                                        If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                                        objPD.DescriPromo = FilaI.item("Promocion").ToString
                                        objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                        objPD.DtoPromo = objpro.DtoEuro
                                        objPD.Idpromo = lngIdpromocion
                                        objPD.PromocionSelecionada = True

                                        ppD.Add(objPD)
                                        objpro.Promo = ppD.AsEnumerable()
                                        intF += 1
                                    End If
                                End If
                            Next
                        End If
                        dtrsADO.Dispose()
                    Next

                End If
            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows
                            objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoCascada))

                            '**** REDONDEO PROMO ****
                            If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                Select Case gblfkRedondeo
                                    'Case 1, 2 ' redondeo por exceso
                                    ' 15/11/2012
                                    Case 2 ' redondeo por exceso
                                        objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                    Case 3 ' redondeo por defecto                                        
                                        objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                End Select
                                objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                            End If

                            objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                            '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****                              
                            objpro.Promo = Nothing
                            If objpro.Promo Is Nothing Then
                                If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                                objPD.DescriPromo = FilaI.item("Promocion").ToString
                                objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                objPD.DtoPromo = objpro.DtoEuro
                                objPD.Idpromo = lngIdpromocion
                                objPD.PromocionSelecionada = True

                                ppD.Add(objPD)
                                objpro.Promo = ppD.AsEnumerable()
                                intF += 1
                            End If
                        Next
                    End If
                    dtrsADO.Dispose()
                Next
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoDescuentoCascada " & ex.Message, ex.InnerException)
        End Try

    End Sub

    'Public Function BuscoPromoCuponesActivosNew(ByRef MSComunicaciones As MSComm, ByRef cnADO As ADODB.Connection, ByVal srtVentaTCliente As String, ByVal strIdTicket As String, ByVal str_ws_ParamSend As String, ByVal lngEmpleado As Long, _
    '        ByVal lngClienteID As Long, ByVal blnRetencionCli As Boolean, Optional ByRef strValesOnLine0 As String, Optional ByRef strValesOnLine1 As String) As Boolean
    '        Dim strIdTarjetaAnt As String
    '        Dim dblIdTarjetaNew As Double
    '        Dim strTarjetaNew As String
    '        Dim strLlave As String
    '        Dim strCadena As String
    '        Dim strTipoTarj As String
    '        Dim strFCaducidad As String
    '        Dim strSQL As String
    '        Dim strTarjetaNum As String
    '        Dim blnTrans As Boolean
    '        Dim blnSigue As Boolean
    '        Dim SolicitaCambioTarjeta As Boolean
    '        Dim lngID As Long

    '        Dim lngSellos As Long
    '        Dim rsADO As ADODB.Recordset
    '        Dim rsADOTicket As ADODB.Recordset
    '        Dim dblTotalTickets As Double
    '        Dim dblTotalVale As Double
    '        Dim strObservacionVale As String
    '        Dim blnTributaIVA As Boolean
    '        Dim blnClientes As Boolean
    '        Dim strPromoTodosArticulos As String
    '        Dim IdpromocionSeleccionada As Long
    '        Dim IdTipoPromocionSeleccionada As Long
    '        Dim IdTipoValorSeleccionada As Long
    '        Dim strCodigoCupon As String
    '        Dim blnTallasC As Boolean
    '        Dim strTipoVale As String
    '        Dim strTipoPromo As String
    '        Dim blnvalidarFP As Boolean

    '        On Error GoTo ReparaErrores

    '        lngSellos = 0 : lngSellosRumbo = 0 : dblTotalTickets = 0 : dblTotalVale = 0 : blnSigue = False : strObservacionVale = ""
    '        blnTallasC = False : blnvalidarFP = False

    '        If blnRetencionCli Then
    '            strTipoPromo = "(35)"
    '        Else
    '            strTipoPromo = "(7,34)"
    '        End If

    '        strSQL = "Select * from tbPR_clientesBeneficiarios CLI WITH (NOLOCK) " & _
    '           " Inner join tbPR_promociones P WITH (NOLOCK) ON CLI.fkPromocion=P.IdPromocion " & _
    '           " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '           " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & "'" & _
    '           " and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " order by P.FechaValidezInicio"

    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            blnClientes = True
    '        Else
    '            blnClientes = False
    '        End If
    '        CloseRS(rsADO)

    '        '******BUSCAMOS PROMOCIONES CON TODOS LOAS ARTICULOS ACTIVOS
    '        strPromoTodosArticulos = GetPromosTodosArticulos("CUPONES")

    '        '***************************************************************
    '        '********** BUSCAMOS LOS DETALLES DEL TICKET ASOCIADO **********
    '        '***************************************************************

    '        strSQL = "SELECT * FROM N_TICKETS_DETALLES td WITH (NOLOCK) " & _
    '            " INNER JOIN N_TICKETS t WITH (NOLOCK) ON td.Id_Auto=t.Id_Auto " & _
    '            " WHERE t.Id_Ticket='" & strIdTicket & "' And td.ImporteEuros >0"

    '        rsADOTicket = New ADODB.Recordset
    '        rsADOTicket.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)

    '        blnSigue = False
    '        'rsADOTicket!Id_Cabecero_Detalle
    '        'rsADOTicket!Id_Articulo

    '        If Not rsADOTicket.EOF Then
    '            While Not rsADOTicket.EOF
    '                ' 04/09/2013 añadir campo descuentocascada

    '                strSQL = "Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
    '                    " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax, p.fkRedondeo, p.fkAplicacionRedondeo,ARTB.Id_Cabecero_Detalle,RDPI.DescuentoCascada,P.ComunicacionMail from tbPR_promociones P  WITH (NOLOCK) " & _
    '                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                    " INNER join (select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkArticulo=" & EsNuloN(rsADOTicket!Id_Articulo) & _
    '                    " And (Id_Cabecero_Detalle like'%" & EsNuloN(rsADOTicket!Id_Cabecero_Detalle) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
    '                    " LEFT Join (select * from tbPR_articulosDonantes WITH (NOLOCK) where fkArticulo=" & EsNuloN(rsADOTicket!Id_Articulo) & _
    '                    " And (Id_Cabecero_Detalle like'%" & EsNuloN(rsADOTicket!Id_Cabecero_Detalle) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTD ON P.IdPromocion= ARTD.fkPromocion " & _
    '                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                    " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '                    "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo

    '                strSQL = strSQL & _
    '                    " And (" & _
    '                    " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '                    " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "

    '                ' 04/09/2013 añadir campo descuentocascada
    '                strSQL = strSQL & _
    '                    " UNION " & _
    '                    " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
    '                    " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax, p.fkRedondeo, p.fkAplicacionRedondeo,ARTB.Id_Cabecero_Detalle,RDPI.DescuentoCascada,P.ComunicacionMail from tbPR_promociones P WITH (NOLOCK) " & _
    '                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                    " LEFT join (select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where fkArticulo=" & EsNuloN(rsADOTicket!Id_Articulo) & _
    '                    " And (Id_Cabecero_Detalle like'%" & EsNuloN(rsADOTicket!Id_Cabecero_Detalle) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTB ON P.IdPromocion= ARTB.fkPromocion " & _
    '                    " INNER Join (select * from tbPR_articulosDonantes " & clsBD.NOLOCK() & " where fkArticulo=" & EsNuloN(rsADOTicket!Id_Articulo) & _
    '                    " And (Id_Cabecero_Detalle like'%" & EsNuloN(rsADOTicket!Id_Cabecero_Detalle) & "%' OR ISNULL(Id_Cabecero_Detalle,'')='')) ARTD ON P.IdPromocion= ARTD.fkPromocion " & _
    '                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                    " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '                    "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & _
    '                    " And (" & _
    '                    " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '                    " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK))) "

    '                If strPromoTodosArticulos <> "" Then
    '                    ' 04/09/2013 añadir campo descuentocascada
    '                    strSQL = strSQL & " UNION " & _
    '                    " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
    '                    " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax, p.fkRedondeo, p.fkAplicacionRedondeo,'' as Id_Cabecero_Detalle,RDPI.DescuentoCascada,P.ComunicacionMail from tbPR_promociones P WITH (NOLOCK) " & _
    '                    " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                    " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                    " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                    " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '                    "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " And P.IdPromocion in " & EsNuloT(strPromoTodosArticulos) & _
    '                    " And (" & _
    '                    " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '                    " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK)))"
    '                    strSQL = strSQL & " ORDER By P.FechaValidezInicio"
    '                Else
    '                    strSQL = strSQL & " ORDER By P.FechaValidezInicio"
    '                End If

    '                rsADO = New ADODB.Recordset
    '                rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '                If Not rsADO.EOF Then
    '                    blnTallasC = False
    '                    '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
    '                    If EsNuloT(rsADO!Id_Cabecero_Detalle) <> "" Then
    '                        If ConfirmacionTallasPromo(EsNuloT(rsADO!Id_Cabecero_Detalle), EsNuloN(rsADOTicket!Id_Cabecero_Detalle)) Then
    '                            blnTallasC = True
    '                        End If
    '                    End If

    '                    If EsNuloT(rsADO!Id_Cabecero_Detalle) = "" Or blnTallasC Then
    '                        dblTotalTickets = dblTotalTickets + ((EsNuloN(rsADOTicket!ImporteEuros) * EsNuloN(rsADOTicket!Estado)) * -1)
    '                        blnSigue = True
    '                        IdpromocionSeleccionada = EsNuloN(rsADO!IdPromocion)
    '                        IdTipoPromocionSeleccionada = EsNuloN(rsADO!IdTipoPromocion)
    '                        '******** VALIDACION DE FP TICKETS ********
    '                        blnvalidarFP = CBool(EsNuloN(rsADO!ComunicacionMail))
    '                    End If

    '                End If
    '                CloseRS(rsADO)

    '                rsADOTicket.MoveNext()
    '            End While
    '        Else
    '            blnSigue = False
    '        End If
    '        CloseRS(rsADOTicket)

    '        '*********************************************************************************
    '        '********* VALIDAMOS FORMAS DE PAGO SELECCIONADA PARA EMITIR LOS CUPONES *********
    '        '*********************************************************************************
    '        If blnvalidarFP Then
    '            If ValidoPromosValidas_FP(strIdTicket, EsNuloN(IdpromocionSeleccionada), "CUPONES") > 0 Then
    '                blnSigue = True
    '            Else
    '                blnSigue = False
    '            End If
    '        End If


    '        If blnSigue Then
    '            ' 04/07/2013 añadimos p.idpromocion
    '            ' 18/07/2013 añadimos trc.tiendascanje
    '            strSQL = "Select P.Promocion, P.fkTipoPromocion,TRC.ValorCupon,TRC.fkTipoValorCupon,TRC.FechaCanjeInicio,TRC.FechaCanjeFinal,TRC.ValorVenta,P.fkRedondeo, P.fkAplicacionRedondeo,TRC.TributaIVA,TRC.TextoAvisoCupones,p.idpromocion,trc.tiendascanje " & _
    '                        " from tbPR_promociones P WITH (NOLOCK) " & _
    '                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                        " Inner join tbPR_recompensasCupones TRC WITH (NOLOCK) ON P.IdPromocion= TRC.fkPromocion " & _
    '                        " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & "'" & _
    '                        " and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " And p.IdPromocion=" & IdpromocionSeleccionada & " Order by P.FechaValidezInicio"
    '            rsADO = New ADODB.Recordset
    '            rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '            If Not rsADO.EOF Then
    '                While Not rsADO.EOF
    '                    dblTotalVale = 0 : strObservacionVale = ""

    '                    'Nueva opción de filtrar si la venta es superior al importe establecido
    '                    If dblTotalTickets > EsNuloN(rsADO!Valorventa) Then
    '                        If EsNuloN(rsADO!fkTipoValorCupon) = 1 Then
    '                            dblTotalVale = EsNuloN(rsADO!ValorCupon)
    '                            IdTipoValorSeleccionada = 1
    '                        ElseIf EsNuloN(rsADO!fkTipoValorCupon) = 2 Then
    '                            dblTotalVale = Round((dblTotalTickets * EsNuloN(rsADO!ValorCupon)) / 100, 2)
    '                            IdTipoValorSeleccionada = 2
    '                        ElseIf EsNuloN(rsADO!fkTipoValorCupon) = 3 Then
    '                            dblTotalVale = EsNuloN(rsADO!ValorCupon)
    '                            IdTipoValorSeleccionada = 3
    '                        End If

    '                        'Aplicar Redondeo
    '                        If EsNuloN(rsADO!fkRedondeo) = 1 Then
    '                            dblTotalVale = dblTotalVale
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 2 Then
    '                            If Int(dblTotalVale) <> dblTotalVale Then
    '                                dblTotalVale = Int(dblTotalVale) + 1
    '                            End If
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 3 Then
    '                            dblTotalVale = Int(dblTotalVale)
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 4 Then
    '                            dblTotalVale = Round(dblTotalVale, 0)
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 5 Then
    '                            dblTotalVale = Int(dblTotalVale)
    '                        End If

    '                        'Validamos Si el cupon tributara Iva o No
    '                        If Trim(EsNuloT(rsADO!TributaIva)) = "" Then
    '                            blnTributaIVA = True
    '                        ElseIf CBool(EsNuloN(rsADO!TributaIva)) Then
    '                            blnTributaIVA = True
    '                        Else
    '                            blnTributaIVA = False
    '                        End If

    '                        If dblTotalVale > 0 Then


    '                            If IdTipoPromocionSeleccionada = 7 Then

    '                                '*****************************
    '                                '***** PROMOCION CUPONES *****
    '                                '*****************************

    '                                If Not blnTributaIVA Then
    '                                    strObservacionVale = "Bono Descuento Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                Else
    '                                    strObservacionVale = "Vale Promoción Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                End If

    '                                'Guardo en la tabla N_TICKETS_PROMOCIONES
    '                                Call GuardoArticulosPromosCupones(strIdTicket, dblTotalVale, cnADO, IdTipoPromocionSeleccionada)

    '                                'generamos el vale
    '                                lngID = GetMaxIdAuto(cnADO, "SELECT Max(Id_vale) AS TOT FROM N_VALES WITH (NOLOCK)  WHERE Id_Tienda='" & gstrTiendaSesion & "'") + 1

    '                                '**** ESTRUCTURA DE TIPO VALE: 1.Identf.= PROMO 2.Separador= ý 3.IDPROMOCION
    '                                '4.Separador= ý 5.CampoDescuento= 1.Descuento Total/0.Vale Normal

    '                                If gblDirFtp <> FTP_NINEWEST_MEX Then
    '                                    If IdTipoValorSeleccionada = 3 Then
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý1"
    '                                    Else
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý0"
    '                                    End If
    '                                ElseIf gblDirFtp = FTP_NINEWEST_MEX Then
    '                                    strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion)
    '                                End If


    '                                strSQL = "INSERT INTO N_VALES(Id_Vale,Id_Tienda,Id_Ticket,Id_Empleado,DNI,FechaEmision,cValePts,cValeEuro,sVenta,ID_TERMINAL_EMITIDO,Observaciones,RefFP,TributaIVA,TipoVale,TiendasCanje) VALUES(" & _
    '                                    lngID & ",'" & gstrTiendaSesion & "','" & strIdTicket & "'," & EsNuloN(frmTickets.txtEmpleado.Text) & ",'0','" & Format$(gdteFechaSesion, gLocaleInfo.ShortDateFormat) & "'," & _
    '                                    EurosPesetas(Abs(dblTotalVale)) & "," & SinComas(Abs(dblTotalVale)) & ",'1','" & gstrTerminalID & "','" & EsNuloT(strObservacionVale) & "','GARA'," & _
    '                                    IIf(blnTributaIVA, 1, 0) & ",'" & EsNuloT(strTipoVale) & "','" & EsNuloT(rsADO!tiendasCanje) & "')"

    '                                cnADO.Execute(strSQL, , adExecuteNoRecords)

    '                                If gblnNewFPagos Then
    '                                    '*** Graba transaccion
    '                                    '*** Fran - 21/11/12 - Si es un vale que tributa iva tiene el titulo no puede ser Bono descuento
    '                                    Save_Hist_Transaccion(cnADO, IIf(blnTributaIVA, "Vale Promoción", "Bono Descuento"), _
    '                                        lngID & IIf(gstrTerminalID = "", "", " - " & gstrTerminalID) & "/" & gstrTiendaSesion, Abs(dblTotalVale))
    '                                End If
    '                                '*** Impresion de Vale de Promocion
    '                                ImprimeValeNW_GESTORPROMOCIONES(cnADO, lngID, gstrTiendaSesion, _
    '                                    IIf(clsLANG.GetIdiomaID = 8, True, False), frmTickets.brcBarras, "Vale", strObservacionVale _
    '                                    & IIf(EsNuloT(rsADO!TextoAvisoCupones) <> "", " " & EsNuloT(rsADO!TextoAvisoCupones), "") & IIf(EsNuloT(rsADO!Promocion) <> "", " " & EsNuloT(rsADO!Promocion), ""), "", "", IdTipoValorSeleccionada, False, GetNombreCliente(cnADO, EsNuloN(lngClienteID)), GetCIFClienteGeneral(cnADO, EsNuloN(lngClienteID)))


    '                                '*** Actualizacion Online de Tienda ( VALES ) ***                                
    '                                strValesOnLine0 = strValesOnLine0 & "ýVALES"
    '                                strValesOnLine1 = strValesOnLine1 & "ý" & lngID & "#" & gstrTiendaSesion

    '                                '*** Actualizacion Estados ( VALES ) ***
    '                                Call DesactivarValesPromosCupones()

    '                            ElseIf IdTipoPromocionSeleccionada = 34 Then

    '                                '****************************
    '                                '***** PROMOCION FLYERS *****
    '                                '****************************

    '                                If Not blnTributaIVA Then
    '                                    strObservacionVale = "Bono Descuento Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                Else
    '                                    strObservacionVale = "Bono Promoción Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                End If

    '                                'Guardo en la tabla N_TICKETS_PROMOCIONES
    '                                Call GuardoArticulosPromosCupones(strIdTicket, dblTotalVale, cnADO, IdTipoPromocionSeleccionada)
    '                                lngID = GetMaxIdAuto(cnADO, "SELECT Max(Id_Cupones) AS TOT FROM N_CUPONES_DESCUENTO WITH (NOLOCK)  WHERE Id_Tienda='" & gstrTiendaSesion & "'") + 1

    '                                strCodigoCupon = Replace(Replace(strIdTicket, "/", ""), "-", "")

    '                                'generamos el Cupon o Flyers
    '                                strSQL = "INSERT INTO N_CUPONES_DESCUENTO(Id_Cupones,Id_Tienda,Id_Ticket,Id_Empleado,DNI,FechaEmision,Fecha_Modificacion,cCuponesPts,sVenta,cCuponesEuro,ID_TERMINAL_EMITIDO,Activo,IdCodigo,Tipo,Observaciones) VALUES(" & _
    '                                  lngID & ",'" & gstrTiendaSesion & "','" & strIdTicket & "'," & EsNuloN(frmTickets.txtEmpleado.Text) & ",'0','" & Format$(gdteFechaSesion, gLocaleInfo.ShortDateFormat) & "','" & Format$(gdteFechaSesion, gLocaleInfo.ShortDateFormat) & "', " & _
    '                                  EurosPesetas(Abs(EsNuloN(dblTotalVale))) & ",1," & SinComas(Abs(EsNuloN(dblTotalVale))) & ",'" & gstrTerminalID & "','1','" & EsNuloT(strCodigoCupon) & "'," & IIf(EsNuloN(IdTipoValorSeleccionada) = 2 Or EsNuloN(IdTipoValorSeleccionada) = 3, 1, 0) & ",'" & EsNuloT(strObservacionVale) & "')"
    '                                cnADO.Execute(strSQL, , adExecuteNoRecords)

    '                                '*** Impresion de Vale de Promocion
    '                                ImprimeFlyers_GESTORPROMOCIONES(cnADO, lngID, gstrTiendaSesion, _
    '                                    IIf(clsLANG.GetIdiomaID = 8, True, False), frmTickets.brcBarras, "Bono", strObservacionVale, EsNuloT(rsADO!TextoAvisoCupones), "")


    '                            ElseIf IdTipoPromocionSeleccionada = 35 Then

    '                                '************************************************
    '                                '***** PROMOCION CUPONES RETENCION CLIENTE  *****
    '                                '************************************************

    '                                If Not blnTributaIVA Then
    '                                    strObservacionVale = "Bono Descuento Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                Else
    '                                    strObservacionVale = "Vale Promoción Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                End If

    '                                'Guardo en la tabla N_TICKETS_PROMOCIONES
    '                                Call GuardoArticulosPromosCupones(strIdTicket, dblTotalVale, cnADO, IdTipoPromocionSeleccionada)

    '                                'generamos el vale
    '                                lngID = GetMaxIdAuto(cnADO, "SELECT Max(Id_vale) AS TOT FROM N_VALES WITH (NOLOCK)  WHERE Id_Tienda='" & gstrTiendaSesion & "'") + 1

    '                                '**** ESTRUCTURA DE TIPO VALE: 1.Identf.= PROMO 2.Separador= ý 3.IDPROMOCION
    '                                '4.Separador= ý 5.CampoDescuento= 1.Descuento Total/0.Vale Normal

    '                                If gblDirFtp <> FTP_NINEWEST_MEX Then
    '                                    If IdTipoValorSeleccionada = 3 Then
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý1"
    '                                    Else
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý0"
    '                                    End If
    '                                ElseIf gblDirFtp = FTP_NINEWEST_MEX Then
    '                                    strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion)
    '                                End If


    '                                strSQL = "INSERT INTO N_VALES(Id_Vale,Id_Tienda,Id_Ticket,Id_Empleado,DNI,FechaEmision,cValePts,cValeEuro,sVenta,ID_TERMINAL_EMITIDO,Observaciones,RefFP,TributaIVA,TipoVale,TiendasCanje) VALUES(" & _
    '                                    lngID & ",'" & gstrTiendaSesion & "','" & strIdTicket & "'," & EsNuloN(frmTickets.txtEmpleado.Text) & ",'0','" & Format$(gdteFechaSesion, gLocaleInfo.ShortDateFormat) & "'," & _
    '                                    EurosPesetas(Abs(dblTotalVale)) & "," & SinComas(Abs(dblTotalVale)) & ",'1','" & gstrTerminalID & "','" & EsNuloT(strObservacionVale) & "','GARA'," & _
    '                                    IIf(blnTributaIVA, 1, 0) & ",'" & EsNuloT(strTipoVale) & "','" & EsNuloT(rsADO!tiendasCanje) & "')"

    '                                cnADO.Execute(strSQL, , adExecuteNoRecords)

    '                                If gblnNewFPagos Then
    '                                    '*** Graba transaccion
    '                                    '*** Fran - 21/11/12 - Si es un vale que tributa iva tiene el titulo no puede ser Bono descuento
    '                                    Save_Hist_Transaccion(cnADO, IIf(blnTributaIVA, "Vale Promoción", "Bono Descuento"), _
    '                                        lngID & IIf(gstrTerminalID = "", "", " - " & gstrTerminalID) & "/" & gstrTiendaSesion, Abs(dblTotalVale))
    '                                End If
    '                                '*** Impresion de Vale de Promocion
    '                                ImprimeValeNW_GESTORPROMOCIONES(cnADO, lngID, gstrTiendaSesion, _
    '                                    IIf(clsLANG.GetIdiomaID = 8, True, False), frmTickets.brcBarras, "Vale", strObservacionVale & IIf(EsNuloT(rsADO!TextoAvisoCupones) <> "", " " & EsNuloT(rsADO!TextoAvisoCupones), ""), "", "", IdTipoValorSeleccionada)


    '                                '*** Actualizacion Online de Tienda ( VALES ) ***
    '                                Subir_OnLine(frmTickets.hwnd, gstrTiendaSesion, gstrTerminalID, gdteFechaSesion, "VALES", lngID & "#" & gstrTiendaSesion, "UP")

    '                                '*** Actualizacion Estados ( VALES ) ***
    '                                Call DesactivarValesPromosCupones()


    '                            End If
    '                        End If
    '                    End If
    '                    rsADO.MoveNext()
    '                End While
    '            End If
    '        End If

    'ReparaErrores:
    '        If blnTrans Then cnADO.RollbackTrans() : blnTrans = False
    '        CloseRS(rsADO) : CloseRS(rsADOTicket)
    '        If Err Then
    '            Call ReparaErrores("modPromociones", "BuscoPromoCuponesActivosNew", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Function BuscoImporteFlyersTG(ByVal lngIdpromocion As Long, ByRef objPromo As IEnumerable(Of Promocion)) As Double

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim blnTallasC As Boolean

        Try


            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            blnSigueBusqueda = False : BuscoImporteFlyersTG = 0 : blnTallasC = False


            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios WITH (NOLOCK) where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()


            If blnArticulosBeneficiarios Then

                '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
                If gblLngIdPromocionTodos Then
                    For Each objpro As Promocion In objPromo
                        BuscoImporteFlyersTG = BuscoImporteFlyersTG + (clsMU.EsNuloN(objpro.Unidades) * clsMU.EsNuloN(objpro.Pvp_Venta))
                    Next
                Else
                    For Each objpro As Promocion In objPromo
                        'TENEMOS LOS ARTICULOS BENEFICIARIOS
                        ' 04/09/2013 añadir campo descuentocascada
                        strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                            " from tbPR_promociones P WITH (NOLOCK) " & _
                            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                            " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                            clsMU.EsNuloN(objpro.Id_Articulo) & " And (ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                            " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=2  and P.IdPromocion= " & lngIdpromocion & _
                            " group by P.Promocion,ARTB.fkArticulo,RDPI.Descuento, P.FechaValidezInicio,P.FechaValidezFin,RDPI.DescuentoCascada order by P.FechaValidezInicio "

                        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                        If dtrsADO.Tables(0).Rows.Count <> 0 Then
                            For Each FilaI In dtrsADO.Tables(0).Rows
                                blnTallasC = False
                                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                                If strTallasValidas <> "" Then
                                    If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                        blnTallasC = True
                                    End If
                                End If

                                If strTallasValidas = "" Or blnTallasC Then
                                    BuscoImporteFlyersTG = BuscoImporteFlyersTG + (clsMU.EsNuloN(objpro.Unidades) * clsMU.EsNuloN(objpro.Pvp_Venta))
                                End If
                            Next
                        End If
                        dtrsADO.Dispose()
                    Next
                End If
            Else
                For Each objpro As Promocion In objPromo
                    BuscoImporteFlyersTG = BuscoImporteFlyersTG + (clsMU.EsNuloN(objpro.Unidades) * clsMU.EsNuloN(objpro.Pvp_Venta))
                Next
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-BuscoImporteFlyersTG " & ex.Message, ex.InnerException)
        End Try

    End Function

    Public Sub Recalculo_Promociones(ByRef objPromo As IEnumerable(Of Promocion))

        Dim dtrsADO As DataSet = New DataSet
        Dim intI As Long
        Dim dblDescuento As Double
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)
        Dim MensajeError As String

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            blnPromoCascada = False : intI = 0

            For Each objpro As Promocion In objPromo
                '***** VARIABLE GLOBAL DEL MODULO ******
                gstrTiendaSesion = objpro.Id_Tienda.ToString
                gdteFechaSesion = objpro.FSesion.ToString("dd/MM/yyyy")
                'gdteFechaSesion = objpro.FSesion.ToString("yyyyMMdd")
                'gdteFechaSesion = objpro.FSesion.ToString()
                gdblTotalVenta = clsMU.EsNuloN(objpro.TotalVenta)

                'RECORREMOS LA REJILLA DE LOS ARTICULOS Y VERIFICAMOS LAS PROMOCIONES DE TODOS
                MensajeError = String.Empty

                dblDescuento = ValidoArticuloPromo(clsMU.EsNuloN(objpro.Id_Articulo), objpro.ClienteID, clsMU.EsNuloN(objpro.Id_cabecero_detalle), "RECALCULO", intI, _
                                    clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(objpro.Pvp_Or), clsMU.EsNuloN(objpro.Unidades), objPromo, MensajeError)

                objpro.MensajeError = MensajeError

                If dblDescuento > 0 Then
                    If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                            If blnPrecioFinal And (gblLngTipoPromocion = 43 Or gblLngTipoPromocion = 44 Or _
                            gblLngTipoPromocion = 45 Or gblLngTipoPromocion = 46 Or gblLngTipoPromocion = 47) Then
                                objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.EsNuloN(dblDescuento), True, True, False)
                            Else
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDescuento))
                            End If
                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                            ' 05/09/2013 modificacion para el tipo de promociones en cascada
                            If gblLngTipoPromocion = 29 Or gblLngTipoPromocion = 30 Or _
                                    gblLngTipoPromocion = 31 Or gblLngTipoPromocion = 32 Or gblLngTipoPromocion = 33 Then
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDescuento))
                            ElseIf Not blnPrecioFinal Then
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDescuento))
                            Else
                                dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDescuento))
                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoDiferencias))
                            End If

                        End If

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                        objpro.Promo = Nothing
                        ppD = New List(Of DetallePromo)
                        If objpro.Promo Is Nothing Then
                            objPD.DescriPromo = strDescPromo.ToString
                            objPD.DescriAmpliaPromo = strDescPromo.ToString
                            objPD.DtoPromo = dblDescuento
                            objPD.Idpromo = clsMU.EsNuloN(gblLngIdPromocion)
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()


                        End If

                    End If
                End If
                intI += 1
            Next

        Catch ex As Exception
            Throw New Exception("ModPromociones-Recalculo_Promociones " & ex.Message, ex.InnerException)
        End Try

    End Sub

    Public Function ConfirmacionTallasPromo(ByVal strTallasValidas As String, ByVal strTallaConfirmar As String) As Boolean

        Dim intI As Long
        Dim arrTallas

        Try

            Dim clsMU As ModUtilidades = New ModUtilidades
            ConfirmacionTallasPromo = False

            '*** CARGANOS EL ARREGLO CON LOS VALORES DE LAS TALLAS VALIDAS ***
            If strTallasValidas <> "" And InStrRev(strTallasValidas, "|") > 0 Then
                arrTallas = Split(Trim$(clsMU.EsNuloT(strTallasValidas)), "|")
                For intI = 0 To UBound(arrTallas)
                    If Trim(clsMU.EsNuloT(strTallaConfirmar)) = clsMU.EsNuloT(arrTallas(intI)) Then
                        '*** SI LA TALLA ES VALIDA ***
                        ConfirmacionTallasPromo = True : Exit For
                    End If
                Next intI
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-ConfirmacionTallas " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function GetValidaPromosTodosArticulos(ByVal strPromoTodosArticulos As String, ByVal lngIdpromocion As Long) As Boolean

        Dim strIdpromo As String
        Dim blnPromoTodos As Boolean
        Dim intI, I As Long
        Dim intF As Long
        Dim arrPromo

        Try


            Dim clsMU As ModUtilidades = New ModUtilidades

            strIdpromo = "" : blnPromoTodos = True : intI = 0 : I = 0 : intF = 0
            GetValidaPromosTodosArticulos = False

            'BUSCAMOS TODAS LA PROMOS ACTIVAS CON TODOS LOS ARTICULOS SI ESTA LA SELECCIONADA
            strIdpromo = Replace(Replace(Trim(strPromoTodosArticulos), "(", ""), ")", "")

            If InStrRev(strIdpromo, ",") > 0 Then
                arrPromo = Split(strIdpromo, ",")
                blnPromoTodos = True
            Else
                strIdpromo = strIdpromo
                blnPromoTodos = False
            End If


            If Not blnPromoTodos And strIdpromo <> "" Then
                If clsMU.EsNuloN(strIdpromo) = lngIdpromocion Then
                    GetValidaPromosTodosArticulos = True
                Else
                    GetValidaPromosTodosArticulos = False
                End If
            ElseIf blnPromoTodos And UBound(arrPromo) > 0 Then
                For I = 0 To UBound(arrPromo)
                    If clsMU.EsNuloN(arrPromo(I)) = lngIdpromocion Then
                        GetValidaPromosTodosArticulos = True
                        Exit For
                    End If
                Next
            Else
                GetValidaPromosTodosArticulos = False
            End If

        Catch ex As Exception
            Throw New Exception("ModPromociones-GetValidaPromosTodosArticulos " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function GetDctoPromo(ByVal lngIdpromocion As Long, ByVal lngTipoPromocion As Long) As Double

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            'BUSCAMOS TODAS LA PROMOS ACTIVAS PARA ESTA TIENDA
            ' 04/09/2013 añadir campo descuentocascada
            strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada " & _
                   " from tbPR_promociones P WITH (NOLOCK) " & _
                   " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                   " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                   " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                   " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"


            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                GetDctoPromo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Descuento"))
            Else
                GetDctoPromo = 0
            End If

            dtrsADO.Dispose()

        Catch ex As Exception
            Throw New Exception("ModPromociones-GetDctoPromo " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function GetNombrePromo(ByVal lngIdpromocion As Long, ByVal lngTipoPromocion As Long) As String

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            'BUSCAMOS TODAS LA PROMOS ACTIVAS PARA ESTA TIENDA
            ' 04/09/2013 añadir campo descuentocascada
            strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada " & _
                   " from tbPR_promociones P WITH (NOLOCK) " & _
                   " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                   " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                   " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                   " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"


            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                GetNombrePromo = clsMU.EsNuloT(dtrsADO.Tables(0).Rows(0).Item("Promocion"))
            Else
                GetNombrePromo = ""
            End If

            dtrsADO.Dispose()

        Catch ex As Exception
            Throw New Exception("ModPromociones-GetNombrePromo " & ex.Message, ex.InnerException)
        End Try


    End Function


    '    Public Function GetFormasDePagoPromo(ByVal lngIdFormaPago As Long, ByVal lngIdFormaPagoDetalle As Long, ByVal strFP As String) As String

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String


    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        GetFormasDePagoPromo = ""

    '        'BUSCAMOS FORMAS DE PAGO DE LAS PROMOS ACTIVAS PARA ESTA TIENDA

    '        strSQL = "Select TPD.Descripcion " & _
    '               " from tbPR_promociones P WITH (NOLOCK) " & _
    '               " Inner join tbPR_formasPagodetalle PFPD  WITH (NOLOCK) ON P.IdPromocion=  PFPD.fkPromocion " & _
    '               " Inner join TIPOS_DE_PAGO_DETALLES TPD WITH (NOLOCK) ON PFPD.FkPago=TPD.IdPago And PFPD.FkOrden=TPD.IdOrden " & _
    '               " where P.promocion in ( " & strFP & " ) And PFPD.FkPago = " & EsNuloN(lngIdFormaPago) & " Group by TPD.Descripcion order by TPD.Descripcion"


    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)

    '        If Not rsADO.EOF Then
    '            GetFormasDePagoPromo = "["
    '            While Not rsADO.EOF
    '                GetFormasDePagoPromo = GetFormasDePagoPromo & EsNuloT(rsADO!Descripcion) & "-"
    '                rsADO.MoveNext()
    '            End While
    '            GetFormasDePagoPromo = Mid(GetFormasDePagoPromo, 1, Len(GetFormasDePagoPromo) - 1) & "]"
    '        Else
    '            GetFormasDePagoPromo = "[SIN DEFINIR -TARJETA DETALLES]"
    '        End If
    '        CloseRS(rsADO)


    'ReparaErrores:
    '        CloseRS(rsADO) : CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GetFormasDePagoPromo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    '    ' 10/09/2013
    '    Public Function GetTipoPromo(ByVal IdPromo As Long) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        strSQL = "select fkTipoPromocion from tbPR_promociones WITH (NOLOCK) " & _
    '            " where idpromocion= " & IdPromo
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetTipoPromo = rsADO!fkTipoPromocion
    '        Else
    '            GetTipoPromo = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GetTipoPromo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function
    '    ' 10/09/2013


    '    Public Function BuscoPromoCuponesRetencionCli(ByRef cnADO As ADODB.Connection, ByVal lngEmpleado As Long, _
    '            ByVal lngClienteID As Long, ByVal blnRetencionCli As Boolean, ByVal lngIdPromo As Long, _
    '            ByVal strNunAsigBanco As String) As Boolean
    '        Dim strIdTarjetaAnt As String
    '        Dim dblIdTarjetaNew As Double
    '        Dim strTarjetaNew As String
    '        Dim strLlave As String
    '        Dim strCadena As String
    '        Dim strTipoTarj As String
    '        Dim strFCaducidad As String
    '        Dim strSQL As String
    '        Dim strTarjetaNum As String
    '        Dim blnTrans As Boolean
    '        Dim blnSigue As Boolean
    '        Dim SolicitaCambioTarjeta As Boolean
    '        Dim lngID As Long

    '        Dim lngSellos As Long
    '        Dim rsADO As ADODB.Recordset
    '        Dim rsADOTicket As ADODB.Recordset
    '        Dim dblTotalTickets As Double
    '        Dim dblTotalVale As Double
    '        Dim strObservacionVale As String
    '        Dim blnTributaIVA As Boolean
    '        Dim blnClientes As Boolean
    '        Dim strPromoTodosArticulos As String
    '        Dim IdpromocionSeleccionada As Long
    '        Dim IdTipoPromocionSeleccionada As Long
    '        Dim IdTipoValorSeleccionada As Long
    '        Dim strCodigoCupon As String
    '        Dim blnTallasC As Boolean
    '        Dim strTipoVale As String
    '        Dim strTipoPromo As String
    '        Dim strIdTicket As String

    '        On Error GoTo ReparaErrores

    '        lngSellos = 0 : lngSellosRumbo = 0 : dblTotalTickets = 0 : dblTotalVale = 0 : blnSigue = False : strObservacionVale = ""
    '        blnTallasC = False : strIdTicket = ""

    '        If blnRetencionCli Then
    '            strTipoPromo = "(35)"
    '        Else
    '            strTipoPromo = "(7,34)"
    '        End If

    '        strSQL = "Select * from tbPR_clientesBeneficiarios CLI WITH (NOLOCK) " & _
    '           " Inner join tbPR_promociones P WITH (NOLOCK) ON CLI.fkPromocion=P.IdPromocion " & _
    '           " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '           " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & "'" & _
    '           " and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " order by P.FechaValidezInicio"

    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            blnClientes = True
    '        Else
    '            blnClientes = False
    '        End If
    '        CloseRS(rsADO)

    '        '******BUSCAMOS PROMOCIONES CON TODOS LOAS ARTICULOS ACTIVOS
    '        strPromoTodosArticulos = GetPromosTodosArticulos("CUPONES_RETENCION")

    '        blnSigue = False
    '        strSQL = " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
    '            " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax, p.fkRedondeo, p.fkAplicacionRedondeo,'' as Id_Cabecero_Detalle,RDPI.DescuentoCascada from tbPR_promociones P WITH (NOLOCK) " & _
    '            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '            " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '            "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " And P.IdPromocion in (" & EsNuloN(lngIdPromo) & ")" & _
    '            " And (" & _
    '            " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '            " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK)))"

    '        If strPromoTodosArticulos <> "" Then
    '            ' 04/09/2013 añadir campo descuentocascada
    '            strSQL = strSQL & " UNION " & _
    '            " Select distinct P.Promocion, RDPI.Descuento ,P.FechaValidezInicio,P.FechaValidezFin,TP.TipoPromocion,TP.IdTipoPromocion," & _
    '            " P.IdPromocion,RDPI.Restricciones,RDPI.ValorVenta,RDPI.DescuentoMax, p.fkRedondeo, p.fkAplicacionRedondeo,'' as Id_Cabecero_Detalle,RDPI.DescuentoCascada from tbPR_promociones P WITH (NOLOCK) " & _
    '            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '            " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & _
    '            "' and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " And P.IdPromocion in " & EsNuloT(strPromoTodosArticulos) & _
    '            " And (" & _
    '            " P.IdPromocion in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK) where fkCliente=" & EsNuloN(lngClienteID) & " ) " & _
    '            " Or P.IdPromocion not in (select fkPromocion from tbPR_clientesBeneficiarios WITH (NOLOCK)))"
    '            strSQL = strSQL & " ORDER By P.FechaValidezInicio"
    '        Else
    '            strSQL = strSQL & " ORDER By P.FechaValidezInicio"
    '        End If

    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            blnSigue = True
    '            IdpromocionSeleccionada = EsNuloN(rsADO!IdPromocion)
    '            IdTipoPromocionSeleccionada = EsNuloN(rsADO!IdTipoPromocion)

    '        End If
    '        CloseRS(rsADO)


    '        If blnSigue Then
    '            ' 04/07/2013 añadimos p.idpromocion
    '            ' 18/07/2013 añadimos trc.tiendascanje
    '            strSQL = "Select P.Promocion, P.fkTipoPromocion,TRC.ValorCupon,TRC.fkTipoValorCupon,TRC.FechaCanjeInicio,TRC.FechaCanjeFinal,TRC.ValorVenta,P.fkRedondeo, P.fkAplicacionRedondeo,TRC.TributaIVA,TRC.TextoAvisoCupones,p.idpromocion,trc.tiendascanje " & _
    '                        " from tbPR_promociones P WITH (NOLOCK) " & _
    '                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
    '                        " Inner join tbPR_recompensasCupones TRC WITH (NOLOCK) ON P.IdPromocion= TRC.fkPromocion " & _
    '                        " where P.FechaValidezInicio <= '" & EC(gdteFechaSesion) & "' And P.FechaValidezFin >= '" & EC(gdteFechaSesion) & "'" & _
    '                        " and T.fkTienda= '" & EC(gstrTiendaSesion) & "' And P.fkTipoPromocion in " & strTipoPromo & " And p.IdPromocion=" & IdpromocionSeleccionada & " Order by P.FechaValidezInicio"
    '            rsADO = New ADODB.Recordset
    '            rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '            If Not rsADO.EOF Then
    '                While Not rsADO.EOF
    '                    dblTotalVale = 0 : strObservacionVale = ""

    '                    'Nueva opción de filtrar si la venta es superior al importe establecido
    '                    If dblTotalTickets >= EsNuloN(rsADO!Valorventa) Then
    '                        If EsNuloN(rsADO!fkTipoValorCupon) = 1 Then
    '                            dblTotalVale = EsNuloN(rsADO!ValorCupon)
    '                            IdTipoValorSeleccionada = 1
    '                        ElseIf EsNuloN(rsADO!fkTipoValorCupon) = 2 Then
    '                            dblTotalVale = Round((dblTotalTickets * EsNuloN(rsADO!ValorCupon)) / 100, 2)
    '                            IdTipoValorSeleccionada = 2
    '                        ElseIf EsNuloN(rsADO!fkTipoValorCupon) = 3 Then
    '                            dblTotalVale = EsNuloN(rsADO!ValorCupon)
    '                            IdTipoValorSeleccionada = 3
    '                        End If

    '                        'Aplicar Redondeo
    '                        If EsNuloN(rsADO!fkRedondeo) = 1 Then
    '                            dblTotalVale = dblTotalVale
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 2 Then
    '                            If Int(dblTotalVale) <> dblTotalVale Then
    '                                dblTotalVale = Int(dblTotalVale) + 1
    '                            End If
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 3 Then
    '                            dblTotalVale = Int(dblTotalVale)
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 4 Then
    '                            dblTotalVale = Round(dblTotalVale, 0)
    '                        ElseIf EsNuloN(rsADO!fkRedondeo) = 5 Then
    '                            dblTotalVale = Int(dblTotalVale)
    '                        End If

    '                        'Validamos Si el cupon tributara Iva o No
    '                        If Trim(EsNuloT(rsADO!TributaIva)) = "" Then
    '                            blnTributaIVA = True
    '                        ElseIf CBool(EsNuloN(rsADO!TributaIva)) Then
    '                            blnTributaIVA = True
    '                        Else
    '                            blnTributaIVA = False
    '                        End If

    '                        If dblTotalVale > 0 Then


    '                            If IdTipoPromocionSeleccionada = 35 Then

    '                                '************************************************
    '                                '***** PROMOCION CUPONES RETENCION CLIENTE  *****
    '                                '************************************************

    '                                If Not blnTributaIVA Then
    '                                    strObservacionVale = "Bono Descuento Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                Else
    '                                    strObservacionVale = "Vale Promoción Canjeable Del " & EsNuloT(rsADO!FechaCanjeInicio) & " Al " & EsNuloT(rsADO!FechaCanjeFinal) & "."
    '                                End If

    '                                strObservacionVale = strObservacionVale & "[" & strNunAsigBanco & "]"

    '                                'generamos el vale
    '                                lngID = GetMaxIdAuto(cnADO, "SELECT Max(Id_vale) AS TOT FROM N_VALES WITH (NOLOCK)  WHERE Id_Tienda='" & gstrTiendaSesion & "'") + 1

    '                                '**** ESTRUCTURA DE TIPO VALE: 1.Identf.= PROMO 2.Separador= ý 3.IDPROMOCION
    '                                '4.Separador= ý 5.CampoDescuento= 1.Descuento Total/0.Vale Normal

    '                                If gblDirFtp <> FTP_NINEWEST_MEX Then
    '                                    If IdTipoValorSeleccionada = 3 Then
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý1"
    '                                    Else
    '                                        strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion) & "ý0"
    '                                    End If
    '                                ElseIf gblDirFtp = FTP_NINEWEST_MEX Then
    '                                    strTipoVale = "PROMOý" & EsNuloT(rsADO!IdPromocion)
    '                                End If


    '                                strSQL = "INSERT INTO N_VALES(Id_Vale,Id_Tienda,Id_Ticket,Id_Empleado,DNI,FechaEmision,cValePts,cValeEuro,sVenta,ID_TERMINAL_EMITIDO,Observaciones,RefFP,TributaIVA,TipoVale,TiendasCanje,Id_Cliente) VALUES(" & _
    '                                    lngID & ",'" & gstrTiendaSesion & "','" & strIdTicket & "'," & EsNuloN(frmTickets.txtEmpleado.Text) & ",'0','" & Format$(gdteFechaSesion, gLocaleInfo.ShortDateFormat) & "'," & _
    '                                    EurosPesetas(Abs(dblTotalVale)) & "," & SinComas(Abs(dblTotalVale)) & ",'1','" & gstrTerminalID & "','" & EsNuloT(strObservacionVale) & "','GARA'," & _
    '                                    IIf(blnTributaIVA, 1, 0) & ",'" & EsNuloT(strTipoVale) & "','" & EsNuloT(rsADO!tiendasCanje) & "'," & lngClienteID & ")"

    '                                cnADO.Execute(strSQL, , adExecuteNoRecords)

    '                                If gblnNewFPagos Then
    '                                    '*** Graba transaccion
    '                                    '*** Fran - 21/11/12 - Si es un vale que tributa iva tiene el titulo no puede ser Bono descuento
    '                                    Save_Hist_Transaccion(cnADO, IIf(blnTributaIVA, "Vale Promoción", "Bono Descuento"), _
    '                                        lngID & IIf(gstrTerminalID = "", "", " - " & gstrTerminalID) & "/" & gstrTiendaSesion, Abs(dblTotalVale))
    '                                End If
    '                                '*** Impresion de Vale de Promocion
    '                                ImprimeValeNW_GESTORPROMOCIONES(cnADO, lngID, gstrTiendaSesion, _
    '                                    IIf(clsLANG.GetIdiomaID = 8, True, False), frmTickets.brcBarras, "Vale", strObservacionVale & IIf(EsNuloT(rsADO!TextoAvisoCupones) <> "", " " & EsNuloT(rsADO!TextoAvisoCupones), ""), "", "", IdTipoValorSeleccionada, True, GetNombreCliente(cnADO, EsNuloN(lngClienteID)), GetCIFClienteGeneral(cnADO, EsNuloN(lngClienteID)))


    '                                '*** Actualizacion Online de Tienda ( VALES ) ***
    '                                Subir_OnLine(frmTickets.hwnd, gstrTiendaSesion, gstrTerminalID, gdteFechaSesion, "VALES", lngID & "#" & gstrTiendaSesion, "UP")

    '                                '*** Actualizacion Estados ( VALES ) ***
    '                                Call DesactivarValesPromosCupones()


    '                            End If
    '                        End If
    '                    End If
    '                    rsADO.MoveNext()
    '                End While
    '            End If
    '        End If

    'ReparaErrores:
    '        If blnTrans Then cnADO.RollbackTrans() : blnTrans = False
    '        CloseRS(rsADO) : CloseRS(rsADOTicket)
    '        If Err Then
    '            Call ReparaErrores("modPromociones", "BuscoPromoCuponesRetencionCli", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    '    Public Function ValidoPromosValidasImpresionSinImagen_FP(ByVal strTicket As String, ByVal lngIdPromo As Long) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim dblDto As Double
    '        Dim blnTodasFP As Boolean
    '        Dim blnModdocardFP As Boolean

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        ValidoPromosValidasImpresionSinImagen_FP = 0 : blnModdocardFP = False

    '        '****** BUSCAMOS LAS FORMAS DE PAGO DE LA PROMOCION A IMPRIMIR ******


    '        '****** VERIFICAMOS SI ESTAN TODAS LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******
    '        blnTodasFP = False
    '        strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion   " & _
    '               " from tbPR_promociones P " & clsBD.NOLOCK() & _
    '               " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & " ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '               " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '               " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '               " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FP.fkPromocion " & _
    '               " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '               " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " )"
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            blnTodasFP = False
    '            While Not rsADO.EOF
    '                If EsNuloT(rsADO!Descripcion_pago) = "MODDOCARD" Then
    '                    blnModdocardFP = True
    '                End If
    '                rsADO.MoveNext()
    '            End While
    '        Else
    '            blnTodasFP = True
    '            ValidoPromosValidasImpresionSinImagen_FP = ValidoPromosValidasImpresionSinImagen_FP + 1
    '        End If

    '        CloseRS(rsADO)


    '        If Not blnTodasFP Then

    '            '****** VERIFICAMOS  LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******

    '            strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,TFP.tipo,P.Promocion,P.idPromocion,FPD.fkorden, " & _
    '             " ISNULL(TFPD.Descripcion,'') as Descripcion_Pago_Detalle   from tbPR_promociones P " & clsBD.NOLOCK() & _
    '             " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & "  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '             " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '             " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '             " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & " ON P.IdPromocion=  FP.fkPromocion " & _
    '             " LEFT join tbPR_formasPagodetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '             " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '             " LEFT join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & " ON TFP.IdPago=TFPD.idpago and  FPD.fkorden = TFPD.IdOrden " & _
    '             " INNER JOIN N_TICKETS_FPAGOS FTP " & clsBD.NOLOCK() & " ON FTP.id_ticket='" & strTicket & "' and FTP.Tipo=TFP.tipo" & _
    '             " and FTP.FPago = TFP.Descripcion_Pago and FTP.FPagoDetalle= ISNULL(TFPD.Descripcion,'') " & _
    '             " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " )"

    '            rsADO = New ADODB.Recordset
    '            rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '            If Not rsADO.EOF Then
    '                While Not rsADO.EOF
    '                    ValidoPromosValidasImpresionSinImagen_FP = ValidoPromosValidasImpresionSinImagen_FP + 1
    '                    rsADO.MoveNext()
    '                End While
    '            Else
    '                ValidoPromosValidasImpresionSinImagen_FP = 0
    '            End If

    '            CloseRS(rsADO)

    '            '******* VALIDAMOS SI EXISTE MODDOCARD *******
    '            If ValidoPromosValidasImpresionSinImagen_FP = 0 And blnModdocardFP Then
    '                strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,TFP.tipo,P.Promocion,P.idPromocion,FPD.fkorden, " & _
    '                   " ISNULL(TFPD.Descripcion,'') as Descripcion_Pago_Detalle   from tbPR_promociones P " & clsBD.NOLOCK() & _
    '                   " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & "  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                   " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '                   " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                   " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & " ON P.IdPromocion=  FP.fkPromocion " & _
    '                   " LEFT join tbPR_formasPagodetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '                   " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '                   " LEFT join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & " ON TFP.IdPago=TFPD.idpago and  FPD.fkorden = TFPD.IdOrden " & _
    '                   " INNER JOIN N_TICKETS_FPAGOS FTP " & clsBD.NOLOCK() & " ON FTP.id_ticket='" & strTicket & "' and FTP.Tipo=TFP.tipo" & _
    '                   " and FTP.FPago = TFP.Descripcion_Pago " & _
    '                   " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " ) And TFP.Descripcion_Pago='MODDOCARD' "

    '                rsADO = New ADODB.Recordset
    '                rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '                If Not rsADO.EOF Then
    '                    While Not rsADO.EOF
    '                        ValidoPromosValidasImpresionSinImagen_FP = ValidoPromosValidasImpresionSinImagen_FP + 1
    '                        rsADO.MoveNext()
    '                    End While
    '                Else
    '                    ValidoPromosValidasImpresionSinImagen_FP = 0
    '                End If
    '            End If



    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoPromosValidasImpresionSinImagen_FP", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    Public Sub InsertoDescuentoEscalonado(ByVal lngIdpromocion As Long, ByVal dblDtoEscalonado As Double, ByVal dblDtoInicial As Double, _
                 ByVal dblDtoIncremento As Double, ByVal lngUnidades As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal lngRegRecalculo As Long)

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim intI As Long
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim dblDtoEscalonadoReal As Double
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)
        Dim intF As Long = 0
        Dim lngCountFila, lngRegRejilla As Long


        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueBusqueda = False : blnTallasC = False
            dblDtoEscalonadoReal = dblDtoEscalonado : intI = 1


            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)

            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()


            If blnArticulosBeneficiarios Then

                '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
                If gblLngIdPromocionTodos Then
                    For Each objpro As Promocion In objPromo
                        If intI >= lngUnidades Then

                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                If intI = lngUnidades Then
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial)), True, True, False)
                                Else
                                    dblDtoEscalonado = (dblDtoIncremento * (intI - lngUnidades))
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial) + clsMU.EsNuloN(dblDtoEscalonado)), True, True, False)
                                End If
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                If intI = lngUnidades Then
                                    dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDtoInicial))
                                    dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoDiferencias)), True, True, False)
                                Else
                                    dblDtoEscalonado = dblDtoInicial * (intI - lngUnidades)
                                    dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - ((dblDtoInicial) + clsMU.EsNuloN(dblDtoEscalonado)))
                                    dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoDiferencias)), True, True, False)
                                End If

                            End If

                            '**** REDONDEO PROMO ****
                            If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                Select Case gblfkRedondeo
                                    'Case 1, 2 ' redondeo por exceso
                                    ' 15/11/2012
                                    Case 2 ' redondeo por exceso
                                        objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                    Case 3 ' redondeo por defecto                                        
                                        objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                End Select
                                objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                            End If
                            objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                            '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                            objpro.Promo = Nothing
                            If objpro.Promo Is Nothing Then
                                If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                                objPD.DescriPromo = strDescPromo.ToString
                                objPD.DescriAmpliaPromo = strDescPromo.ToString
                                objPD.DtoPromo = objpro.DtoEuro
                                objPD.Idpromo = lngIdpromocion
                                objPD.PromocionSelecionada = True

                                ppD.Add(objPD)
                                objpro.Promo = ppD.AsEnumerable()
                                intF += 1
                            End If
                        End If
                        intI += 1
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                    Next
                Else
                    For Each objpro As Promocion In objPromo
                        If intI >= lngUnidades Then
                            'TENEMOS LOS ARTICULOS BENEFICIARIOS
                            ' 04/09/2013 añadir campo descuentocascada
                            strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                                " from tbPR_promociones P WITH (NOLOCK) " & _
                                " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                                " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                                " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='') " & _
                                " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                                " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                                For Each FilaI In dtrsADO.Tables(0).Rows
                                    blnTallasC = False
                                    '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                                    If strTallasValidas <> "" Then
                                        If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                            blnTallasC = True
                                        End If
                                    End If

                                    If strTallasValidas = "" Or blnTallasC Then
                                        '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                                        If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                            If intI = lngUnidades Then
                                                objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial)), True, True, False)
                                            Else
                                                dblDtoEscalonado = (dblDtoIncremento * (intI - lngUnidades))
                                                objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial) + clsMU.EsNuloN(dblDtoEscalonado)), True, True, False)
                                            End If
                                        ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                            If intI = lngUnidades Then
                                                dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDtoInicial))
                                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                                objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoDiferencias)), True, True, False)
                                            Else
                                                dblDtoEscalonado = dblDtoInicial * (intI - lngUnidades)
                                                dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - ((dblDtoInicial) + clsMU.EsNuloN(dblDtoEscalonado)))
                                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                                objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoDiferencias)), True, True, False)
                                            End If

                                        End If


                                        '**** REDONDEO PROMO ****
                                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                            Select Case gblfkRedondeo
                                                'Case 1, 2 ' redondeo por exceso
                                                ' 15/11/2012
                                                Case 2 ' redondeo por exceso
                                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                                Case 3 ' redondeo por defecto                                        
                                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                            End Select
                                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                        End If
                                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                        objpro.Promo = Nothing
                                        If objpro.Promo Is Nothing Then
                                            If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                                            objPD.DescriPromo = FilaI.item("Promocion").ToString
                                            objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                            objPD.DtoPromo = objpro.DtoEuro
                                            objPD.Idpromo = lngIdpromocion
                                            objPD.PromocionSelecionada = True

                                            ppD.Add(objPD)
                                            objpro.Promo = ppD.AsEnumerable()
                                            intF += 1
                                        End If
                                    End If
                                Next
                                dtrsADO.Dispose()
                            End If
                        End If
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                        intI += 1
                    Next
                End If

            Else
                For Each objpro As Promocion In objPromo
                    If intI >= lngUnidades Then
                        'TENEMOS LOS ARTICULOS BENEFICIARIOS
                        ' 04/09/2013 añadir campo descuentocascada
                        strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                            " from tbPR_promociones P WITH (NOLOCK) " & _
                            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                            " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                        If dtrsADO.Tables(0).Rows.Count <> 0 Then
                            For Each FilaI In dtrsADO.Tables(0).Rows
                                If intI = lngUnidades Then
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial)), True, True, False)
                                Else
                                    dblDtoEscalonado = dblDtoInicial * (intI - lngUnidades)
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoInicial) + clsMU.EsNuloN(dblDtoEscalonado)), True, True, False)
                                End If

                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), clsMU.EsNuloN(dblDtoEscalonado))

                                '**** REDONDEO PROMO ****
                                If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                    Select Case gblfkRedondeo
                                        'Case 1, 2 ' redondeo por exceso
                                        ' 15/11/2012
                                        Case 2 ' redondeo por exceso
                                            objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                        Case 3 ' redondeo por defecto                                        
                                            objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                    End Select
                                    objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                End If
                                objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                                objpro.Promo = Nothing
                                If objpro.Promo Is Nothing Then
                                    If intF > 0 Then objPD = New DetallePromo : ppD = New List(Of DetallePromo)
                                    objPD.DescriPromo = FilaI.item("Promocion").ToString
                                    objPD.DescriAmpliaPromo = FilaI.item("Promocion").ToString
                                    objPD.DtoPromo = objpro.DtoEuro
                                    objPD.Idpromo = lngIdpromocion
                                    objPD.PromocionSelecionada = True

                                    ppD.Add(objPD)
                                    objpro.Promo = ppD.AsEnumerable()
                                    intF += 1
                                End If
                            Next
                        End If
                        dtrsADO.Dispose()
                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    intI += 1
                Next

            End If

            dblDtoEscalonado = dblDtoEscalonadoReal


        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoDescuentoEscalonado " & ex.Message, ex.InnerException)
        End Try

    End Sub


    '    Public Function ValidoPromosValidas_FP(ByVal strTicket As String, ByVal lngIdPromo As Long, ByVal strOP As String) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim intI As Long
    '        Dim dblDto As Double
    '        Dim blnTodasFP As Boolean
    '        Dim blnModdocardFP As Boolean
    '        Dim strDetallesPagoM As String
    '        Dim arrDetallesPagoM

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        ValidoPromosValidas_FP = 0 : blnModdocardFP = False : strDetallesPagoM = ""

    '        '****** BUSCAMOS LAS FORMAS DE PAGO DE LA PROMOCION A IMPRIMIR ******


    '        '****** VERIFICAMOS SI ESTAN TODAS LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******
    '        blnTodasFP = False
    '        strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion   " & _
    '               " from tbPR_promociones P " & clsBD.NOLOCK() & _
    '               " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & " ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '               " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '               " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '               " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FP.fkPromocion " & _
    '               " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '               " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " )"
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            blnTodasFP = False
    '            While Not rsADO.EOF
    '                If EsNuloT(rsADO!Descripcion_pago) = "MODDOCARD" Then
    '                    blnModdocardFP = True
    '                End If
    '                rsADO.MoveNext()
    '            End While
    '        Else
    '            blnTodasFP = True
    '            ValidoPromosValidas_FP = ValidoPromosValidas_FP + 1
    '        End If

    '        CloseRS(rsADO)


    '        '******* FORMAS DE PAGO VALIDAS CON TARJETA SI EXISTE MODDOCARD *******
    '        If Not blnTodasFP Then
    '            strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,TFP.tipo,P.Promocion,P.idPromocion,FPD.fkorden, " & _
    '               " ISNULL(TFPD.Descripcion,'') as Descripcion_Pago_Detalle   from tbPR_promociones P " & clsBD.NOLOCK() & _
    '               " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & "  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '               " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '               " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '               " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & " ON P.IdPromocion=  FP.fkPromocion " & _
    '               " LEFT join tbPR_formasPagodetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '               " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '               " LEFT join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & " ON TFP.IdPago=TFPD.idpago and  FPD.fkorden = TFPD.IdOrden " & _
    '               " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " ) And TFP.Descripcion_Pago='TARJETA' "

    '            rsADO = New ADODB.Recordset
    '            rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '            If Not rsADO.EOF Then
    '                blnTodasFP = False
    '                While Not rsADO.EOF
    '                    strDetallesPagoM = strDetallesPagoM & EsNuloT(rsADO!Descripcion_Pago_Detalle) & "|"
    '                    rsADO.MoveNext()
    '                End While
    '                'strDetallesPagoM = Mid(strDetallesPagoM, 1, Len(strDetallesPagoM) - 1)
    '            Else
    '                strDetallesPagoM = ""
    '            End If

    '            CloseRS(rsADO)
    '        End If



    '        If Not blnTodasFP Then

    '            '****** VERIFICAMOS  LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******

    '            strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,TFP.tipo,P.Promocion,P.idPromocion,FPD.fkorden, " & _
    '             " ISNULL(TFPD.Descripcion,'') as Descripcion_Pago_Detalle   from tbPR_promociones P " & clsBD.NOLOCK() & _
    '             " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & "  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '             " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '             " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '             " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & " ON P.IdPromocion=  FP.fkPromocion " & _
    '             " LEFT join tbPR_formasPagodetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '             " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '             " LEFT join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & " ON TFP.IdPago=TFPD.idpago and  FPD.fkorden = TFPD.IdOrden " & _
    '             " INNER JOIN N_TICKETS_FPAGOS FTP " & clsBD.NOLOCK() & " ON FTP.id_ticket='" & strTicket & "' and FTP.Tipo=TFP.tipo" & _
    '             " and FTP.FPago = TFP.Descripcion_Pago and FTP.FPagoDetalle= ISNULL(TFPD.Descripcion,'') " & _
    '             " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " )"

    '            rsADO = New ADODB.Recordset
    '            rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '            If Not rsADO.EOF Then
    '                While Not rsADO.EOF
    '                    ValidoPromosValidas_FP = ValidoPromosValidas_FP + 1
    '                    rsADO.MoveNext()
    '                End While
    '            Else
    '                ValidoPromosValidas_FP = 0
    '            End If

    '            CloseRS(rsADO)

    '            '******* VALIDAMOS SI EXISTE MODDOCARD *******
    '            If ValidoPromosValidas_FP = 0 And blnModdocardFP Then
    '                strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,TFP.tipo,P.Promocion,P.idPromocion,FPD.fkorden, " & _
    '                   " ISNULL(TFPD.Descripcion,'') as Descripcion_Pago_Detalle, FTP.FPagoDetalle    from tbPR_promociones P " & clsBD.NOLOCK() & _
    '                   " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & "  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '                   " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '                   " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '                   " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & " ON P.IdPromocion=  FP.fkPromocion " & _
    '                   " LEFT join tbPR_formasPagodetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '                   " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '                   " LEFT join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & " ON TFP.IdPago=TFPD.idpago and  FPD.fkorden = TFPD.IdOrden " & _
    '                   " INNER JOIN N_TICKETS_FPAGOS FTP " & clsBD.NOLOCK() & " ON FTP.id_ticket='" & strTicket & "' and FTP.Tipo=TFP.tipo" & _
    '                   " and FTP.FPago = TFP.Descripcion_Pago " & _
    '                   " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " ) And TFP.Descripcion_Pago='MODDOCARD' "

    '                rsADO = New ADODB.Recordset
    '                rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '                If Not rsADO.EOF Then
    '                    If strDetallesPagoM <> "" And InStr(strDetallesPagoM, "|") > 0 Then
    '                        arrDetallesPagoM = Split(strDetallesPagoM, "|")
    '                    End If

    '                    While Not rsADO.EOF
    '                        If UBound(arrDetallesPagoM) > 0 Then
    '                            For intI = 0 To UBound(arrDetallesPagoM)
    '                                If InStr(arrDetallesPagoM(intI), EsNuloT(rsADO!FpagoDetalle)) > 0 Then
    '                                    ValidoPromosValidas_FP = ValidoPromosValidas_FP + 1
    '                                End If
    '                            Next
    '                        End If
    '                        rsADO.MoveNext()
    '                    End While
    '                Else
    '                    ValidoPromosValidas_FP = 0
    '                End If
    '            End If



    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoPromosValidas_FP", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    '    Public Function ValidoFP_Promo(ByVal lngIdPromo As Long, ByVal strFP As String, ByVal strFPD As String) As Boolean

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)


    '        '****** VERIFICAMOS SI ESTAN TODAS LAS FORMAS DE PAGOS VALIDAS PARA LA PROMO ******
    '        ValidoFP_Promo = False
    '        strSQL = "Select DISTINCT FP.fkPago,TFP.Descripcion_Pago,P.Promocion,P.idPromocion,TFPD.Descripcion  " & _
    '               " from tbPR_promociones P " & clsBD.NOLOCK() & _
    '               " Inner join tbPR_tiposPromocion TP " & clsBD.NOLOCK() & " ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
    '               " Inner join tbPR_tiendasBeneficiarias T " & clsBD.NOLOCK() & " ON P.IdPromocion= T.fkPromocion " & _
    '               " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI " & clsBD.NOLOCK() & "  ON P.IdPromocion=  RDPI.fkPromocion " & _
    '               " Inner join tbPR_formasPago FP " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FP.fkPromocion " & _
    '               " Inner join tbPR_formasPagoDetalle FPD " & clsBD.NOLOCK() & "  ON P.IdPromocion=  FPD.fkPromocion " & _
    '               " Inner join TIPOS_DE_PAGO TFP " & clsBD.NOLOCK() & "  ON FP.fkPago=  TFP.IdPago " & _
    '               " Inner join TIPOS_DE_PAGO_DETALLES TFPD " & clsBD.NOLOCK() & "  ON FPD.fkPago=  TFPD.IdPago AND FPD.fkOrden=  TFPD.IdOrden " & _
    '               " where  P.IdPromocion in ( " & EsNuloN(lngIdPromo) & " ) AND TFPD.Descripcion='" & strFPD & "'"
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            ValidoFP_Promo = True
    '        Else
    '            ValidoFP_Promo = False
    '        End If

    '        CloseRS(rsADO)



    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidoFP_Promo", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    '    Public Function ValidaBonosCanjeables(ByVal IdPromo As Long) As Boolean

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        strSQL = "select BonoCanjeable from tbPR_promociones " & clsBD.NOLOCK() & _
    '            " where idpromocion= " & IdPromo
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            ValidaBonosCanjeables = CBool(rsADO!BonoCanjeable)
    '        Else
    '            ValidaBonosCanjeables = False
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "ValidaBonosCanjeables", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function


    '    Public Function GetBonoCanjeablesCliente(ByVal IdPromo As Long, ByVal idCliente As Long) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        strSQL = "select NumBono from tbPR_ClientesBeneficiarios " & clsBD.NOLOCK() & _
    '            " where fkpromocion= " & IdPromo & " And fkCliente= " & idCliente
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetBonoCanjeablesCliente = EsNuloN(rsADO!numBono)
    '        Else
    '            GetBonoCanjeablesCliente = 0
    '        End If

    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GetBonoCanjeablesCliente", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    '    Public Function GetBonoCobradosCliente(ByVal IdPromo As Long, ByVal idCliente As Long) As Long

    '        Dim cnADO As ADODB.Connection
    '        Dim rsADO As ADODB.Recordset
    '        Dim strSQL As String
    '        Dim FIni, FFin As Date

    '        On Error GoTo ReparaErrores

    '        'Abre conexion a base de datos
    '        cnADO = New ADODB.Connection
    '        ConnectBD(cnADO)

    '        '**** Buscamos las fechas Validas para la busqueda de los bonos Cobrados ****
    '        strSQL = "select FechaValidezInicio,FechaValidezFin from tbPR_Promociones " & clsBD.NOLOCK() & _
    '            " where Idpromocion= " & IdPromo
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            FIni = Format(EsNuloT(rsADO!FechaValidezInicio), gLocaleInfo.ShortDateFormat)
    '            FFin = Format(EsNuloT(rsADO!FechaValidezFin), gLocaleInfo.ShortDateFormat)
    '        Else
    '            FIni = Format(gdteFechaSesion, gLocaleInfo.ShortDateFormat)
    '            FFin = Format(gdteFechaSesion, gLocaleInfo.ShortDateFormat)
    '        End If

    '        CloseRS(rsADO)

    '        strSQL = "SELECT COUNT(*) As TotalBonoCobrados  from N_CUPONES_DESCUENTO cd With (NOLOCK) " & _
    '           " INNER JOIN N_TICKETS t With (NOLOCK) On cd.Id_Ticket=t.Id_Ticket " & _
    '           " WHERE cd.Cobrado=1 AND cd.FechaCobrado is not null AND t.Id_Cliente_N = " & idCliente & _
    '            clsBD.SQL_POR_FECHAS(" AND ", "cd.FechaCobrado", Format(FIni, gLocaleInfo.ShortDateFormat), Format(FFin, gLocaleInfo.ShortDateFormat))
    '        rsADO = New ADODB.Recordset
    '        rsADO.Open(strSQL, cnADO, adOpenStatic, adLockReadOnly)
    '        If Not rsADO.EOF Then
    '            GetBonoCobradosCliente = EsNuloN(rsADO!TotalBonoCobrados)
    '        Else
    '            GetBonoCobradosCliente = 0
    '        End If


    'ReparaErrores:
    '        CloseRS(rsADO)
    '        CloseCN(cnADO)
    '        If Err Then
    '            Call ReparaErrores("ModPromociones", "GetBonoCobradosCliente", Err.Description, Err.Number)
    '            Err.Clear()
    '        End If
    '    End Function

    Public Function InsertoArtBeneficiarioDescuentoMenor_SPRING(lngIdpromocion As Long, ByVal lngTipoPromocion As Long, _
                    ByVal lngRegRecalculo As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal dblPVPVig As Double, Optional blnPrimeraB As Boolean = False) As Boolean

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim intI As Long
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim dblDescuento As Double
        Dim strPromocion As String
        Dim blnSigueDescuento As Boolean
        Dim lngRegRejilla As Long
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim lngCountFila As Long
        Dim lngCountMenor As Long = 0
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades

            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueDescuento = False : blnTallasC = False : lngCountMenor = 1

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", objPromo.Count, objPromo.Count)


            '*** CREAMOS TABLA TEMPORAL PARA CARGAR LOS DATOS DE LA REJILLA DE DATOS ***            
            clsAD.EjecutarSQL("DROP TABLE #REJILLA_VENTA")
            clsAD.EjecutarSQL("CREATE TABLE #REJILLA_VENTA(IdArticulo [int] NOT NULL,Dto [FLOAT] NOT NULL,PVP [FLOAT] NOT NULL,POS [int] NOT NULL)")

            '***** INSERTAMOS LOS DATOS DE LA REJILLA EN LA NUEVA TABLA *****

            If gblLngIdPromocionTodos Then

                For Each objpro As Promocion In objPromo
                    If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                        '*** INSERTAMOS LOS VALORES EN LA NUEVA TABLA ***
                        clsAD.EjecutarSQL("INSERT INTO #REJILLA_VENTA(IdArticulo,Dto,PVP,POS) VALUES (" & clsMU.EsNuloN(objpro.Id_Articulo) & "," & clsMU.EsNuloN(objpro.DtoEuro) & "," & clsMU.EsNuloN(objpro.Pvp_Vig) & "," & lngCountMenor & ")")
                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    lngCountMenor += 1
                Next

            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK)  ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK)  ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK)  ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)   ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows

                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then
                                    '*** INSERTAMOS LOS VALORES EN LA NUEVA TABLA ***
                                    clsAD.EjecutarSQL("INSERT INTO #REJILLA_VENTA(IdArticulo,Dto,PVP,POS) VALUES (" & clsMU.EsNuloN(objpro.Id_Articulo) & "," & clsMU.EsNuloN(objpro.DtoEuro) & "," & clsMU.EsNuloN(objpro.Pvp_Vig) & "," & lngCountMenor & ")")
                                End If
                            End If

                        Next
                    End If
                    dtrsADO.Dispose()
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    lngCountMenor += 1
                Next
            End If


            '***** BUSCASMOS EL MENOR DE LA TABLA 3X2 *****

            strSQL = "Select * FROM #REJILLA_VENTA WITH (NOLOCK) order by PVP desc"
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                intI = 0
                For Each FilaI In dtrsADO.Tables(0).Rows
                    intI = intI + 1
                    If intI <= 3 Then
                        If IntMenor = 0 Then IntMenor = clsMU.EsNuloN(FilaI.item("pos")) : dblPrecioMenor = clsMU.EsNuloN(FilaI.item("PVP"))
                        If clsMU.EsNuloN(FilaI.item("PVP")) <= dblPrecioMenor Then
                            IntMenor = clsMU.EsNuloN(FilaI.item("pos"))
                            dblPrecioMenor = clsMU.EsNuloN(FilaI.item("PVP"))
                            dblDescuento = GetDctoPromo(lngIdpromocion, lngTipoPromocion)
                            strPromocion = GetNombrePromo(lngIdpromocion, lngTipoPromocion)
                        End If
                    End If
                    If intI = 3 Then Exit For
                Next
            End If
            dtrsADO.Dispose()




            'valido que si es el que entra menor valor asigno el descuento
            If IntMenor <> 0 And dblPrecioMenor <= clsMU.EsNuloN(dblPVPVig) Then
                blnSigueDescuento = True
            ElseIf IntMenor <> 0 And blnPrimeraB Then
                blnSigueDescuento = True
            ElseIf IntMenor <> 0 And dblPrecioMenor > clsMU.EsNuloN(dblPVPVig) Then
                blnSigueDescuento = False
            End If

            If gblstrTipoPromocion = "RECALCULO" Then
                blnSigueDescuento = True
            End If


            If IntMenor <> 0 And blnSigueDescuento Then
                lngCountFila = 0 : lngCountMenor = 1
                For Each objpro As Promocion In objPromo
                    If lngCountMenor = IntMenor Then
                        If lngTipoPromocion = 13 Or lngTipoPromocion = 22 Or lngTipoPromocion = 23 Or lngTipoPromocion = 24 Or lngTipoPromocion = 27 Then
                            objpro.Pvp_Venta = dblDescuento
                        ElseIf lngTipoPromocion = 11 Or lngTipoPromocion = 16 Or lngTipoPromocion = 18 Or lngTipoPromocion = 20 Or lngTipoPromocion = 36 Then
                            '*** TIPO DE DESCUENTO (IMPORTE/%) ****
                            If clsMU.EsNuloN(gblLngTipoDescuento) = 0 Then
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDescuento)
                            ElseIf clsMU.EsNuloN(gblLngTipoDescuento) = 1 Then
                                dblDiferencia = (clsMU.EsNuloN(objpro.Pvp_Vig) - clsMU.EsNuloN(dblDescuento))
                                dblDtoDiferencias = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), dblDiferencia)
                                objpro.Pvp_Venta = clsMU.GetDtoPc(clsMU.EsNuloN(objpro.Pvp_Vig), dblDtoDiferencias)
                            End If
                        End If

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                        If objpro.Promo Is Nothing Then
                            objPD.DescriPromo = strPromocion.ToString
                            objPD.DescriAmpliaPromo = strPromocion.ToString
                            objPD.DtoPromo = objpro.DtoEuro
                            objPD.Idpromo = lngIdpromocion
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()

                        End If

                    End If
                    lngCountFila += 1
                    lngCountMenor += 1
                Next
            End If


            InsertoArtBeneficiarioDescuentoMenor_SPRING = blnSigueDescuento

            '*** BORRAMOS LA TABLA TEMPORAL ***
            clsAD.EjecutarSQL("DROP TABLE #REJILLA_VENTA")

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtBeneficiarioDescuentoMenor_SPRING " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function GetDtoMaxRejilla(ByRef objPromo As IEnumerable(Of Promocion)) As Double

        Dim dtrsADO As DataSet = New DataSet
        Dim intI As Long
        Dim IntMayor As Long
        Dim dblPrecioMAyor As Double
        Dim lngRegRejilla As Long
        Dim lngCountFila As Long

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            GetDtoMaxRejilla = 0

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", objPromo.Count, objPromo.Count)


            '***** BUSCAMOS EL DESCUENTO MAYOR *****
            For Each objpro As Promocion In objPromo

                If clsMU.EsNuloN(objpro.DtoEuro) <> 0 Then
                    If IntMayor = 0 Then IntMayor = intI : dblPrecioMAyor = clsMU.EsNuloN(objpro.DtoEuro)
                    If clsMU.EsNuloN(objpro.DtoEuro) >= dblPrecioMAyor Then
                        IntMayor = intI
                        dblPrecioMAyor = clsMU.EsNuloN(objpro.DtoEuro)
                    End If
                End If
                If lngCountFila = lngRegRejilla Then Exit For
                lngCountFila += 1
            Next

            GetDtoMaxRejilla = dblPrecioMAyor

        Catch ex As Exception
            Throw New Exception("ModPromociones-GetDtoMaxRejilla " & ex.Message, ex.InnerException)
        End Try

    End Function



    Public Sub InsertoImporteReducido_DescuentoCascada(ByVal lngIdpromocion As Long, ByVal dblDtoCascada As Double, _
        ByVal dblDtoIRCascada As Double, ByRef objPromo As IEnumerable(Of Promocion))

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim strPromocion As String
        Dim blnSigueBusqueda As Boolean
        Dim blnArticulosBeneficiarios As Boolean
        Dim blnTallasC As Boolean
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueBusqueda = False : blnTallasC = False

            'BUSCAMOS SI LA PROMOCION TIENE ARTICULOS BENEFICIARIOS
            strSQL = "Select * from tbPR_articulosBeneficiarios where  fkPromocion= " & lngIdpromocion
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                blnArticulosBeneficiarios = True
            Else
                blnArticulosBeneficiarios = False
            End If

            dtrsADO.Dispose()

            If blnArticulosBeneficiarios Then

                '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
                If gblLngIdPromocionTodos Then
                    For Each objpro As Promocion In objPromo

                        objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIRCascada), clsMU.EsNuloN(dblDtoCascada)), True, True, False)

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                        If objpro.Promo Is Nothing Then
                            objPD.DescriPromo = strPromocion.ToString
                            objPD.DescriAmpliaPromo = strPromocion.ToString
                            objPD.DtoPromo = objpro.DtoEuro
                            objPD.Idpromo = lngIdpromocion
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()

                        End If

                    Next

                Else
                    For Each objpro As Promocion In objPromo

                        'TENEMOS LOS ARTICULOS BENEFICIARIOS
                        ' 04/09/2013 añadir campo descuentocascada
                        strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                            " from tbPR_promociones P WITH (NOLOCK) " & _
                            " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                            " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                            " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='') " & _
                            " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                            " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                        If dtrsADO.Tables(0).Rows.Count <> 0 Then
                            For Each FilaI In dtrsADO.Tables(0).Rows
                                blnTallasC = False
                                '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                                If strTallasValidas <> "" Then
                                    If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                        blnTallasC = True
                                    End If
                                End If

                                If strTallasValidas = "" Or blnTallasC Then
                                    If clsMU.EsNuloN(dblDtoCascada) <= 0 Then
                                        objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.EsNuloN(dblDtoIRCascada), True, True, False)
                                    Else
                                        objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIRCascada), clsMU.EsNuloN(dblDtoCascada)), True, True, False)
                                    End If

                                    '**** REDONDEO PROMO ****
                                    If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                        Select Case gblfkRedondeo
                                            'Case 1, 2 ' redondeo por exceso
                                            ' 15/11/2012
                                            Case 2 ' redondeo por exceso
                                                objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                            Case 3 ' redondeo por defecto                                        
                                                objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                        End Select
                                        objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                    End If

                                    objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                    '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                                    If objpro.Promo Is Nothing Then
                                        objPD.DescriPromo = strPromocion.ToString
                                        objPD.DescriAmpliaPromo = strPromocion.ToString
                                        objPD.DtoPromo = objpro.DtoEuro
                                        objPD.Idpromo = lngIdpromocion
                                        objPD.PromocionSelecionada = True

                                        ppD.Add(objPD)
                                        objpro.Promo = ppD.AsEnumerable()

                                    End If

                                End If
                            Next

                        End If
                        dtrsADO.Dispose()
                    Next

                End If
            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,RDPI.Descuento,RDPI.DescuentoCascada  " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK) ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "' And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows

                            objpro.Pvp_Venta = clsMU.FormatoEuros(clsMU.GetDtoPc(clsMU.EsNuloN(dblDtoIRCascada), clsMU.EsNuloN(dblDtoCascada)), True, True, False)


                            '**** REDONDEO PROMO ****
                            If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                Select Case gblfkRedondeo
                                    'Case 1, 2 ' redondeo por exceso
                                    ' 15/11/2012
                                    Case 2 ' redondeo por exceso
                                        objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                    Case 3 ' redondeo por defecto                                        
                                        objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                End Select
                                objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                            End If

                            objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                            '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS *****  
                            If objpro.Promo Is Nothing Then
                                objPD.DescriPromo = strPromocion.ToString
                                objPD.DescriAmpliaPromo = strPromocion.ToString
                                objPD.DtoPromo = objpro.DtoEuro
                                objPD.Idpromo = lngIdpromocion
                                objPD.PromocionSelecionada = True

                                ppD.Add(objPD)
                                objpro.Promo = ppD.AsEnumerable()

                            End If

                        Next

                    End If

                    dtrsADO.Dispose()

                Next
            End If


        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoImporteReducido_DescuentoCascada " & ex.Message, ex.InnerException)
        End Try


    End Sub


    Public Function GetTipoClientePromo(lngClienteID As Long) As Long

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String        

        Try
            Dim clsMU As ModUtilidades = New ModUtilidades

            strSQL = ""

            'BUSCAMOS TODAS LA PROMOS ACTIVAS PARA ESTA TIENDA

           strSQL = "Select Id_Tipo from N_CLIENTES_GENERAL WITH (NOLOCK) Where Id_cliente= " & lngClienteID

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                GetTipoClientePromo = clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Id_Tipo"))
            Else
                GetTipoClientePromo = -1
            End If

            dtrsADO.Dispose()


        Catch ex As Exception
            Throw New Exception("ModPromociones-GetTipoClientePromo " & ex.Message, ex.InnerException)
        End Try
      
    End Function


    Public Function ValidaFiltroPrimerasCompras(lngClienteID As Long, lngTipoPrimerasCompras As Long) As Boolean

        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim strFechaIni As String
        Dim strFechaFin As String

        Try
            Dim clsMU As ModUtilidades = New ModUtilidades

            strSQL = ""

            '*** BUSCAMOS LAS PRIMERAS COMPRAS ACTIVAS PARA EL CLIENTE ***


            ValidaFiltroPrimerasCompras = False


            '***** VALORES DE PRIMERAS COMPRAS *****
            '** 0 - del día **
            '** 1 - de la semana **
            '** 2 - del mes **
            '** 3 - del Año **
            '** 4 - Ninguna **

            If clsMU.EsNuloN(lngTipoPrimerasCompras) = 0 Then
                strFechaIni = gdteFechaSesion & " 00:00"
                strFechaFin = gdteFechaSesion & " 23:59"
            ElseIf clsMU.EsNuloN(lngTipoPrimerasCompras) = 1 Then
                strFechaIni = gdteFechaSesion & " 00:00"
                strFechaFin = gdteFechaSesion & " 23:59"
            ElseIf clsMU.EsNuloN(lngTipoPrimerasCompras) = 2 Then
                strFechaIni = Format("01/" & Month(gdteFechaSesion) & "/" & Year(gdteFechaSesion), "dd/mm/yyyy") & " 00:00"
                strFechaFin = Format(gintObtenerUltimoDiadeMes(Month(gdteFechaSesion), Year(gdteFechaSesion)) & "/" & Month(gdteFechaSesion) & "/" & Year(gdteFechaSesion), "dd/mm/yyyy") & " 23:59"
            ElseIf clsMU.EsNuloN(lngTipoPrimerasCompras) = 3 Then
                strFechaFin = Format("01/01/" & Year(gdteFechaSesion), "dd/mm/yyyy") & " 00:00"
                strFechaFin = Format("31/12/" & Year(gdteFechaSesion), "dd/mm/yyyy") & " 23:59"
            Else
                ValidaFiltroPrimerasCompras = True
            End If

            If Not ValidaFiltroPrimerasCompras Then

                strSQL = "SELECT COUNT(*) as total FROM N_TICKETS_ESTADOS  WITH (NOLOCK) WHERE Id_Cliente=" & clsMU.EsNuloN(lngClienteID) & " AND Fecha between '" & clsMU.EsNuloT(strFechaIni) & "' AND '" & _
                clsMU.EsNuloT(strFechaFin) & "' AND MotivoCambioPrecio like '%GESTOR PROMOCIONES%' "

                Dim clsAD As clsAccesoDatos = New clsAccesoDatos
                clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                If dtrsADO.Tables(0).Rows.Count <> 0 Then
                    If clsMU.EsNuloN(dtrsADO.Tables(0).Rows(0).Item("Total")) > 0 Then
                        ValidaFiltroPrimerasCompras = False
                    Else
                        ValidaFiltroPrimerasCompras = True
                    End If
                Else
                    ValidaFiltroPrimerasCompras = True
                End If
            End If


            dtrsADO.Dispose()


        Catch ex As Exception
            Throw New Exception("ModPromociones-ValidaFiltroPrimerasCompras " & ex.Message, ex.InnerException)
        End Try

    End Function


    Public Function gintObtenerUltimoDiadeMes(ByVal mes As Integer, ByVal Año As Integer) As Integer
        Select Case mes
            Case 1, 3, 5, 7, 8, 10, 12
                gintObtenerUltimoDiadeMes = 31
            Case 4, 6, 9, 11
                gintObtenerUltimoDiadeMes = 30
            Case 2
                If (Año Mod 4 = 0) And (Año Mod 100 <> 0) Or (Año Mod 400 = 0) Then
                    gintObtenerUltimoDiadeMes = 29
                Else
                    gintObtenerUltimoDiadeMes = 28
                End If
        End Select
        Return gintObtenerUltimoDiadeMes

    End Function

    Public Function InsertoArtBeneficiarioDescuentoConjunto(lngIdpromocion As Long, ByVal lngTipoPromocion As Long, _
                    ByVal lngRegRecalculo As Long, ByRef objPromo As IEnumerable(Of Promocion), ByVal dblPVPVig As Double, Optional blnPrimeraB As Boolean = False) As Boolean


        Dim dtrsADO As DataSet = New DataSet
        Dim strSQL As String
        Dim IntMenor As Long
        Dim dblPrecioMenor As Double
        Dim dblDescuento As Double
        Dim strPromocion As String
        Dim blnSigueDescuento As Boolean
        Dim lngRegRejilla As Long
        Dim blnTallasC As Boolean
        Dim dblDiferencia As Double
        Dim dblDtoDiferencias As Double
        Dim lngCountFila As Long
        Dim lngCountMenor As Long = 0
        Dim objPD As New DetallePromo
        Dim ppD As New List(Of DetallePromo)

        Try

            Dim clsAD As clsAccesoDatos = New clsAccesoDatos
            Dim clsMU As ModUtilidades = New ModUtilidades
            IntMenor = 0 : dblPrecioMenor = 0 : strPromocion = "" : blnSigueDescuento = False : blnTallasC = False : lngCountFila = 0
            lngCountMenor = 1

            '******* SI ES RECALCULO - TOMAMOS EL REGISTRO QUE NOS ENVIAN *******
            lngRegRejilla = IIf(gblstrTipoPromocion = "RECALCULO", lngRegRecalculo, objPromo.Count)


            '***** PROMO SELECCIONADA ES DE TODOS LOS ARTICULOS *****
            If gblLngIdPromocionTodos Then
                For Each objpro As Promocion In objPromo
                    If objpro.DtoEuro = 0 Then                        
                        dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                        dblDescuento = GetDctoPromo(lngIdpromocion, lngTipoPromocion)
                        strPromocion = GetNombrePromo(lngIdpromocion, lngTipoPromocion)

                        If lngTipoPromocion = 48 Or lngTipoPromocion = 49 Or lngTipoPromocion = 50 Or lngTipoPromocion = 51 Then
                            objpro.Pvp_Venta = dblDescuento
                        End If

                        '**** REDONDEO PROMO ****
                        If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                            Select Case gblfkRedondeo
                                'Case 1, 2 ' redondeo por exceso
                                ' 15/11/2012
                                Case 2 ' redondeo por exceso
                                    objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                Case 3 ' redondeo por defecto                                        
                                    objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                            End Select
                            objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                        End If

                        objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                        '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                        ppD = New List(Of DetallePromo)
                        objpro.Promo = Nothing
                        If objpro.Promo Is Nothing Then
                            objPD.DescriPromo = strPromocion.ToString
                            objPD.DescriAmpliaPromo = strPromocion.ToString
                            objPD.DtoPromo = dblDescuento
                            objPD.Idpromo = lngIdpromocion
                            objPD.PromocionSelecionada = True

                            ppD.Add(objPD)
                            objpro.Promo = ppD.AsEnumerable()

                        End If
                    End If
                    If lngCountFila = lngRegRejilla Then Exit For
                    lngCountFila += 1
                    lngCountMenor += 1
                Next
            Else
                For Each objpro As Promocion In objPromo

                    'TENEMOS LOS ARTICULOS BENEFICIARIOS
                    ' 04/09/2013 añadir campo descuentocascada
                    strSQL = "Select P.Promocion,ARTB.fkArticulo,RDPI.Descuento,RDPI.DescuentoCascada " & _
                        " from tbPR_promociones P WITH (NOLOCK) " & _
                        " Inner join tbPR_tiposPromocion TP WITH (NOLOCK) ON P.fkTipoPromocion=TP.IdTipoPromocion " & _
                        " Inner join tbPR_tiendasBeneficiarias T WITH (NOLOCK) ON P.IdPromocion= T.fkPromocion " & _
                        " Inner join tbPR_articulosBeneficiarios ARTB WITH (NOLOCK) ON P.IdPromocion= ARTB.fkPromocion And ARTB.fkArticulo= " & _
                       clsMU.EsNuloN(objpro.Id_Articulo) & " And ( ARTB.Id_Cabecero_Detalle like '%" & clsMU.EsNuloN(objpro.Id_cabecero_detalle) & "%' OR ISNULL(ARTB.Id_Cabecero_Detalle,'')='' ) " & _
                        " Inner join tbPR_recompensasDescuentosPorcentajeImporte RDPI WITH (NOLOCK)  ON P.IdPromocion=  RDPI.fkPromocion " & _
                        " where P.FechaValidezInicio <= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) And P.FechaValidezFin >= CONVERT(DATETIME,'" & (gdteFechaSesion) & "',103) and T.fkTienda= '" & (gstrTiendaSesion) & "'  And P.fkTipoPromocion=" & lngTipoPromocion & " And P.IdPromocion= " & lngIdpromocion & " order by P.FechaValidezInicio"
                    clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

                    If dtrsADO.Tables(0).Rows.Count <> 0 Then
                        For Each FilaI In dtrsADO.Tables(0).Rows

                            blnTallasC = False
                            '*** CODIGO INCORPORADO CONFIRMACION DE TALLAS VALIDAS ***
                            If strTallasValidas <> "" Then
                                If ConfirmacionTallasPromo(strTallasValidas, clsMU.EsNuloN(objpro.Id_cabecero_detalle)) Then
                                    blnTallasC = True
                                End If
                            End If

                            If strTallasValidas = "" Or blnTallasC Then
                                'objpro.Promo = Nothing
                                If clsMU.EsNuloN(objpro.DtoEuro) = 0 Then                                   
                                    dblPrecioMenor = clsMU.EsNuloN(objpro.Pvp_Vig)
                                    dblDescuento = clsMU.EsNuloN(FilaI.item("Descuento"))
                                    strPromocion = clsMU.EsNuloT(FilaI.item("Promocion"))

                                    If lngTipoPromocion = 48 Or lngTipoPromocion = 49 Or lngTipoPromocion = 50 Or lngTipoPromocion = 51 Then
                                        objpro.Pvp_Venta = dblDescuento
                                    End If

                                    '**** REDONDEO PROMO ****
                                    If Math.Round(objpro.Pvp_Vig, 2) - Math.Round(objpro.Pvp_Venta, 2) > 0 Then ' para ver si hay decimales
                                        Select Case gblfkRedondeo
                                            'Case 1, 2 ' redondeo por exceso
                                            ' 15/11/2012
                                            Case 2 ' redondeo por exceso
                                                objpro.Pvp_Venta = Math.Round(clsMU.EsNuloN(objpro.Pvp_Venta) + 1)
                                            Case 3 ' redondeo por defecto                                        
                                                objpro.Pvp_Venta = Math.Round(objpro.Pvp_Venta, 2)
                                        End Select
                                        objpro.Pvp_Venta = clsMU.FormatoEuros(objpro.Pvp_Venta, True, True, False)
                                    End If

                                    objpro.DtoEuro = clsMU.GetPcDto(clsMU.EsNuloN(objpro.Pvp_Vig), objpro.Pvp_Venta)

                                    '***** INSERTAMOS LA PROMOCIÓN EN EL OBJETO SI NO EXISTE REGISTROS ***** 
                                    ppD = New List(Of DetallePromo)
                                    objpro.Promo = Nothing
                                    If objpro.Promo Is Nothing Then
                                        objPD.DescriPromo = strPromocion.ToString
                                        objPD.DescriAmpliaPromo = strPromocion.ToString
                                        objPD.DtoPromo = dblDescuento
                                        objPD.Idpromo = lngIdpromocion
                                        objPD.PromocionSelecionada = True

                                        ppD.Add(objPD)
                                        objpro.Promo = ppD.AsEnumerable()

                                    End If

                                End If
                            End If

                        Next
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                        lngCountMenor += 1
                    Else
                        If lngCountFila = lngRegRejilla Then Exit For
                        lngCountFila += 1
                        lngCountMenor += 1
                    End If
                Next

            End If


            If gblstrTipoPromocion = "RECALCULO" Then
                blnSigueDescuento = True
            End If

            InsertoArtBeneficiarioDescuentoConjunto = blnSigueDescuento
            Return InsertoArtBeneficiarioDescuentoConjunto

        Catch ex As Exception
            Throw New Exception("ModPromociones-InsertoArtBeneficiarioDescuentoConjunto " & ex.Message, ex.InnerException)
        End Try

    End Function



End Class
