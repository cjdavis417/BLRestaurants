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
        public List<string> ListOfCommands = new List<string> { "Help", "Print All", "Write Review", "Exit", "Select Restaurant Reviews" };
        string command;

        static void Main(string[] args)
        {
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
                Console.Write("Command:> ");
                command = Console.ReadLine();
                command = command.ToLower(); // this makes all commands lowercase
                //string.Compare(command, "help", true) == 0 //(command, "help");

                if (command == "help")
                    Commands(ListOfCommands);
                else if (command == "print all")
                    ListRestaurants(restaurants.restaurants);
                else if (command == "write review")
                    WriteReview(restaurants.restaurants);
                else if (command == "select restaurant reviews")
                    SelectRestaurantReview(restaurantsList);
                else if (command == "exit")
                    break;
                else
                    Console.WriteLine("Please enter a valid command.");
            } while (true);
        }

        private void SelectRestaurantReview(List<Restaurant> restaurants)
        {
            Console.Write("Which restaurant reviews do you want to see the reviews for? ");
            var restaurantToReview = Console.ReadLine();

            // object to hold the selected reviews.
            Reviews Reviews = new Reviews();

            foreach (var restaurant in restaurants)
            {
                if (restaurantToReview == restaurant.RestaurantName)
                {
                    foreach (var review in restaurant.Reviews)
                    {
                        Reviews.ReviewsList.Add(review);
                    }
                }
            }

            foreach (var gatheredReview in Reviews.ReviewsList)
            {
                Console.WriteLine(gatheredReview);
            }

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
                Console.WriteLine("{0}, Average: {1}", restaurant.RestaurantName, restaurant.CalcAverage(restaurant.Reviews));
                Console.WriteLine(restaurant.Restaurant_Type);
                foreach (var review in restaurant.Reviews)
                {
                    Console.WriteLine("Title: {0}, Rating: {1}/5", review.Title, review.StarRating);
                    Console.WriteLine(review.Writeup);
                }
                Console.WriteLine();
            }

            ReceiveCommand();
        }

        public void WriteReview(List<Restaurant> restaurants)
        {
            Review review = new Review();
            Console.WriteLine("What restaurant are you reviewing? ");
            var reviewRestaurant = Console.ReadLine();

            Console.WriteLine("Which location? ");
            LocationBuilder(restaurants, reviewRestaurant);
            var loc = Console.ReadLine();

            Console.Write("What star rating are you giving it? (1 through 5, whole numbers) ");
            var rating = Console.ReadLine();
            review.StarRating = Int32.Parse(rating);

            Console.Write("Please type your review. ");
            review.Writeup = Console.ReadLine();
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
    }
}
