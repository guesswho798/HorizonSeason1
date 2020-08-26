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
        //board URL = https://trello.com/b/yWm2HwiC/horizon-season-1

        public static GameManager manager;

        static void Main()
        {
            //window stuff
            Console.Title = "Horizons: Season One";
            Console.CursorVisible = false;
            Console.SetWindowSize(131, 40);

            //game manager
            manager = new GameManager();

            
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorVisible = false;
                Console.Clear();

                //generating background
                string s = "";
                Random r = new Random();
                for (int i = 0; i < 5239; i++)
                    if (r.Next(0, 40) == 0)
                        s += "*";
                    else
                        s += " ";

                //drawing
                Console.Write(s);
                setCur(0, 7);
                Console.Write(@"
                                                          *                  _,'/
       *                                                                _.-''._:
                                                                ,-:`-.-'    .:.|
                      *                                        ;-.''       .::.|
                                    *           _..------.._  / (:.       .:::.|
                                             ,'.   .. . .  .`/  : :.     .::::.|
    *                                      ,'. .    .  .   ./    \ ::. .::::::.|
                                 *       ,'. .  .    .   . /      `.,,::::::::.;\
                 *                      /  .            . /       ,',';_::::::,:_:
                                       / . .  .   .      /      ,',','::`--'':;._;
                                *     : .             . /     ,',',':::::::_:'_,'
             *                        |..  .   .   .   /    ,',','::::::_:'_,'
                                      |.              /,-. /,',':::::_:'_,'
                                      | ..    .    . /) /-:/,'::::_:',-'
                                      : . .     .   // / ,'):::_:',' ;
        *                              \ .   .     // /,' /,-.','  ./
                                        \ . .  `::./,// ,'' ,'   . /
                                         `. .   . `;;;,/_.'' . . ,'
                                          ,`. .   :;;' `:.  .  ,'
                  *                      /   `-._,'  ..  ` _.-'
                                        (     _,'``------''
                                         `--''");

                //input
                string[] options = { "Start new game", "    credits", "     exit" };
                int selector = Menu(65, 11, options, false);

                //output
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

        //easy way to get input from player
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
                    return selector;
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
                key = Console.ReadKey(true).Key;
            }
            
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


            manager.Galaxy = new Galaxy(difficulty, dense, rand);

            Thread t = new Thread(blink);
            t.IsBackground = true;
            t.Start();

            Console.Clear();

            //galaxy.info();

            Game(Console.WindowWidth / 2 - manager.Galaxy.S / 2, Console.WindowHeight - manager.Galaxy.S / 2 - 1);
        }

        public static void Game(int offsetx, int offsety)
        {
            //drawing the background and map
            loadBackground(offsetx, offsety, manager.Galaxy.Getstar().Planets.Length + 1);
            manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);

            manager.Showmove = true;
            int space = 0;
            while (true)
            {
                Console.CursorVisible = false;
                //fixing bug
                setCur(0, 0);
                Console.Write(" ");

                //showing info of system if visible
                Console.ForegroundColor = ConsoleColor.White;
                if (manager.Galaxy.Getstar() != null)
                {
                    //in here i used the .visible to make sure that if there is no star then an error whould be triggerd
                    if (manager.Galaxy.Getstar().Visible)
                    {
                        //the space is the number of planet, the star info and the line in the button so planets + 1 for the drawing and + 2 for the remove
                        space = manager.Galaxy.Getstar().Planets.Length + 1;
                        manager.clear(0, 2, 67, space + 1);
                        setCur(0, 2);
                        Console.WriteLine(manager.Galaxy.Getstar().GetInfo());
                        loadBackground(offsetx, offsety, space);
                        manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                    }
                }
                else
                {
                    manager.clear(0, 2, 67, space + 1);
                    setCur(0, 2);
                    loadBackground(offsetx, offsety);
                    manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                }

                //stoping to read input
                ConsoleKey key = Console.ReadKey(true).Key;
                
                if (key == ConsoleKey.Enter)
                {
                    if (manager.Galaxy.Getstar() != null)
                    {
                        manager.Galaxy.Getstarname(); //checking if place has a system
                        manager.Showmove = false;
                        Console.Clear();
                        manager.Galaxy.Getstar().drawSystem(); //drawing the system
                        manager.Showmove = true;
                        loadBackground(offsetx,offsety, space);
                        manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                    }
                    else
                    {
                        manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                        manager.Show = true;
                        manager.Showmove = true;
                    }
                }
                if (key == ConsoleKey.Escape)
                {
                    escape();
                    manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                }
                if (key == ConsoleKey.R)
                {
                    manager.nextRound();
                    loadBackground(offsetx, offsety, space);
                    manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                }
                if (key == ConsoleKey.T)
                {
                    techTree();
                    manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                }
                if (key == ConsoleKey.F)
                {
                    fleetManager();
                    manager.Galaxy.GetMap(manager.Fleet, offsetx, offsety);
                }
                if (key == ConsoleKey.H) //delete this later
                {
                    manager.Galaxy.Getstar().HomeStar = true;
                }

                //deleting trace
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                {
                    setCur(manager.Galaxy.Getx() + offsetx, manager.Galaxy.Gety() + offsety);
                    if (manager.Galaxy.Getstarvisible())
                    {
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
                    else { Console.Write(" "); }
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

            for (int x = 0; x < manager.Techcards.GetLength(0); x++)
            {
                for (int y = 0; y < manager.Techcards.GetLength(1); y++)
                {
                    if (manager.Techcards[x, y] == null)
                    {
                        continue;
                    }

                    if (manager.Techcards[x, y].Active)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    if (manager.Techcards[x, y].Done)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;

                    string name = manager.Techcards[x, y].Name;
                    string line = "";
                    string endline = "";
                    string bump = "";
                    for (int i = 0; i < name.Length; i++)
                    {
                        line += "─";
                    }
                    for (int i = 0; i < 16 - name.Length; i++)
                    {
                        endline += "─";
                    }
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (i != name.Length / 2 - 1)
                        {
                            bump += "─";
                        }
                        else
                        {
                            bump += "┴";
                        }
                    }
                    setCur(10 + 40 * x, 3 + y * 10);
                    if (y == 0)
                    {
                        Console.WriteLine("      ┌─" + line  + "─┐");
                    }
                    else
                    {
                        Console.WriteLine("      ┌─" + bump + "─┐");
                    }
                    setCur(10 + 40 * x, 4 + y * 10);
                    Console.WriteLine("      │ " + name + " │");
                    setCur(10 + 40 * x, 5 + y * 10);
                    Console.WriteLine("┌─────┴──" + line + "┴" + endline + "┐");
                    setCur(10 + 40 * x, 6 + y * 10);
                    Console.WriteLine("│ *                       │");
                    setCur(10 + 40 * x, 7 + y * 10);
                    Console.WriteLine("│ *                       │");
                    setCur(14 + 40 * x, 6 + y * 10);
                    Console.WriteLine(manager.Techcards[x, y].Desc1);
                    setCur(14 + 40 * x, 7 + y * 10);
                    Console.WriteLine(manager.Techcards[x, y].Desc1);
                    setCur(10 + 40 * x, 8 + y * 10);
                    Console.WriteLine("│                         │");
                    setCur(10 + 40 * x, 9 + y * 10);
                    Console.WriteLine("│  Tech Points =          │");
                    setCur(10 + 40 * x, 10 + y * 10);
                    Console.WriteLine("│  Turns =                │");
                    setCur(27 + 40 * x, 9 + y * 10);
                    Console.WriteLine(manager.Techcards[x, y].Cost);
                    setCur(21 + 40 * x, 10 + y * 10);
                    Console.WriteLine(manager.Techcards[x, y].Turns);
                    setCur(10 + 40 * x, 11 + y * 10);
                    if (y + 1 < manager.Techcards.GetLength(1) && manager.Techcards[x, y + 1] != null)
                    {
                        string linenext = "";
                        string endlinenext = "";
                        for (int i = 0; i < manager.Techcards[x, y + 1].Name.Length / 2; i++)
                        {
                            linenext += "─";
                        }
                        for (int i = 0; i < 18 - manager.Techcards[x, y + 1].Name.Length / 2; i++)
                        {
                            endlinenext += "─";
                        }
                        Console.WriteLine("└──────" + linenext + "┬" + endlinenext + "┘");
                        setCur(17 + manager.Techcards[x, y + 1].Name.Length / 2 + 40 * x, 12 + y * 10);
                        Console.WriteLine("│");
                    }
                    else
                    {
                        setCur(10 + 40 * x, 11 + y * 10);
                        Console.WriteLine("└─────────────────────────┘");
                    }
                }
            }

            //Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
            setCur(18, 4);
            Console.WriteLine(manager.Techcards[0, 0].Name);
            int cx = 0;
            int cy = 0;
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                // Console.BackgroundColor = ConsoleColor.Black;
                if (manager.Techcards[cx, cy].Active)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                if (manager.Techcards[cx, cy].Done)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                setCur(18 + cx * 40, 4 + cy * 10);
                Console.WriteLine(manager.Techcards[cx, cy].Name);

                if (key == ConsoleKey.Escape)
                {
                    break;
                }
                if (key == ConsoleKey.Enter && !manager.Techcards[cx, cy].Active && !manager.Techcards[cx, cy].Done && manager.Tech >= manager.Techcards[cx, cy].Cost)
                {
                    bool available = true;
                    for (int i = 0; i < manager.Techcards.GetLength(0); i++)
                    {
                        for (int j = 0; j < manager.Techcards.GetLength(1); j++)
                        {
                            if (manager.Techcards[i, j] != null && manager.Techcards[i, j].Active)
                            {
                                available = false;
                                break;
                            }
                        }
                    }

                    for (int i = 1; i < 5; i++)
                    {
                        if (cy - i > -1 && manager.Techcards[cx, cy - i] != null && !manager.Techcards[cx, cy - i].Active)
                        {
                            available = false;
                            break;
                        }
                    }
                    if (available)
                    {
                        manager.Tech -= manager.Techcards[cx, cy].Cost;
                        manager.Techcards[cx, cy].Active = true;
                        break;
                    }
                }
                if (key == ConsoleKey.DownArrow || key ==  ConsoleKey.S)
                {
                    if (cy + 1 < manager.Techcards.GetLength(1) && manager.Techcards[cx, cy+1] != null)
                    {
                        cy++;
                    }
                }
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    if (cy - 1 > -1 && manager.Techcards[cx, cy-1] != null)
                    {
                        cy--;
                    }
                }
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    if (cx + 1 < manager.Techcards.GetLength(0) && manager.Techcards[cx+1, cy] != null)
                    {
                        cx++;
                    }
                }
                if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    if (cx - 1 > -1 && manager.Techcards[cx-1, cy] != null)
                    {
                        cx--;
                    }
                }

                //Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                setCur(18 + cx * 40, 4 + cy * 10);
                Console.WriteLine(manager.Techcards[cx, cy].Name);
            }


            Console.Clear();
            manager.Showmove = true;
        }

        public static void fleetManager()
        {

            Console.Clear();
            manager.Showmove = false;

            int numOfFleets = 0;

            fleet[] f = manager.Fleet;

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] != null)
                {
                    f[i].GetInfo();
                    numOfFleets++;
                }
            }

            setCur(Console.WindowWidth / 2, Console.WindowHeight / 2);

            if (numOfFleets == 0)
            {
                Console.CursorVisible = true;
                Console.WriteLine("No Fleets...");
                Console.CursorVisible = false;
                return;
            }

            //int i is used to go thrue all the fleets and counter is used to
            //count the usable fleets to find which one is the last one that is an actual fleet
            int counter = 1;

            //title
            setCur(Console.WindowWidth / 2 - 20, 6);
            Console.Write("┌────────┬───────────────────────────────────────┬────────┬────────┐");
            setCur(Console.WindowWidth / 2 - 20, 7);
            Console.Write("│  Name  │                  Ships                │ (x,y)  │  ETA   │");
            setCur(Console.WindowWidth / 2 - 20, 8);
            Console.Write("├────────┼───────────────────────────────────────┼────────┼────────┤");


            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] != null)
                {
                    //calculating the space between each │ in the table
                    string sn = ""; //space name
                    for (int j = 0; j < 8 - f[i].Name.Length; j++) { sn += " "; }

                    setCur(Console.WindowWidth / 2 - 20, 6 + (i + 1) * 2);
                    Console.Write("├────────┼───────────────────────────────────────┼────────┼────────┤");
                    setCur(Console.WindowWidth / 2 - 20, 7 + (i + 1) * 2);
                    Console.Write("│" + f[i].Name + sn + "│" + f[i].getInfoString());
                    setCur(Console.WindowWidth / 2 + 29, 7 + (i + 1) * 2);
                    Console.Write("│ (" + f[i].X + "," + f[i].Y + ")");
                    setCur(Console.WindowWidth / 2 + 38, 7 + (i + 1) * 2);
                    Console.Write("│  " + f[i].Eta + " / R");
                    setCur(Console.WindowWidth / 2 + 47, 7 + (i + 1) * 2);
                    Console.Write("│");
                    setCur(Console.WindowWidth / 2 - 20, 8 + (i + 1) * 2);

                    //not the last one
                    if (counter != numOfFleets)
                    {
                        Console.Write("├────────┼───────────────────────────────────────┼────────┼────────┤");
                    }
                    //the last one
                    else
                    {
                        Console.Write("└────────┴───────────────────────────────────────┴────────┴────────┘");
                    }

                    counter++;
                }
            }

            Console.ReadKey();
            Console.Clear();
            manager.Showmove = true;
        }
        public static void fleetPicker(int x, int y, Planet p)
        {
            Console.Clear();
            manager.Showmove = false;

            int numOfFleets = 0;

            fleet[] f = manager.Fleet;

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] != null)
                {
                    f[i].calcETA(x, y);
                    numOfFleets++;
                }
            }


            if (numOfFleets == 0)
            {
                Console.CursorVisible = true;
                setCur(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("No Fleets...");
                Console.CursorVisible = false;
                return;
            }
            int selector = 1;
            while (true)
            {
                Console.Clear();

                //int i is used to go thrue all the fleets and counter is used to
                //count the usable fleets to find which one is the last one that is an actual fleet
                int counter = 1;

                //title
                setCur(Console.WindowWidth / 2 - 20, 6);
                Console.Write("┌────────┬───────────────────────────────────────┬───────┬───────┐");
                setCur(Console.WindowWidth / 2 - 20, 7);
                Console.Write("│  Name  │                  Ships                │ speed │  ETA  │");
                setCur(Console.WindowWidth / 2 - 20, 8);
                Console.Write("├────────┼───────────────────────────────────────┼───────┼───────┤");


                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i] != null)
                    {
                        //calculating the space between each │ in the table
                        string sn1 = ""; //space name
                        string sn2 = ""; //space ships
                        for (int j = 0; j < 8 - f[i].Name.Length; j++) { sn1 += " "; }
                        for (int j = 0; j < 39 - f[i].getInfoString().Length; j++) { sn2 += " "; }

                        setCur(Console.WindowWidth / 2 - 20, 6 + (i + 1) * 2);
                        Console.Write("├────────┼───────────────────────────────────────┼───────┼───────┤");
                        setCur(Console.WindowWidth / 2 - 20, 7 + (i + 1) * 2);
                        Console.Write("│                                                                │");

                        if (selector == counter)
                            Console.BackgroundColor = ConsoleColor.Red;
                        setCur(Console.WindowWidth / 2 - 19, 7 + (i + 1) * 2);
                        Console.Write(f[i].Name + sn1);
                        Console.BackgroundColor = ConsoleColor.Black;

                        Console.Write("│" + f[i].getInfoString() + sn2 + "│" + "   " + f[i].getSpeed() + "   │" + f[i].calcETA(x, y) + " / R");

                        setCur(Console.WindowWidth / 2 - 20, 8 + (i + 1) * 2);

                        //not the last one
                        if (counter != numOfFleets)
                        {
                            Console.Write("├────────┼───────────────────────────────────────┼───────┼───────┤");
                        }
                        //the last one
                        else
                        {
                            Console.Write("└────────┴───────────────────────────────────────┴───────┴───────┘");
                        }

                        counter++;
                    }
                }

                //input
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    selector++;
                    if (selector == counter)
                    {
                        selector = 1;
                    }
                }
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    selector--;
                    if (selector == 0)
                    {
                        selector = counter - 1;
                    }
                }
                if (key == ConsoleKey.Escape)
                {
                    break;
                }
                if (key == ConsoleKey.Enter)
                {
                    selector--;
                    //x and y to calc distance, true to lunch and p is the target planet
                    f[selector].calcETA(x, y, true, p);
                    break;
                }
            }

            Console.Clear();
            manager.Showmove = true;
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

            string[] options = {"Back", "Credits", "Options", "Exit"};
            switch (Menu(62, 11, options, false, true))
            {
                case 1:
                    Credits();
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("Coming soon...");
                    Console.CursorVisible = true;
                    Console.ReadKey();
                    Console.CursorVisible = false;
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
            #region square around the galaxy
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

            #region next round, tech and fleet manager buttons

            //left side
            setCur(0, Console.WindowHeight - 6);
            Console.Write("───────────┐");
            setCur(0, Console.WindowHeight - 5);
            Console.Write(" Tech tree │");
            setCur(0, Console.WindowHeight - 4);
            Console.Write("  Press T  │");
            setCur(0, Console.WindowHeight - 3);
            Console.Write("───────────┴───┐");
            setCur(0, Console.WindowHeight - 2);
            Console.Write(" Fleet manager │");
            setCur(0, Console.WindowHeight - 1);
            Console.Write("    Press F    │");

            //right side
            setCur(Console.WindowWidth - 13, Console.WindowHeight - 5);
            Console.Write("┌────────────");
            setCur(Console.WindowWidth - 13, Console.WindowHeight - 4);
            Console.Write("│ Round = " + manager.Round);
            setCur(Console.WindowWidth - 15, Console.WindowHeight - 3);
            Console.Write("┌─┴────────────");
            setCur(Console.WindowWidth - 15, Console.WindowHeight - 2);
            Console.Write("│ Next round");
            setCur(Console.WindowWidth - 15, Console.WindowHeight - 1);
            Console.Write("│  Press R");
            #endregion

            #region top resources info
            setCur(3, 0);
            Console.WriteLine("Metals: " + manager.Metals + "   Energy: " + manager.Energy + "   Food: " + manager.Food + "   Happiness:  " + manager.Happiness + "   Tech:  " + manager.Tech);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }

            setCur(80, 1);
            Console.Write("┴");
            setCur(80, 0);
            Console.Write("│");
            #endregion

            #region situation report
            setCur(90, 1);
            Console.Write("┬");
            for (int i = 0; i < 13; i++)
            {
                setCur(90, 2 + i);
                Console.Write("│");
            }
            setCur(96, 3);
            Console.WriteLine("Situation Report");
            setCur(96, 4);
            Console.WriteLine("────────────────");

            setCur(90, 15);
            Console.WriteLine("└────────────────────────────────────────");
            #endregion

            #region hovered system info
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
                    if (manager.Galaxy.Getstarvisible())
                    {
                        switch (manager.Galaxy.Getstarname())
                        {
                            case "White Sun": Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("*"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("*"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("*"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("*"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("*"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write(":"); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("."); Console.BackgroundColor = ConsoleColor.Black; break;
                            case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write("."); Console.BackgroundColor = ConsoleColor.Black; break;
                            default: Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Write(" "); break;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        manager.Show = false;
                    }
                    else
                    {
                        Console.Write(" ");
                    }
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
