using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thing;
using Programer;
using Monster;
using System.Numerics;
using System.Threading;
using Event;

namespace Fauna;

class Forest
{
   
    
   public static void WoodExp(Player player, Things Wood, List<Things> Invent)
    {
        int amount = Wood.TQuality;
        
        player.Energy -= 10;

        if (player.Energy >= 15)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine($"Вы добыли {amount} дерева ");
                Invent.Add(Wood);
            }
        }
        else
        {
            Console.WriteLine("У вас недостаочно энергии!");
        }
    }
    public static void StoneExp(Player player, Things Stone, List<Things> Invent)
    {
        int amount = Stone.TQuality;
        
        player.Energy -= 10;

        if (player.Energy >= 15)
        {
            Console.WriteLine($"Вы добыли {amount} камня ");
            for (int i = 0; i < amount; i++)
            {
                Invent.Add(Stone);
            }
        }
        else
        {
            Console.WriteLine("У вас недостаочно энергии!");
        }

    }

    public static void EnterToForest(Player player, List<Things> Invent)
    {
        Console.WriteLine("Ты в лесу.Что будешь делать?");
        Console.WriteLine("5.Добыть дерева");
        Console.WriteLine("6.Добыть камня");
        Console.WriteLine("7.Открыть меню");

        int choise = int.Parse(Console.ReadLine());

        


        switch (choise)
        {
            case 5:
                Forest.WoodExp(player, Things.Wood, Invent);
                player.Language_Knowledge += 5;
                break;
            case 6:
                Forest.StoneExp(player, Things.Stone, Invent);
                break;
            case 7:
                player.Menu(player, Invent);
                break;
            
            default:
                Console.WriteLine("ERROR");
                break;
        }
        
    }

    public static void MetMonster(Monsterin monster, Player player, List<Things> Invent)
    {
        
        Random rand = new Random();
        int roll = rand.Next(0, 5); 



        if (roll == 0) //20%
            monster = new Wolf(player);
        else if (roll == 1) //20%
            monster = new Bear(player);
        else //60%
            monster = new Slime(player);

        Console.WriteLine($"Вы встретили {monster.Name}!");
        player.Fight(player, monster, Invent);
    }
    
}
