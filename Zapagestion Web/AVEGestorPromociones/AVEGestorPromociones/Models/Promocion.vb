﻿Namespace GestorPromociones

    Public Class Promocion

        Public Property Id_Articulo() As Integer
            Get
                Return m_Id_Articulo
            End Get
            Set(ByVal value As Integer)
                m_Id_Articulo = value
            End Set
        End Property
        Private m_Id_Articulo As Integer
        Public Property Id_cabecero_detalle() As Integer
            Get
                Return m_Id_cabecero_detalle
            End Get
            Set(ByVal value As Integer)
                m_Id_cabecero_detalle = value
            End Set
        End Property
        Private m_Id_cabecero_detalle As Integer
        Public Property Pvp_Vig() As Double
            Get
                Return m_Pvp_Vig
            End Get
            Set(ByVal value As Double)
                m_Pvp_Vig = value
            End Set
        End Property
        Private m_Pvp_Vig As Double
        Public Property Pvp_Or() As Double
            Get
                Return m_Pvp_Or
            End Get
            Set(ByVal value As Double)
                m_Pvp_Or = value
            End Set
        End Property
        Private m_Pvp_Venta As Double
        Public Property Pvp_Venta() As Double
            Get
                Return m_Pvp_Venta
            End Get
            Set(ByVal value As Double)
                m_Pvp_Venta = value
            End Set
        End Property

        Private m_Pvp_Or As Double
        Public Property DtoEuro() As Nullable(Of Double)
            Get
                Return m_DtoEuro
            End Get
            Set(ByVal value As Nullable(Of Double))
                m_DtoEuro = value
            End Set
        End Property
        Private m_DtoEuro As Nullable(Of Double)
        Public Property NumeroLineaRecalculo() As Integer
            Get
                Return m_NumeroLineaRecalculo
            End Get
            Set(ByVal value As Integer)
                m_NumeroLineaRecalculo = value
            End Set
        End Property
        Private m_NumeroLineaRecalculo As Integer
        Public Property LineaCarritoDetalle() As Integer
            Get
                Return m_LineaCarritoDetalle
            End Get
            Set(ByVal value As Integer)
                m_LineaCarritoDetalle = value
            End Set
        End Property
        Private m_LineaCarritoDetalle As Integer
        Public Property Tipo() As TipoAccion
            Get
                Return m_Tipo
            End Get
            Set(ByVal value As TipoAccion)
                m_Tipo = value
            End Set
        End Property
        Private m_Tipo As TipoAccion
        Public Property ClienteID() As Nullable(Of Integer)
            Get
                Return m_ClienteID
            End Get
            Set(ByVal value As Nullable(Of Integer))
                m_ClienteID = value
            End Set
        End Property
        Private m_ClienteID As Nullable(Of Integer)
        Public Property Id_Tienda() As String
            Get
                Return m_Id_Tienda
            End Get
            Set(ByVal value As String)
                m_Id_Tienda = value
            End Set
        End Property
        Private m_Unidades As Nullable(Of Integer)
        Public Property Unidades() As String
            Get
                Return m_Unidades
            End Get
            Set(ByVal value As String)
                m_Unidades = value
            End Set
        End Property
        Private m_Id_Tienda As String
        Public Property FSesion() As DateTime
            Get
                Return m_FSesion
            End Get
            Set(ByVal value As DateTime)
                m_FSesion = value
            End Set
        End Property
        Private m_FSesion As DateTime
        Public Property TotalVenta() As Nullable(Of Double)
            Get
                Return m_TotalVenta
            End Get
            Set(ByVal value As Nullable(Of Double))
                m_TotalVenta = value
            End Set
        End Property
        Private m_TotalVenta As Double

        Public Property Promo() As IEnumerable(Of DetallePromo)
            Get
                Return m_Promo
            End Get
            Set(ByVal value As IEnumerable(Of DetallePromo))
                m_Promo = value
            End Set
        End Property
        Private m_Promo As IEnumerable(Of DetallePromo)
        Public Property MensajeError() As String
            Get
                Return m_Mensaje
            End Get
            Set(ByVal value As String)
                m_Mensaje = value
            End Set
        End Property
        Private m_Mensaje As String
    End Class
    Public Enum TipoAccion
        VENTA = 1
        RECALCULO = 2
    End Enum
    Public Class DetallePromo

        Public Property Idpromo() As Nullable(Of Integer)
            Get
                Return m_Idpromo
            End Get
            Set(ByVal value As Nullable(Of Integer))
                m_Idpromo = value
            End Set
        End Property
        Private m_Idpromo As Nullable(Of Integer)
        Public Property DtoPromo() As Nullable(Of Double)
            Get
                Return m_DtoPromo
            End Get
            Set(ByVal value As Nullable(Of Double))
                m_DtoPromo = value
            End Set
        End Property
        Private m_DtoPromo As Nullable(Of Double)
        Public Property DescriPromo() As String
            Get
                Return m_DescriPromo
            End Get
            Set(ByVal value As String)
                m_DescriPromo = value
            End Set
        End Property
        Private m_DescriPromo As String
        Public Property DescriAmpliaPromo() As String
            Get
                Return m_DescriAmpliaPromo
            End Get
            Set(ByVal value As String)
                m_DescriAmpliaPromo = value
            End Set
        End Property
        Private m_DescriAmpliaPromo As String
        Public Property PromocionSelecionada() As Boolean
            Get
                Return m_PromocionSelecionada
            End Get
            Set(ByVal value As Boolean)
                m_PromocionSelecionada = value
            End Set
        End Property
        Private m_PromocionSelecionada As Boolean
        Public Property PromocionSelecionadaUS() As Boolean
            Get
                Return m_PromocionSelecionadaUS
            End Get
            Set(ByVal value As Boolean)
                m_PromocionSelecionadaUS = value
            End Set
        End Property
        Private m_PromocionSelecionadaUS As Boolean

    End Class
End Namespace
