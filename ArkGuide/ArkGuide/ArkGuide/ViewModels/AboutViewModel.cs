using ArkGuide.Database;
using ArkGuide.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArkGuide.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
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

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        public void ResetView()
        {
            MapLayout.Scale = 1;
            MapLayout.AnchorY = 0.5;
            MapLayout.AnchorX = 0.5;
        }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            LoadMap("The Island");
        }

        public async Task LoadMap(string mapName)
        {
            IsLoading = true;
            ArkGuideDatabase db = await ArkGuideDatabase.Instance;
            Map = await db.GetMapAsync(mapName);
            PopulateGrid();
            IsLoading = false;
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
                    var layoutX = MapLayout.Bounds.Width;
                    var layoutY = MapLayout.Bounds.Height;
                    var lat = (double.Parse(np.Lat) / 100) * layoutX;
                    var lon = (double.Parse(np.Long) / 100) * layoutY;

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
            if (MapLayout.Scale >= 1)
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
                    MapLayout.AnchorY -= 0.2;
                    break;
                case "down":
                    MapLayout.AnchorY += 0.2;
                    break;
                case "right":
                    MapLayout.AnchorX += 0.2;
                    break;
                case "left":
                    MapLayout.AnchorX -= 0.2;
                    break;
            }
        }

        public void LoadPins(string selectedItem)
        {
            NotableLocationType = selectedItem;
            PopulateGrid();
        }

        public ICommand OpenWebCommand { get; }
    }
}