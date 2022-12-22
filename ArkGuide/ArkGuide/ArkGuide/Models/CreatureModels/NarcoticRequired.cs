using System;
using System.Collections.Generic;
using System.Text;

namespace ArkGuide.Models.CreatureModels
{
    public class NarcoticRequired
    {
        public List<string> Narcotic { get; set; }
        public List<int> NarcoticCount { get; set; }
        public NarcoticRequired()
        {
            Narcotic = new List<string>();
            NarcoticCount = new List<int>();
        }
    }
}
