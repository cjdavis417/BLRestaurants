using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace BLRestaurants
{
    class Program
    {
        public List<string> ListOfCommands = new List<string> { "Help", "Print All", "Exit", "Pull Reviews" };
        string command;

        static void Main(string[] args)
        {
            Console.Title = "Brunch Louisville";
            
            Program program = new Program();
            Console.WriteLine("Welcome to Brunch Louisville!");
            Console.WriteLine("Type 'help' at any point to see all available commands.");
            program.ReceiveCommand();

        }

        public void ReceiveCommand()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "restaurants.csv");
            var fileReviews = Path.Combine(directory.FullName, "Reviews.csv");
            //var fileContents = ReadRestaurants(fileName, fileReviews);

            Restaurants restaurants = new Restaurants();
            restaurants.GetCurrentRestaurantList(fileName, fileReviews);
            var restaurantsList = restaurants.restaurants;

            do
            {
                LineSpace();
                Console.Write("Command:> ");
                command = Console.ReadLine();
                command = command.ToLower(); // this makes all commands lowercase
                //string.Compare(command, "help", true) == 0 //(command, "help");

                if (command == "help")
                    Commands(ListOfCommands);
                else if (command == "print all")
                    ListRestaurants(restaurantsList);
                else if (command == "pull reviews")
                    SelectRestaurantReview(restaurantsList);
                else if (command == "exit")
                    Environment.Exit(0); // completely exits the program
                else
                    Console.WriteLine("Please enter a valid command.");
            } while (true);
        }

        private void SelectRestaurantReview(List<Restaurant> restaurants)
        {
            Restaurant restaurantReviews = new Restaurant();
            LineSpace();
            Console.WriteLine("Select the correlating number of the restaurant to see reviews: ");
            Console.WriteLine("1: Ramsis Cafe on the World");
            Console.WriteLine("2: Bristols Bar and Grill");
            Console.WriteLine("3: Louvino");
            LineSpace();

            string restaurantToReview = "";

            restaurantToReview = Console.ReadLine();

            int restaurantInt;
            Int32.TryParse(restaurantToReview, out restaurantInt);
            var restaurantName = "";
            bool validRestaurant;
            switch (restaurantInt)
            {
                case 1:
                    restaurantName = "Ramsis Cafe on the World";
                    validRestaurant = true;
                    break;
                case 2:
                    restaurantName = "Bristol Bar and Grill";
                    validRestaurant = true;
                    break;
                case 3:
                    restaurantName = "Louvino";
                    validRestaurant = true;
                    break;
                default:
                    Console.WriteLine("Opps! Please type 1, 2, or 3.");
                    validRestaurant = false;
                    break;
            }

            if (validRestaurant)
            {
                foreach (var restaurant in restaurants)
                {
                    if (restaurant.RestaurantName == restaurantName)
                    {
                        restaurantReviews = restaurant;
                    }
                }

                LineSpace();
                Console.WriteLine("Here is your selected Data: ");
                LineSpace();
                Console.WriteLine(restaurantReviews.RestaurantName);
                Console.WriteLine("Tele: {0}", restaurantReviews.Telephone);
                Console.WriteLine(restaurantReviews.StreetAddress);
                Console.WriteLine("{0}, {1} {2}", restaurantReviews.City, restaurantReviews.State, restaurantReviews.Zip);
                Console.WriteLine("Average Rating: {0} ({1} reviews)",
                    restaurantReviews.ReviewAverage, restaurantReviews.Reviews.Count);

            }

            
            LineSpace();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Your Restaurant Selection is now saving to file.");
            LineSpace();

            // saves info to new csv file
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Restaurant_Reviews.json");
            SerializeRestaurantToFile(restaurantReviews, fileName);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Save Success!");
            Console.WriteLine("Your file is save here: {0}", fileName);
            Console.ResetColor();
        }

        public void Commands(List<string> commands)
        {
            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
            ReceiveCommand();
        }

        public void ListRestaurants(List<Restaurant> restaurantList)
        {

            foreach (var restaurant in restaurantList)
            {
                Console.WriteLine();
                Console.WriteLine("{0}, Average: {1}", restaurant.RestaurantName, restaurant.CalcAverage(restaurant.Reviews));
                Console.WriteLine(restaurant.Restaurant_Type);
                foreach (var review in restaurant.Reviews)
                {
                    Console.WriteLine("Title: {0}, Rating: {1}/5", review.Title, review.StarRating);
                    Console.WriteLine(review.Writeup);
                    LineSpace();
                }
                Console.WriteLine();
            }

            ReceiveCommand();
        }

        public void LocationBuilder(List<Restaurant> restaurants, string restaurantName)
        {
            List<Restaurant> restaurantLocs = new List<Restaurant>();
            foreach (var restaurant in restaurants)
            {
                if (restaurant.RestaurantName == restaurantName)
                {
                    restaurantLocs.Add(restaurant);
                }
            }

            foreach (var restaurant in restaurantLocs)
            {
                Console.Write("{0} {1}", restaurant.StreetAddress, restaurant.City);
            }
        }

        /// <summary>
        /// Adds a line space
        /// </summary>
        public void LineSpace()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Writes data to .csv file format
        /// </summary>
        /// <param name="restaurant">Object of type Restaurants</param>
        /// <param name="newFileName">Name of the new file</param>
        public static void SerializeRestaurantToFile(Restaurant restaurant, string newFileName)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(newFileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, restaurant);
            }
        }
    }
}
