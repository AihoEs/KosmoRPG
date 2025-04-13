using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programer;
using Thing;
using Fauna;
using System.Security.Cryptography.X509Certificates;

namespace Event
{
    class Events
    {
        public static bool KnowNorthTown = false;
        
        public static void Mysterious_Table(Player player)
        {


            Console.WriteLine("По дороге вы обнаруживайте табличку с незнакомыми вам символами.Стоит изучить её?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");

            string choise = Console.ReadLine().ToLower();
            switch (choise)
            {
                case "yes":
                    if (player.Energy >= 15)
                    {
                        Console.WriteLine("Попытаясь соотнести картинки к символам,вам удалось распознать несколько слов");
                        player.Language_Knowledge += 5;
                        Console.WriteLine($"Ваш уровень языка увеличился на 5! на данный момент он равен {player.Language_Knowledge}");
                    }
                    else
                    {
                        Console.WriteLine("Вы слишком устали и вам лень заниматься ерундой");
                    }
                    break;
                case "no":
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        public void Mysterious_Table_NorthTown(Player player)
        {
            if (KnowNorthTown == false)
            {
                Console.WriteLine("По дороге вы обнаруживайте огромную серебряную табличку с незнакомыми вам символами.Стоит изучить её?");
                Console.WriteLine("yes");
                Console.WriteLine("no");

                string choise = Console.ReadLine().ToLower();
                switch (choise)
                {
                    case "yes":
                        if (player.Energy >= 15)
                        {
                            Console.WriteLine("Попытаясь соотнести картинки к символам,вам удалось распознать несколько слов.Оказывается,к северу от вас есть город, и теперь вы " +
                                "знаете дорогу!");
                            player.Language_Knowledge += 10;
                            Console.WriteLine($"Ваш уровень языка увеличился на 10! на данный момент он равен {player.Language_Knowledge}");
                            KnowNorthTown = true;
                        }
                        else
                        {
                            Console.WriteLine("Вы слишком устали и вам лень заниматься ерундой");
                        }
                        break;
                    case "no":
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Вы прошли мимо знакомой вам таблички в город.");
            }
        }
    }
}
