﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programer;
using Fauna;
using System.Net.Mime;
using Monster;
using System.Net.Quic;
//THINGS CLASS
namespace Thing
{
    public class Things
    {
        public int TDamage;
        public int TPrice { get; set; }
        public string TName { get; set; }

        public int TQuality;

        public int EnergyAm;
        public int HPAM;



        public Things(int tDamage, int tPrice, string tName, int tQuality)
        {
            TDamage = tDamage;
            TPrice = tPrice;
            TName = tName;
            TQuality = tQuality;
        }
        public Things(string tName, int energyAm, int hpAm)
        {
            TName = tName;
            EnergyAm = energyAm;
            HPAM = hpAm;
        }








        public static Things Wood = new Things(0, 2, "Wood", 1);
        public static Things Stone = new Things(1, 1, "Stone", 1);

        public static Things GoldChain = new Things(0, 10, "Золотая цепочка", 1);
        public static Things SilverChain = new Things(0, 6, "Серебряная цепочка", 1);








    }
    class Food : Things
    {
        public static Food WolfMeat = new Food(0, 4, "Сырое мясо волка", 1, 20, 40);
        public static Food BearMeat = new Food(0, 6, "Сырое мясо медведя", 1, 35, 70);


        public Food(int damage, int price, string name, int quality, int healthRestore, int energyRestore)
            : base(damage, price, name, quality)
        {
            HPAM = healthRestore;
            EnergyAm = energyRestore;
        }


        public void Use(Player player)
        {
            player.HP += HPAM;
            player.Energy += EnergyAm;
            Console.WriteLine($"Player использует {TName} и восстанавливает {HPAM} здоровья и {EnergyAm} .");

        }

    }
    class Shop : Things

    {
        public static bool HaveSword = false;
        static Shop Sword = new Shop(20, 20, "Меч", 1);
        public Shop(int tDamage, int tPrice, string tName, int tQuality) : base(tDamage, tPrice, tName, tQuality)
        {

        }



        public void ShopWIP(Player player, List<Things> Invent)
        {
            if (player.HaveFire == true)
            {
                Food.WolfMeat.TName = "Жаренное мясо Волка";
                Food.WolfMeat.EnergyAm += 15;
                Food.WolfMeat.HPAM += 10;


                Food.BearMeat.TName = "Жаренное мясо Медведя";
                Food.BearMeat.EnergyAm += 15;
                Food.BearMeat.HPAM += 10;

            }
            if (player.Language_Knowledge >= 20)
            {
                try
                {
                    player.HaveFire = true;

                    switch (player.GetRepTier())
                    {
                        case PReputation.UnFamous:
                            Food.WolfMeat.TPrice += 4;
                            Food.BearMeat.TPrice += 4;
                            Shop.Sword.TPrice += 4;
                            break;
                        case PReputation.Citizen:
                            break;
                        case PReputation.LocalStar:
                            Food.WolfMeat.TPrice -= 2;
                            Food.BearMeat.TPrice -= 2;
                            Shop.Sword.TPrice -= 2;
                            break;
                        case PReputation.Famous:
                            Food.WolfMeat.TPrice -= 4;
                            Food.BearMeat.TPrice -= 4;
                            Shop.Sword.TPrice -= 4;

                            break;
                    }




                    Console.WriteLine("Вы заходите в магазин,где вас встречает незнакомый вам причудливый торговец,предлагающий свои товары");
                    Console.WriteLine("Что хотите купить?");
                    Console.WriteLine("1.Жареное мясо волка");
                    Console.WriteLine("2. Жареное мясо медведя");
                    Console.WriteLine("3. Меч");
                    Console.WriteLine("4. Карта в Южный город");
                    Console.WriteLine("5. Карта в Восточный город");
                    Console.WriteLine("6.EXIT");

                    int choise = int.Parse(Console.ReadLine());
                    switch (choise)
                    {
                        case 1:
                            Console.WriteLine("Name:" + Food.WolfMeat.TName);
                            Console.WriteLine("Price:" + Food.WolfMeat.TPrice);
                            Console.WriteLine("HPAmount:" + Food.WolfMeat.HPAM);
                            Console.WriteLine("EnergyAmount:" + Food.WolfMeat.EnergyAm);

                            Console.WriteLine("Купить?");
                            Console.WriteLine("y");
                            Console.WriteLine("n");
                            string choise1 = Console.ReadLine();
                            switch (choise1)
                            {
                                case "y":
                                    Invent.Add(Food.WolfMeat);
                                    player.Money -= Food.WolfMeat.TPrice;
                                    break;

                                case "n":
                                    break;
                            }
                            break;
                        case 2:

                            Console.WriteLine("Name:" + Food.BearMeat.TName);
                            Console.WriteLine("Price:" + Food.BearMeat.TPrice);
                            Console.WriteLine("HPAmount:" + Food.BearMeat.HPAM);
                            Console.WriteLine("EnergyAmount:" + Food.BearMeat.EnergyAm);

                            Console.WriteLine("Купить?");
                            Console.WriteLine("y");
                            Console.WriteLine("n");
                            string choise2 = Console.ReadLine();
                            switch (choise2)
                            {
                                case "y":
                                    Invent.Add(Food.BearMeat);
                                    player.Money -= Food.BearMeat.TPrice;
                                    break;

                                case "n":
                                    break;
                            }
                            break;
                        case 3:

                            Console.WriteLine("Name:" + Sword.TName);
                            Console.WriteLine("Price:" + Sword.TPrice);
                            Console.WriteLine("Damage:" + Sword.TDamage);

                            Console.WriteLine("Купить?");
                            Console.WriteLine("y");
                            Console.WriteLine("n");
                            string choise3 = Console.ReadLine();
                            switch (choise3)
                            {
                                case "y":
                                    HaveSword = true;
                                    player.Money -= Sword.TPrice;
                                    break;

                                case "n":
                                    break;
                            }
                            break;
                        case 4:
                            Console.WriteLine("WIP");
                            break;
                        case 5:
                            Console.WriteLine("WIP");
                            break;
                        case 6:
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("ERROR");
                }


            }
        }
    }
    class Smith
    {
        
        public static void UpgradeWeapon(Player player, List<Things> Invent)
        {

            Console.WriteLine("Вы пришли к кузнецу. Что хотите?");
            Console.WriteLine("1.Прокачать оружие");
            Console.WriteLine("2.Зачаровать оружие(нужны кристаллы)");
            Console.WriteLine("3.EXIT");

            var playerSword = Invent.FirstOrDefault(i => i.TName == "Меч");
            int choise = int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    if (Invent.Any(i => i.TName == "Меч"))
                    {
                        Console.WriteLine("1. +5 Урона, 10 монет");
                        Console.WriteLine("2. +10 Урона, 20 монет ");
                        Console.WriteLine("3. +15 Урона, 30 монет");

                        int choise1 = int.Parse(Console.ReadLine());
                        switch (choise1)
                        {
                            case 1:
                                playerSword.TDamage += 5;
                                player.Money -= 10;
                                break;
                            case 2:
                                playerSword.TDamage += 10;
                                player.Money -= 20;
                                break;
                            case 3:
                                playerSword.TDamage += 20;
                                player.Money -= 30;
                                break;
                        }

                    }
                    break;
                case 2:
                    Console.WriteLine("WIP");
                    break;
                case 3:
                    break;
            }

        }   }

        class CraftItems : Things
        {
            public CraftItems(int tDamage, int tPrice, string tName, int tQuality) : base(tDamage, tPrice, tName, tQuality)
            {
            }
            public static CraftItems StoneAxe = new CraftItems(10, 9, "Каменный топор", 1);
            public static CraftItems Stick = new CraftItems(0, 1, "Палки", 1);

            public void Craft(List<Things> Invent, Player player)
            {
                Console.WriteLine("Что вы хотите скрафтить?");
                Console.WriteLine("1.Палки(2шт), Требуется: Дерево(1шт) ");
                Console.WriteLine("2. Каменный топор(1шт), Требуется: Палки(2шт), Камень(2шт)");
                Console.WriteLine("3.Смастерить огонь, Требуется: Палки(1шт), Камень(2шт), Дерево(2шт)");
                Console.WriteLine("4.EXIT");

                int choise = int.Parse(Console.ReadLine());

                switch (choise)
                {
                    case 1:
                        if (Invent.Any(i => i.TName == "Wood"))
                        {
                            Console.WriteLine("В ваш инвентарь были добавлены 2 палки");
                            Invent.Remove(Wood);
                            Invent.Add(Stick);
                            Invent.Add(Stick);
                        }
                        else
                        {
                            Console.WriteLine("Ресурсов недостаточно");
                        }
                        break;
                    case 2:

                        bool hasEnoughSticks = Invent.Where(i => i.TName == "Палки").Sum(i => i.TQuality) >= 2;

                        bool hasEnoughStones = Invent.Where(i => i.TName == "Stone").Sum(i => i.TQuality) >= 2;
                        if (hasEnoughSticks && hasEnoughStones)
                        {
                            Console.WriteLine("В ваш инвентарь был добавлен Каменный топор");
                            Invent.Add(StoneAxe);
                            Invent.Remove(Stick);
                            Invent.Remove(Stick);
                            Invent.Remove(Stone);
                            Invent.Remove(Stone);
                        }
                        player.HaveStoneAxe = true;
                        break;
                    case 3:
                        bool hasEnoughStonesForFire = Invent.Where(i => i.TName == "Stone").Sum(i => i.TQuality) >= 2;
                        bool hasEnoughWood = Invent.Where(i => i.TName == "Wood").Sum(i => i.TQuality) >= 2;
                        if (Invent.Any(i => i.TName == "Wood") && hasEnoughStonesForFire && hasEnoughWood) ;
                        player.HaveFire = true;
                        break;





                }
            }
        }

    
}

