using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Vehicle
    {
        public string v_type { get; set; }
        public int speed { get; set; }
        public bool in_motion = false;
        public int time_remaining = 0;
        public int x_val = 0;
        public int y_val = 0;
        public int dest_x_val = -1;
        public int dest_y_val = -1;
        public bool in_station = true;
        public string name;
        //public Station station;

        public Vehicle (string name = "")
        {
            this.name = name;
        }

        public void display_info(int longenst_v_name)
        {
            if (v_type == "Bus")
            {
                Console.Write("|" + v_type + ":   " + name);
                for (int i = 0; i < longenst_v_name - name.Length; i++)
                    Console.Write(" ");
                Console.WriteLine("| |x:" + x_val + "| |y:" + y_val + "| |In Motion:" + in_motion + "| |Remaining:" + time_remaining);
            }
            else
            {
                Console.Write("|" + v_type + ": " + name);
                for (int i = 0; i < longenst_v_name - name.Length; i++)
                    Console.Write(" ");
                Console.WriteLine("| |x:" + x_val + "| |y:" + y_val + "| |In Motion:" + in_motion + "| |Remaining:" + time_remaining);
            }
        }

        public void move_to_station(Station station)
        {
            in_motion = true;
            time_remaining = Convert.ToInt32(station.distance_to_location(x_val, y_val));

            dest_x_val = station.x_val;
            dest_y_val = station.y_val;
            //Console.WriteLine(station.distance_to_location(x_val, y_val));
            //Console.WriteLine(time_remaining);
        }

        public void increment_time()
        {
            bool skip = false;
            if (in_motion)
            {
                if (time_remaining < speed)
                {
                    time_remaining = 0;
                    in_motion = false;
                    skip = true;
                }
                if(!skip)
                    time_remaining = time_remaining - speed;

                if (time_remaining == 0)
                {
                    in_motion = false;
                    x_val = dest_x_val;
                    y_val = dest_y_val;
                    dest_x_val = -1;
                    dest_y_val = -1;
                }

            }
        }
    }

    class Bus : Vehicle
    {
        public Bus(string name = "")
        {
            this.name = name;
            v_type = "Bus";
            speed = 2;
        }

        public string modelName;
    }

    class Truck : Vehicle
    {
        public Truck(string name = "")
        {
            this.name = name;
            v_type = "Truck";
            speed = 1;
        }

        public string modelName;
    }
}
