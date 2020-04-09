using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    class GameManager
    {
        //prices is an int array [Metals, Energy, Food, Happiness, pops]
        //generate is  an int arr[Metals, Energy, Food, Happiness]
        //life form's traits go in an int array [] (add when you know)
        private int metals;
        private int energy;
        private int food;
        private int pops;
        private int happiness;
        private int tech;

        private PlanetBuildings[] planetbuildings;
        private fleet[] fleet = new fleet[10];
        private Ship[] ships;
        private Galaxy galaxy;

        private bool showmove;
        private bool show;
        private bool hasShipyard;

        private int round;

        public GameManager()
        {
            this.metals = 1000;
            this.energy = 1000;
            this.food = 1000;
            this.tech = 100;
            this.pops = 1;
            this.happiness = 100;
            this.showmove = false;
            this.show = true;
            round = 0;

            #region initialize planet buildings

            planetbuildings = new PlanetBuildings[7];


            planetbuildings[0] = new PlanetBuildings("Housing district", "Increases population capacity. takes 3 turns", new int[] { 25, 15, 5, 0 }, new int[] { 0, 0, 0, 0, 0 }, 100, 50, 3);


            planetbuildings[1] = new PlanetBuildings("Energy Plant", "Generates 50 energy per turn. takes 3 turns", new int[] { 25, 0, 0, 0 }, new int[]{ 0, 50, 0, 0, 0 }, 100, 50, 3);
            

            planetbuildings[2] = new PlanetBuildings("Mines", "Generates 25 metals per turn. takes 3 turns", new int[] { 0, 25, 0, 0 }, new int[] { 25, 0, 0, 0, 0 }, 100, 50, 3);


            planetbuildings[3] = new PlanetBuildings("Farms", "Generates 5 food per turn. takes 3 turns", new int[] { 0, 25, 1, 0 }, new int[] { 0, 0, 0, 50, 0 }, 100, 50, 3);


            planetbuildings[4] = new PlanetBuildings("Capitol", "Increases stability. takes 6 turns", new int[] { 100, 150, 50, 0 }, new int[] { 0, 0, 0, 1, 0 }, 100, 50, 6);


            planetbuildings[5] = new PlanetBuildings("Shipyard", "Allows ship building. takes 5 turns", new int[] { 25, 10, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, 100, 50, 5);

            //Dock
            //Garrison
            //Fort


            planetbuildings[6] = new PlanetBuildings("Mega-malls", "Increases happiness by 10 points. takes 4 turns", new int[] { 15, 25, 50, 0 }, new int[] { 0, 0, 0, 0, 10 }, 100, 50, 4);

            #endregion

            #region initialize ships
            
            Ships = new Ship[12];


            
            Ships[0] = new Ship("Corvette", "Small strike craft.", new int[] { 50, 25, 0, 0, 0 }, 200, 100, 10);

            
            Ships[1] = new Ship("Cruiser", "Medium sized scout vessel.", new int[] { 100, 50, 0, 0, 0 }, 400, 200, 25);


            Ships[2] = new Ship("Destroyer", "Large combat vessel", new int[] { 150, 75, 0, 0, 0 }, 600, 400, 50);

            
            Ships[3] = new Ship("Battlecruiser", "Massive combat vessel..", new int[] { 250, 100, 0, 0, 0 }, 1200, 600, 100);


            Ships[4] = new Ship("Battleship", "Gigantic combat vessel.", new int[] { 400, 200, 0, 0, 0 }, 2000, 1000, 200);
            

            Ships[5] = new Ship("Carrier", "Good against smaller craft, but needs escorts.", new int[] { 500, 300, 0, 0, 0 }, 200, 500, 100); //make this ignores shields on attacks
            

            Ships[6] = new Ship("Titan", "Practically indestructible. Size of a moon.", new int[] { 2000, 800, 0, 0, 0 }, 8000, 4000, 400);

            
            //Production Ships
            Ships[7] = new Ship("Colonizer", "Colonizes new planets", new int[] { 50, 25, 0, 0, 1 }, 100, 100, 0);

            
            Ships[8] = new Ship("Miner", "Produces 25 of the resource its harvesting", new int[] { 50, 25, 0, 0, 0 }, 100, 100, 0);

            
            Ships[9] = new Ship("Science Ship", "Scans new star systems", new int[] { 50, 25, 0, 0, 0 }, 100, 100, 0);

            
            Ships[10] = new Ship("Trade Ship", "Produces 50 Energy per turn", new int[] { 50, 25, 0, 0, 0 }, 100, 100, 0);
            #endregion

        }

        //this is used for the buy options
        public string[] stringBuildingsName()
        {
            string[] names = new string[100];

            int counter = 0;
            for (int i = 0; i < Planetbuildings.Length; i++)
            {
                try
                {
                    names[i] = planetbuildings[counter].Name;
                    counter++;
                }
                catch
                {
                }
            }

            Array.Resize(ref names, counter);
            return names;
        }
        public string[] stringShipsName()
        {
            string[] names = new string[100];

            int counter = 0;
            for (int i = 0; i < Ships.Length; i++)
            {
                if (ships[counter] != null)
                {
                    names[i] = Ships[counter].Name;
                    counter++;
                }
            }

            Array.Resize(ref names, counter);
            return names;
        }

        //clear a certen block
        public void clear(int x, int y, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    Console.Write(" ");
                }
            }
        }

        public void nextRound()
        {
            round++;
            for (int i = 0; i < galaxy.Xy.Length; i++)
            {
                galaxy.Xy[i].turn();
            }
        }

        public int Metals { get => metals; set => metals = value; }
        public int Energy { get => energy; set => energy = value; }
        public int Food { get => food; set => food = value; }
        public int Pops { get => pops; set => pops = value; }
        public int Happiness { get => happiness; set => happiness = value; }
        public fleet[] Fleet { get => fleet; set => fleet = value; }
        public Galaxy Galaxy { get => galaxy; set => galaxy = value; }
        public bool Showmove { get => showmove; set => showmove = value; }
        public bool Show { get => show; set => show = value; }
        public bool HasShipyard { get => hasShipyard; set => hasShipyard = value; }
        public PlanetBuildings[] Planetbuildings { get => planetbuildings; set => planetbuildings = value; }
        public int Round { get => round; set => round = value; }
        public Ship[] Ships { get => ships; set => ships = value; }
        public int Tech { get => tech; set => tech = value; }
    }
}
