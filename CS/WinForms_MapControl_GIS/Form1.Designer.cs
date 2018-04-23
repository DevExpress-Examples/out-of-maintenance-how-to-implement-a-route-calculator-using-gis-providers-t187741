namespace WinForms_MapControl_GIS {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            DevExpress.XtraMap.ImageLayer imageTilesLayer1 = new DevExpress.XtraMap.ImageLayer();
            DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider1 = new DevExpress.XtraMap.BingMapDataProvider();
            DevExpress.XtraMap.InformationLayer informationLayer1 = new DevExpress.XtraMap.InformationLayer();
            DevExpress.XtraMap.BingGeocodeDataProvider bingGeocodeDataProvider1 = new DevExpress.XtraMap.BingGeocodeDataProvider();
            DevExpress.XtraMap.InformationLayer informationLayer2 = new DevExpress.XtraMap.InformationLayer();
            DevExpress.XtraMap.BingRouteDataProvider bingRouteDataProvider1 = new DevExpress.XtraMap.BingRouteDataProvider();
            DevExpress.XtraMap.InformationLayer informationLayer3 = new DevExpress.XtraMap.InformationLayer();
            DevExpress.XtraMap.BingSearchDataProvider bingSearchDataProvider1 = new DevExpress.XtraMap.BingSearchDataProvider();
            DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer1 = new DevExpress.XtraMap.VectorItemsLayer();
            DevExpress.XtraMap.MapItemStorage mapItemStorage1 = new DevExpress.XtraMap.MapItemStorage();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.bClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            imageTilesLayer1.DataProvider = bingMapDataProvider1;
            bingGeocodeDataProvider1.GenerateLayerItems = false;
            informationLayer1.DataProvider = bingGeocodeDataProvider1;
            informationLayer1.Name = "GeocodeLayer";
            bingRouteDataProvider1.GenerateLayerItems = false;
            informationLayer2.DataProvider = bingRouteDataProvider1;
            informationLayer2.Name = "RouteLayer";
            bingSearchDataProvider1.GenerateLayerItems = false;
            informationLayer3.DataProvider = bingSearchDataProvider1;
            informationLayer3.Name = "SearchLayer";
            vectorItemsLayer1.Data = mapItemStorage1;
            vectorItemsLayer1.EnableHighlighting = false;
            vectorItemsLayer1.EnableSelection = false;
            vectorItemsLayer1.Name = "ItemsLayer";
            this.mapControl1.Layers.Add(imageTilesLayer1);
            this.mapControl1.Layers.Add(informationLayer1);
            this.mapControl1.Layers.Add(informationLayer2);
            this.mapControl1.Layers.Add(informationLayer3);
            this.mapControl1.Layers.Add(vectorItemsLayer1);
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(811, 540);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.MapItemClick += new DevExpress.XtraMap.MapItemClickEventHandler(this.mapControl1_MapItemClick);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(12, 12);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 1;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 540);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mapControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl mapControl1;
        private System.Windows.Forms.Button bClear;
    }
}

