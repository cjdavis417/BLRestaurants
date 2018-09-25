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
            var fileName = Path.Combine(directory.FullName, "restaurant.csv");
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

                    int ID;
                    if (int.TryParse(values[0], out ID))
                    {
                        restaurant.ID = ID;
                    }

                    listRestaurants.Add(restaurant);
                }
            }


                return listRestaurants;
        }
    }
}
