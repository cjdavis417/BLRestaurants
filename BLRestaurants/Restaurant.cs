using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRestaurants
{
    class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public string Restaurant_Type { get; set; }
        public string Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<Review> Reviews { get; set; }
        public double ReviewAverage { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Telephone { get; set; }
        

        public double CalcAverage(List<Review> reviews)
        {
            double average = 0.0;
            foreach (var review in reviews)
            {
                average = average + review.Star_Rating;
            }
            average = average / reviews.Count;

            return average;
        }

        

    }
}
