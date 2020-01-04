using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HorizonSeason1
{
    class Program
    {
        //there is a map for the stars but every other thing just has its own x and y just so things could go on each other

            
        /// <summary>
        /// to do list
        /// constroctors
        /// LoadGame func
        /// </summary>
        /// <param name="args"></param>

        ///ideas:
        ///pops adds 0.something every cycle and will cost food per billion and you can stop births at 2 per family --> happiness down but growth stops
        ///

        //prices is an int array [Metals, Energy, Pops, Food, Happiness]
        //life form's traits go in an int array [] (add when you know)

        //Resources
        public static int Metals;
        public static int Energy;
        public static int Food;
        public static int Pops;
        public static int Happiness;

        public static fleet[] Fleet = new fleet[10];
        public static Galaxy galaxy;

        public static bool showmove = false;
        public static bool show = true;

        static void Main()
        {
            Console.Title = "Horizons: Season One";
            Console.CursorVisible = false;
            Console.SetWindowSize(131, 40);
            
            //start of program
            string[] options = { "Start new game", "    credits", "     exit" };
            
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.CursorVisible = false;
                Console.Clear();

                Console.WriteLine(@"
                                                       _,'/
       *                           *              _.-''._:
                      *                   ,-:`-.-'    .:.|   *        *
   *                                     ;-.''       .::.|                    
                          _..------.._  / (:.       .:::.|          *
                       ,'.   .. . .  .`/  : :.     .::::.|    * 
                     ,'. .    .  .   ./    \ ::. .::::::.|
 *          *      ,'. .  .    .   . /      `.,,::::::::.;\       *
                  /  .            . /       ,',';_::::::,:_:
                 / . .  .   .      /      ,',','::`--'':;._;      *
        *       : .             . /     ,',',':::::::_:'_,'
                |..  .   .   .   /    ,',','::::::_:'_,'          *
                |.              /,-. /,',':::::_:'_,'                *
                | ..    .    . /) /-:/,'::::_:',-'          *
         *      : . .     .   // / ,'):::_:',' ;
                 \ .   .     // /,' /,-.','  ./   *              *
                  \ . .  `::./,// ,'' ,'   . /
                   `. .   . `;;;,/_.'' . . ,'            *          
          *         ,`. .   :;;' `:.  .  ,'          *
                  /   `-._,'  ..  ` _.-'
                  (     _,'``------''       *                    *
                   `--''");

                int selector = Menu(41, 7, options, false);

                switch (selector)
                {
                    case 0:
                        LoadGame();
                        break;
                    case 1:
                        Credits();
                        break;
                    case 2:
                        System.Environment.Exit(1);
                        break;
                }
            }
        }

        public static int Menu(int x, int y, string[] options, bool fullscreen = true, bool blue = false)
        {
            int max = options.Max(w => w.Length);
            int selector = 0;
            ConsoleKey key = new ConsoleKey();

            if (fullscreen)
            {
                max = Console.WindowWidth - (x * 2 + 1);
            }

            //getting lines down after lines 
            for (int i = 0; i < options.Length - 1; i++)
            {
                options[i] += "\n";
            }

            while (true)
            {
                //deleting only what is nesesery
                for (int i = 0; i < options.Length; i++)
                {
                    for (int j = 0; j < max; j++)
                    {
                        setCur(j + x, i + y);
                        Console.Write(" ");
                    }
                }

                //logic
                if (key == ConsoleKey.DownArrow)
                {
                    selector++;
                    if (selector >= options.Length)
                    {
                        selector = 0;
                    }
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selector--;
                    if (selector < 0)
                    {
                        selector = options.Length - 1;
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }

                //writing options
                for (int i = 0; i < options.Length; i++)
                {
                    if (selector == i)
                    {
                        if (blue)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    setCur(x, y + i);
                    Console.Write(options[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //getting input
                key = Console.ReadKey(false).Key;
            }
            return selector;
        }

        public static void Credits()
        {
            Console.Clear();

            Console.WriteLine(@"
            ************************************
            *           credits:               *
            *                                  *
            *      Raz Shneider -              *
            *      Aden Prescot -              *
            *                                  *
            ************************************
              press any button to continue...");


            Console.ReadKey();
        }

        public static void LoadGame()
        {
            Console.Clear();

            string[] diff = { "1.easy (young galaxy)", "2.medium (normal galaxy)", "3.hard (old galaxy)" };
            int width = Console.WindowWidth / 2 - diff[0].Length / 2;
            Console.WriteLine("\n\n                                 game difficulty:");
            int difficulty = Menu(width, 3, diff, false);

            Console.Clear();

            string[] mapsize = {"1.small", "2.normal", "3.big" };
            Console.WriteLine("\n\n                              size of map:");
            int size = Menu(width, 3, mapsize);

            Console.Clear();

            string[] mapdensity = { "1.spacious", "2.normal", "3.dense" };
            Console.WriteLine("\n\n                              density of galaxy:");
            int dense = Menu(width, 3, mapdensity);

            Random rand = new Random();
            Console.Clear();

            Console.WriteLine("\n\nchoose a life form:");
            string[] options = { "Humanoid", "Deouring Swarm", "Mammalian" };

            LifeForm life = new LifeForm(Menu(0, 3, options));

            Console.Clear();
            Console.WriteLine("creating a new galaxy...\n");

            

            galaxy = new Galaxy(difficulty, size, dense, rand);

            Thread t = new Thread(blink);
            t.IsBackground = true;
            t.Start();

            Console.Clear();

            galaxy.info();

            Game(Console.WindowWidth / 2 - galaxy.S / 2, Console.WindowHeight - galaxy.S / 2 - 1);
        }

        public static void Game(int offsetx, int offsety)
        {
            galaxy.GetMap(offsetx, offsety);

            showmove = true;

            while (true)
            {
                Console.CursorVisible = false;
                //fixing bug
                setCur(0, 0);
                Console.Write(" ");

                //stoping to read input
                ConsoleKey key = Console.ReadKey(false).Key;
                
                if (key == ConsoleKey.Enter)
                {
                    try
                    {
                        galaxy.Getstarname(); //checking if place has a system
                        showmove = false;
                        Console.Clear();
                        galaxy.Getstar().drawSystem(); //drawing the system
                        showmove = true;
                        loadBackground();
                        galaxy.GetMap(offsetx, offsety);
                    }
                    catch
                    {
                        galaxy.GetMap(offsetx, offsety);
                        show = true;
                        showmove = true;
                    }
                }
                if (key == ConsoleKey.Escape)
                {
                    escape();
                    galaxy.GetMap(offsetx, offsety);
                }

                //deleting trace
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                {
                    //deleting trace
                    setCur(galaxy.Getx() + offsetx, galaxy.Gety() + offsety);
                    switch (galaxy.Getstarname())
                    {
                        case "White Sun": Console.ForegroundColor = ConsoleColor.White; Console.Write("*"); break;
                        case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("*"); break;
                        case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; Console.Write("*"); break;
                        case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("*"); break;
                        case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; Console.Write("*"); break;
                        case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(":"); break;
                        case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; Console.Write("."); break;
                        case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; Console.Write("."); break;
                        default: Console.Write(" "); break;
                    }
                }

                //moving player
                switch (key)
                {
                    case ConsoleKey.UpArrow: if (galaxy.Gety() > 0) galaxy.Sety(galaxy.Gety() - 1); break;
                    case ConsoleKey.DownArrow: if (galaxy.Gety() < galaxy.S / 2 - 1) galaxy.Sety(galaxy.Gety() + 1); break;
                    case ConsoleKey.LeftArrow: if (galaxy.Getx() > 0) galaxy.Setx(galaxy.Getx() - 1); break;
                    case ConsoleKey.RightArrow: if (galaxy.Getx() < galaxy.S - 1) galaxy.Setx(galaxy.Getx() + 1); break;
                }


                //drawing the player
                if (show == true)
                {
                    setCur(galaxy.Getx() + offsetx, galaxy.Gety() + offsety);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("█");
                }
            }
        }

        public static void escape()
        {
            Console.Clear();
            showmove = false;
            Console.Write(" ");
            setCur(0, 5);
            Console.WriteLine(@"
                                        ┌────────────────────────────────────────────────────┐
                                        │                     escape                         │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        │                                                    │
                                        └────────────────────────────────────────────────────┘");

            string[] options = {"Back","Credits" , "Options", "Exit"};
            switch (Menu(62, 11, options, false, true))
            {
                case 2:
                    Credits();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
            showmove = true;
            Console.Clear();
        }

        public static void loadBackground()
        {

        }

        static void blink()
        {
            int offsetx = Console.WindowWidth / 2 - galaxy.S / 2;
            int offsety = Console.WindowHeight - galaxy.S / 2 - 1;

            while (true)
            {
                if (showmove) //making sure it wont blink in options
                {
                    setCur(galaxy.Getx() + offsetx, galaxy.Gety() + offsety);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("█");
                    show = true;
                    setCur(50, 0);
                }

                Thread.Sleep(1000);

                if (showmove) //making sure it wont blink in options
                {
                    setCur(galaxy.Getx() + offsetx, galaxy.Gety() + offsety);
                    switch (galaxy.Getstarname())
                    {
                        case "White Sun": Console.ForegroundColor = ConsoleColor.White; Console.Write("*"); break;
                        case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("*"); break;
                        case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; Console.Write("*"); break;
                        case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("*"); break;
                        case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; Console.Write("*"); break;
                        case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(":"); break;
                        case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; Console.Write("."); break;
                        case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; Console.Write("."); break;
                        default: Console.Write(" "); break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    show = false;

                    setCur(50, 0);
                }
                Thread.Sleep(500);
            }
        }

        public static void setCur(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
