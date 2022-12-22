using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class SpawningMapDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public string MapImage { get; set; }
        public string OverlayImage { get; set; }
    }
}
