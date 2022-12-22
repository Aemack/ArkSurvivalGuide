using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models
{
    public class ResourceMap
    {
        public string MapName { get; set; }
        public List<NotableLocation> NotableLocations { get; set; }
        public string MapImage { get; set; }

    }
}
