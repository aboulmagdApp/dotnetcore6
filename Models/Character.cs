using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "aboulmagd";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligense { get; set; } = 10;
        public RgbClass Class { get; set; } = RgbClass.Knight;
    }
}