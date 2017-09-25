Public Class ModUtilidades
    Function GetCountTable(ByVal strSQL As String) As Double
        Dim dtrsADO As New DataSet

        Dim clsAD As clsAccesoDatos = New clsAccesoDatos
        clsAD.CargarDataset(dtrsADO, strSQL, "ValidarArticuloPromo")

        If dtrsADO.Tables(0).Rows.Count <> 0 Then
            GetCountTable = EsNuloN(dtrsADO.Tables(0).Rows(0).Item(0))
        Else
            GetCountTable = 0
        End If        

        dtrsADO.Dispose()

    End Function


    Function EsNuloT(ByVal X As Object) As String
        ' Función simple de evaluar si es nulo, nos ahorramos los if
        ' en todos los demas apartados, este es para textos
        If IsDBNull(X) Then
            EsNuloT = ""
        Else
            EsNuloT = Trim(X)
        End If
    End Function

    Function EsNuloN(ByVal X As Object) As Double
        ' Función simple de evaluar si es nulo, nos ahorramos los if
        ' en todos los demas apartados, este es para números
        If IsDBNull(X) Then
            EsNuloN = 0
        ElseIf Trim(X) = "" Then
            EsNuloN = 0
        Else
            EsNuloN = X
        End If
    End Function

    Function Null_SD(ByRef expresion As Object) As String
        'Puesto que IIF da un error al compilar cuando hay un null
        'Usamos esta funcion para sustituir los null por "SIN DEFINIR"
        If IsDBNull(expresion) Then
            Return "SIN DEFINIR"
        Else
            Return Trim(expresion)
        End If
    End Function

    Function Null_Bool(ByRef expresion As Object) As Boolean
        'Puesto que IIF da un error al compilar cuando hay un null
        'Usamos esta funcion para sustituir los null por "SIN DEFINIR"
        If IsDBNull(expresion) Then
            Return False
        Else
            Return expresion
        End If
    End Function

    Function Null_Vac(ByRef expresion As Object) As String
        'Puesto que IIF da un error al compilar cuando hay un null
        'Usamos esta funcion para sustituir los null por ""
        If IsDBNull(expresion) Then
            Return ""
        Else
            Return Trim(expresion)
        End If
    End Function

    Function GetPcDto(ByVal dblPVP_Origin As Double, ByVal dblPVP_Modif As Double) As String        
        If dblPVP_Origin <> 0 Then
            GetPcDto = FormatoEuros(100 * (dblPVP_Origin - dblPVP_Modif) / (dblPVP_Origin))
        Else
            GetPcDto = FormatoEuros(0)
        End If
    End Function

    Function FormatoEuros(ByVal cdblImporte As Double, Optional ByVal blnSepMiles As Boolean = False, Optional blnAjuste As Boolean = False, Optional blnNoRedondeo As Boolean = False) As String
        'blnAjuste = False
        'If (UCase(Trim(BuscaParametros("DirFTP"))) = FTP_SPRINGSTEP Or PaisSinCentimos()) And blnAjuste Then
        '    If Not blnNoRedondeo Then cdblImporte = RedondeoImportes(cdblImporte)
        '    If UCase(Trim(BuscaParametros("DirFTP"))) = FTP_SPRINGSTEP Then
        '        FormatoEuros = Format$(Round(cdblImporte), IIf(blnSepMiles, "#,##0", "##0"))
        '    Else
        '        FormatoEuros = Format$(Round(cdblImporte), IIf(blnSepMiles, "#,##0.00", "##0.00"))
        '    End If
        'ElseIf blnSepMiles Or clsLANG.GetIdiomaID = 8 Then
        '    FormatoEuros = Format(Round(cdblImporte, 2), "#,##0.00")
        'Else
        '    FormatoEuros = Format(Round(cdblImporte, 2), "##0.00")
        'End If

        FormatoEuros = Format(Math.Round(cdblImporte, 2), "#,##0.00")

    End Function

    Function GetDtoPc(ByVal dblPVP_Origin As Double, ByVal dblDto As Double) As String
        GetDtoPc = FormatoEuros((100 - dblDto) * dblPVP_Origin / 100, True, True)
    End Function

    Function GetPorcentajeDtoArticulo(ByVal lngArticulo As Long, gstrTiendaSesion As String) As Double

        Dim dtrsADO As DataSet = New DataSet
        Dim StrSqlDescuento As String

        Try
            Dim clsAD As clsAccesoDatos = New clsAccesoDatos

            StrSqlDescuento = "select a.idarticulo,a.PrecioVentaEuro as PrecioLleno, pa.PrecioEuro as PrecioVigente "
            StrSqlDescuento = StrSqlDescuento & ", case a.PrecioVentaEuro"
            StrSqlDescuento = StrSqlDescuento & " when 0 then 0"
            StrSqlDescuento = StrSqlDescuento & " else (a.PrecioVentaEuro - pa.PrecioEuro)/a.PrecioVentaEuro * 100"
            StrSqlDescuento = StrSqlDescuento & " end as PorcentajeDto"
            StrSqlDescuento = StrSqlDescuento & " from articulos a inner join PRECIOS_ARTICULOS pa on a.IdArticulo = pa.IdArticulo"
            StrSqlDescuento = StrSqlDescuento & " where pa.IdArticulo = " & lngArticulo
            StrSqlDescuento = StrSqlDescuento & " and pa.Activo=1"
            StrSqlDescuento = StrSqlDescuento & " and pa.IdTienda = '" & gstrTiendaSesion & "'"

            clsAD.CargarDataset(dtrsADO, StrSqlDescuento, "GetPorcentajeDtoArticulo")

            If dtrsADO.Tables(0).Rows.Count <> 0 Then
                GetPorcentajeDtoArticulo = EsNuloN(dtrsADO.Tables(0).Rows(0).Item("PorcentajeDto"))
            Else
                GetPorcentajeDtoArticulo = 0
            End If

            dtrsADO.Dispose()

        Catch ex As Exception
            Throw New Exception("ModUtilidades-GetPorcentajeDtoArticulo " & ex.Message, ex.InnerException)
        End Try

    End Function


End Class
