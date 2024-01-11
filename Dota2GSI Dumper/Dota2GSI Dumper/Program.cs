using System.Diagnostics;
using Dota2GSI;
using Dota2GSI.Nodes;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Dota2GSI_Dumper
{
    class Program
    {
        private static string _dumps_directory = @$".\dumps\";
        private static string _dumps_directory_spectator = @$".\dumps_spectator\";

        private static bool _test_gsi_components = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Dota 2 GSI Dumper");
            Console.WriteLine("=================");

            do
            {
                Console.WriteLine("[1] Record spectator GSI states.");
                Console.WriteLine("[2] Record playing GSI states.");
                Console.WriteLine("[3] Generate known values json.");
                Console.WriteLine($"[T] Test GameState components during recording. [{_test_gsi_components}]");
                Console.WriteLine("[X] Exit");
                Console.Write("Select option: ");

                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        RecordSpectatorGSI();
                        break;
                    case "2":
                        RecordPlayingGSI();
                        break;
                    case "3":
                        ExtractSeenValues();
                        break;
                    case "t":
                    case "T":
                        _test_gsi_components = !_test_gsi_components;
                        break;
                    case "x":
                    case "X":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            while (true);
        }

        private static void TestGSIComponents(GameState state)
        {
            // Fetch every component to ensure no exceptions
            // No try-catch so that the exception is brought up immedeately
            var a = state.Auth;
            var b = state.Provider;
            var c = state.Map;
            var e = state.Player;
            var f = state.Hero;
            var g = state.Abilities;
            var h = state.Items;
            var i = state.Events;
            var j = state.Buildings;
            var k = state.League;
            var l = state.Draft;
            var m = state.Wearables;
            var n = state.Minimap;
            var o = state.Roshan;
            var p = state.Couriers;
            var q = state.NeutralItems;
            var r = state.Previously;
        }

        private static void RecordSpectatorGSI()
        {
            // Ensure the dumps directory exists.
            Directory.CreateDirectory(_dumps_directory_spectator);

            var gsl = new GameStateListener(4000);
            gsl.NewGameState += (GameState state) =>
            {
                Node state_node = state as Node;

                if (state_node != null)
                {
                    var node_json = state_node.ToString();
                    var save_path = _dumps_directory_spectator + @$"{Guid.NewGuid()}.json";
                    File.WriteAllText(save_path, node_json);
                }

                if (_test_gsi_components)
                {
                    TestGSIComponents(state);
                }
            };

            if (!gsl.Start())
            {
                Console.WriteLine("GameStateListener could not start. Try running this program as Administrator.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Capturing GSI events.");
            Console.WriteLine("Press ESC to stop.");

            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            gsl.Stop();
        }

        private static void RecordPlayingGSI()
        {
            // Ensure the dumps directory exists.
            Directory.CreateDirectory(_dumps_directory);

            var gsl = new GameStateListener(4000);
            gsl.NewGameState += (GameState state) =>
            {
                Node state_node = state as Node;

                if (state_node != null)
                {
                    var node_json = state_node.ToString();
                    var save_path = _dumps_directory + @$"{Guid.NewGuid()}.json";
                    File.WriteAllText(save_path, node_json);
                }

                if (_test_gsi_components)
                {
                    TestGSIComponents(state);
                }
            };

            if (!gsl.Start())
            {
                Console.WriteLine("GameStateListener could not start. Try running this program as Administrator.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Capturing GSI events.");
            Console.WriteLine("Press ESC to stop.");

            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            gsl.Stop();
        }

        private static void ProcessJsonObject(ref JObject seen_obj, JObject obj)
        {
            foreach (var property in obj.Properties())
            {
                string property_name = property.Name;

                if (property.Value.Type == JTokenType.Object)
                {
                    if (Regex.IsMatch(property_name, @"ability\d+"))
                    {
                        property_name = "ability#";
                    }
                    else if (Regex.IsMatch(property_name, @"slot\d+"))
                    {
                        property_name = "slot#";
                    }
                    else if (Regex.IsMatch(property_name, @"stash\d+"))
                    {
                        property_name = "stash#";
                    }
                    else if (Regex.IsMatch(property_name, @"teleport\d+"))
                    {
                        property_name = "teleport#";
                    }
                    else if (Regex.IsMatch(property_name, @"neutral\d+"))
                    {
                        property_name = "neutral#";
                    }
                    else if (Regex.IsMatch(property_name, @"player\d+"))
                    {
                        property_name = "player#";
                    }
                    else if (Regex.IsMatch(property_name, @"team\d+"))
                    {
                        property_name = "team#";
                    }
                    else if (Regex.IsMatch(property_name, @"tier\d+"))
                    {
                        property_name = "tier#";
                    }
                    else if (Regex.IsMatch(property_name, @"item\d+"))
                    {
                        property_name = "item#";
                    }
                    else if (Regex.IsMatch(property_name, @"o\d+"))
                    {
                        property_name = "o#";
                    }
                    else if (Regex.IsMatch(property_name, @"courier\d+"))
                    {
                        property_name = "courier#";
                    }
                    else if (Regex.IsMatch(property_name, @"victimid_\d+"))
                    {
                        property_name = "victimid_#";
                    }
                    else if (Regex.IsMatch(property_name, @"victimid_\d+"))
                    {
                        property_name = "victimid_#";
                    }
                    else if (Regex.IsMatch(property_name, @"style\d+"))
                    {
                        property_name = "style#";
                    }
                    else if (Regex.IsMatch(property_name, @"wearable\d+"))
                    {
                        property_name = "wearable#";
                    }
                }

                try
                {
                    var seen_property = seen_obj.Properties().First(seen_property => seen_property.Name.Equals(property_name));

                    // If exception hasn't been thrown yet, we have already seen the value.
                    if (seen_property.Value.Type == JTokenType.Object && property.Value.Type == JTokenType.Object)
                    {
                        // Need to compare sub-properties
                        JObject sub_object = seen_property.Value as JObject;
                        ProcessJsonObject(ref sub_object, property.Value.ToObject<JObject>());
                    }
                    else if (seen_property.Value.Type == JTokenType.Array && property.Value.Type == JTokenType.Array)
                    {
                        // Need to compare two arrays
                        JArray sub_array = seen_property.Value as JArray;

                        foreach (var element in property.Value.Children())
                        {
                            bool is_contained = sub_array.Children().Contains(element);

                            if (is_contained)
                            {
                                continue;
                            }

                            sub_array.Add(element);
                        }
                    }
                    else if (seen_property.Value.Type == JTokenType.Array && property.Value.Type != JTokenType.Object)
                    {
                        // Add to array
                        JArray sub_array = seen_property.Value as JArray;
                        bool is_contained = sub_array.Values().Contains(property.Value);

                        if (is_contained)
                        {
                            continue;
                        }

                        if (property.Value.Type == JTokenType.Integer && sub_array.Values().Count() >= 3)
                        {
                            // No need for more than 3 examples for int values.
                            continue;
                        }

                        sub_array.Add(property.Value);
                    }

                    continue;
                }
                catch (Exception exc)
                {
                }

                if (property.Value.Type == JTokenType.Object)
                {
                    // Need to compare sub-properties
                    seen_obj.Add(property_name, new JObject());
                }
                else if (property.Value.Type == JTokenType.Array)
                {
                    // Add to array
                    JArray seen_value = [.. property.Value.Children()];
                    seen_obj.Add(property_name, seen_value);
                }
                else if (property.Value.Type != JTokenType.Object)
                {
                    // Add to array
                    JArray seen_value = [property.Value];
                    seen_obj.Add(property_name, seen_value);
                }
            }
        }


        private static void ExtractSeenValues()
        {
            string playing_json_path = @$".\playing_gsi.json";
            JObject playing_json_values = ExtractSeenValues(_dumps_directory);
            File.WriteAllText(playing_json_path, playing_json_values.ToString());
            Console.WriteLine($"Saved {playing_json_path}");

            string spectator_json_path = @$".\spectator_gsi.json";
            JObject spectator_json_values = ExtractSeenValues(_dumps_directory_spectator);
            File.WriteAllText(spectator_json_path, spectator_json_values.ToString());
            Console.WriteLine($"Saved {spectator_json_path}");
        }

        private static JObject ExtractSeenValues(string dumps_directory)
        {
            JObject seen_json_values = new JObject();

            if (Directory.Exists(dumps_directory))
            {
                foreach (var file in Directory.GetFiles(dumps_directory))
                {
                    using (StreamReader r = new StreamReader(file))
                    {
                        string json_data = r.ReadToEnd();
                        if (string.IsNullOrEmpty(json_data))
                        {
                            continue;
                        }

                        var parsed_json = JObject.Parse(json_data);

                        ProcessJsonObject(ref seen_json_values, parsed_json);
                    }
                }
            }

            return seen_json_values;
        }
    }
}
