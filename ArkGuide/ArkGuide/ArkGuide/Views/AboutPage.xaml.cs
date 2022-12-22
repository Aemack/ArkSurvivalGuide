using ArkGuide.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkGuide.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;
        public AboutPage()
        {
            InitializeComponent();
            vm = (AboutViewModel)BindingContext;
            vm.MapLayout = MapLayout;
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