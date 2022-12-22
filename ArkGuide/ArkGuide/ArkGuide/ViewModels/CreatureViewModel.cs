using ArkGuide.Database;
using ArkGuide.Models.CreatureModels;
using FFImageLoading;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkGuide.ViewModels
{
    [QueryProperty(nameof(CreatureName), nameof(CreatureName))]
    public class CreatureViewModel : BaseViewModel
    {
        private SpawningMap _currentSpawningMap;
        public SpawningMap CurrentSpawningMap
        {
            get
            {
                return _currentSpawningMap;
            }
            set
            {
                _currentSpawningMap = value;
                OnPropertyChanged(nameof(CurrentSpawningMap));
            }
        }
        private string creatureName;
        public string CreatureName
        {
            get
            {
                return creatureName;
            }
            set
            {
                creatureName = value;
                LoadCreature(value);
            }
        }

        internal void SpawnMapChanged(SpawningMap spawnMap)
        {
            CurrentSpawningMap = spawnMap;
        }

        private Image mapimage;

        private async Task LoadCreature(string value)
        {
            Creature = await db.GetCreatureByName(value);
            
        }


        private Creature _creature;
        public Creature Creature
        {
            get
            {
                return _creature;
            }
            set
            {
                _creature = value;
                OnPropertyChanged(nameof(Creature));
            }
        }

        private ArkGuideDatabase db;

        public CreatureViewModel()
        {
            db = new ArkGuideDatabase();
        }
    }
}
