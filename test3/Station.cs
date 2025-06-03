using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Station
    {
        public int x_val;
        public int y_val;
        public string name = "";
        public bool bus_station = false;
        public bool truck_station = false;

        public Station(int x_val, int y_val)
        {
            this.x_val = x_val;
            this.y_val = y_val;
        }

        public double distance_to_station(Station diff_station)
        {
            // Calculate and return the Euclidean distance
            return Math.Sqrt(Math.Pow((diff_station.x_val - x_val), 2)
                             + Math.Pow((diff_station.y_val - y_val), 2));
        }

        public double distance_to_location(int dest_x_val, int dest_y_val)
        {
            // Calculate and return the Euclidean distance
            return Math.Sqrt(Math.Pow((dest_x_val - x_val), 2)
                             + Math.Pow((dest_y_val - y_val), 2));
        }
    }
}
