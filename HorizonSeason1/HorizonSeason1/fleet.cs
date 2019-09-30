using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class fleet
    {
        int attackPoints = 0;
        int hp = 0;
        int shield = 0;
        int[] ships = new int[7];


        public void add(int id, int number, int attackPoints, int hp, int shield)
        {
            ships[id] += number;
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

        public int[] GetInfoNumber()
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
