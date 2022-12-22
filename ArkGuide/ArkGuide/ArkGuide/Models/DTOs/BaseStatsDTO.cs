using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class BaseStatsDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public string Health { get; set; }
        public string Stamina { get; set; }
        public string Oxygen { get; set; }
        public string Food { get; set; }
        public string Weight { get; set; }
        public string MeleeDamage { get; set; }
        public string MovementSpeed { get; set; }
        public string Torpidity { get; set; }
    }
}
