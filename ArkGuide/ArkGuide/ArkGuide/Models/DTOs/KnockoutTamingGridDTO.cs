using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class KnockoutTamingGridDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public string Level { get; set; }
    }
}
