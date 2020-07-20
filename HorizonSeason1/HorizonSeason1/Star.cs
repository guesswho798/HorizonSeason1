using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class Star
    {
        private string name = "";
        private string starName;
        private string description;
        private bool homeStar; //if this star is the home system
        private int counter; //id number of the star
        private bool astroidbelt; //weather the planet is soraunded by an astroid belt
        private bool visible;
        private int x; //galaxy map x
        private int y; //galaxy map y
        private int sx; //system x
        private int sy; //system y
        private int radius; //radius of star
        private int range; //the scanners range
        Planet[] planets;

        public int Radius { get => radius; set => radius = value; }
        public Planet[] Planets { get => planets; set => planets = value; }
        public bool HomeStar { get => homeStar; set => homeStar = value; }
        public bool Visible { get => visible; set => visible = value; }
        public int Range { get => range; set => range = value; }
        public string StarName { get => starName; set => starName = value; }

        public Star(int id, int difficulty, int numofplanets, int num, Random rand, int counter, string starname, int x, int y, bool homeStar = false)
        {
            this.starName = starname;
            this.homeStar = homeStar;
            this.visible = homeStar;
            if (visible)
                range = 7;
            this.counter = counter;
            this.x = x;
            this.y = y;

            //which star
            if (difficulty == 0 && id <= 19 || difficulty == 1 && id <= 7 || difficulty == 2 && id <= 7)
            {
                name = "White Sun";
                description = "A star with heavy hydrogen content. There may be some easily colonizable planets, but a better probability of planets that are slightly more difficult to colonize.Gas Giants are very rare.";
            }
            else if (difficulty == 0 && id >= 20 && id <= 39 || difficulty == 1 && id >= 8 && id <= 20 || difficulty == 2 && id >= 8 && id <= 15)
            {
                name = "Yellow Sun";
                description = "A star whose heat is average -- for a blazing ball of nuclear fusion. There is a high probability of finding planets that require minimal research to colonize. Gas Giants are very rare.";
            }
            else if (difficulty == 0 && id >= 40 && id <= 52 || difficulty == 1 && id >= 21 && id <= 40 || difficulty == 2 && id >= 16 && id <= 22)
            {
                name = "Blue Sun";
                description = "Among the hottest and brightest of all stars. Warmer, drier planets are common; easily colonizable planets are less common";
            }
            else if (difficulty == 0 && id >= 53 && id <= 65 || difficulty == 1 && id >= 41 && id <= 60 || difficulty == 2 && id >= 23 && id <= 29)
            {
                name = "Protostar";
                description = "A young star that is weak and cold; little more than a dense molecular cloud.Colder, drier planets are common; easily colonizable plantets are rare.There is a slightly better chance of finding Gas Giants.";
            }
            else if (difficulty == 0 && id >= 66 && id <= 78 || difficulty == 1 && id >= 61 && id <= 72 || difficulty == 2 && id >= 30 && id <= 43)
            {
                name = "Red Supergiant";
                description = "A very large star in terms of volume, though not massive, and relatively cool. It is unlikely to have either easily colonizable planets or Gas Giants.";
            }
            else if (difficulty == 0 && id >= 79 && id <= 86 || difficulty == 1 && id >= 73 && id <= 84 || difficulty == 2 && id >= 44 && id <= 65)
            {
                name = "Binary";
                description = "A system of two stars orbiting around their common barycenter. Dry, hot, and rocky planets are more common, and rarely Gas Giants.";
            }
            else if (difficulty == 0 && id >= 87 && id <= 92 || difficulty == 1 && id >= 85 && id <= 92 || difficulty == 2 && id >= 66 && id <= 87)
            {
                name = "Red Dwarf";
                description = "A relatively small and cool star that is long-lived. Easily colonizable planets are very rare; less hospitable planets are common. there is a greater chance of Gas Giants.";
            }
            else if (difficulty == 0 && id >= 93 || difficulty == 1 && id >= 93 || difficulty == 2 && id >= 88)
            {
                name = "White Dwarf";
                description = "A very small, very old, very dense and very weak star near the end of its life. The less easily colonizable rocky planets are more common here, and gas may be found";
            }

            //number of planets
            if (homeStar == true)
            {
                if (id <= 25)
                {
                    this.planets = new Planet[3];
                }
                else if (id >= 26 && id <= 50)
                {
                    this.planets = new Planet[4];
                }
                else if (id >= 51 && id <= 75)
                {
                    this.planets = new Planet[5];
                }
                else if (id >= 76)
                {
                    this.planets = new Planet[6];
                }
            }
            else if (homeStar == false)
            {
                if (numofplanets == 0 && num <= 16 || numofplanets == 1 && num <= 8)
                {
                    this.planets = new Planet[1];
                }
                else if (numofplanets == 0 && num >= 17 && num <= 41 || numofplanets == 1 && num >= 9 && num <= 25 || numofplanets == 2 && num <= 9)
                {
                    this.planets = new Planet[2];
                }
                else if (numofplanets == 0 && num >= 42 && num <= 67 || numofplanets == 1 && num >= 26 && num <= 51 || numofplanets == 2 && num >= 10 && num <= 28)
                {
                    this.planets = new Planet[3];
                }
                else if (numofplanets == 0 && num >= 68 && num <= 84 || numofplanets == 1 && num >= 52 && num <= 77 || numofplanets == 2 && num >= 29 && num <= 45)
                {
                    this.planets = new Planet[4];
                }
                else if (numofplanets == 0 && num >= 85 && num <= 93 || numofplanets == 1 && num >= 78 && num <= 93 || numofplanets == 2 && num >= 46 && num <= 73)
                {
                    this.planets = new Planet[5];
                }
                else if (numofplanets == 0 && num >= 94 || numofplanets == 1 && num >= 94 || numofplanets == 2 && num >= 74)
                {
                    this.planets = new Planet[6];
                }
            }

            //randing an astroid belt
            if (rand.Next(0, 5) == 0)
            {
                astroidbelt = true;
            }
            else
            {
                astroidbelt = false;
            }

            int[] positionstaken = new int[planets.Length];

            for (int a = 0; a < positionstaken.Length; a++)
            {
                positionstaken[a] = 999;
            }

            //choosing home planet. not effection anything if this star is not home star
            int homePlanet = rand.Next(0, planets.Length);

            //which planets
            for (int i = 0; i < planets.Length; i++)
            {
                int[] array = new int[3];
                array[0] = rand.Next(1, 11);
                array[1] = rand.Next(1, 18);
                array[2] = rand.Next(2, 4);

                //giving position
                bool taken;
                do
                {
                    taken = false;
                    positionstaken[i] = rand.Next(0, 8);

                    for (int a = 0; a < planets.Length; a++)
                    {
                        if (positionstaken[a] == positionstaken[i] && a != i) //if position is all ready taken
                        {
                            taken = true;
                        }
                    }
                }
                while (taken == true);


                if (this.homeStar)
                    planets[i] = new Planet(i == homePlanet, array, radius, rand, positionstaken[i], x, y);
                else
                    planets[i] = new Planet(false, array, radius, rand, positionstaken[i], x , y);

            }

            //sort planets with their position
            this.Planets = planets.OrderBy(c => c.Position).ToArray();

            //setting radius
            switch (name)
            {
                case "White Sun": radius = 4; break;
                case "Yellow Sun": radius = 4; break;
                case "Blue Sun": radius = 4; break;
                case "Protostar": radius = 4; break;
                case "Red Supergiant": radius = 5; break;
                case "Binary": radius = 4; break;
                case "Red Dwarf": radius = 3; break;
                case "White Dwarf": radius = 3; break;
            }
        }
        public Star(int id, int difficulty, int numofplanets, int num, Random rand, int counter, string starname, int x, int y, LifeForm life)
        {
            this.starName = starname;
            this.homeStar = false;
            this.counter = counter;
            this.visible = false;
            this.x = x;
            this.y = y;

            //which star
            if (difficulty == 0 && id <= 19 || difficulty == 1 && id <= 7 || difficulty == 2 && id <= 7)
            {
                name = "White Sun";
                description = "A star with heavy hydrogen content. There may be some easily colonizable planets, but a better probability of planets that are slightly more difficult to colonize.Gas Giants are very rare.";
            }
            else if (difficulty == 0 && id >= 20 && id <= 39 || difficulty == 1 && id >= 8 && id <= 20 || difficulty == 2 && id >= 8 && id <= 15)
            {
                name = "Yellow Sun";
                description = "A star whose heat is average -- for a blazing ball of nuclear fusion. There is a high probability of finding planets that require minimal research to colonize. Gas Giants are very rare.";
            }
            else if (difficulty == 0 && id >= 40 && id <= 52 || difficulty == 1 && id >= 21 && id <= 40 || difficulty == 2 && id >= 16 && id <= 22)
            {
                name = "Blue Sun";
                description = "Among the hottest and brightest of all stars. Warmer, drier planets are common; easily colonizable planets are less common";
            }
            else if (difficulty == 0 && id >= 53 && id <= 65 || difficulty == 1 && id >= 41 && id <= 60 || difficulty == 2 && id >= 23 && id <= 29)
            {
                name = "Protostar";
                description = "A young star that is weak and cold; little more than a dense molecular cloud.Colder, drier planets are common; easily colonizable plantets are rare.There is a slightly better chance of finding Gas Giants.";
            }
            else if (difficulty == 0 && id >= 66 && id <= 78 || difficulty == 1 && id >= 61 && id <= 72 || difficulty == 2 && id >= 30 && id <= 43)
            {
                name = "Red Supergiant";
                description = "A very large star in terms of volume, though not massive, and relatively cool. It is unlikely to have either easily colonizable planets or Gas Giants.";
            }
            else if (difficulty == 0 && id >= 79 && id <= 86 || difficulty == 1 && id >= 73 && id <= 84 || difficulty == 2 && id >= 44 && id <= 65)
            {
                name = "Binary";
                description = "A system of two stars orbiting around their common barycenter. Dry, hot, and rocky planets are more common, and rarely Gas Giants.";
            }
            else if (difficulty == 0 && id >= 87 && id <= 92 || difficulty == 1 && id >= 85 && id <= 92 || difficulty == 2 && id >= 66 && id <= 87)
            {
                name = "Red Dwarf";
                description = "A relatively small and cool star that is long-lived. Easily colonizable planets are very rare; less hospitable planets are common. there is a greater chance of Gas Giants.";
            }
            else if (difficulty == 0 && id >= 93 || difficulty == 1 && id >= 93 || difficulty == 2 && id >= 88)
            {
                name = "White Dwarf";
                description = "A very small, very old, very dense and very weak star near the end of its life. The less easily colonizable rocky planets are more common here, and gas may be found";
            }

            //number of planets
            if (numofplanets == 0 && num <= 16 || numofplanets == 1 && num <= 8)
            {
                this.planets = new Planet[1];
            }
            else if (numofplanets == 0 && num >= 17 && num <= 41 || numofplanets == 1 && num >= 9 && num <= 25 || numofplanets == 2 && num <= 9)
            {
                this.planets = new Planet[2];
            }
            else if (numofplanets == 0 && num >= 42 && num <= 67 || numofplanets == 1 && num >= 26 && num <= 51 || numofplanets == 2 && num >= 10 && num <= 28)
            {
                this.planets = new Planet[3];
            }
            else if (numofplanets == 0 && num >= 68 && num <= 84 || numofplanets == 1 && num >= 52 && num <= 77 || numofplanets == 2 && num >= 29 && num <= 45)
            {
                this.planets = new Planet[4];
            }
            else if (numofplanets == 0 && num >= 85 && num <= 93 || numofplanets == 1 && num >= 78 && num <= 93 || numofplanets == 2 && num >= 46 && num <= 73)
            {
                this.planets = new Planet[5];
            }
            else if (numofplanets == 0 && num >= 94 || numofplanets == 1 && num >= 94 || numofplanets == 2 && num >= 74)
            {
                this.planets = new Planet[6];
            }


            //randing an astroid belt
            if (rand.Next(0, 4) == 0)
            {
                astroidbelt = true;
            }
            else
            {
                astroidbelt = false;
            }

            int[] positionstaken = new int[planets.Length];
            for (int a = 0; a < positionstaken.Length; a++)
            {
                positionstaken[a] = 999;
            }

            //choosing home planet. not effection anything if this star is not home star
            int homePlanet = rand.Next(0, planets.Length);

            //which planets
            for (int i = 0; i < planets.Length; i++)
            {
                int[] array = new int[3];
                array[0] = rand.Next(1, 11);
                array[1] = rand.Next(1, 18);
                array[2] = rand.Next(2, 4);

                //giving position
                bool taken;
                do
                {
                    taken = false;
                    positionstaken[i] = rand.Next(0, 8);

                    for (int a = 0; a < planets.Length; a++)
                    {
                        if (positionstaken[a] == positionstaken[i] && a != i) //if position is all ready taken
                        {
                            taken = true;
                        }
                    }
                }
                while (taken == true);

                //if the life form should take this planet
                if (i == homePlanet)
                {
                    life.addPlanet(planets[i]);
                    planets[i] = new Planet(array, radius, rand, positionstaken[i], life, x, y);
                }
                else
                    planets[i] = new Planet(false, array, radius, rand, positionstaken[i], x , y); //empty planet

            }

            //sort planets with their position
            this.Planets = planets.OrderBy(c => c.Position).ToArray();

            //setting radius
            switch (name)
            {
                case "White Sun": radius = 4; break;
                case "Yellow Sun": radius = 4; break;
                case "Blue Sun": radius = 4; break;
                case "Protostar": radius = 4; break;
                case "Red Supergiant": radius = 5; break;
                case "Binary": radius = 4; break;
                case "Red Dwarf": radius = 3; break;
                case "White Dwarf": radius = 3; break;
            }
        }

        //drawing and logic
        public void drawstar(int offsetx, int offsety)
        {
            double r = radius;
            int binary = 1;
            //what color
            switch (name)
            {
                case "White Sun": Console.ForegroundColor = ConsoleColor.White; break;
                case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; binary = 2; offsetx -= 10; break;
                case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; break;
                case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; break;
            }

            double r_in = r - 0.0;
            double r_out = r + 0.1;

            offsety -= Convert.ToInt32(r);

            this.sx = offsetx;
            this.sy = offsety;

            for (int i = 0; i < binary; i++)
            {
                if (i == 1)
                {
                    offsetx += Convert.ToInt32(r) * 4 + 5;
                }

                int counterx = 0;
                int countery = 0;

                for (double y = r; y >= -r; --y)
                {
                    counterx = 0;
                    for (double x = -r; x < r_out; x += 0.5)
                    {
                        Console.SetCursorPosition(counterx + offsetx, countery + offsety);
                        double value = x * x + y * y;
                        if (value >= r_in * r_in && value <= r_out * r_out)
                        {
                            Console.Write("*");
                        }
                        else if (value < r_in * r_in && value < r_out * r_out)
                        {

                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                        counterx++;
                    }
                    countery++;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void drawAstroidBelt()
        {
            int radius = 13;
            //15.0 / 3.0   full screen
            double console_ratio = Convert.ToDouble(11.5 / 3.0);
            double a = console_ratio * radius;
            double b = radius;
            Random randy = new Random();

            for (int y = -radius; y <= radius; y++)
            {
                for (double x = -a; x < a; x++)
                {
                    double d = (x / a) * (x / a) + (y / b) * (y / b);
                    if (d > 0.8 && d < 1.0 && randy.Next(0, 11) < 9)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void drawplanets(int selected)
        {
            //int[,] offsettaken = new int[2, planets.Length];
            for (int j = 0; j < planets.Length; j++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Planet p = planets[j];


                Random rand = new Random();

                bool exist = true;

                double r = p.Size - 1;
                //what color
                switch (p.TypeName)
                {
                    case "Destroyed Planet": Console.ForegroundColor = ConsoleColor.White; break;
                    case "Gaia Planet": Console.ForegroundColor = ConsoleColor.Green; break;
                    case "Temperate Planet": Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case "Cold Planet": Console.ForegroundColor = ConsoleColor.Blue; break;
                    case "Hot Planet": Console.ForegroundColor = ConsoleColor.Red; break;
                    case "Ocean Planet": Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case "Jungle Planet": Console.ForegroundColor = ConsoleColor.Green; break;
                    case "Permafrost Planet": Console.ForegroundColor = ConsoleColor.White; break;
                    case "Frigid Planet": Console.ForegroundColor = ConsoleColor.White; break;
                    case "Scorched Planet": Console.ForegroundColor = ConsoleColor.Red; break;
                    case "Tropical Planet": Console.ForegroundColor = ConsoleColor.Green; break;
                    case "Tundra Planet": Console.ForegroundColor = ConsoleColor.White; break;
                    case "Desert Planet": Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case "Frozen Planet": Console.ForegroundColor = ConsoleColor.White; break;
                    case "Extreme Planet": Console.ForegroundColor = ConsoleColor.DarkGray; break;
                    case "Dead Planet (hot)": Console.ForegroundColor = ConsoleColor.Red; break;
                    case "Dead Planet (cold)": Console.ForegroundColor = ConsoleColor.Blue; break;
                    case "Tomb Planet": Console.ForegroundColor = ConsoleColor.Gray; break;
                    default: exist = false; break;
                }

                if (!exist && p.TypeName != "")
                {
                    Console.WriteLine("'" + p.TypeName + "' does not exist");
                }

                int offsetx = 0;
                int offsety = 0;
                if (exist)
                {
                    double r_in = r - 0.0;
                    double r_out = r + 0.1;

                    int middlex = sx;
                    int middley = sy;

                    if (name != "Binary")
                    {
                        switch (p.Position)
                        {
                            case 0:
                                offsetx = middlex; //middle top
                                offsety = middley - 2 * (radius);
                                break;
                            case 1:
                                offsetx = middlex + radius * 5; //right top
                                offsety = middley - (radius * 2);
                                break;
                            case 2:
                                offsetx = middlex + 4 * (p.Size + radius); //right middle
                                offsety = middley;
                                break;
                            case 3:
                                offsetx = middlex + radius * 5; //right bottom
                                offsety = middley + radius * 2;
                                break;
                            case 4:
                                offsetx = middlex; //middle bottom
                                offsety = middley + 2 * (radius + p.Size);
                                break;
                            case 5:
                                offsetx = middlex - Radius * 5; //left bottom
                                offsety = middley + radius * 2;
                                break;
                            case 6:
                                offsetx = middlex - 4 * (p.Size + radius); //left middle
                                offsety = middley;
                                break;
                            case 7:
                                offsetx = middlex - radius * 5; //top left
                                offsety = middley - radius * 2;
                                break;
                        }
                    }
                    else
                    {
                        switch (p.Position)
                        {
                            case 0:
                                offsetx = middlex + radius * 2; //middle top
                                offsety = middley - 2 * (radius);
                                break;
                            case 1:
                                offsetx = middlex + radius * 8; //right top
                                offsety = middley - (radius * 2);
                                break;
                            case 2:
                                offsetx = middlex + 8 * (p.Size + radius); //right middle
                                offsety = middley;
                                break;
                            case 3:
                                offsetx = middlex + radius * 10; //right bottom
                                offsety = middley + radius * 2;
                                break;
                            case 4:
                                offsetx = middlex + radius; //middle bottom
                                offsety = middley + 2 * radius + p.Size;
                                break;
                            case 5:
                                offsetx = middlex - Radius * 5; //left bottom
                                offsety = middley + radius * 2;
                                break;
                            case 6:
                                offsetx = middlex - 4 * (p.Size + radius); //left middle
                                offsety = middley;
                                break;
                            case 7:
                                offsetx = middlex - radius * 5; //top left
                                offsety = middley - radius * 2;
                                break;
                        }

                    }

                    int counterx = 0;
                    int countery = 0;

                    for (double y = r; y >= -r; --y)
                    {
                        counterx = 0;
                        for (double x = -r; x < r_out; x += 0.5)
                        {
                            Console.SetCursorPosition(counterx + offsetx, countery + offsety);
                            double value = x * x + y * y;
                            if (value >= r_in * r_in && value <= r_out * r_out)
                            {
                                //the outline of a destroyed plant will be drawn
                                Console.Write("*");
                            }
                            else if (value < r_in * r_in && value < r_out * r_out)
                            {
                                if (p.TypeName == "Destroyed Planet" && rand.Next(0, 3) != 0)
                                {
                                    Console.Write("*");
                                }
                                else if (p.TypeName != "Destroyed Planet")
                                {
                                    Console.Write("*");
                                }
                            }
                            counterx++;
                        }
                        countery++;
                        Console.WriteLine();
                    }

                    if (j == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(p.Size + offsetx, p.Size + offsety - 1);
                        if (p.Size == 3)
                        {
                            Console.SetCursorPosition(p.Size + offsetx + 1, p.Size + offsety - 1);
                        }
                        Console.Write("@");
                    }
                }

                //drawing shipyard
                if (p.HasShipyard)
                {
                    p.ShipyardPosition++;
                    if (p.ShipyardPosition == 4)
                        p.ShipyardPosition = 0;
                    int x = 0;
                    int y = 0;
                    int lastx = 0;
                    int lasty = 0;
                    switch (p.ShipyardPosition)
                    {
                        case 0:
                            lastx = offsetx - 1;
                            lasty = offsety - 1 + p.Size * 2;
                            x = offsetx - 1;
                            y = offsety - 1;
                            break;
                        case 1:
                            lastx = offsetx - 1;
                            lasty = offsety - 1;
                            x = offsetx + 1 + p.Size * 2;
                            y = offsety - 1;
                            break;
                        case 2:
                            lastx = offsetx + 1 + p.Size * 2;
                            lasty = offsety - 1;
                            x = offsetx + 1 + p.Size * 2;
                            y = offsety - 1 + p.Size * 2;
                            break;
                        case 3:
                            lastx = offsetx + 1 + p.Size * 2;
                            lasty = offsety - 1 + p.Size * 2;
                            x = offsetx - 1;
                            y = offsety - 1 + p.Size * 2;
                            break;
                    }
                    //puting borders at the edges
                    if (y < 0)
                        y = 0;
                    if (lasty < 0)
                        lasty = 0;
                    if (y >= 100)
                        y = 99;
                    if (lasty >= 100)
                        lasty = 99;

                    Console.SetCursorPosition(lastx, lasty);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("#");
                }

                //drawing fleet on planet
                for (int i = 0; i < Program.manager.Fleet.Length; i++)
                {
                    if (Program.manager.Fleet[i] != null && Program.manager.Fleet[i].Place == p)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(offsetx, offsety);
                        Console.WriteLine("^");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        public void drawUI(int selected, bool lines = true)
        {
            if (lines)
                drawline();

            Planet selectedP = planets[selected];

            Console.SetCursorPosition(0, 32);
            if (selectedP.PlayerOwned)
                Console.WriteLine("Metals: " + Program.manager.Metals + ", Energy: " + Program.manager.Energy + ", Food: " + Program.manager.Food + "              ");
            Console.WriteLine(GetInfo(true));

            string underline = "";
            for (int i = 0; i < selectedP.TypeName.Length; i++)
            {
                underline += "─";
            }
            Console.SetCursorPosition(105, 2);
            Console.WriteLine(underline);

            //showing info
            Console.SetCursorPosition(0, 35);
            if (selectedP.PlayerOwned)
            {
                Console.WriteLine("Max population: " + selectedP.MaxPop);

                Console.WriteLine("Population: " + selectedP.Pops);

                if (selectedP.GetProduction() == "")
                    Console.WriteLine("Production: None");
                else
                    Console.WriteLine("Production: " + selectedP.GetProduction());

                if (selectedP.GetBuildings() == "")
                    Console.WriteLine("Buildings: None");
                else
                    Console.WriteLine("Buildings: " + selectedP.GetBuildings());

                if (selectedP.GetQueue() == "")
                    Console.Write("Build queue: None");
                else
                    Console.Write("Build queue: " + selectedP.GetQueue());
            }
            else if (selectedP.CreatureOwned)
            {
                Console.WriteLine("Species: " + selectedP.Life.Name);
                Console.WriteLine("Population: " + selectedP.Pops);
                Console.WriteLine("Combat Power: " + selectedP.Life.CP1);
                Console.WriteLine("Defence Power: " + selectedP.Life.DP1);
            }
            else if (selectedP.TypeName != "Destroyed Planet")
            {
                Console.WriteLine("Empty planet");
            }

            Console.SetCursorPosition(105, 1);
            Console.WriteLine(selectedP.TypeName);

            //menu part
            if (selectedP.TypeName == "Destroyed Planet")
            {
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Search");
            }
            else if (selectedP.PlayerOwned)
            {
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Build");

                Console.SetCursorPosition(105, 6);
                Console.WriteLine("Decisions");

                Console.SetCursorPosition(105, 8);
                Console.WriteLine("Back");
            }
            else if (selectedP.CreatureOwned)
            {
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Attack");

                Console.SetCursorPosition(105, 6);
                Console.WriteLine("Trade");

                Console.SetCursorPosition(105, 8);
                Console.WriteLine("Talk");

                Console.SetCursorPosition(105, 10);
                Console.WriteLine("Back");
            }
            else
            {
                //empty planet
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Conquer");

                Console.SetCursorPosition(105, 6);
                Console.WriteLine("Visit");

                Console.SetCursorPosition(105, 8);
                Console.WriteLine("Back");
            }
        }
        public void drawline()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(100, i);

                if (i == 31)
                {
                    Console.WriteLine("┤");
                }
                else if (i != Console.WindowHeight - 1)
                {
                    Console.WriteLine("│");
                }
                else
                {
                    Console.Write("│");
                }
            }

            Console.SetCursorPosition(0, 31);
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            
        }
        public void menuUI(int selected)
        {
            Planet selectedP = planets[selected];

            //selected a destroyed planet
            if (selectedP.TypeName == "Destroyed Planet")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Search");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    //choose a fleet
                    Console.WriteLine("chosen");
                    Console.ReadKey(true);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            //selected a player owned planet
            else if (selectedP.PlayerOwned)
            {
                int selector = 4;
                string[] options = new string[9];
                options[4] = "Build";
                options[6] = "Decisions";
                options[8] = "Back";
                while (true)
                {
                    //drawing
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector]);

                    //reciving input
                    ConsoleKey key = Console.ReadKey(true).Key;

                    //"deleting" trace
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector] + " ");

                    //logic
                    if (key == ConsoleKey.DownArrow)
                    {
                        selector += 2;
                        if (selector == 10)
                            selector = 4;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selector -= 2;
                        if (selector == 2)
                            selector = 8;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        if (selector == 4)
                            build(selectedP);
                        else if (selector == 6)
                           Decisions();
                        else if (selector == 8)
                            break;
                        drawUI(selected);
                    }
                    else if (key == ConsoleKey.Escape || key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow)
                    {
                        break;
                    }
                }
            }
            //selected a creature owned planet
            else if (selectedP.CreatureOwned)
            {
                int selector = 4;
                string[] options = new string[11];
                options[4] = "Attack";
                options[6] = "Trade";
                options[8] = "Talk";
                options[10] = "Back";

                while (true)
                {
                    //drawing
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector]);

                    //reciving input
                    ConsoleKey key = Console.ReadKey(true).Key;

                    //"deleting" trace
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector] + " ");

                    //logic
                    if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    {
                        selector += 2;
                        if (selector == 12)
                            selector = 4;
                    }
                    else if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    {
                        selector -= 2;
                        if (selector == 2)
                            selector = 10;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        //later
                        if (selector == 10)
                            break;
                        Console.WriteLine(options[selector]);
                    }
                    else if (key == ConsoleKey.Escape || key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow)
                    {
                        break;
                    }
                }
            }
            //selected an empty planet
            else
            {
                //empty planet
                int selector = 4;
                string[] options = new string[9];
                options[4] = "Conquer";
                options[6] = "Visit";
                options[8] = "Back";

                while (true)
                {
                    //drawing
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector]);

                    //reciving input
                    ConsoleKey key = Console.ReadKey(true).Key;

                    //"deleting" trace
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(105, selector);
                    Console.Write(options[selector] + " ");

                    //logic
                    if (key == ConsoleKey.DownArrow)
                    {
                        selector += 2;
                        if (selector == 10)
                            selector = 4;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selector -= 2;
                        if (selector == 2)
                            selector = 8;
                    }
                    else if (key == ConsoleKey.Enter)
                        if (selector == 4)
                        {
                            Program.fleetPicker(this.x, this.y, selectedP);
                            
                            break;
                        }
                        if (selector == 8)
                            break;
                    else if (key == ConsoleKey.Escape || key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow)
                    {
                        break;
                    }
                }
            }
        }
        public void drawSystem()
        {
            //rotating planets
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].MovePlanet();
            }

            //initializing selector to start on a special planet
            int selector = 0;
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i].CreatureOwned)
                    selector = i;
            }
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i].PlayerOwned)
                    selector = i;
            }

            if (astroidbelt)
                drawAstroidBelt();
            drawstar(Console.WindowWidth / 2 - radius * 6, Console.WindowHeight / 2 - radius);
            drawUI(selector);

            bool stay = true;
            while (stay)
            {
                //drawing
                drawplanets(selector);
                drawUI(selector, false);


                //reciving input
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    selector++;
                    if (selector == planets.Length)
                    {
                        selector = 0;
                    }
                }
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    selector--;
                    if (selector == -1)
                    {
                        selector = planets.Length - 1;
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    menuUI(selector);
                    Program.manager.Showmove = false;
                    Program.manager.Show = false;
                    drawUI(selector);
                    drawstar(Console.WindowWidth / 2 - radius * 6, Console.WindowHeight / 2 - radius);
                    if (astroidbelt)
                        drawAstroidBelt();
                }
                else if (key == ConsoleKey.Escape || key == ConsoleKey.Backspace)
                {
                    stay = false;
                }
                Program.manager.clear(0, 32, 100, Console.WindowHeight - 31);
                Program.manager.clear(101, 0, Console.WindowWidth - 101, 20);
            }
            Console.Clear();
        }
        public void build(Planet p)
        {
            //buy only buildings
            if (!p.HasShipyard)
            {
                buyBuilding(p);
            }
            //buy buildings or ships
            else
            {
                //delete trace of the option "back"
                Console.SetCursorPosition(105, 8);
                Console.Write("    ");

                string[] options = { "Buildings", "Ships" , "Back"};
                int selector = Program.Menu(105, 4, options, false);
                if (selector == 0)
                    buyBuilding(p);
                else if (selector == 1)
                    buyShips(p);

            }
        }
        public void buyBuilding(Planet p)
        {
            //if the player chooses not to buy after he looks at description he will be put back in the list to look for something else
            bool stay;
            do
            {
                stay = false;

                //which building to buy
                string[] options = Program.manager.stringBuildingsName();
                options[options.Length - 1] = "Back";
                int selector = Program.Menu(105, 4, options, false);

                //really buy?
                if (selector != options.Length - 1)
                {
                    //displaying info
                    Console.SetCursorPosition(105, 4);
                    Console.WriteLine(Program.manager.Planetbuildings[selector].Name);
                    Console.SetCursorPosition(105, 6);
                    Console.WriteLine(Program.manager.Planetbuildings[selector].Description);
                    Console.SetCursorPosition(105, 7);
                    Console.WriteLine(Program.manager.Planetbuildings[selector].Description2);

                    string[] options1 = { "Buy", "Back" };
                    int buy = Program.Menu(105, 9, options1, false);
                    if (buy == 0 && Program.manager.Planetbuildings[selector].build())
                    {
                        stay = false;
                        PlanetBuildings pb = Program.manager.Planetbuildings[selector].copy();
                        pb.attachp(p);
                        p.addQueue(pb);
                    }
                    else if (buy == 1)
                        stay = true;
                }

                //makes sure nothing is left over
                Program.manager.clear(101, 4, Console.WindowWidth - 101, Console.WindowHeight - 5);
            }
            while (stay);
        }
        public void buyShips(Planet p)
        {
            //if the player chooses not to buy after he looks at description he will be put back in the list to look for something else
            bool stay;
            do
            {
                stay = false;

                //which building to buy
                string[] options = Program.manager.stringShipsName();
                options[options.Length - 1] = "Back";
                int selector = Program.Menu(105, 4, options, false);

                //really buy?
                if (selector != options.Length - 1)
                {
                    //displaying info
                    Console.SetCursorPosition(105, 4);
                    Console.WriteLine(Program.manager.Ships[selector].Name);
                    Console.SetCursorPosition(105, 6);
                    Console.WriteLine(Program.manager.Ships[selector].Description);
                    Console.SetCursorPosition(105, 7);
                    Console.WriteLine(Program.manager.Ships[selector].Description2);

                    string[] options1 = { "Buy", "Back" };
                    int buy = Program.Menu(105, 9, options1, false);
                    int numOfShips = 1;
                    Program.manager.clear(105, 9, Console.WindowWidth - 106, 20);
                    if (buy == 0 || numOfShips == 0)
                    {
                        Console.SetCursorPosition(105, 9);
                        Console.WriteLine("Price per ship (Metal:" + Program.manager.Ships[selector].Price[0] + ", ");
                        Console.SetCursorPosition(105, 10);
                        Console.WriteLine("Energy: " + Program.manager.Ships[selector].Price[1] + ", Pops: "  + Program.manager.Ships[selector].Price[4] +  ")");
                        Console.SetCursorPosition(105, 11);
                        Console.Write("How many(?): ");
                        Console.CursorVisible = true;
                        bool valid = true;
                        while (valid)
                        {
                            try { numOfShips = int.Parse(Console.ReadLine()); valid = false; }
                            catch { valid = true; Program.manager.clear(118, 11, 10, 2); Console.SetCursorPosition(118, 11); }
                        } 
                        Console.CursorVisible = false;
                    }

                    //buying the ship
                    if (buy == 0 && Program.manager.Ships[selector].build(numOfShips))
                    {
                        stay = false;
                        Ship s = Program.manager.Ships[selector].copy();
                        Program.manager.clear(105, 4, Console.WindowWidth - 106, 20);

                        //after you bought assigning to a fleet
                        Console.SetCursorPosition(105, 4);
                        Console.Write("Assign to a fleet:");

                        string[] options2 = new string[1];
                        int index = 0;

                        //runing through the whole fleet array
                        for (int i = 0; i < Program.manager.Fleet.Length; i++)
                        {
                            //adding it to the options to display later
                            if (Program.manager.Fleet[i] != null)
                            {
                                string show = Program.manager.Fleet[i].Name;
                                options2[index] = show;
                                index++;
                                Array.Resize(ref options2, options2.Length + 1);
                            }
                            else
                            {
                                //if there is an empty fleet then should break
                                break;
                            }
                        }

                        options2[index] = "Add a new fleet";
                        int selector1 = Program.Menu(105, 5, options2, false);

                        //assign to a fleet right here
                        if (selector1 != options2.Length - 1)
                            Program.manager.Fleet[selector1].add(s, numOfShips);
                        else
                        {
                            Program.manager.clear(105, 4, Console.WindowWidth - 106, 20);
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(105, 4);
                            Console.Write("fleets name: ");
                            string n = "";
                            while (n == "") { n = Console.ReadLine(); Console.SetCursorPosition(118, 4); }
                            Program.manager.Fleet[selector1] = new fleet(n, p, this.x, this.y);
                            Console.CursorVisible = false;
                            Program.manager.Fleet[selector1].add(s, numOfShips);
                        }

                        Program.manager.clear(105, 4, Console.WindowWidth - 106, 20);

                        Console.SetCursorPosition(105, 9);
                        Console.Write("you bought " + numOfShips + " " + Program.manager.Ships[selector].Name);
                        Console.SetCursorPosition(105, 10);
                        Console.Write("added to fleet: " + Program.manager.Fleet[selector1].Name + "...");
                        Console.CursorVisible = true;
                        Console.ReadKey(true);
                        Console.CursorVisible = false;
                    }
                    else if (buy == 1)
                        stay = true;
                }

                //makes sure nothing is left over
                Program.manager.clear(101, 4, Console.WindowWidth - 101, Console.WindowHeight - 5);
            }
            while (stay);
        }
        public void Decisions()
        {

        }
        public void turn()
        {
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].turn();
            }
        }

        //values
        public int getx()
        {
            return x;
        }
        public int gety()
        {
            return y;
        }
        public void setx(int x)
        {
            this.x = x;
        }
        public void sety(int y)
        {
            this.y = y;
        }
        public string getName()
        {
            return name;
        }
        public int getCounter()
        {
            return counter;
        }
        public bool GetAstroidBelt()
        {
            return astroidbelt;
        }
        public string GetInfo(bool onlySun = false)
        {
            try
            {
                string pla = "";
                string form = "";
                //creating string that has all planets names
                for (int i = 0; i < planets.Length; i++)
                {
                    if (planets[i].CreatureOwned)
                        form = planets[i].Life.Name;
                    pla += planets[i].TypeName;
                    if (i != planets.Length - 1)
                    {
                        pla += "\n";
                    }
                }
                //showing the suns and planets stats
                if (!onlySun)
                {
                    if (form == "")
                        return "'" + starName + "' " + name + " - home star = " + homeStar + "\n" + pla;
                    else
                        return "'" + starName + "' " + name + " - Life form = " + form + "\n" + pla;
                }
                //only showing suns stats
                else
                {
                    if (form == "")
                        return name + " - home star = " + homeStar;
                    else
                        return name + " - Life form = " + form;
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
