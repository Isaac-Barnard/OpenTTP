using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using test3;

namespace test3
{
    class Displayer
    {
        public string display_instructions = "                   Press Q to quit\n               Press ENTER to pass time.\n           Press S to create/view stations.\n         Press V to assign routes to vehicles.";
        public string display_line = "-----------------------------------------------------------";
        public string alt_display_line = "----------- ----------- ----------- ----------- -----------";

        public void main_display(Clock clock, List<Vehicle> vehicles)
        {
            Console.WriteLine(display_instructions);
            if (clock.current_time % 2 == 0)
                Console.WriteLine(display_line);
            else
                Console.WriteLine(alt_display_line);
            Console.WriteLine("                         Time: " + clock.current_time);

            for (int k = 0; k < vehicles.Count; k++)
            {
                int longenst_v_name = 0;
                Console.Write(+(k + 1) + ": ");
                //Add extra spaces after shorter names so later data lines up
                for (int i = 0; i < vehicles.Count; i++)
                {
                    if (vehicles[i].name.Length > longenst_v_name)
                        longenst_v_name = vehicles[i].name.Length;
                }
                vehicles[k].display_info(longenst_v_name);
            }
            if (clock.current_time % 2 == 0)
                Console.WriteLine(display_line);
            else
                Console.WriteLine(alt_display_line);
        }


        public void station_creation_display(int[,] station_map, Station[,] station_object_map, int[] cursor, List<Station> stations, bool display_station_created, bool display_station_already_there, bool rename_station)
        {
            Console.WriteLine("Pick a location with the arrow keys.\n    Create a station with ENTER.\n      Rename a station with R");
            for (int i = 0; i < station_map.GetLength(1) * 3 - 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            for (int i = 0; i < station_map.GetLength(0); i++)
            {
                for (int j = 0; j < station_map.GetLength(1); j++)
                {
                    if (i == cursor[0] && j == cursor[1])
                        if (station_map[i, j] == 0)
                            Console.Write("%  ");
                        else
                            Console.Write("#  ");
                    // -1 = Empty, 0 = Station
                    else if (station_map[i, j] == -1)
                        Console.Write("o  ");
                    else if (station_map[i, j] == 0)
                        Console.Write("S  ");
                }
                Console.WriteLine();
            }
            for (int x = 0; x < station_map.GetLength(1) * 3 - 2; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine("Cursor: " + cursor[0] + ", " + cursor[1] + " | Rows:" + station_map.GetLength(1) + ", Columns:" + station_map.GetLength(0));
            Console.WriteLine("# of stations: " + stations.Count);

            if (station_map[cursor[0], cursor[1]] == -1)
                Console.WriteLine("Current selection: Empty");
            else if (station_map[cursor[0], cursor[1]] == 0)
            {
                if (string.IsNullOrEmpty(station_object_map[cursor[0], cursor[1]].name))
                    Console.WriteLine("Station: Station" + cursor[0] + cursor[1]);
                else
                    Console.WriteLine("Station: " + station_object_map[cursor[0], cursor[1]].name);

                Console.WriteLine("Bus: " + station_object_map[cursor[0], cursor[1]].bus_station + " | Truck: " + station_object_map[cursor[0], cursor[1]].truck_station);
            }


            // Various action messages
            Console.WriteLine();
            if (display_station_created)
                Console.WriteLine("  Station created at " + cursor[0] + ", " + cursor[1] + "!");
            else if (display_station_already_there)
                Console.WriteLine("  There's already a station there!");

            if (rename_station)
            {
                if (station_map[cursor[0], cursor[1]] == 0)
                {
                    rename_station = true;
                    string old_name;
                    if (string.IsNullOrEmpty(station_object_map[cursor[0], cursor[1]].name))
                    {
                        Console.WriteLine("Enter new name for Station" + cursor[0] + cursor[1] + ":");
                        old_name = "Station" + cursor[0] + cursor[1];
                    }
                    else
                    {
                        Console.WriteLine("Enter new name for " + station_object_map[cursor[0], cursor[1]].name + ":");
                        old_name = station_object_map[cursor[0], cursor[1]].name;
                    }
                    var new_name = Console.ReadLine();
                    if (new_name != "")
                    {
                        station_object_map[cursor[0], cursor[1]].name = new_name;
                        Console.WriteLine("\nStation " + old_name + "has been renamed to " + new_name);
                    }

                }
                else
                    Console.WriteLine("No station to rename!");
            }
        }


        public void vehicle_selection_display(List<Vehicle> vehicles, bool display_unavailable_v_error, int cursor, bool rename_vehicle, bool vehicle_chosen_created, string chosen_vehicle_created)
        {
            Console.WriteLine("Pick a vehicle with the arrow keys.\n    Select a vehicle with ENTER.\n      Rename a vehicle with R.");
            Console.WriteLine(display_line);
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].in_motion)
                {
                    if (cursor == i)
                    {
                        if (vehicles[i].v_type == "Bus")
                            Console.WriteLine(" # " + vehicles[i].v_type + "   - UNIVAILABLE " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                        else
                            Console.WriteLine(" # " + vehicles[i].v_type + " - UNIVAILABLE " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                    }
                    else
                    {
                        if (vehicles[i].v_type == "Bus")
                            Console.WriteLine(" - " + vehicles[i].v_type + "   - UNIVAILABLE " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                        else
                            Console.WriteLine(" - " + vehicles[i].v_type + " - UNIVAILABLE " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                    }
                }
                else
                {
                    if (cursor == i)
                    {
                        if (vehicles[i].v_type == "Bus")
                            Console.WriteLine(" # " + vehicles[i].v_type + "   - " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                        else
                            Console.WriteLine(" # " + vehicles[i].v_type + " - " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                    }
                    else
                    {
                        if (vehicles[i].v_type == "Bus")
                            Console.WriteLine(" - " + vehicles[i].v_type + "   - " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                        else
                            Console.WriteLine(" - " + vehicles[i].v_type + " - " + vehicles[i].name + " (" + vehicles[i].x_val + ", " + vehicles[i].y_val + ")");
                    }
                }
            }
            Console.WriteLine(display_line);
            Console.WriteLine("Cursor: " + (cursor + 1));
            if (display_unavailable_v_error)
                Console.WriteLine("\n  Vehicle already on a route. Wait for it to reach its destination");
            if (vehicle_chosen_created)
            {
                Console.WriteLine("\n New " + chosen_vehicle_created + " created!");
            }

            if (rename_vehicle)
            {
                string old_name;
                Console.WriteLine("Enter new name for " + vehicles[cursor].name + ":");
                old_name = vehicles[cursor].name;

                var new_name = Console.ReadLine();
                if (new_name != "")
                {
                    vehicles[cursor].name = new_name;
                    Console.WriteLine("\nStation " + old_name + " has been renamed to " + new_name);
                }
            }
        }


        public void vehicle_route_selection_display(List<Vehicle> vehicles, List<Station> stations, bool display_unavailable_s_error, int cursor, int v_selection_cursor)
        {
            Console.WriteLine("Pick a station with the arrow keys.\n Select a station with ENTER");
            Console.WriteLine(display_line);
            Console.WriteLine(" " + vehicles[cursor].v_type + " - " + vehicles[cursor].name + " (" + vehicles[cursor].x_val + ", " + vehicles[cursor].y_val + ")");
            Console.WriteLine(display_line);
            Console.WriteLine("stations: ");
            for (int i = 0; i < stations.Count; i++)
            {
                if (vehicles[cursor].x_val == stations[i].x_val && vehicles[cursor].y_val == stations[i].y_val)
                {
                    if (v_selection_cursor == i)
                    {
                        Console.WriteLine(" # " + stations[i].name + " - UNIVAILABLE (" + stations[i].x_val + ", " + stations[i].y_val + ")");
                    }
                    else
                    {
                        Console.WriteLine(" - " + stations[i].name + " - UNIVAILABLE (" + stations[i].x_val + ", " + stations[i].y_val + ")");
                    }
                }
                else
                {
                    if (v_selection_cursor == i)
                    {
                        Console.WriteLine(" # " + stations[i].name + " - (" + stations[i].x_val + ", " + stations[i].y_val + ")");
                    }
                    else
                    {
                        Console.WriteLine(" - " + stations[i].name + " - (" + stations[i].x_val + ", " + stations[i].y_val + ")");
                    }
                }
            }
            Console.WriteLine(display_line);
            Console.WriteLine("Cursor: " + (v_selection_cursor + 1));
            if (display_unavailable_s_error)
                Console.WriteLine("\n  Vehicle is already in this station");
        }


        public void vehicle_creation_display(List<Vehicle> vehicles, List<Vehicle> types_of_vehicles, int v_creation_cursor, bool vehicle_chosen, string chosen_vehicle)
        {
            Console.WriteLine("Pick a vehicle with the arrow keys.\n    Select a vehicle with ENTER.");
            Console.WriteLine(display_line);
            for (int i = 0; i < types_of_vehicles.Count; i++)
            {
                if (v_creation_cursor == i)
                    Console.WriteLine("# " + types_of_vehicles[i].v_type + "|  Speed: " + types_of_vehicles[i].speed);
                else
                    Console.WriteLine("- " + types_of_vehicles[i].v_type + "|  Speed: " + types_of_vehicles[i].speed);
            }
            Console.WriteLine(display_line);
            Console.WriteLine("Cursor: " + (v_creation_cursor+1));

            if (vehicle_chosen)
                Console.WriteLine("\n New " + chosen_vehicle + " created!");

        }
    }
}
