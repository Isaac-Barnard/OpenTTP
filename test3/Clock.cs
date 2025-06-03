using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Clock
    {
        public int current_time = 0;
        public void pass_time(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                vehicles[i].increment_time();
            }
            current_time++;
            //Console.WriteLine("Time: " +  current_time);
        }

    }
}
