using ArkGuide.Database;
using ArkGuide.Models.CreatureModels;
using ArkGuide.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkGuide.ViewModels
{
    public class CreaturesViewModel : BaseViewModel
    {
        private IEnumerable<Creature> _creatures;
        public IEnumerable<Creature> Creatures
        {
            get
            {
                return _creatures;
            }
            set
            {
                _creatures = value;
                OnPropertyChanged(nameof(Creatures));
            }
        }
        public ArkGuideDatabase db { get; }
        public CreaturesViewModel()
        {
            db = new ArkGuideDatabase();
            LoadCreatures();
        }

        public async Task CreatureSelected(Creature c)
        {
            await Shell.Current.GoToAsync($"{nameof(CreaturePage)}?{nameof(CreatureViewModel.CreatureName)}={c.Name}");
        }

        private async Task LoadCreatures()
        {
            Creatures = await db.GetAllCreatures();
        }
    }
}
