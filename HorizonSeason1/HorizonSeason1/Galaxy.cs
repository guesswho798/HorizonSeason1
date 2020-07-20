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
        private LifeForm[] lifeForms;
        private string[] names = new string[50];

        public int S { get => s; set => s = value; }
        public LifeForm[] LifeForms { get => lifeForms; set => lifeForms = value; }
        public Star[] Xy { get => xy; set => xy = value; }

        public Galaxy(int difficulty, int dense, Random rand)
        {
            #region making names for stars
            names[0] = "Zura";
            names[1] = "Lightaiat";
            names[2] = "Mudwauy";
            names[3] = "Osca";
            names[4] = "Lema";
            names[5] = "Idrioy";
            names[6] = "Etrol";
            names[7] = "Kood";
            names[8] = "Neekloys";
            names[9] = "Chugrae";
            names[10] = "Traevi";
            names[11] = "Qofaecs";
            names[12] = "Ucleeddem";
            names[13] = "Vian";
            names[14] = "Phallitronium";
            names[15] = "Pyozus";
            names[16] = "Bluening";
            names[17] = "Panogonal";
            names[18] = "Corpumodo";
            names[19] = "Airemaximus";
            names[20] = "Bygiecane";
            names[21] = "Bygietrooper";
            names[22] = "Viamesis";
            names[23] = "Wipolic";
            names[24] = "Myameda";
            names[25] = "Drefthya";
            names[26] = "Moonrani";
            names[27] = "Koarature";
            names[28] = "Brahtra";
            names[29] = "Panginal";
            names[30] = "Zerontino";
            names[31] = "Kryteminatron";
            names[32] = "Isivo";
            names[33] = "Comboxanoid";
            names[34] = "Sanamonica";
            names[35] = "Nexusonix";
            names[36] = "Carbocho";
            names[37] = "Uranopolitan";
            names[38] = "Kyatic";
            names[39] = "Swordkira";
            names[40] = "Erikinetic";
            names[41] = "Thundrakinetic";
            names[42] = "Neptomon";
            names[43] = "Marette";
            names[44] = "Liasaur";
            names[45] = "Cruentums";
            names[46] = "Pliatsikt";
            names[47] = "Bliabror";
            names[48] = "Iblaaziph";
            names[49] = "Ushualsox";
            #endregion

            switch (dense)
            {
                case 0:
                    this.y = 15;
                    this.x = 20;
                    break;
                case 1:
                    this.y = 20;
                    this.x = 25;
                    break;
                case 2:
                    this.y = 25;
                    this.x = 30;
                    break;
            }

            this.s = 45;

            stars = new Star[this.s, this.s / 2];
            this.numOfStars = rand.Next(y, x);
            int homeStar = rand.Next(2, numOfStars - 1);

            //creating life forms in the galaxy
            this.lifeForms = new LifeForm[rand.Next(3, numOfStars / 2)];
            for (int i = 0; i < lifeForms.Length; i++)
            {
                lifeForms[i] = new LifeForm(rand);
            }
            int counterLifForms = 0;

            Console.WriteLine("max: " + this.numOfStars);
            Console.WriteLine("home: " + homeStar);

            int counter = 0;
            while (counter <= numOfStars)
            {
                for (int y = 0; y < stars.GetLength(1); y++)
                {
                    for (int x = 0; x < stars.GetLength(0); x++)
                    {
                        //making sure im not placing 2 planets on each pther
                        if (stars[x, y] == null)
                        {
                            if (rand.Next(0, 100) == 0)
                            {
                                bool stay = true;
                                string name = "";
                                while (stay)
                                {
                                    int nameindex = rand.Next(0, names.Length);
                                    if (names[nameindex] != "")
                                    {
                                        stay = false;
                                        name = names[nameindex];
                                        names[nameindex] = "";
                                    }
                                }
                                if (counter != homeStar)
                                {
                                    
                                    //creating a star inhabited by other life form
                                    if (rand.Next(0, 3) == 0 && counterLifForms < lifeForms.Length)
                                    {
                                        
                                        //the first rand is for what star it whould be and the secound rand is for how many planets will be
                                        stars[x, y] = new Star(rand.Next(0, 101), difficulty, dense, rand.Next(0, 101), rand, counter, name, x, y, lifeForms[counterLifForms]);
                                        counterLifForms++;
                                    }
                                    else
                                    {
                                         //the first rand is for what star it whould be and the secound rand is for how many planets will be
                                        stars[x, y] = new Star(rand.Next(0, 101), difficulty, dense, rand.Next(0, 101), rand, counter, name, x, y);
                                    }
                                }
                                else
                                {
                                    stars[x, y] = new Star(rand.Next(0, 101), difficulty, dense, rand.Next(0, 101), rand, counter, name, x, y, true);
                                    //starting point wil be home planet
                                    cx = x;
                                    cy = y;
                                }
                                counter++;
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
            this.xy = new Star[numOfStars];
            int counter1 = 0;
            for (int y = 0; y < s / 2; y++)
            {
                for (int x = 0; x < s; x++)
                {
                    if (numOfStars == counter1)
                        break;

                    if (stars[x, y] != null)
                    {
                        stars[x, y].getName();
                        xy[counter1] = stars[x, y];
                        xy[counter1].setx(x);
                        xy[counter1].sety(y);
                        counter1++;
                    }
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

        public void GetMap(fleet[] f, int offsetx = 0, int offsety = 0)
        {
            //the color of the scanned area
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            //searching for visible stars
            for (int i = 0; i < numOfStars; i++)
            {
                Star s = stars[xy[i].getx(), xy[i].gety()];

                //calculating distence from owned stars
                if (s.HomeStar)
                {
                    double r_in = s.Range - 0.0;
                    double r_out = s.Range + 0.1;
                    int countery = 0;
                    for (double y = s.Range; y >= -s.Range; --y)
                    {
                        int counterx = 0;
                        for (double x = -s.Range; x < r_out; x += 0.5)
                        {
                            int placex = counterx + s.getx() - s.Range - 3;
                            int placey = countery + s.gety() - s.Range;

                            double value = x * x + y * y;


                            if (placex >= 0 && placex < this.s && placey >= 0 && placey < this.s / 2)
                            {
                                Console.SetCursorPosition(offsetx + placex, offsety + placey);

                                //showing only the stars in range
                                if (value >= r_in * r_in && value <= r_out * r_out)
                                {
                                    Console.Write(" ");

                                    if (stars[placex, placey] != null)
                                        stars[placex, placey].Visible = true;
                                }
                                if (value < r_in * r_in && value < r_out * r_out)
                                {
                                    Console.Write(" ");

                                    if (stars[placex, placey] != null)
                                        stars[placex, placey].Visible = true;
                                }
                            }
                            counterx++;
                        }
                        countery++;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            //showing the stars
            for (int i = 0; i < numOfStars; i++)
            {
                Console.SetCursorPosition(xy[i].getx() + offsetx, xy[i].gety() + offsety);

                string name = stars[xy[i].getx(), xy[i].gety()].getName();
                //only show visible stars
                if (stars[xy[i].getx(), xy[i].gety()].Visible)
                {
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
            Console.BackgroundColor = ConsoleColor.Black;

            //DrawFleet(f, offsetx, offsety); was supposed to draw fleets in the galaxy
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
            if (stars[cx, cy] != null)
            {
                return stars[cx, cy].getName();
            }
            else
            {
                return "";
            }
        }
        public bool Getstarvisible()
        {
            if (stars[cx, cy] != null)
            {
                return stars[cx, cy].Visible;
            }
            else
            {
                return false;
            }
        }
        public Star Getstar()
        {
            try
            {
                return stars[cx, cy];
            }
            catch
            {
                return null;
            }
        }
    }
}
