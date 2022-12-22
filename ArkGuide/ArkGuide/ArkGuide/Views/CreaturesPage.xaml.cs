using ArkGuide.Models.CreatureModels;
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
    public partial class CreaturesPage : ContentPage
    {
        CreaturesViewModel vm;
        public CreaturesPage()
        {
            InitializeComponent();
            vm = (CreaturesViewModel)BindingContext;
        }

        private async void CreatureClicked(object sender, SelectedItemChangedEventArgs e)
        {

            ListView lv = (ListView)sender;
            Creature c = (Creature)lv.SelectedItem;
            try
            {
                await vm.CreatureSelected(c);
            } catch (Exception d)
            {
                Console.WriteLine(d.Message);
            }
        }
    }
}