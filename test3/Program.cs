using System;
using System.Collections.Generic;


namespace test3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bus bus1 = new Bus();
            Truck truck1 = new Truck();

            Station station1 = new Station(0, 0);
            station1.name = "The Grand Stationo";
            Station station2 = new Station(5, 8);

            //Console.WriteLine("Raw distance: " + station1.distance_to_station(station2));
            bus1.move_to_station(station2);
            truck1.move_to_station(station2);

            List<Vehicle> vehicles = new List<Vehicle>() { bus1, truck1 };
            List<Station> stations = new List<Station>() { station1, station2 };

            Bus info_bus = new Bus();
            Truck info_truck = new Truck();
            List<Vehicle> types_of_vehicles = new List<Vehicle> { info_bus, info_truck };

            /*
            * -----------------------------------------------
            *                   Main Page
            * -----------------------------------------------
            */
            Clock clock = new Clock();
            Displayer displayer = new Displayer();

            for (int i = 0; i < vehicles.Count; i++)
            {
                if (string.IsNullOrEmpty(vehicles[i].name))
                    vehicles[i].name = vehicles[i].v_type + i;
            }

            // Display initial page (Main Page)
            displayer.main_display(clock, vehicles);

            bool end_program = false;

            while (!end_program)
            {
                var input = Console.ReadKey();

                // Exit program logic
                if (input.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("\n          |###############################|");
                    Console.WriteLine("           Are you sure you want to exit?\n                Press Q to exit.");
                    Console.WriteLine("          |###############################|");
                    var input2 = Console.ReadKey();
                    if (input2.Key == ConsoleKey.Q)
                        end_program = true;
                    else
                    {
                        Console.Clear();
                        displayer.main_display(clock, vehicles);
                    }
                }

                //Pass time
                if (input.Key == ConsoleKey.Enter)
                {

                    // Display interactive page (Main Page)
                    Console.Clear();
                    displayer.main_display(clock, vehicles);
                    clock.pass_time(vehicles);
                }

                /*
                 * -----------------------------------------------
                 *            Station Creation Page
                 * -----------------------------------------------
                 */
                int[,] station_map = { { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 } };
                Station[,] station_object_map = { { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null } };

                for (int i = 0; i < stations.Count; i++)
                {
                    station_map[stations[i].x_val, stations[i].y_val] = 0;
                    station_object_map[stations[i].x_val, stations[i].y_val] = stations[i];
                    if (string.IsNullOrEmpty(stations[i].name))
                        stations[i].name = "Station" + i.ToString();
                }

                int[] s_cursor = { 0, 0 };
                bool end_selection = false;
                bool display_station_created = false;
                bool display_station_already_there = false;
                bool rename_station = false;

                if (input.Key == ConsoleKey.S)
                {
                    //Display Initial Screen
                    Console.Clear();
                    displayer.station_creation_display(station_map, station_object_map, s_cursor, stations, display_station_created, display_station_already_there, rename_station);


                    while (!end_selection)
                    {
                        var input3 = Console.ReadKey();
                        display_station_created = false;
                        display_station_already_there = false ;
                        rename_station = false;

                        if (input3.Key == ConsoleKey.Escape)
                            end_selection = true;
                        if (input3.Key == ConsoleKey.RightArrow)
                        {
                            if (s_cursor[1] < station_map.GetLength(1)-1)
                                s_cursor[1]++;
                        }
                        if (input3.Key == ConsoleKey.LeftArrow)
                        {
                            if (s_cursor[1] > 0)
                                s_cursor[1]--;
                        }
                        if (input3.Key == ConsoleKey.UpArrow)
                        {
                            if (s_cursor[0] > 0)
                                s_cursor[0]--;
                        }
                        if (input3.Key == ConsoleKey.DownArrow)
                        {
                            if (s_cursor[0] < station_map.GetLength(1)-1)
                                s_cursor[0]++;
                        }
                        if (input3.Key == ConsoleKey.Enter)
                        {
                            if (station_map[s_cursor[0], s_cursor[1]] == -1){
                                stations.Add(new Station(s_cursor[0], s_cursor[1]));
                                station_object_map[s_cursor[0], s_cursor[1]] = stations[stations.Count - 1];
                                for (int i = 0; i < stations.Count; i++)
                                {
                                    station_map[stations[i].x_val, stations[i].y_val] = 0;
                                }
                                display_station_created = true;
                            }
                            display_station_already_there = true;
                        }
                        if (input3.Key == ConsoleKey.R)
                        {
                            rename_station = true;
                        }

                        //Display interactive Screen
                        Console.Clear();
                        displayer.station_creation_display(station_map,station_object_map, s_cursor, stations,display_station_created,display_station_already_there, rename_station);

                    }
                    Console.Clear();
                    displayer.main_display(clock, vehicles);
                }

                /*
                 * -----------------------------------------------
                 *              Vehicle Selection Page
                 * -----------------------------------------------
                 */
                //Vehicle selection page
                if (input.Key == ConsoleKey.V)
                {
                    for (int k = 0; (k < vehicles.Count); k++)
                    {
                        if (string.IsNullOrEmpty(vehicles[k].name))
                            vehicles[k].name = vehicles[k].v_type + k.ToString();
                    }

                    bool display_unavailable_v_error = false;
                    bool end_v_selection = false;
                    int cursor = 0;
                    bool rename_vehicle = false;
                    bool vehicle_chosen_created = false;
                    string chosen_vehicle_created = "";


                    //Display initial page (Vehicle Selection)
                    Console.Clear();
                    displayer.vehicle_selection_display(vehicles, display_unavailable_v_error, cursor, rename_vehicle, vehicle_chosen_created, chosen_vehicle_created);


                    while (!end_v_selection)
                    {
                        rename_vehicle = false;
                        var input4 = Console.ReadKey();

                        if (input4.Key == ConsoleKey.Escape)
                            end_v_selection = true;
                        if (input4.Key == ConsoleKey.DownArrow)
                        {
                            if (cursor < vehicles.Count-1)
                            {
                                cursor++;
                            }
                        }
                        if (input4.Key == ConsoleKey.UpArrow)
                        {
                            if (cursor > 0)
                            {
                                cursor--;
                            }
                        }
                        if(input4.Key == ConsoleKey.R)
                        {
                            rename_vehicle = true;
                        }

                        //Display interactive page (Vehicle Selection)
                        Console.Clear();
                        displayer.vehicle_selection_display(vehicles, display_unavailable_v_error, cursor, rename_vehicle, vehicle_chosen_created, chosen_vehicle_created);
                        

                        bool display_unavailable_s_error = false;
                        bool v_selection_made = true;
                        int v_selection_cursor = 0;

                        //Vehicle route selection page
                        //--------------------------------
                        if (input4.Key == ConsoleKey.Enter)
                        {
                            //Display initial page (Vehicle Route Selection)
                            Console.Clear();
                            displayer.vehicle_route_selection_display(vehicles, stations, display_unavailable_s_error, cursor, v_selection_cursor);
                            

                            while (v_selection_made)
                            {
                                display_unavailable_v_error = false;
                                if (vehicles[cursor].in_motion)
                                {
                                    display_unavailable_v_error = true;
                                    break;
                                }

                                var input5 = Console.ReadKey();

                                if (input5.Key == ConsoleKey.Escape)
                                    v_selection_made = false;
                                if (input5.Key == ConsoleKey.DownArrow)
                                {
                                    if (v_selection_cursor < stations.Count - 1)
                                    {
                                        v_selection_cursor++;
                                    }
                                }
                                if (input5.Key == ConsoleKey.UpArrow)
                                {
                                    if (v_selection_cursor > 0)
                                    {
                                        v_selection_cursor--;
                                    }
                                }


                                //Display interactive page (Vehicle Route Selection)
                                Console.Clear();
                                displayer.vehicle_route_selection_display(vehicles, stations, display_unavailable_s_error, cursor, v_selection_cursor);
                                

                                if (input5.Key == ConsoleKey.Enter)
                                    display_unavailable_s_error = false;
                                {
                                    if (stations[v_selection_cursor].x_val == vehicles[cursor].x_val && stations[v_selection_cursor].y_val == vehicles[cursor].y_val)
                                    { 
                                        display_unavailable_s_error = true;
                                    }
                                    if (!display_unavailable_s_error)
                                    {
                                        vehicles[cursor].move_to_station(stations[v_selection_cursor]);
                                        v_selection_made = false;
                                    }
                                }
                            }

                            //Display intital page after route selection (Vehicle Selection)
                            Console.Clear();
                            displayer.vehicle_selection_display(vehicles, display_unavailable_v_error, cursor, rename_vehicle, vehicle_chosen_created, chosen_vehicle_created);
                        }


                        //Vehicle creation page
                        //--------------------------------
                        bool vehicle_created = true;
                        int v_creation_cursor = 0;

                        if (input4.Key == ConsoleKey.N)
                        {
                            //Display initial page (Vehicle Creation)
                            Console.Clear();
                            displayer.vehicle_creation_display(vehicles, types_of_vehicles, v_creation_cursor, vehicle_chosen_created, chosen_vehicle_created);


                            while (vehicle_created)
                            {
                                var input6 = Console.ReadKey();

                                if (input6.Key == ConsoleKey.Escape)
                                {
                                    vehicle_created = false;
                                }
                                if (input6.Key == ConsoleKey.DownArrow)
                                {
                                    if (v_creation_cursor < types_of_vehicles.Count - 1)
                                    {
                                        v_creation_cursor++;
                                    }
                                }
                                if (input6.Key == ConsoleKey.UpArrow)
                                {
                                    if (v_creation_cursor > 0)
                                    {
                                        v_creation_cursor--;
                                    }
                                }
                                if (input6.Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine(types_of_vehicles[v_creation_cursor].v_type);
                                    if (types_of_vehicles[v_creation_cursor].v_type == "Bus")
                                    {
                                        vehicles.Add(new Bus("bus" + vehicles.Count));
                                        vehicle_chosen_created = true;
                                        chosen_vehicle_created = "bus";
                                        vehicle_created = false;
                                    }
                                    else if (types_of_vehicles[v_creation_cursor].v_type == "Truck")
                                    {
                                        vehicles.Add(new Truck("truck" + vehicles.Count));
                                        vehicle_chosen_created = true;
                                        chosen_vehicle_created = "truck";
                                        vehicle_created = false;
                                    }



                                }

                                //Display interactive page (Vehicle Selection)
                                Console.Clear();
                                displayer.vehicle_creation_display(vehicles, types_of_vehicles, v_creation_cursor, vehicle_chosen_created, chosen_vehicle_created);
                            }


                        }
                        //display initial page (vehicle selection)
                        Console.Clear();
                        displayer.vehicle_selection_display(vehicles, display_unavailable_v_error, cursor, rename_vehicle, vehicle_chosen_created, chosen_vehicle_created);
                    }

                    Console.Clear();
                    displayer.main_display(clock, vehicles);
                }
            }
        }
    }
}

