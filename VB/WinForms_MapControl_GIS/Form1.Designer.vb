Namespace WinForms_MapControl_GIS
    Partial Public Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim imageTilesLayer1 As New DevExpress.XtraMap.ImageLayer()
            Dim bingMapDataProvider1 As New DevExpress.XtraMap.BingMapDataProvider()
            Dim informationLayer1 As New DevExpress.XtraMap.InformationLayer()
            Dim bingGeocodeDataProvider1 As New DevExpress.XtraMap.BingGeocodeDataProvider()
            Dim informationLayer2 As New DevExpress.XtraMap.InformationLayer()
            Dim bingRouteDataProvider1 As New DevExpress.XtraMap.BingRouteDataProvider()
            Dim informationLayer3 As New DevExpress.XtraMap.InformationLayer()
            Dim bingSearchDataProvider1 As New DevExpress.XtraMap.BingSearchDataProvider()
            Dim vectorItemsLayer1 As New DevExpress.XtraMap.VectorItemsLayer()
            Dim mapItemStorage1 As New DevExpress.XtraMap.MapItemStorage()
            Me.mapControl1 = New DevExpress.XtraMap.MapControl()
            Me.bClear = New System.Windows.Forms.Button()
            DirectCast(Me.mapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' mapControl1
            ' 
            Me.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill
            imageTilesLayer1.DataProvider = bingMapDataProvider1
            bingGeocodeDataProvider1.GenerateLayerItems = False
            informationLayer1.DataProvider = bingGeocodeDataProvider1
            informationLayer1.Name = "GeocodeLayer"
            bingRouteDataProvider1.GenerateLayerItems = False
            informationLayer2.DataProvider = bingRouteDataProvider1
            informationLayer2.Name = "RouteLayer"
            bingSearchDataProvider1.GenerateLayerItems = False
            informationLayer3.DataProvider = bingSearchDataProvider1
            informationLayer3.Name = "SearchLayer"
            vectorItemsLayer1.Data = mapItemStorage1
            vectorItemsLayer1.EnableHighlighting = False
            vectorItemsLayer1.EnableSelection = False
            vectorItemsLayer1.Name = "ItemsLayer"
            Me.mapControl1.Layers.Add(imageTilesLayer1)
            Me.mapControl1.Layers.Add(informationLayer1)
            Me.mapControl1.Layers.Add(informationLayer2)
            Me.mapControl1.Layers.Add(informationLayer3)
            Me.mapControl1.Layers.Add(vectorItemsLayer1)
            Me.mapControl1.Location = New System.Drawing.Point(0, 0)
            Me.mapControl1.Name = "mapControl1"
            Me.mapControl1.Size = New System.Drawing.Size(811, 540)
            Me.mapControl1.TabIndex = 0
            ' 
            ' bClear
            ' 
            Me.bClear.Location = New System.Drawing.Point(12, 12)
            Me.bClear.Name = "bClear"
            Me.bClear.Size = New System.Drawing.Size(75, 23)
            Me.bClear.TabIndex = 1
            Me.bClear.Text = "Clear"
            Me.bClear.UseVisualStyleBackColor = True
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(811, 540)
            Me.Controls.Add(Me.bClear)
            Me.Controls.Add(Me.mapControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.mapControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private WithEvents mapControl1 As DevExpress.XtraMap.MapControl
        Private WithEvents bClear As System.Windows.Forms.Button
    End Class
End Namespace

