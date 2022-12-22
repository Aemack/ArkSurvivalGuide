using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.DTOs
{
    public class FoodRequiredDTO

    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int KnockoutTamingGridId { get; set; }
        public string Food { get; set; }
        public int FoodCount { get; set; }
    }
}
