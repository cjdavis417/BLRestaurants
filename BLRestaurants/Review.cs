using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRestaurants
{
    class Review
    {
        public int ID { get; set; }
        public int Restaurant_ID { get; set; }
        public string Title { get; set; }
        public int Star_Rating { get; set; }
        public string Writeup { get; set; }
    }
}
