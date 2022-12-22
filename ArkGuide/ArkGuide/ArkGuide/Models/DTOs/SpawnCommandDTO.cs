using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class SpawnCommandDTO
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public string SpawnCommand { get; set; }
    }
}
