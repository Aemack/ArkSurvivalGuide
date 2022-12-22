using ArkGuide.ViewModels;
using ArkGuide.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ArkGuide
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(CreaturePage), typeof(CreaturePage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            
        }

    }
}
