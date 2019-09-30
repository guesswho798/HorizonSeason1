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
        private string description;
        private bool homeStar;
        private int counter;
        private bool astroidbelt;
        private int x;
        private int y;
        Planet[] planets;

        public Star(int id, int difficulty, int numofplanets, int num, Random rand, int counter, bool homeStar = false)
        {
            this.homeStar = homeStar;
            this.counter = counter;

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
                astroidbelt = true;
            }


            //which planets
            for (int i = 0; i < planets.Length; i++)
            {
                int[] array = new int[3];
                array[0] = rand.Next(1, 11);
                array[1] = rand.Next(1, 18);
                array[2] = rand.Next(1, 5);

                planets[i] = new Planet(homeStar, array);
            }
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
        public string GetInfo()
        {
            string pla = "";
            //creating string that has all planets names
            for (int i = 0; i < planets.Length; i++)
            {
                pla += planets[i].TypeName;
                if (i != planets.Length - 1)
                {
                    pla += ", ";
                }
            }
            return name + " - home star = " + homeStar + ", anstroid belt = " + astroidbelt + "\n[" + pla + "]";
        }

        public void drawstar(int offsetx, int offsety)
        {
            double r = 0;
            int binary = 1;
            //what color
            switch (name)
            {
                case "White Sun": Console.ForegroundColor = ConsoleColor.White; r = 4; break;
                case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; r = 4; break;
                case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; r = 4; break;
                case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; r = 4; break;
                case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; r = 5; break;
                case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; r = 4; binary = 2; break;
                case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; r = 2; break;
                case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; r = 2; break;
            }

            double r_in = r - 0.0;
            double r_out = r + 0.1;

            if (binary == 1) //offsetting to the middle of not binnary
            {
                offsetx += Convert.ToInt32(r) * 2;
            }

            offsety -= Convert.ToInt32(r) * 2;

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
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void drawAstroidBelt()
        {
            int radius = 20;
            double console_ratio = Convert.ToDouble(15.0 / 3.0);
            double a = console_ratio * radius;
            double b = radius;
            Random randy = new Random();

            for (int y = -radius; y <= radius; y++)
            {
                for (double x = -a; x < a; x++)
                {
                    double d = (x / a) * (x / a) + (y / b) * (y / b);
                    if (d > 0.9 && d < 1.0 && randy.Next(0, 7) < 6)
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
        public void drawplanets(int offsetx, int offsety)
        {
            for (int j = 0; j < planets.Length; j++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Planet p = planets[j];
                Random rand = new Random();

                bool exist = true;

                double r = p.Size;
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

                if (exist)
                {
                    double r_in = r - 0.0;
                    double r_out = r + 0.1;

                    offsety -= Convert.ToInt32(r) + rand.Next(-10, 10);
                    offsetx += Convert.ToInt32(r) + rand.Next(-25, 25);

                    for (int i = 0; i < planets.Length; i++)
                    {
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
                                    if (p.TypeName == "Destroyed Planet" && rand.Next(0, 2) == 0)
                                    {
                                        Console.Write("*");
                                    }
                                    else if (p.TypeName != "Destroyed Planet")
                                    {
                                        Console.Write("*");
                                    }
                                }
                                else if (value < r_in * r_in && value < r_out * r_out)
                                {
                                    if (p.TypeName == "Destroyed Planet" && rand.Next(0, 2) == 0)
                                    {
                                        Console.Write("*");
                                    }
                                    else if (p.TypeName != "Destroyed Planet")
                                    {
                                        Console.Write("*");
                                    }
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                                counterx++;
                            }
                            countery++;
                            Console.WriteLine();
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void drawSystem(int offsetx, int offsety)
        {
            if (astroidbelt == true)
            {
                drawAstroidBelt();
            }

            drawstar(offsetx, offsety);
            drawplanets(offsetx, offsety);
        }

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
    }
}
