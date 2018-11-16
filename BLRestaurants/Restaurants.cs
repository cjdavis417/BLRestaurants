using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BLRestaurants
{
    class Restaurants
    {
        public List<Restaurant> restaurants { get; set; }

        /// <summary>
        /// Compiles all the restaurants and reviews from two .csv's into a List
        /// </summary>
        /// <param name="fileName">This is the Path.Combine object of the restaurants</param>
        /// <param name="fileReviews">This is the Path.Combine object of the reviews</param>
        public void GetCurrentRestaurantList(string fileName, string fileReviews)
        {
            var RestaurantList = new List<Restaurant>();
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
                            

                            if (values[1] == values2[1]) // the compares the restaurant name
                            {
                                int intParse2;
                                if (int.TryParse(values2[0], out intParse2))
                                {
                                    review.ID = intParse2;
                                }
                                
                                review.RestaurantName = values2[1];
                                
                                review.Title = values2[2];
                                if (int.TryParse(values2[3], out intParse2))
                                {
                                    review.StarRating = intParse2;
                                }
                                review.Writeup = values2[4];

                                Reviews.Add(review);
                               
                            }

                         
                        }
                        
                    }
                    restaurant.Reviews = Reviews;
                    double average = restaurant.CalcAverage(Reviews);
                    restaurant.ReviewAverage = average;

                    restaurant.StreetAddress = values[5];
                    restaurant.City = values[6];
                    restaurant.State = values[7];

                    if (int.TryParse(values[8], out intParse))
                    {
                        restaurant.Zip = intParse;
                    }

                    restaurant.Telephone = values[9];
                    RestaurantList.Add(restaurant);

                    restaurants = RestaurantList;


                }

            }

        }

    }

}


