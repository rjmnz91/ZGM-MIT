Imports AVEGestorPromociones

Public Class frmPruebasDLL
   

    Private Sub btnInvocarDll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim objPrueba As New clsPromociones
        objPrueba.Mensaje("Prueba DLL")
    End Sub

    Private Sub btnPruebaDatos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim objCalculoPromo As New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8020
        objP.Id_cabecero_detalle = 1842
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 780
        objP.Pvp_Vig = 780
        objP.Pvp_Venta = 780
        objP.Unidades = 1

        '***** PROMO PRIMER ARTICULO *****
        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True


        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        '***** SEGUNDO ARTICULO *****


        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8021
        objP.Id_cabecero_detalle = 1840
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 650
        objP.Pvp_Vig = 650
        objP.Pvp_Venta = 650
        objP.Unidades = 1

        '***** PROMO SEGUNDO ARTICULO *****

        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True

        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = objCalculoPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

    End Sub

    Private Sub btnPruebaDatosConexion_Click(sender As System.Object, e As System.EventArgs)
        Dim objPrueba As New clsPromociones

        'connectionStrings = name="zapanetConnection" connectionString="metadata=res://*/Models.zapanet.csdl|res://*/Models.zapanet.ssdl|res://*/Models.zapanet.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESARROLLOLUIS\SQL2008R2;initial catalog=T_AVE_CHARLY;persist security info=True;user id=sa;password=moddo;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"

    End Sub

    Private Sub btnPruebasIris_Click(sender As System.Object, e As System.EventArgs)
        Dim objCalculoPromo As New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8020
        objP.Id_cabecero_detalle = 1842
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 780
        objP.Pvp_Vig = 780
        objP.Pvp_Venta = 780
        objP.Unidades = 1

        '***** PROMO PRIMER ARTICULO *****
        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True


        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        '***** SEGUNDO ARTICULO *****


        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8021
        objP.Id_cabecero_detalle = 1840
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 650
        objP.Pvp_Vig = 650
        objP.Pvp_Venta = 650
        objP.Unidades = 1

        '***** PROMO SEGUNDO ARTICULO *****

        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True

        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = objCalculoPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

    End Sub

    Private Sub btnPruebaDatos_Click_1(sender As System.Object, e As System.EventArgs) Handles btnPruebaDatos.Click
        Dim objCalculoPromo As New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)

        '***************************
        '**** BBDD T_AVE_CHARLY ****
        '***************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8020
        objP.Id_cabecero_detalle = 1842
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 780
        objP.Pvp_Vig = 780
        objP.Pvp_Venta = 780
        objP.Unidades = 1

        '***** PROMO PRIMER ARTICULO *****
        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True


        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        '***** SEGUNDO ARTICULO *****


        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        'objP.FSesion = "01/02/2014"
        objP.FSesion = "24/01/2014"
        objP.Id_Articulo = 8021
        objP.Id_cabecero_detalle = 1840
        objP.Id_Tienda = "T-9"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 650
        objP.Pvp_Vig = 650
        objP.Pvp_Venta = 650
        objP.Unidades = 1

        '***** PROMO SEGUNDO ARTICULO *****

        '***** PROMO 1 *****
        objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        objPD.DtoPromo = 100
        objPD.Idpromo = 21
        objPD.PromocionSelecionada = False

        ppD.Add(objPD)
        objPD = New DetallePromo

        '***** PROMO 2 *****
        objPD.DescriPromo = "SUDADERAS CH Y SKX"
        objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        objPD.DtoPromo = 99
        objPD.Idpromo = 129
        objPD.PromocionSelecionada = True

        ppD.Add(objPD)

        objP.Promo = ppD.AsEnumerable()


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = objCalculoPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub btnInvocarDll_Click_1(sender As System.Object, e As System.EventArgs) Handles btnInvocarDll.Click

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0        
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 4
        objP.Id_cabecero_detalle = 377
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 4
        'objP.Id_cabecero_detalle = 377
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 49
        'objP.Pvp_Vig = 49
        'objP.Pvp_Venta = 49
        'objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 5
        objP.Id_cabecero_detalle = 377
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 6
        objP.Id_cabecero_detalle = 377
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 7
        objP.Id_cabecero_detalle = 377
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 8
        objP.Id_cabecero_detalle = 377
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 9
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 115
        objP.Pvp_Vig = 115
        objP.Pvp_Venta = 115
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 10
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 115
        objP.Pvp_Vig = 115
        objP.Pvp_Venta = 115
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 11
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 115
        objP.Pvp_Vig = 115
        objP.Pvp_Venta = 115
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 12
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 115
        objP.Pvp_Vig = 115
        objP.Pvp_Venta = 115
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 13
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 115
        objP.Pvp_Vig = 115
        objP.Pvp_Venta = 115
        objP.Unidades = 1

        ''***** INICIO PROMO CUARTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO CUARTO ARTICULO *****

        pp.Add(objP)


        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 14
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 125
        objP.Pvp_Vig = 125
        objP.Pvp_Venta = 125
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 15
        objP.Id_cabecero_detalle = 314
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 39
        objP.Pvp_Vig = 39
        objP.Pvp_Venta = 39
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 16
        objP.Id_cabecero_detalle = 314
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 39
        objP.Pvp_Vig = 39
        objP.Pvp_Venta = 39
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 17
        objP.Id_cabecero_detalle = 314
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 39
        objP.Pvp_Vig = 39
        objP.Pvp_Venta = 39
        objP.Unidades = 1

        ''***** INICIO PROMO CUARTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO CUARTO ARTICULO *****

        pp.Add(objP)


        '***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 18
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO QUINTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO QUINTO ARTICULO *****

        pp.Add(objP)



        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 19
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 25
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 21
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 27
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 28
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 22
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1        

        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 29
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 22
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 29
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO CUARTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO CUARTO ARTICULO *****

        pp.Add(objP)


        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 30
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 23
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 30
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 23
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO CUARTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO CUARTO ARTICULO *****

        pp.Add(objP)


        '***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 30
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO QUINTO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO QUINTO ARTICULO *****

        pp.Add(objP)



        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 20
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        '***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 26
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        '***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 20
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1

        ''***** INICIO PROMO TERCER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True

        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO TERCER ARTICULO *****

        pp.Add(objP)


        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 24
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 31
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        'pp.Add(objP)


        ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 31
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 31
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 31
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf    
    End Sub

    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 32
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 33
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 33
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 33
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 32
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 49
        'objP.Pvp_Vig = 49
        'objP.Pvp_Venta = 49
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 34
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 35
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 35
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 35
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 34
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 49
        'objP.Pvp_Vig = 49
        'objP.Pvp_Venta = 49
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 36
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 37
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 37
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 37
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        '***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 36
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 38
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 39
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 39
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 39
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        '***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 38
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)

        '***** 6 ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 38
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf

    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 49
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 50
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 50
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 50
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 49
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 51
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 52
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 52
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 52
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 51
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button18_Click(sender As System.Object, e As System.EventArgs) Handles Button18.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 53
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 54
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 54
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 54
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 53
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button19_Click(sender As System.Object, e As System.EventArgs) Handles Button19.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 55
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 56
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 56
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 56
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 55
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button20_Click(sender As System.Object, e As System.EventArgs) Handles Button20.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 57
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 58
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 58
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 58
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 57
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button21_Click(sender As System.Object, e As System.EventArgs) Handles Button21.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 59
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 60
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ' ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ' ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 59
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button22_Click(sender As System.Object, e As System.EventArgs) Handles Button22.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 61
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 62
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ' ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 62
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 62
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 61
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button23_Click(sender As System.Object, e As System.EventArgs) Handles Button23.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 63
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 64
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 64
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 64
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 63
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button24_Click(sender As System.Object, e As System.EventArgs) Handles Button24.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 66
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 66
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 66
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 66
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 65
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button25_Click(sender As System.Object, e As System.EventArgs) Handles Button25.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 68
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        ''***** INICIO PROMO PRIMER ARTICULO *****
        ''***** PROMO 1 *****
        'objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        'objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        'objPD.DtoPromo = 100
        'objPD.Idpromo = 21
        'objPD.PromocionSelecionada = False

        'ppD.Add(objPD)
        'objPD = New DetallePromo

        ''***** PROMO 2 *****
        'objPD.DescriPromo = "SUDADERAS CH Y SKX"
        'objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        'objPD.DtoPromo = 99
        'objPD.Idpromo = 129
        'objPD.PromocionSelecionada = True


        'ppD.Add(objPD)

        'objP.Promo = ppD.AsEnumerable()

        ''***** FIN PROMO PRIMER ARTICULO *****

        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 68
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1

        ' ''***** INICIO PROMO SEGUNDO ARTICULO *****
        ' ''***** PROMO 1 *****
        ''objPD.DescriPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DescriAmpliaPromo = "2 x 1 TENIS CASUAL"
        ''objPD.DtoPromo = 100
        ''objPD.Idpromo = 21
        ''objPD.PromocionSelecionada = False

        ''ppD.Add(objPD)
        ''objPD = New DetallePromo

        ' ''***** PROMO 2 *****
        ''objPD.DescriPromo = "SUDADERAS CH Y SKX"
        ''objPD.DescriAmpliaPromo = "SUDADERAS CH Y SKX"
        ''objPD.DtoPromo = 99
        ''objPD.Idpromo = 129
        ''objPD.PromocionSelecionada = True

        ''ppD.Add(objPD)

        ''objP.Promo = ppD.AsEnumerable()

        ' ''***** FIN PROMO SEGUNDO ARTICULO *****

        pp.Add(objP)


        ''***** TERCER ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 68
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)

        '***** CUARTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 68
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ''***** QUINTO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 67
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button31_Click(sender As System.Object, e As System.EventArgs) Handles Button31.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 71
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 72
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ' ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ' ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 59
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button32_Click(sender As System.Object, e As System.EventArgs) Handles Button32.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 73
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 74
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ' ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ' ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 59
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button33_Click(sender As System.Object, e As System.EventArgs) Handles Button33.Click
        Dim clsPromo As clsPromociones = New clsPromociones
        Dim objP As New Promocion
        Dim objPD As New DetallePromo
        Dim objPromo As IEnumerable(Of Promocion)

        Dim pp As New List(Of Promocion)
        Dim ppD As New List(Of DetallePromo)


        '*********************************
        '***** BBDD T_01_PEDROGARCIA *****
        '*********************************

        '**** RELLENAMOS EL OBJETO MANUALMENTE ****
        '***** PRIMER ARTICULO *****
        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 75
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 49
        objP.Pvp_Vig = 49
        objP.Pvp_Venta = 49
        objP.Unidades = 1


        pp.Add(objP)


        ''***** SEGUNDO ARTICULO *****

        objP = New Promocion
        objPD = New DetallePromo
        ppD = New List(Of DetallePromo)

        objP.ClienteID = -1
        objP.DtoEuro = 0
        objP.FSesion = "04/02/2014"
        objP.Id_Articulo = 76
        objP.Id_cabecero_detalle = 418
        objP.Id_Tienda = "T-01"
        objP.NumeroLineaRecalculo = 0
        objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        objP.Pvp_Or = 59
        objP.Pvp_Vig = 59
        objP.Pvp_Venta = 59
        objP.Unidades = 1


        pp.Add(objP)


        ' ''***** TERCER ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        ' ''***** CUARTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 60
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)


        ' ''***** QUINTO ARTICULO *****

        'objP = New Promocion
        'objPD = New DetallePromo
        'ppD = New List(Of DetallePromo)

        'objP.ClienteID = -1
        'objP.DtoEuro = 0
        'objP.FSesion = "04/02/2014"
        'objP.Id_Articulo = 59
        'objP.Id_cabecero_detalle = 418
        'objP.Id_Tienda = "T-01"
        'objP.NumeroLineaRecalculo = 0
        'objP.Tipo = AVEGestorPromociones.TipoAccion.RECALCULO
        'objP.Pvp_Or = 59
        'objP.Pvp_Vig = 59
        'objP.Pvp_Venta = 59
        'objP.Unidades = 1


        'pp.Add(objP)

        objPromo = pp.AsEnumerable()

        objPromo = clsPromo.Promocion_Calculo(objPromo)




        '***** CARGAMOS LOS VALORES *****
        rtbInforma.Text = ""
        rtbInforma.Text = "INICIO INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ") / [T_01_PEDROGARCIA]." & vbCrLf & vbCrLf

        For Each objpro As Promocion In objPromo
            rtbInforma.Text = rtbInforma.Text & "DATOS ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdArticulo: " & objpro.Id_Articulo & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTalla: " & objpro.Id_cabecero_detalle & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "IdTienda: " & objpro.Id_Tienda & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_ORG: " & objpro.Pvp_Or & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VIG: " & objpro.Pvp_Vig & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "PVP_VENT: " & objpro.Pvp_Venta & vbCrLf
            rtbInforma.Text = rtbInforma.Text & "Dto: " & objpro.DtoEuro & vbCrLf & vbCrLf

            If objpro.Promo IsNot Nothing Then
                '***** PROMOCIONES *****
                For Each objproD As DetallePromo In objpro.Promo
                    rtbInforma.ForeColor = Color.Blue
                    rtbInforma.Text = rtbInforma.Text & "DATOS PROMOCION ARTICULOS VENTA - AVE " & vbCrLf & vbCrLf
                    rtbInforma.ForeColor = Color.Black
                    rtbInforma.Text = rtbInforma.Text & "Nombre Promo: " & objproD.DescriPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Descri Promo: " & objproD.DescriAmpliaPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Dto Promo: " & objproD.DtoPromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "ID Promo: " & objproD.Idpromo & vbCrLf
                    rtbInforma.Text = rtbInforma.Text & "Aplica Promo: " & IIf(objproD.PromocionSelecionada, "SI", "NO") & vbCrLf & vbCrLf
                Next
            End If
        Next

        rtbInforma.Text = rtbInforma.Text & "FIN INFORMACION DEL OBJETO PROMOCIONES / FECHA:(" & Now & ")" & vbCrLf & vbCrLf
    End Sub
End Class
