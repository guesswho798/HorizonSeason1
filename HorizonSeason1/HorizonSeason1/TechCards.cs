using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    class TechCard
    {
        private string name;
        private string desc1;
        private string desc2;
        private int cost;
        private int turns;
        private bool active;

        public TechCard(string name, string desc1, string desc2, int turns, int cost)
        {
            this.name = name;
            this.turns = turns;
            this.Desc1 = desc1;
            this.Desc2 = desc2;
            this.cost = cost;
            this.active = false;
        }

        public string Name { get => name; set => name = value; }
        public int MaxTurns { get => turns; set => turns = value; }
        public int Turns { get => turns; set => turns = value; }
        public bool Done { get => turns == 0; }
        public string Desc1 { get => desc1; set => desc1 = value; }
        public string Desc2 { get => desc2; set => desc2 = value; }
        public int Cost { get => cost; set => cost = value; }
        public bool Active { get => active; set => active = value; }
    }
}
