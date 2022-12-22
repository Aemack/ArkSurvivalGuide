using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class NotableLocationDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ResourceMapId { get; set; }
        public string LocationCategory { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
