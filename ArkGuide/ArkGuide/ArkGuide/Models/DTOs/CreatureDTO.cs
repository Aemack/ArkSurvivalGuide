using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class CreatureDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Dossier { get; set; }
        public string Diet { get; set; }
        public string Temperament { get; set; }
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
        public string Egg { get; set; }
        public string ImageUrl { get; set; }
    }
}
