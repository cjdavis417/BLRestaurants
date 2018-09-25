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
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "restaurants.csv");
            var fileContents = ReadRestaurants(fileName);
        }

        public static List<Restaurant> ReadRestaurants(string fileName)
        {
            var listRestaurants = new List<Restaurant>();
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
                    restaurant.RestaurantName = values[1];

                    double doubleParse;
                    if (double.TryParse(values[3], out doubleParse))
                    {
                        restaurant.Longitude = doubleParse;
                    }
                    if (double.TryParse(values[4], out doubleParse))
                    {
                        restaurant.Latitude = doubleParse;
                    }

                    listRestaurants.Add(restaurant);
                }
            }


                return listRestaurants;
        }
    }
}
