using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRestaurants
{
    class Program
    {
        public List<string> ListOfCommands = new List<string> { "Help" };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Brunch Louisville");
            Console.WriteLine("Type 'help' at any point to see all available commands.");
            ListRestaurants();

        }



        void Commands(List<string> commands)
        {
            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
        }

        public static void ListRestaurants()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "restaurants.csv");
            var fileContents = ReadRestaurants(fileName);

            foreach (var content in fileContents)
            {
                Console.WriteLine(content.RestaurantName);
                Console.WriteLine(content.Restaurant_Type);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        public static List<Restaurant> ReadRestaurants(string fileName)
        {
            var Restaurants = new List<Restaurant>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var restaurant = new Restaurant();
                    string[] values = line.Split(',');

                    int intParse;
                    if (int.TryParse(values[0], out intParse))
                    {
                        restaurant.ID = intParse;
                    }
                    if (int.TryParse(values[1], out intParse))
                    {
                        restaurant.Restaurant_ID = intParse;
                    }
                    restaurant.RestaurantName = values[2];
                    restaurant.Restaurant_Type = values[3];

                    double doubleParse;
                    if (double.TryParse(values[4], out doubleParse))
                    {
                        restaurant.Longitude = doubleParse;
                    }
                    if (double.TryParse(values[5], out doubleParse))
                    {
                        restaurant.Latitude = doubleParse;
                    }

                    Restaurants.Add(restaurant);
                }
            }


                return Restaurants;
        }
    }
}
