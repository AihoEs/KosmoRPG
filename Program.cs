using System;
using System.ComponentModel;
using System.Threading.Channels;
using Fauna;
using Monster;
using System.Collections.Generic;
using Event;
using System.Security.Policy;
using NT;
using Thing;
using static Thing.Shop;


// PLAYER + INVENTORY + MAIN CLASS
namespace Programer
{
    enum Knowledge {
        Nonspeaking,
        Speaking,
        Native

    }
    class Player
    {
        
        public int HP;
       public int Damage;
       public int Energy;
       public int Language_Knowledge = 0;
       public int Level;
       public int Reputation = 0;
        public int Money = 0;

        public bool HaveStoneAxe = false;
        public bool HaveFire = false;
        public Knowledge GetLanguageTier()
        {
            if (Language_Knowledge < 20)
                return Knowledge.Nonspeaking;
            else if (Language_Knowledge < 50)
                return Knowledge.Speaking;
            else
                return Knowledge.Native;
        }


        public Player(int hp, int damage,int energy, int lang, int level, int reputation)
        {
            HP = hp;
            Damage = damage * level / 3 + 10;
            Energy = energy;
            Language_Knowledge = lang;
            Level = level;
            Reputation = reputation;
        }

      

        public void AttackMonster(Monsterin monster, Player player)
        {
            Console.WriteLine($"вы нанесли монстру {player.Damage }урона!Вы израсходовали 15 энергии!у вас осталось{player.Energy - 15} энергии ");
            monster.HP -= player.Damage;
            player.Energy -= 15;
            Console.WriteLine($"У монстра осталось {monster.HP} хп!");
        }

        public void Defend(Monsterin monster,Player player)
        {
            int amount = monster.Damage / 3;
            Console.WriteLine($"Вы защищайтесь, монстр нанес вам {amount} урона!Вы восстановили 10 энергии!У вас {player.Energy + 10} ");
            player.HP -= amount;
            player.Energy += 10;
            Console.WriteLine($"У вас осталось {player.HP} ХП!");

        }
        public void Fight(Player player, Monsterin monster, List<Things> Invent)
        {
            if (HaveStoneAxe == true)
            {
                player.Damage += CraftItems.StoneAxe.TDamage;
            }

            if (HaveFire == true)
            {
                Food.WolfMeat.TName = "Жаренное мясо Волка";
                Food.WolfMeat.EnergyAm += 15;
                Food.WolfMeat.HPAM += 10;


                Food.BearMeat.TName = "Жаренное мясо Медведя";
                Food.BearMeat.EnergyAm += 15;
                Food.BearMeat.HPAM += 10;
            }

            Console.WriteLine("Вы начал бой,что будете делать?");
            Console.WriteLine("1.Атаковать");
            Console.WriteLine("2.Защищаться");

            while (true)
            {
                try {
                    int choise = int.Parse(Console.ReadLine());
                    switch (choise)
                    {
                        case 1:
                            if (player.Energy >= 15)
                            {
                                player.AttackMonster(monster, player);
                            }
                            else if (monster.HP >= 10)
                            {
                                monster.AttackPlayer(player);
                            }


                            break;
                        case 2:
                            player.Defend(monster, player);
                            break;

                            Thread.Sleep(1000);


                    }
                    if (player.HP <= 0 || player.Energy <= 0)
                    {
                        Console.WriteLine("Вы погибли");
                        Invent.Clear();
                        break;
                    }

                    else if (monster.HP <= 0)
                    {
                        Console.WriteLine("Вы победили");
                        Level += 1;
                        Invent.Add(monster.DropItem);
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("ERROR");
                }
        } }

        public void ShowStats(Player player)
        {
            Console.WriteLine("Ваши характеристики:");
            Console.WriteLine("HP:" + player.HP);
            Console.WriteLine("Damage:" + player.Damage);
            Console.WriteLine("Energy:" + player.Energy);
            Console.WriteLine("Level:" + player.Level);
            Console.WriteLine("Language knowledge:" + player.Language_Knowledge);
            Console.WriteLine("Reputation:" + player.Reputation);
            Console.WriteLine("Money:" + player.Money);
        }
            
            
            





        public void AddItem(Things thing, List<Things> Invent)
        {
            Invent.Add(new Things(thing.TDamage, thing.TPrice, thing.TName, thing.TQuality));
            Console.WriteLine($"Предмет {thing.TName} в количестве{thing.TQuality} добавлен в ваш инвентарь!");
        }

        public void Check(List<Things>Invent, Player player)
        {
            foreach(Things th in Invent)
            {
                Console.WriteLine(th.TName , th.TQuality);
            }

            Console.WriteLine("Хотите использовать какой либо предмет?");
            Console.WriteLine("y/n");

            string choise = Console.ReadLine();
            switch (choise)
            {
                case "y":
                    Console.WriteLine("Что вы хотите съесть?");
                    Console.WriteLine("1. Мясо Волка");
                    Console.WriteLine("2. Мясо Медведя");
                    int choise1 = int.Parse(Console.ReadLine());

                    switch (choise1)
                    {
                        case 1:
                            if (Invent.Any(i => i.TName == "Сырое мясо волка"))
                            {

                                player.UseItem(Food.WolfMeat, player);
                                Console.WriteLine($"Вы съели{Food.WolfMeat.TName}и восстановили{Food.WolfMeat.EnergyAm} энергии и{Food.WolfMeat.HPAM} здоровья!");
                                Console.WriteLine($"На данный момент у вас {player.HP} здоровья и {player.Energy} энергии");
                                Invent.Remove(Food.WolfMeat);
                            }
                            else
                            {
                                Console.WriteLine("Мясо не найдено");
                            }
                            break;
                        case 2:
                            {
                                if (Invent.Any(i => i.TName == "Сырое мясо медведя"))
                                {
                                    player.UseItem(Food.BearMeat, player);
                                    Console.WriteLine($"Вы съели{Food.BearMeat.TName}и восстановили{Food.BearMeat.EnergyAm} энергии и{Food.BearMeat.HPAM} здоровья!");
                                    Console.WriteLine($"На данный момент у вас {player.HP} здоровья и {player.Energy} энергии");
                                    Invent.Remove(Food.BearMeat);
                                }
                                else
                                {
                                    Console.WriteLine("Мясо не найдено");
                                }
                                break;
                            }
                    }
                    break;

                case "n":
                    break;

            }

        }


        public void Menu(Player player,List<Things> Invent)
        {
            CraftItems crafting = new CraftItems(0, 0, "null", 0);
            Console.WriteLine("1.Проверить инвентарь");
            Console.WriteLine("2. Посмотреть характеристики");
            Console.WriteLine("3.Открыть меню крафта");
            Console.WriteLine("4.Отправиться в Северный город(Только если вы знайте дорогу туда!)");
            

            int choise = int.Parse(Console.ReadLine());

            switch (choise)
            {
                case 1:
                    player.Check(Invent,player);
                    break;
                case 2:
                    player.ShowStats(player);
                    break;
                case 3:
                     crafting.Craft(Invent, player);
                    break;
                case 4:
                    Events evi = new Events();
                    if(Events.KnowNorthTown == true)
                    {
                        NorthTown.EnterToTown(player, Invent);
                    }
                    else
                    {
                        Console.WriteLine("Вы не знайте дорогу,вам нужно найти указатель");
                    }
                    break;


            }

            
        }
        public void UseItem(Food Meat,Player player)
        {
            Meat.Use(player);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Things> Invent = new List<Things>();
            Monsterin monster = null;
            Player player = new Player(100, 20, 100, 0, 0, 0);
            
            while (true)
            {
                Forest.EnterToForest(player, Invent);
                


                if (new Random().Next(1, 100) == 2)
                {
                    Forest.MetMonster(monster, player, Invent);
                }

                if (new Random().Next(1,5) == 2)
                {
                    Events.Mysterious_Table(player);
                }
                if (new Random().Next(1,3) == 2)
                {
                    Events evi = new Events();
                    evi.Mysterious_Table_NorthTown(player);
                }
            }
        }



        }
    }
