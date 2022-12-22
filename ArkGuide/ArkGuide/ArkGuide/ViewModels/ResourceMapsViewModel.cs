using ArkGuide.Database;
using ArkGuide.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkGuide.ViewModels
{
    public class ResourceMapsViewModel : BaseViewModel
    {

        private Image _mapImage;
        public Image MapImage
        {
            get
            {
                return _mapImage;
            }
            set
            {
                _mapImage = value;
                OnPropertyChanged(nameof(MapImage));
            }
        }
        private ResourceMap _map;
        public ResourceMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
                OnPropertyChanged(nameof(Map));
            }
        }

        private AbsoluteLayout _mapLayout;

        public AbsoluteLayout MapLayout
        {
            get
            {
                return _mapLayout;
            }
            set
            {
                _mapLayout = value;
                OnPropertyChanged(nameof(MapLayout));
            }
        }

        private string _notableLocationType;
        public string NotableLocationType
        {
            get
            {
                return _notableLocationType;
            }
            set
            {
                _notableLocationType = value;
                OnPropertyChanged(nameof(NotableLocationType));
            }
        }

        private string selectedMap;
        public string SelectedMap
        {
            get
            {
                return selectedMap;
            }
            set
            {
                selectedMap = value;
                OnPropertyChanged(nameof(SelectedMap));
            }
        }

        public void ResetView()
        {
            MapLayout.Scale = 1;
            MapLayout.AnchorY = 0.5;
            MapLayout.AnchorX = 0.5;
        }

        public ResourceMapsViewModel()
        {
            Title = "Resource Maps";
            LoadMap("The Island");
        }

        public async Task LoadMap(string mapName)
        {
            IsBusy = true;
            ArkGuideDatabase db = await ArkGuideDatabase.Instance;
            Map = await db.GetMapAsync(mapName);
            PopulateGrid();
            IsBusy = false;
        }




        private void PopulateGrid()
        {
            List<BoxView> pins = new List<BoxView>();
            foreach (var c in MapLayout.Children)
            {
                if (c.ClassId == "pin")
                {
                    pins.Add((BoxView)c);
                }
            }

            foreach (var p in pins)
            {
                MapLayout.Children.Remove(p);
            }

            foreach (var np in Map.NotableLocations)
            {
                if (np.LocationName == NotableLocationType)
                {

                    var pin = new BoxView();
                    pin.BackgroundColor = Color.Red;
                    pin.ClassId = "pin";
                    //var layoutX = MapImage.Bounds.Width;
                    //var layoutY = MapImage.Bounds.Height;

                    double lat;
                    double lon;
                    if (Map.MapName == "The Island")
                    {

                        var layoutX = MapLayout.Bounds.Width + 20;
                        var layoutY = MapLayout.Bounds.Height + 20;
                        lat = ((double.Parse(np.Lat) - 7.2) / 92.7) * layoutX;
                        lon = ((double.Parse(np.Long) - 7.2) / 92.7) * layoutY;
                    } else if (Map.MapName == "Ragnarok")
                    {
                        var layoutX = MapLayout.Bounds.Width;
                        var layoutY = MapLayout.Bounds.Height;
                        lat = ((double.Parse(np.Lat)) / 100) * layoutX;
                        lon = ((double.Parse(np.Long) + 2) / 104) * layoutY;
                    } else if (Map.MapName == "Crystal Isles")
                    {
                        var layoutX = MapLayout.Bounds.Width +35;
                        var layoutY = MapLayout.Bounds.Height +35;
                        lat = ((double.Parse(np.Lat) - 10.5) / 86.6) * layoutX;
                        lon = ((double.Parse(np.Long) - 14) / 85.8) * layoutY;
                    }
                    else
                    {
                        var layoutX = MapLayout.Bounds.Width;
                        var layoutY = MapLayout.Bounds.Height;
                        lat = ((double.Parse(np.Lat)) / 100) * layoutX;
                        lon = ((double.Parse(np.Long)) / 100) * layoutY;
                    }
                    

                    AbsoluteLayout.SetLayoutBounds(pin, new Rectangle(lon, lat, 4, 4));
                    MapLayout.Children.Add(pin);
                }

            }
        }

        private int ToMapCoords(string lat)
        {
            var floatVal = float.Parse(lat);
            return (int)Math.Round(floatVal * 10);
        }

        public void ZoomIn()
        {
            MapLayout.Scale += 0.2;
        }

        public void ZoomOut()
        {
            if (MapLayout.Scale <= 1)
            {
                MapLayout.Scale = 1;
                return;
            }
            MapLayout.Scale -= 0.2;
        }

        public void Move(string v)
        {
            switch (v)
            {
                case "up":
                    if (MapLayout.AnchorY > 0)
                    MapLayout.AnchorY -= 0.2;
                    break;
                case "down":
                    if (MapLayout.AnchorY < 1)
                        MapLayout.AnchorY += 0.2;
                    break;
                case "right":

                    if (MapLayout.AnchorX < 1)
                        MapLayout.AnchorX += 0.2;
                    break;
                case "left":

                    if (MapLayout.AnchorX > 0)
                        MapLayout.AnchorX -= 0.2;
                    break;
            }
        }

        public void LoadPins(string selectedItem)
        {
            NotableLocationType = selectedItem;
            PopulateGrid();
        }


    }
}
