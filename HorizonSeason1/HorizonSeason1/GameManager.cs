﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    class GameManager
    {
        //prices is an int array [Metals, Energy, Food, Happiness]
        //generate is  an int arr[Metals, Energy, Food, Happiness, maxpop]
        //life form's traits go in an int array [] (add when you know)
        private int metals;
        private int energy;
        private int food;
        private int pops;
        private int happiness;

        private PlanetBuildings[] planetbuildings; // dont forget to finish inizialaiz
        private fleet[] fleet = new fleet[10];
        private Galaxy galaxy;

        private bool showmove;
        private bool show;
        private bool hasShipyard;

        public GameManager()
        {
            this.metals = 100;
            this.energy = 100;
            this.food = 100;
            this.pops = 1;
            this.happiness = 100;
            this.showmove = false;
            this.show = true;


            planetbuildings = new PlanetBuildings[7];
            int[] prices = {25, 15, 5, 0};
            int[] generate = { 0, 0, 0, 0, 1 };
            planetbuildings[0] = new PlanetBuildings("Housing district", "Increases pop capacity on a planet by 1", prices, generate, 100, 50);

            prices[2] = 0;
            prices[1] = 0;
            generate[4] = 0;
            generate[1] = 50;
            planetbuildings[1] = new PlanetBuildings("Energy Plant", "Generates 50 energy per turn", prices, generate, 100, 50);

            prices[0] = 0;
            prices[1] = 25;
            generate[1] = 25;
            planetbuildings[2] = new PlanetBuildings("Mines", "Generates 25 metals per turn", prices, generate, 100, 50);

            prices[2] = 1;
            generate[1] = 0;
            generate[2] = 5;
            planetbuildings[3] = new PlanetBuildings("Farms", "Generates 5 food per turn", prices, generate, 100, 50);

            prices[2] = 5;
            prices[1] = 150;
            prices[0] = 100;
            generate[2] = 0;
            generate[3] = 1;
            planetbuildings[4] = new PlanetBuildings("Capitol", "Increases stability by 1 per turn", prices, generate, 100, 50);

            prices[0] = 25;
            prices[1] = 10;
            generate[3] = 0;
            planetbuildings[5] = new PlanetBuildings("Shipyard", "Allows ship building", prices, generate, 100, 50);

            //Dock
            //Garrison
            //Fort

            prices[0] = 15;
            prices[1] = 25;
            prices[2] = 8;
            generate[3] = 10;
            planetbuildings[6] = new PlanetBuildings("Mega-malls", "Increases happiness by 10 points", prices, generate, 100, 50);

        }

        public string[] stringBuildingsName()
        {
            string[] tmpname = new string[10];

            int counter = 0;
            for (int i = 0; i < Planetbuildings.Length; i++)
            {
                try
                {
                    tmpname[i] = planetbuildings[counter].Name;
                    counter++;
                }
                catch
                {
                }
            }

            Array.Resize(ref tmpname, counter);
            return tmpname;
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
    }
}