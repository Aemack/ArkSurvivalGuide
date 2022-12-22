using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.CreatureModels
{

    public class Creature
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Diet { get; set; }
        public string Temperament { get; set; }
        public string Dossier { get; set; }
        public bool Tameable { get; set; }
        public bool Rideable { get; set; }
        public bool Breedable { get; set; }
        public bool TorporImmune { get; set; }
        public string TamingMethod { get; set; }
        public string PreferredKibble { get; set; }
        public string PreferredFood { get; set; }
        public string Equipment { get; set; }
        public bool RiderWeaponry { get; set; }
        public bool RadiationImmune { get; set; }
        public BaseStats BaseStats { get; set; }
        public List<string> CarryableBy { get; set; }
        public List<string> ImmobilizedBy { get; set; }
        public List<string> CanDamage { get; set; }
        public string Egg { get; set; }
        public string ImageUrl { get; set; }
        public List<KnockoutTamingGrid> TamingGrids { get; set; }
        public List<string> UtilityRoles { get; set; }
        public List<string> SpawnCommands { get; set; }
        public List<string> AvailableIn { get; set; }
        public List<SpawningMap> SpawningMaps { get; set; }
    }
}
