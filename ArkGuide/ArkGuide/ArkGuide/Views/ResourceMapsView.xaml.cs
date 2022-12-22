using ArkGuide.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceMapsView : ContentPage
    {
        ResourceMapsViewModel vm;
        public ResourceMapsView()
        {
            InitializeComponent();
            vm = (ResourceMapsViewModel)BindingContext;
            vm.MapLayout = MapLayout;
            vm.MapImage = MapImage;
        }


        private void ZoomIn(object sender, EventArgs e)
        {
            vm.ZoomIn();
        }

        private void ZoomOut(object sender, EventArgs e)
        {
            vm.ZoomOut();
        }

        private void MoveUp(object sender, EventArgs e)
        {
            vm.Move("up");
        }

        private void MoveDown(object sender, EventArgs e)
        {
            vm.Move("down");
        }

        private void MoveRight(object sender, EventArgs e)
        {
            vm.Move("right");
        }

        private void MoveLeft(object sender, EventArgs e)
        {
            vm.Move("left");
        }

        private async void MapChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            await vm.LoadMap((string)picker.SelectedItem);
        }

        private void NotableLocationChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            vm.LoadPins((string)picker.SelectedItem);
        }

        private void ResetView(object sender, EventArgs e)
        {
            vm.ResetView();
        }
    }
}