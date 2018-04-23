Imports DevExpress.XtraMap
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace WinForms_MapControl_GIS
    Partial Public Class Form1
        Inherits Form

        #Region "#Members"
        Private ReadOnly Property SearchLayer() As InformationLayer
            Get
                Return CType(mapControl1.Layers("SearchLayer"), InformationLayer)
            End Get
        End Property
        Private ReadOnly Property SearchProvider() As BingSearchDataProvider
            Get
                Return CType(SearchLayer.DataProvider, BingSearchDataProvider)
            End Get
        End Property

        Private ReadOnly Property GeocodeLayer() As InformationLayer
            Get
                Return CType(mapControl1.Layers("GeocodeLayer"), InformationLayer)
            End Get
        End Property
        Private ReadOnly Property GeocodeProvider() As BingGeocodeDataProvider
            Get
                Return CType(GeocodeLayer.DataProvider, BingGeocodeDataProvider)
            End Get
        End Property

        Private ReadOnly Property RouteLayer() As InformationLayer
            Get
                Return CType(mapControl1.Layers("RouteLayer"), InformationLayer)
            End Get
        End Property
        Private ReadOnly Property RouteProvider() As BingRouteDataProvider
            Get
                Return CType(RouteLayer.DataProvider, BingRouteDataProvider)
            End Get
        End Property

        Private ReadOnly Property ItemsLayer() As VectorItemsLayer
            Get
                Return CType(mapControl1.Layers("ItemsLayer"), VectorItemsLayer)
            End Get
        End Property
        Private ReadOnly Property Storage() As MapItemStorage
            Get
                Return CType(ItemsLayer.Data, MapItemStorage)
            End Get
        End Property

        Private helper As New RoutingHelper()
        #End Region ' #Members

        Public Sub New()
            InitializeComponent()

'            #Region "#SubscribeEvents"
            AddHandler SearchProvider.SearchCompleted, AddressOf SearchProvider_SearchCompleted
            AddHandler GeocodeProvider.LocationInformationReceived, AddressOf GeocodeProvider_LocationInformationReceived
            AddHandler RouteProvider.RouteCalculated, AddressOf RouteProvider_RouteCalculated
'            #End Region ' #SubscribeEvents
        End Sub

        #Region "#CalculateRoute"
        Private Sub CalculateRoute()
            If helper.Count < 2 Then
                Return
            End If
            RouteProvider.CalculateRoute(helper.Waypoints)
        End Sub
        #End Region ' #CalculateRoute

        #Region "#GenerateItems"
        Private Sub GenerateItems(ByVal locations As IEnumerable(Of LocationInformation))
            UpdateStorage()

            For Each location_Renamed In locations
                Dim pushpin As New MapPushpin() With {.Location = location_Renamed.Location, .Information = location_Renamed}
                Storage.Items.Add(pushpin)
            Next location_Renamed
        End Sub

        Private Sub UpdateStorage()
            Storage.Items.Clear()
            Storage.Items.Add(helper.Route)
            For Each pushpin As MapPushpin In helper.Pushpins
                Storage.Items.Add(pushpin)
            Next pushpin
        End Sub
        #End Region ' #GenerateItems

        #Region "#SearchCompleted"
        Private Sub SearchProvider_SearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            If e.Cancelled OrElse (e.RequestResult.ResultCode <> RequestResultCode.Success) Then
                Return
            End If

            If e.RequestResult.SearchResults.Count <> 0 Then
                GenerateItems(e.RequestResult.SearchResults)
            Else
                GenerateItems(New LocationInformation() { e.RequestResult.SearchRegion })
            End If
        End Sub
        #End Region ' #SearchCompleted

        #Region "#GeocodeCompleted"
        Private Sub GeocodeProvider_LocationInformationReceived(ByVal sender As Object, ByVal e As LocationInformationReceivedEventArgs)
            If e.Cancelled Then
                Return
            End If

            GenerateItems(e.Result.Locations)
        End Sub
        #End Region ' #GeocodeCompleted

        #Region "#RouteCalculated"
        Private Sub RouteProvider_RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            If e.Cancelled Then
                Return
            End If

            If e.CalculationResult.ResultCode = RequestResultCode.Success Then
                helper.BuildRoute(e.CalculationResult.RouteResults(0).RoutePath)
                UpdateStorage()
            End If
        End Sub
        #End Region ' #RouteCalculated

        #Region "#MapItemClick"
        Private Sub mapControl1_MapItemClick(ByVal sender As Object, ByVal e As MapItemClickEventArgs) Handles mapControl1.MapItemClick
            Dim pushpin As MapPushpin = TryCast(e.Item, MapPushpin)
            If pushpin Is Nothing Then
                Return
            End If
            If helper.Pushpins.Contains(pushpin) Then
                Return
            End If

            helper.AddItem(pushpin)
            CalculateRoute()
            e.Handled = True
        End Sub
        #End Region ' #MapItemClick

        Private Sub bClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bClear.Click
            helper.Clear()
            UpdateStorage()
        End Sub
    End Class

    #Region "#Helper"
    Friend Class RoutingHelper

        Private pushpins_Renamed As New List(Of MapPushpin)()

        Private route_Renamed As New MapPolyline() With {.StrokeWidth = 4, .SelectedStrokeWidth = 4, .Stroke = Color.FromArgb(&HFF, &HFE, &H72, &HFF)}

        Private waypoints_Renamed As New List(Of RouteWaypoint)()
        Private currentLatter As Char = "A"c

        Public ReadOnly Property Pushpins() As List(Of MapPushpin)
            Get
                Return pushpins_Renamed
            End Get
        End Property
        Public ReadOnly Property Waypoints() As List(Of RouteWaypoint)
            Get
                Return waypoints_Renamed
            End Get
        End Property
        Public Property Route() As MapPolyline
            Get
                Return route_Renamed
            End Get
            Set(ByVal value As MapPolyline)
                route_Renamed = value
            End Set
        End Property
        Public ReadOnly Property Count() As Integer
            Get
                Return pushpins_Renamed.Count
            End Get
        End Property

        Public Sub BuildRoute(ByVal points As IEnumerable(Of GeoPoint))
            route_Renamed.Points.Clear()
            For Each point As GeoPoint In points
                route_Renamed.Points.Add(point)
            Next point
        End Sub

        Public Sub AddItem(ByVal pushpin As MapPushpin)
            pushpins_Renamed.Add(pushpin)
            waypoints_Renamed.Add(New RouteWaypoint(CType(pushpin.Information, LocationInformation).DisplayName, CType(pushpin.Location, GeoPoint)))
            pushpin.Text = (currentLatter).ToString()
            currentLatter = ChrW(AscW(currentLatter) + 1)
        End Sub

        Public Sub Clear()
            route_Renamed.Points.Clear()
            pushpins_Renamed.Clear()
            waypoints_Renamed.Clear()
            currentLatter = "A"c
        End Sub
    End Class
    #End Region ' #Helper
End Namespace
