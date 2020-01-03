using System;

namespace HorizonSeason1
{
    public class Galaxy
    {
        private Star[,] stars;
        private int y = 15;
        private int x = 30;
        private int cx;
        private int cy;
        private int s;
        private int numOfStars;
        private Star[] xy;

        public int S { get => s; set => s = value; }

        public Galaxy(int difficulty, int size, int dense, Random rand)
        {
            switch (dense)
            {
                case 0:
                    if (size == 0)
                    {
                        this.y = 10;
                        this.x = 15;
                    }
                    else if (size == 1)
                    {
                        this.y = 15;
                        this.x = 20;
                    }
                    else if (size == 2)
                    {
                        this.y = 20;
                        this.x = 25;
                    }
                    break;
                case 1:
                    if (size == 0)
                    {
                        this.y = 15;
                        this.x = 20;
                    }
                    else if (size == 1)
                    {
                        this.y = 20;
                        this.x = 25;
                    }
                    else if (size == 2)
                    {
                        this.y = 25;
                        this.x = 30;
                    }
                    break;
                case 2:
                    if (size == 0)
                    {
                        this.y = 20;
                        this.x = 25;
                    }
                    else if (size == 1)
                    {
                        this.y = 25;
                        this.x = 30;
                    }
                    else if (size == 2)
                    {
                        this.y = 30;
                        this.x = 35;
                    }
                    break;
            }

            if (size == 0)
            {
                size = 30;
            }
            else if (size == 1)
            {
                size = 45;
            }
            else if (size == 2)
            {
                size = 60;
            }
            else
            {
                size = 45;
            }
            this.s = size;

            stars = new Star[size, size / 2];

            int numOfStars = rand.Next(y, x);
            this.numOfStars = numOfStars;
            this.xy = new Star[numOfStars];
            int homeStar = rand.Next(2, numOfStars - 1);

            Console.WriteLine("max: " + numOfStars);
            Console.WriteLine("home: " + homeStar);

            int counter = 0;
            while (counter <= numOfStars)
            {
                for (int y = 0; y < size / 2; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        //making sure im not placing 2 planets on each pther
                        try
                        {
                            stars[x, y].getName();
                        }
                        catch
                        {
                            if (rand.Next(0, 100) == 0)
                            {
                                counter++;
                                if (counter != homeStar)
                                {
                                    stars[x, y] = new Star(rand.Next(0, 101), difficulty, dense, rand.Next(0, 101), rand, counter);
                                }
                                else
                                {
                                    stars[x, y] = new Star(rand.Next(0, 101), difficulty, dense, rand.Next(0, 101), rand, counter, true);
                                    //starting point wil be home planet
                                    cx = x;
                                    cy = y;
                                }
                            }
                        }
                        if (counter > numOfStars)
                        {
                            break;
                        }
                    }
                }
            }

            //inserting all stars into an easier and readable map
            int counter1 = 0;
            for (int y = 0; y < s / 2; y++)
            {
                for (int x = 0; x < s; x++)
                {
                    try
                    {
                        stars[x, y].getName();
                        xy[counter1] = stars[x, y];
                        xy[counter1].setx(x);
                        xy[counter1].sety(y);
                        counter1++;
                    }
                    catch
                    { }
                }
            }
        }

        public void info()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < numOfStars; i++)
            {
                Console.WriteLine($"{i}: ({xy[i].getx()},{xy[i].gety()}). {xy[i].GetInfo()}");
            }

        }

        public void GetMap(int offsetx = 0, int offsety = 0)
        {
            for (int i = 0; i < numOfStars; i++)
            {
                Console.SetCursorPosition(xy[i].getx() + offsetx, xy[i].gety() + offsety);

                string name = stars[xy[i].getx(), xy[i].gety()].getName();
                switch (name)
                {
                    case "White Sun": Console.ForegroundColor = ConsoleColor.White; Console.Write("*"); break;
                    case "Yellow Sun": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("*"); break;
                    case "Blue Sun": Console.ForegroundColor = ConsoleColor.Blue; Console.Write("*"); break;
                    case "Protostar": Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("*"); break;
                    case "Red Supergiant": Console.ForegroundColor = ConsoleColor.Red; Console.Write("*"); break;
                    case "Binary": Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(":"); break;
                    case "Red Dwarf": Console.ForegroundColor = ConsoleColor.Red; Console.Write("."); break;
                    case "White Dwarf": Console.ForegroundColor = ConsoleColor.White; Console.Write("."); break;
                    default: Console.Write("ERROR " + name); break;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public int Getx()
        {
            return cx;
        }
        public int Gety()
        {
            return cy;
        }
        public void Setx(int x)
        {
            cx = x;
        }
        public void Sety(int y)
        {
            cy = y;
        }

        public string Getstarname()
        {
            try
            {
                return stars[cx, cy].getName();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public Star Getstar()
        {
            try
            {
                return stars[cx, cy];
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
