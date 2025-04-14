using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programer;
using Thing;
using Fauna;
using System.Security.Cryptography.X509Certificates;
using Monster;

namespace Event
{
    class Events
    {
        public static bool KnowNorthTown = true;
        
        public static void Mysterious_Table(Player player)
        {
            if (player.Language_Knowledge <= 25)
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
            else
            {
                Console.WriteLine("Вы прошли мимо знакомых вам табличек.Все символы на них вам давно понятны,так что нужны в них больше нет");
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

    class TownEvents : Events
    {
       public void Mysterious_Man(Player player)
        {
            Console.WriteLine("Гуляя по городу,вы встречайте неизвестного вам мужчину,который о чем то упрашивает вас.Поняв несколько слов,вы пришли к выводу,что это просто нищий," +
                "что просит у вас немного денег ");
            Console.WriteLine("Дать ему 5 монет? Yes/No");

            string choise = Console.ReadLine().ToLower();
            {
                switch (choise)
                {
                    case "yes":
                        Console.WriteLine("Вы дали нищему несколько монет,радостный мужчина мигом убежал от вас,по пути обранив лист с непонятными символами и картинками" +
                            ".Похоже.Это был лист из детской книги.Изучив его,вы смогли расшифровать несколько символов");
                        player.Language_Knowledge += 5;
                        player.Money -= 5;
                            if(player.HaveReputation = true)
                        {
                            player.Reputation += 2;
                            Console.WriteLine("Ваше действие увеличило вашу репутация на 2!");

                        }
                        break;
                    case "no":
                        break;

                       
                }
            }
        }

        public static void BanditAttack(Player player, List<Things> Invent)
        {
            Bandit banditos = new Bandit(player);

            Console.WriteLine("Прогуливаясь ночью,вы встречайте мужчину в капюшоне.Внезапно,тот достаёт нож и нападает на вас");
            player.Fight(player, banditos, Invent);

            if (player.PlayerWin == true)
            {
                Console.WriteLine("Вы смогли вырубить бандита");
                var drop = new Random().Next(1, 10);

                if (drop >= 1 && drop <= 4)
                {
                    Console.WriteLine("Обыскав тело бандита,вы находите несколько монет");
                    player.Money += 4;
                }
                else if (drop >= 5 && drop <= 8)
                {
                    Console.WriteLine("Обыскав тело бандита,вы находите Серебряную цепочку");
                    Invent.Add(Things.SilverChain);
                }
                else
                {
                    Console.WriteLine("Обыскав тело бандита,вы находите Золотую цепочку");
                    Invent.Add(Things.GoldChain);
                }
            }
            else
            {
                player.Lose(player, banditos, Invent);
            }
        }   }
        class Reputation
        {
            public static void GetReputation(Player player)
            {
                if (player.Language_Knowledge >= 50)
                {
                    Console.WriteLine("Ваш уровень понимания языка улучшился.Теперь вы можете спокойно воспринимать неизвестную ранее речь, и даже поддерживать разговор." +
                        "Благодаря этому,ваша активность в городе увеличилась,вы стали более узнаваемым.И у вас даже стала появлятся кое какая репутация.");
                    Console.WriteLine("ДОБАВЛЕН НОВЫЙ ПАРАМЕТР: РЕПУТАЦИЯ");
                    player.Reputation += 5;
                    Console.WriteLine( $"На данный момент ваш уровень репутации равен {player.Reputation}");
                    player.HaveReputation = true;

                }
            }
            
        }
    
}
