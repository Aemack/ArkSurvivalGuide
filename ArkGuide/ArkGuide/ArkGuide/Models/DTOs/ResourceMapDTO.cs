using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class ResourceMapDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MapName { get; set; }
        public string MapImage { get; set; }

    }
}
