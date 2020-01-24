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
        

        ///ideas:
        ///pops adds 0.something every cycle and will cost food per billion and you can stop births at 2 per family --> happiness down but growth stops
        ///

        public static GameManager manager;

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
            *      Discord server              *
            ************************************
              press any button to continue...");


            Console.ReadKey();
        }

        public static void LoadGame()
        {
            Console.Clear();

            manager = new GameManager();

            string[] diff = { "1.easy (young galaxy)", "2.medium (normal galaxy)", "3.hard (old galaxy)" };
            int width = Console.WindowWidth / 2 - diff[0].Length / 2;
            Console.WriteLine("\n\n                                                       game difficulty:");
            int difficulty = Menu(width, 3, diff, false);

            Console.Clear();

            string[] mapsize = {"1.small", "2.normal", "3.big" };
            Console.WriteLine("\n\n                                                       size of map:");
            int size = Menu(width, 3, mapsize);
            size = 1;
            Console.Clear();

            string[] mapdensity = { "1.spacious", "2.normal", "3.dense" };
            Console.WriteLine("\n\n                                                       density of galaxy:");
            int dense = Menu(width, 3, mapdensity);

            Random rand = new Random();
            Console.Clear();

            Console.WriteLine("\n\n                                                       choose a life form:");
            string[] options = { "Humanoid", "Deouring Swarm", "Mammalian" };

            LifeForm life = new LifeForm(Menu(width, 3, options));

            Console.Clear();
            Console.WriteLine("creating a new galaxy...\n");


            manager.Galaxy = new Galaxy(difficulty, size, dense, rand);

            Thread t = new Thread(blink);
            t.IsBackground = true;
            t.Start();

            Console.Clear();

            //galaxy.info();

            Game(Console.WindowWidth / 2 - manager.Galaxy.S / 2, Console.WindowHeight - manager.Galaxy.S / 2 - 1);
        }

        public static void Game(int offsetx, int offsety)
        {
            loadBackground(offsetx, offsety, manager.Galaxy.Getstar().Planets.Length + 1);
            manager.Galaxy.GetMap(offsetx, offsety);

            manager.Showmove = true;
            int space = 0;
            while (true)
            {
                Console.CursorVisible = false;
                //fixing bug
                setCur(0, 0);
                Console.Write(" ");

                //showing info of system
                Console.ForegroundColor = ConsoleColor.White;
                try 
                {
                    //the space is the number of planet, the star info and the line in the button so planets + 1 for the drawing and + 2 for the remove
                    space = manager.Galaxy.Getstar().Planets.Length + 1;
                    manager.clear(0, 2, 67, space + 1);
                    setCur(0, 2);
                    Console.WriteLine(manager.Galaxy.Getstar().GetInfo());
                    loadBackground(offsetx, offsety, space);
                    manager.Galaxy.GetMap(offsetx, offsety);
                } 
                catch 
                {
                    manager.clear(0, 2, 67, space + 1);
                    setCur(0, 2);
                    loadBackground(offsetx, offsety);
                    manager.Galaxy.GetMap(offsetx, offsety);
                }

                //stoping to read input
                ConsoleKey key = Console.ReadKey(false).Key;
                
                if (key == ConsoleKey.Enter)
                {
                    try
                    {
                        manager.Galaxy.Getstarname(); //checking if place has a system
                        manager.Showmove = false;
                        Console.Clear();
                        manager.Galaxy.Getstar().drawSystem(); //drawing the system
                        manager.Showmove = true;
                        loadBackground(offsetx,offsety, space);
                        manager.Galaxy.GetMap(offsetx, offsety);
                    }
                    catch
                    {
                        manager.Galaxy.GetMap(offsetx, offsety);
                        manager.Show = true;
                        manager.Showmove = true;
                    }
                }
                //menu
                if (key == ConsoleKey.Escape)
                {
                    escape();
                    manager.Galaxy.GetMap(offsetx, offsety);
                }
                //next round
                if (key == ConsoleKey.R)
                {
                    manager.nextRound();
                    loadBackground(offsetx, offsety, space);
                    manager.Galaxy.GetMap(offsetx, offsety);
                }
                //tech tree
                if (key == ConsoleKey.T)
                {
                    techTree();
                    manager.Galaxy.GetMap(offsetx, offsety);
                }

                //deleting trace
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                {
                    //deleting trace
                    setCur(manager.Galaxy.Getx() + offsetx, manager.Galaxy.Gety() + offsety);
                    switch (manager.Galaxy.Getstarname())
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
                    case ConsoleKey.UpArrow: if (manager.Galaxy.Gety() > 0) manager.Galaxy.Sety(manager.Galaxy.Gety() - 1); break;
                    case ConsoleKey.DownArrow: if (manager.Galaxy.Gety() < manager.Galaxy.S / 2 - 1) manager.Galaxy.Sety(manager.Galaxy.Gety() + 1); break;
                    case ConsoleKey.LeftArrow: if (manager.Galaxy.Getx() > 0) manager.Galaxy.Setx(manager.Galaxy.Getx() - 1); break;
                    case ConsoleKey.RightArrow: if (manager.Galaxy.Getx() < manager.Galaxy.S - 1) manager.Galaxy.Setx(manager.Galaxy.Getx() + 1); break;
                }

                //drawing the player
                if (manager.Show == true)
                {
                    setCur(manager.Galaxy.Getx() + offsetx, manager.Galaxy.Gety() + offsety);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("█");
                }
            }
        }

        public static void techTree()
        {
            Console.Clear();
            manager.Showmove = false;


            Console.WriteLine("in tech tree");



            manager.Showmove = true;
            Console.Clear();
            Console.ReadKey();
        }

        public static void escape()
        {
            Console.Clear();
            manager.Showmove = false;
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
            manager.Showmove = true;
            Console.Clear();
        }

        public static void loadBackground(int offsetx, int offsety, int planetsToShow = 0)
        {
            #region square to galaxy
            setCur(offsetx - 1, offsety - 1);
            string line = "┌";
            for (int i = 0; i < manager.Galaxy.S; i++)
            {
                line += "─";
            }
            line += "┐";
            Console.Write(line);

            line = "│";
            for (int j = 0; j < manager.Galaxy.S; j++)
            {
                line += " ";
            }
            line += "│";

            for (int i = 0; i < manager.Galaxy.S / 2; i++)
            {
                setCur(offsetx - 1, offsety + i);
                Console.Write(line);
            }

            setCur(offsetx - 1, Console.WindowHeight - 1);
            line = "└";
            for (int i = 0; i < manager.Galaxy.S; i++)
            {
                line += "─";
            }
            line += "┘";
            Console.Write(line);
            #endregion

            #region next round and tech buttons

            Console.SetCursorPosition(Console.WindowWidth - 13, Console.WindowHeight - 8);
            Console.Write("┌────────────");
            Console.SetCursorPosition(Console.WindowWidth - 13, Console.WindowHeight - 7);
            Console.Write("│ tech tree");
            Console.SetCursorPosition(Console.WindowWidth - 13, Console.WindowHeight - 6);
            Console.Write("│  press T");
            Console.SetCursorPosition(Console.WindowWidth - 13, Console.WindowHeight - 5);
            Console.Write("├────────────");
            Console.SetCursorPosition(Console.WindowWidth - 13, Console.WindowHeight - 4);
            Console.Write("│ round = " + manager.Round);
            Console.SetCursorPosition(Console.WindowWidth - 15, Console.WindowHeight - 3);
            Console.Write("┌─┴────────────");
            Console.SetCursorPosition(Console.WindowWidth - 15, Console.WindowHeight - 2);
            Console.Write("│ next round");
            Console.SetCursorPosition(Console.WindowWidth - 15, Console.WindowHeight - 1);
            Console.Write("│  press R");
            #endregion

            #region top resources info
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Metals: " + manager.Metals + "   Energy: " + manager.Energy + "   Food: " + manager.Food + "   Happiness:  " + manager.Happiness + "   Tech:  " + manager.Tech);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }

            Console.SetCursorPosition(80, 1);
            Console.Write("┴");
            Console.SetCursorPosition(80, 0);
            Console.Write("│");
            #endregion

            #region round info
            Console.SetCursorPosition(90, 1);
            Console.Write("┬");
            for (int i = 0; i < 13; i++)
            {
                Console.SetCursorPosition(90, 2 + i);
                Console.Write("│");
            }
            Console.SetCursorPosition(96, 3);
            Console.WriteLine("Situation Report");
            Console.SetCursorPosition(96, 4);
            Console.WriteLine("────────────────");

            Console.SetCursorPosition(90, 15);
            Console.WriteLine("└────────────────────────────────────────");
            #endregion

            #region galaxy info
            if (planetsToShow != 0)
            {
                

                setCur(66, 1);
                Console.Write("┬");
                for (int i = 0; i < planetsToShow; i++)
                {
                    setCur(66, 2 + i);
                    Console.Write("│");
                }

                setCur(0, 2 + planetsToShow);
                Console.Write("──────────────────────────────────────────────────────────────────┘");
                
            }
            #endregion
        }

        static void blink()
        {
            int offsetx = Console.WindowWidth / 2 - manager.Galaxy.S / 2;
            int offsety = Console.WindowHeight - manager.Galaxy.S / 2 - 1;

            while (true)
            {
                if (manager.Showmove) //making sure it wont blink in options
                {
                    setCur(manager.Galaxy.Getx() + offsetx, manager.Galaxy.Gety() + offsety);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("█");
                    manager.Show = true;
                    setCur(50, 0);
                }

                Thread.Sleep(1000);

                if (manager.Showmove) //making sure it wont blink in options
                {
                    setCur(manager.Galaxy.Getx() + offsetx, manager.Galaxy.Gety() + offsety);
                    switch (manager.Galaxy.Getstarname())
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
                    manager.Show = false;

                    setCur(50, 0);
                }
                Thread.Sleep(500);

                //just to stop the function from running all the time
                while (!manager.Showmove)
                {

                }
            }
        }

        public static void setCur(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
