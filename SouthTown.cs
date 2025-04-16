using Event;
using Fauna;
using NT;
using Programer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thing;

namespace South
{
    class SouthTown
    {
        public static void EnterToTown(Player player, List<Things> Invent)
        {
            Console.WriteLine("Ты стоишь перед воротами  южного города.Рядом ты видишь стражников,стоит подойти к ним?");
            Console.WriteLine("yes");
            Console.WriteLine("no");
            string choise = Console.ReadLine().Trim().ToLower();


            switch (choise)
            {
                case "yes":
                    Console.WriteLine("Ты подошел к стражникам и поздоровался");
                    switch (player.GetLanguageTier())
                    {
                        case Knowledge.Nonspeaking:

                            Console.WriteLine("--ZT,ENDJ.REPBRE GJ CJ,FXB ,ELNJ GCBYE");
                            Console.WriteLine("Услышав непонятный бубнеж вы лишь повели головой и попытались как то ответить.Стражник подумал что вы насмехайтесь над ним и решил не продолжать разговор");
                            player.Reputation -= 1;
                            Console.WriteLine("Ваша репутация понизилась.");
                            break;


                        case Knowledge.Speaking:
                            Console.WriteLine("Вы поняли речь стражника и вежливо ответили.");
                            Console.WriteLine("Вас пропустили в город.");
                            NorthTown.City(player, Invent);


                            break;

                        case Knowledge.Native:
                            Console.WriteLine("Вы подошли к стражникам,которые стали вам уже чуть ли не друзьями,вы радостно обменялись приветствием и вошли в город");
                            NorthTown.City(player, Invent);
                            break;

                    }
                    break;


                case "no":
                    Forest.EnterToForest(player, Invent);
                    break;


            }

        }
        public static void City(Player player, List<Things> Invent)
        {
            Console.WriteLine("Вы вошли в шумный город.Куда вы отправитесь?");

            while (true)

            {

                if (new Random().Next(1, 10) == 2)
                {
                    TownEvents.BanditAttack(player, Invent);
                }
                else if (player.Reputation >= 15)
                {
                    var rand = new Random().Next(1, 30);
                    if (rand == 2)
                    {
                        TownEvents.BanditAttack(player, Invent);
                    }
                }
                Console.WriteLine("1.Магазин");
                Console.WriteLine("2.Рынок");
                Console.WriteLine("3.Кузница");
                Console.WriteLine("4.Выйти из города");

                int choise = int.Parse(Console.ReadLine());



                switch (choise)
                {
                    case 1:
                        Shop SouthTownShop = new Shop(5, 9, 18);

                        SouthTownShop.ShopWIP(player, Invent);
                        break;
                    case 2:
                        Market.Shuk(player, Invent);
                        break;
                    case 3:
                        Smith.UpgradeWeapon(player, Invent);
                        break;
                    case 4:
                        NorthTown.EnterToTown(player, Invent);
                        break;
                }
            }
        }
    }
}

    

