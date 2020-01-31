using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class LifeForm
    {
        private string name;
        private string bio;
        private Planet[] planetsOwned;
        private int[] traits;
        private int CP; //Combat Power
        private int DP; //Defence Power

        //choose a specific life form
        public LifeForm(int id)
        {
            string name = "";
            string bio = "";
            int[] traits = new int[0];

            //choose
            switch (id)
            {
                case 0:
                    this.name = "Humanoid";
                    this.bio = "The people of the Furlae Directorate are some of the earliest living beings ever created. Soon after the big bang, life forms on Urdun crawled out of the abyss soon after their planet’s formation. Their early civilizations were all based on their main continent, P’anholst. In the early centuries, there was lots of fighting, with a single tribe rising above the others: The Furlae. The Furlae quickly dominated P’anholst, as many tribes took to the oceans and scampered to the smaller continents and archipelagos surrounding P’anholst. The Furlae become a directorate in the equivalent of the Medieval age, uniting multiple continents and islands one by one under their flag. However, the small continent of Withas rebelled at the start of their gunpowder age. The rebellion lasted for many years, and a new nation, the Shing’Hoi Republic was founded. The Shing’Hoi grew rapidly, taking multiple other continents nearby. However, in the year (in their time) of 1926, an alien invasion began. The Luvril Hordes had discovered life on the planet, and went to war with the intent of making the Furlae and the Shing’Hoi battle thralls for their armies. However, a war with the Henpho drew the Luvril back, and causing the Furlae to have access to incredible space-age technology. They quickly started to mass-produce the technology, taking back the areas that the Shing’Hoi controlled, and uniting the entire planet under one banner once again, before taking to the stars, searching for new planets to conquer. ";
                    //this.traits = { };
                    break;
                case 1:
                    this.name = "Deouring Swarm";
                    this.bio = "The Luvril Hordes are not a normal civilization. They exist purely to feed upon other species, then to move on and conquer others. They started like any normal civilization, as mere molecules floating in the ocean, and they then evolved. However, the normality stopped once they stepped onto land. They killed everything in sight, without mercy. They quickly began to drive their planet to extinction, until their Hive Mother ordered food processing factories to be built. These factories systematically slaughtered every living creature that wasn’t Luvril, and sometimes even defective drones. Their society is simple, with one Hive Mother controlling everything, a handful of Apex Drones commanding each continent, and the normal Drones doing everything that needed to be done. They discovered space technology by pure chance, when an anomaly opened up right on their planet. They swarmed to it, and the Apex Drone of the continent began experimenting with it, until they found they could use it to get to another planet. Hordes of drones swarmed through, destroying the unfortunate civilization that existed on the other planet. Fortunately for the Luvril, unfortunately for the rest of the galaxy, that civilization had basic space age technology, which the Luvril used. Since then, the Luvril have been attacking each and every one of their neighbors, with no mercy, and no slowing down.";
                    break;
                case 2:
                    this.name = "Mammalian";
                    this.bio = "";
                    break;
            }



            this.name = name;
            this.bio = bio;
            this.traits = traits;
        }
        //a random life form
        public LifeForm(Random rand)
        {
            int[] traits = new int[0];
            planetsOwned = new Planet[0];

            CP = rand.Next(10, 250);
            DP = rand.Next(10, 250);

            //randomize
            int r = rand.Next(0, 3);
            switch (r)
            {
                case 0:
                    this.name = "Humanoid";
                    this.bio = "The people of the Furlae Directorate are some of the earliest living beings ever created. Soon after the big bang, life forms on Urdun crawled out of the abyss soon after their planet’s formation. Their early civilizations were all based on their main continent, P’anholst. In the early centuries, there was lots of fighting, with a single tribe rising above the others: The Furlae. The Furlae quickly dominated P’anholst, as many tribes took to the oceans and scampered to the smaller continents and archipelagos surrounding P’anholst. The Furlae become a directorate in the equivalent of the Medieval age, uniting multiple continents and islands one by one under their flag. However, the small continent of Withas rebelled at the start of their gunpowder age. The rebellion lasted for many years, and a new nation, the Shing’Hoi Republic was founded. The Shing’Hoi grew rapidly, taking multiple other continents nearby. However, in the year (in their time) of 1926, an alien invasion began. The Luvril Hordes had discovered life on the planet, and went to war with the intent of making the Furlae and the Shing’Hoi battle thralls for their armies. However, a war with the Henpho drew the Luvril back, and causing the Furlae to have access to incredible space-age technology. They quickly started to mass-produce the technology, taking back the areas that the Shing’Hoi controlled, and uniting the entire planet under one banner once again, before taking to the stars, searching for new planets to conquer. ";
                    //this.traits = { };
                    break;
                case 1:
                    this.name = "Deouring Swarm";
                    this.bio = "The Luvril Hordes are not a normal civilization. They exist purely to feed upon other species, then to move on and conquer others. They started like any normal civilization, as mere molecules floating in the ocean, and they then evolved. However, the normality stopped once they stepped onto land. They killed everything in sight, without mercy. They quickly began to drive their planet to extinction, until their Hive Mother ordered food processing factories to be built. These factories systematically slaughtered every living creature that wasn’t Luvril, and sometimes even defective drones. Their society is simple, with one Hive Mother controlling everything, a handful of Apex Drones commanding each continent, and the normal Drones doing everything that needed to be done. They discovered space technology by pure chance, when an anomaly opened up right on their planet. They swarmed to it, and the Apex Drone of the continent began experimenting with it, until they found they could use it to get to another planet. Hordes of drones swarmed through, destroying the unfortunate civilization that existed on the other planet. Fortunately for the Luvril, unfortunately for the rest of the galaxy, that civilization had basic space age technology, which the Luvril used. Since then, the Luvril have been attacking each and every one of their neighbors, with no mercy, and no slowing down.";
                    break;
                case 2:
                    this.name = "Mammalian";
                    this.bio = "";
                    break;
            }
        }

        public void addPlanet(Planet p)
        {
            Array.Resize(ref planetsOwned, planetsOwned.Length + 1);
            planetsOwned[planetsOwned.Length - 1] = p;
        }

        public string Name { get => name; set => name = value; }
        public string Bio { get => bio; set => bio = value; }
        public int[] Traits { get => traits; set => traits = value; }
        public Planet[] PlanetsOwned { get => planetsOwned; set => planetsOwned = value; }
        public int CP1 { get => CP; set => CP = value; }
        public int DP1 { get => DP; set => DP = value; }
    }
}
