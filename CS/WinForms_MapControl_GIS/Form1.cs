using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms_MapControl_GIS {
    public partial class Form1 : Form {
        #region #Members
        InformationLayer SearchLayer { get { return (InformationLayer)mapControl1.Layers["SearchLayer"]; } }
        BingSearchDataProvider SearchProvider { get { return (BingSearchDataProvider)SearchLayer.DataProvider; } }

        InformationLayer GeocodeLayer { get { return (InformationLayer)mapControl1.Layers["GeocodeLayer"]; } }
        BingGeocodeDataProvider GeocodeProvider { get { return (BingGeocodeDataProvider)GeocodeLayer.DataProvider; } }

        InformationLayer RouteLayer { get { return (InformationLayer)mapControl1.Layers["RouteLayer"]; } }
        BingRouteDataProvider RouteProvider { get { return (BingRouteDataProvider)RouteLayer.DataProvider; } }

        VectorItemsLayer ItemsLayer { get { return (VectorItemsLayer)mapControl1.Layers["ItemsLayer"]; } }
        MapItemStorage Storage { get { return (MapItemStorage)ItemsLayer.Data; } }

        RoutingHelper helper = new RoutingHelper();
        #endregion #Members

        public Form1() {
            InitializeComponent();

            #region #SubscribeEvents
            SearchProvider.SearchCompleted += SearchProvider_SearchCompleted;
            GeocodeProvider.LocationInformationReceived += GeocodeProvider_LocationInformationReceived;
            RouteProvider.RouteCalculated += RouteProvider_RouteCalculated;
            #endregion #SubscribeEvents
        }

        #region #CalculateRoute
        void CalculateRoute() {
            if (helper.Count < 2) return;
            RouteProvider.CalculateRoute(helper.Waypoints);
        }
        #endregion #CalculateRoute

        #region #GenerateItems
        void GenerateItems(IEnumerable<LocationInformation> locations) {
            UpdateStorage();
            foreach (var location in locations) {
                MapPushpin pushpin = new MapPushpin() { Location = location.Location, Information = location };
                Storage.Items.Add(pushpin);
            }
        }

        void UpdateStorage() {
            Storage.Items.Clear();
            Storage.Items.Add(helper.Route);
            foreach (MapPushpin pushpin in helper.Pushpins)
                Storage.Items.Add(pushpin);
        }
        #endregion #GenerateItems

        #region #SearchCompleted
        void SearchProvider_SearchCompleted(object sender, BingSearchCompletedEventArgs e) {
            if (e.Cancelled || (e.RequestResult.ResultCode != RequestResultCode.Success)) return;

            if (e.RequestResult.SearchResults.Count != 0)
                GenerateItems(e.RequestResult.SearchResults);
        }
        #endregion #SearchCompleted

        #region #GeocodeCompleted
        void GeocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e) {
            if (e.Cancelled) return;

            GenerateItems(e.Result.Locations);
        }
        #endregion #GeocodeCompleted

        #region #RouteCalculated
        void RouteProvider_RouteCalculated(object sender, BingRouteCalculatedEventArgs e) {
            if (e.Cancelled) return;

            if (e.CalculationResult.ResultCode == RequestResultCode.Success) {
                helper.BuildRoute(e.CalculationResult.RouteResults[0].RoutePath);
                UpdateStorage();
            }
        }
        #endregion #RouteCalculated

        #region #MapItemClick
        private void mapControl1_MapItemClick(object sender, MapItemClickEventArgs e) {
            MapPushpin pushpin = e.Item as MapPushpin;
            if (pushpin == null) return;
            if (helper.Pushpins.Contains(pushpin)) return;

            helper.AddItem(pushpin);
            CalculateRoute();
            e.Handled = true;
        }
        #endregion #MapItemClick

        private void bClear_Click(object sender, EventArgs e) {
            helper.Clear();
            UpdateStorage();
        }
    }

    #region #Helper
    class RoutingHelper {
        List<MapPushpin> pushpins = new List<MapPushpin>();
        MapPolyline route = new MapPolyline() {
            StrokeWidth = 4,
            SelectedStrokeWidth = 4,
            Stroke = Color.FromArgb(0xFF, 0xFE, 0x72, 0xFF)
        };
        List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
        char currentLatter = 'A';

        public List<MapPushpin> Pushpins { get { return pushpins; } }
        public List<RouteWaypoint> Waypoints { get { return waypoints; } }
        public MapPolyline Route { get { return route; } set { route = value; } }
        public int Count { get { return pushpins.Count; } }

        public void BuildRoute(IEnumerable<GeoPoint> points) {
            route.Points.Clear();
            foreach (GeoPoint point in points)
                route.Points.Add(point);
        }

        public void AddItem(MapPushpin pushpin) {
            pushpins.Add(pushpin);
            waypoints.Add(new RouteWaypoint(
                ((LocationInformation)pushpin.Information).DisplayName,
                (GeoPoint)pushpin.Location)
            );
            pushpin.Text = (currentLatter++).ToString();
        }

        public void Clear() {
            route.Points.Clear();
            pushpins.Clear();
            waypoints.Clear();
            currentLatter = 'A';
        }
    }
    #endregion #Helper
}
