using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.CreatureModels
{
    public class KnockoutTamingGrid
    {
        public string Level { get; set; }
        public NarcoticRequired NarcoticRequired { get; set; }
        public FoodRequired FoodRequired { get; set; }
        public KnockoutTamingGrid()
        {
            NarcoticRequired = new NarcoticRequired();
            FoodRequired = new FoodRequired();
        }
    }
}
