using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.CreatureModels
{
    public class SpawningMap
    {
        public string MapImage { get; set; }
        public string OverlayImage { get; set; }

        public override string ToString()
        {
            return OverlayImage.Replace("_", " ").Replace("Spawning", " ").Replace(".svg", "").Trim();
        }
    }
}
