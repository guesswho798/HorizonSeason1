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
        private bool homeStar; //if this star is the home system
        private int counter; //?
        private bool astroidbelt; //weather the planet is soraunded by an astroid belt
        private int x; //galaxy map x
        private int y; //galaxy map y
        private int sx; //system x
        private int sy; //system y
        private int radius; //radius of star
        Planet[] planets;

        public int Radius { get => radius; set => radius = value; }
        public Planet[] Planets { get => planets; set => planets = value; }

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
                astroidbelt = false;
            }

            int[] positionstaken = new int[planets.Length];

            for (int a = 0; a < positionstaken.Length; a++)
            {
                positionstaken[a] = 999;
            }


            //which planets
            for (int i = 0; i < planets.Length; i++)
            {
                int[] array = new int[3];
                array[0] = rand.Next(1, 11);
                array[1] = rand.Next(1, 18);
                array[2] = rand.Next(2, 4);


                bool taken;
                do
                {
                    taken = false;
                    positionstaken[i] = rand.Next(0, 8);

                    for (int a = 0; a < planets.Length; a++)
                    {
                        if (positionstaken[a] == positionstaken[i] && a != i)
                        {
                            taken = true;
                        }
                    }
                }
                while (taken == true);

                planets[i] = new Planet(homeStar, array, radius, rand, positionstaken[i]);
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
                case "Red Dwarf":  radius = 3; break;
                case "White Dwarf": radius = 3; break;
            }
        }
        
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
            int[,] offsettaken = new int[2, planets.Length];
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
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void drawUI(int selected)
        {
            Planet selectedP = planets[selected];
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(100, i);
                if (i != Console.WindowHeight - 1)
                {
                    Console.WriteLine("│");
                }
                else
                {
                    Console.Write("│");
                }
            }

            Console.SetCursorPosition(105, 1);
            Console.WriteLine(selectedP.TypeName);

            string underline = "";
            for (int i = 0; i < selectedP.TypeName.Length; i++)
            {
                underline += "─";
            }
            Console.SetCursorPosition(105, 2);
            Console.WriteLine(underline);

            
            Console.SetCursorPosition(105, 4);
            if (selectedP.TypeName != "Destroyed Planet")
                Console.WriteLine("Population: " + selectedP.Pops);
            else
                Console.WriteLine("Search");


            if (selectedP.Owened)
            {
                Console.SetCursorPosition(105, 6);
                Console.WriteLine("Max population: " + selectedP.MaxPop);

                Console.SetCursorPosition(105, 8);
                Console.WriteLine("Production: None");

                Console.SetCursorPosition(105, 10);
                Console.WriteLine("Buildings: ");

                Console.SetCursorPosition(105, 12);
                Console.WriteLine("Build queue: ");

                Console.SetCursorPosition(105, 14);
                Console.WriteLine("Build");

                Console.SetCursorPosition(105, 16);
                Console.WriteLine("Decisions");
            }
            else if (selectedP.TypeName != "Destroyed Planet")
            {
                Console.SetCursorPosition(105, 6);
                Console.WriteLine("Attack");

                Console.SetCursorPosition(105, 8);
                Console.WriteLine("Trade");

                Console.SetCursorPosition(105, 10);
                Console.WriteLine("Talk");
            }
        }
        public void menuUI(int selected)
        {
            Planet selectedP = planets[selected];
            if (selectedP.TypeName == "Destroyed Planet")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(105, 4);
                Console.WriteLine("Search");

                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter)
                {
                    //choose a fleet
                    Console.WriteLine("chosen");
                    Console.ReadKey();
                }

                Console.Clear();
            }
            else if (selectedP.Owened)
            {
                int selector = 14;
                string[] options = new string[17];
                options[14] = "Build";
                options[16] = "Decisions";
                while (true)
                {
                    //drawing
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(105, selector);
                    Console.WriteLine(options[selector]);

                    //reciving input
                    ConsoleKey key = Console.ReadKey().Key;

                    //"deleting" trace
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(105, selector);
                    Console.WriteLine(options[selector]);

                    //logic
                    if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    {
                        selector += 2;
                        if (selector == 18)
                            selector = 14;
                    }
                    else if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    {
                        selector -= 2;
                        if (selector == 12)
                            selector = 18;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        //later
                        Console.WriteLine(options[selector]);
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
            else if (!selectedP.Owened)
            {
                int selector = 6;
                string[] options = new string[11];
                options[6] = "Attack";
                options[8] = "Trade";
                options[10] = "Talk";
                while (true)
                {
                    //drawing
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(105, selector);
                    Console.WriteLine(options[selector]);

                    //reciving input
                    ConsoleKey key = Console.ReadKey().Key;

                    //"deleting" trace
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(105, selector);
                    Console.WriteLine(options[selector]);

                    //logic
                    if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    {
                        selector += 2;
                        if (selector == 12)
                            selector = 6;
                    }
                    else if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    {
                        selector -= 2;
                        if (selector == 4)
                            selector = 10;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        //later
                        Console.WriteLine(options[selector]);
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }
        public void drawSystem()
        {
            rotateStar();
            int selector = 0;
            bool stay = true;
            while (stay)
            {
                //drawing
                //Console.WriteLine(GetInfo());
                if (astroidbelt)
                {
                    drawAstroidBelt();
                }
                drawstar(Console.WindowWidth / 2 - radius * 6, Console.WindowHeight / 2 - radius);
                drawplanets(selector);
                drawUI(selector);

                //reciving input
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    selector++;
                    if (selector == planets.Length)
                    {
                        selector = 0;
                    }
                }
                if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    selector--;
                    if (selector == -1)
                    {
                        selector = planets.Length - 1;
                    }
                }
                if (key == ConsoleKey.Enter)
                {
                    menuUI(selector);
                }
                if (key == ConsoleKey.Escape || key == ConsoleKey.Backspace)
                {
                    stay = false;
                }
                Console.Clear();
            }
        }
        public void rotateStar()
        {
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].MovePlanet();
            }
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
    }
}