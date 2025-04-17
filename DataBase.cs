using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event;
using Npgsql;
using Programer;


/*
 * NORTH - 7, 11, 14  
 * SOUTH - 5, 9, 18
 * EAST - 4, 13, 16
 * 
 */
namespace NTDB
{
    class ResourcePrice
    {
        public string ResourceName { get; set; }
        public int Price { get; set; }


        public static Dictionary<string, int> GetPricesForCity(string cityName,Player player)
        {
            var resourceprices = new Dictionary<string, int>();

            using (var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=CityRes"))
            {
                conn.Open();

                string query = @"
            SELECT resource_name, base_price
            FROM prices
            WHERE city_name = @city
        ";
                

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("city", cityName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string resourceName = reader.GetString(0);
                            int basePrice = reader.GetInt32(1);

                            // Учитываем репутацию
                            if (player.Reputation <= 5) // Репутация проверяется как int
                            {
                                basePrice += 4;
                            }

                            else if (player.Reputation <= 15)
                            {
                                basePrice -= 2;
                            }
                            else if (player.Reputation > 15 )
                            {
                                basePrice -= 4;
                            }
                                resourceprices[resourceName] = basePrice;
                        }
                    }
                }
            }

            return resourceprices;
        }
    }
}