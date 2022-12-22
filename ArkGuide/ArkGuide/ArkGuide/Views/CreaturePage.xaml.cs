using ArkGuide.Models.CreatureModels;
using ArkGuide.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreaturePage : ContentPage
    {
        CreatureViewModel vm;
        public CreaturePage(Creature creature)
        {
            InitializeComponent();
            vm = (CreatureViewModel)BindingContext;
            vm.Creature = creature;

            MapsPicker.SelectedIndex = 1;
        }


        public CreaturePage()
        {
            InitializeComponent();
            BindingContext = vm = new CreatureViewModel();
        }

        private void SpawnMapChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var spawnMap = (SpawningMap)picker.SelectedItem;
            vm.SpawnMapChanged(spawnMap);
        }

        private async void Command_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = (ListView)sender;
            var command = (string)lv.SelectedItem;
            await Clipboard.SetTextAsync(command);

        }
    }
}