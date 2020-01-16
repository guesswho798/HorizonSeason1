using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class fleet
    {
        static int fleetId;
        int attackPoints;
        int hp;
        int shield;
        int numOfShips;
        MilitaryShips[] ships = new MilitaryShips[7];

        public fleet()
        {
            attackPoints = 0;
            hp = 0;
            shield = 0;
            numOfShips = 0;
            fleetId++;
        }

        public void add(MilitaryShips s ,int number, int attackPoints, int hp, int shield)
        {
            ships[numOfShips] = s;
            this.numOfShips++;
            this.attackPoints += attackPoints * number;
            this.hp += hp * number;
            this.shield += shield * number;
        }

        public void subtract(int damage)
        {
            int tmp = 0;
            if (shield > 0)
            {
                shield -= damage;
                if (shield < 0)
                {
                    tmp = shield * -1;
                    hp -= tmp;
                }
            }
            else
            {
                hp -= damage;
            }
        }

        public MilitaryShips[] GetInfoNumber()
        {
            return ships;
        }
        public int GetHP()
        {
            return hp;
        }
        public int GetShield()
        {
            return shield;
        }
        public int Getatt()
        {
            return attackPoints;
        }
    }
}
