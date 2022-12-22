using ArkGuide.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ArkGuide.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}