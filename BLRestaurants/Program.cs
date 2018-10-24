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
        public List<string> ListOfCommands = new List<string> { "Help", "Print All", "Write Review", "Exit" };
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
            do
            {
                Console.Write("Command:> ");
                command = Console.ReadLine();
                command = command.ToLower(); // this makes all commands lowercase
                //string.Compare(command, "help", true) == 0 //(command, "help");

                if (command == "help")
                    Commands(ListOfCommands);
                else if (command == "print all")
                    ListRestaurants();
                else if (command == "write review")
                    WriteReview();
                else if (command == "exit")
                    break;
                else
                    Console.WriteLine("Please enter a valid command.");
            } while (true);
        }

        public void Commands(List<string> commands)
        {
            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
            ReceiveCommand();
        }

        public void ListRestaurants()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "restaurants.csv");
            var fileReviews = Path.Combine(directory.FullName, "Reviews.csv");
            var fileContents = ReadRestaurants(fileName, fileReviews);

            foreach (var content in fileContents)
            {
                Console.WriteLine("{0}, Average: {1}", content.RestaurantName, content.CalcAverage(content.Reviews));
                Console.WriteLine(content.Restaurant_Type);
                foreach (var review in content.Reviews)
                {
                    Console.WriteLine("Title: {0}, Rating: {1}/5", review.Title, review.Star_Rating);
                    Console.WriteLine(review.Writeup);
                }
                Console.WriteLine();
            }

            ReceiveCommand();
        }

        public static List<Restaurant> ReadRestaurants(string fileName, string fileReviews)
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
                    restaurant.RestaurantName = values[1];
                    restaurant.Restaurant_Type = values[2];

                    double doubleParse;
                    if (double.TryParse(values[3], out doubleParse))
                    {
                        restaurant.Longitude = doubleParse;
                    }
                    if (double.TryParse(values[4], out doubleParse))
                    {
                        restaurant.Latitude = doubleParse;
                    }

                    // brings in the reviews here
                    var Reviews = new List<Review>();
                    using (var reviewReader = new StreamReader(fileReviews))
                    {
                        string line2 = "";
                        reviewReader.ReadLine();
                        while ((line2 = reviewReader.ReadLine()) != null)
                        {
                            var review = new Review();
                            string[] values2 = line2.Split(',');

                            if (values[0] == values2[1]) // (if ID of restaurant equals the reviews' restaurant ID. values2 is the list of reviews
                            { 
                                int intParse2;
                                if (int.TryParse(values2[0], out intParse2))
                                {
                                    review.ID = intParse2;
                                }
                                if (int.TryParse(values2[1], out intParse2))
                                {
                                    review.Restaurant_ID = intParse2;
                                }
                                review.Title = values2[2];
                                if (int.TryParse(values2[3], out intParse2))
                                {
                                    review.Star_Rating = intParse2;
                                }
                                review.Writeup = values2[4];

                                Reviews.Add(review);
                            }
                        }
                        
                    }

                    restaurant.Reviews = Reviews;

                    Restaurants.Add(restaurant);
                }
            }

                return Restaurants;
        }

        public void WriteReview()
        {
            Review review = new Review();
            Console.Write("What restaurant are you reviewing? ");
            var restaraunt = Console.ReadLine();
            Console.Write("Which location? ");
            var loc = Console.ReadLine();
            Console.Write("What star rating are you giving it? (1 through 5, whole numbers) ");
            var rating = Console.ReadLine();
            review.Star_Rating = Int32.Parse(rating);
            Console.Write("Please type your review. ");
            review.Writeup = Console.ReadLine();
        }
    }
}
