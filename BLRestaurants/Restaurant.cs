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
        public BLType Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public enum BLType
        {
            Buffet = 1,
            Brunch = 2 
        }
    }
}
