using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fauna;
using Thing;
using Programer;


namespace Monster
{
    class Monsterin
    {
        public string Name;
        public int HP;
        public int Damage;
        public Things DropItem;


        public int Level;
        public void AttackPlayer(Player player)
        {

            player.HP -= Damage;
        }
    }
    class Wolf : Monsterin
    {
        public Wolf(Player player) //Wolf Meat
        {
            Name = "Wolf";
            HP = 70;
            Damage = (35 * Level / 5) + 10;
            Level = new Random().Next(0, player.Level);
            DropItem = Food.WolfMeat;


        }
        public void AttackPlayer(Player player)
        {
            base.AttackPlayer(player);
            if (new Random().Next(1,3) == 3)
            {
                Console.WriteLine("Игрок кровоточит!");
                player.HP -= Damage / 3;
            }
        }
    }
    class Slime : Monsterin
    {
      public Slime(Player player)
        {
            Name = "Slime";
            HP = 20;
            Damage = Damage = (15* Level / 5) + 10;
            Level = new Random().Next(0, player.Level);
            
        }
    }
    class Bear : Monsterin
    {
        public Bear(Player player) //Bear Meat
        {
            Name = "Bear";
            HP = 120;
            Damage = Damage = (50 * Level / 5) + 10;
            Level = new Random().Next(0, player.Level);
            DropItem = Food.BearMeat;
        }
        public void AttackPlayer(Player player)
        {
            base.AttackPlayer(player);
            if (new Random().Next(1, 3) == 3)
            {
                Console.WriteLine("Игрок кровоточит!");
                player.HP -= Damage / 3;
            }
        }
    }

}
