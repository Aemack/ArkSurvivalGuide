using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.CreatureModels
{
    public class FoodRequired
    {
        public List<string> Food { get; set; }
        public List<int> FoodCount { get; set; }
        public FoodRequired()
        {
            Food = new List<string>();
            FoodCount = new List<int>();
        }
    }
}
