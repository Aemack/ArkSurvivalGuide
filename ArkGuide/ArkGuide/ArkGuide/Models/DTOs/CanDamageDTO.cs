using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class CanDamageDTO
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public string Name { get; set; }

    }
}
