using ArkGuide.Database;
using ArkGuide.Models;
using ArkGuide.Services;
using ArkGuide.Services.ResourceMapServices;
using ArkGuide.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkGuide
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Xamarin.Forms.Extensions.Svg.SvgImage.RegisterAssembly();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var db = await ArkGuideDatabase.Instance;
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
