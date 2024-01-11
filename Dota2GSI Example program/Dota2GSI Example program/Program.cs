using System;
using System.Diagnostics;
using System.IO;
using Dota2GSI;
using Microsoft.Win32;
using System.Threading;
using System.Text;
using Dota2GSI.Nodes;

namespace Dota2GSI_Example_program
{
    class Program
    {
        static GameStateListener _gsl;

        static void Main(string[] args)
        {
            CreateGsifile();

            Process[] pname = Process.GetProcessesByName("Dota2");
            if (pname.Length == 0)
            {
                Console.WriteLine("Dota 2 is not running. Please start Dota 2.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            _gsl = new GameStateListener(4000);
            _gsl.NewGameState += OnNewGameState;

            if (!_gsl.Start())
            {
                Console.WriteLine("GameStateListener could not start. Try running this program as Administrator. Exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Listening for game integration calls...");

            Console.WriteLine("Press ESC to quit");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
        static void OnNewGameState(GameState gs)
        {
            Console.Clear();
            
            Console.WriteLine("Press ESC to quit");
            Console.WriteLine("Current Dota version: " + gs.Provider.Version);
            Console.WriteLine("Current time as displayed by the clock (in seconds): " + gs.Map.ClockTime);

            if (gs.IsSpectator)
            {
                foreach (var team in gs.Teams?.All)
                {
                    Console.WriteLine($"Team {team.Key}:");
                    foreach (var player in team.Value)
                    {
                        Player plyr = player.Value.Player;
                        Hero hero = player.Value.Hero;
                        Abilities abilities = player.Value.Abilities;
                        Items items = player.Value.Items;

                        Console.WriteLine($"{plyr.Name}|{hero.Name}|{abilities.Count}|{items.CountInventory}");
                        Console.WriteLine("Talent Tree:");
                        for (int i = hero.TalentTree.Length - 1; i >= 0; i--)
                        {
                            var level = hero.TalentTree[i];
                            Console.WriteLine($"{level}");
                        }

                        Console.WriteLine("Kill list:");
                        foreach (var item in plyr.KillList)
                        {
                            Console.WriteLine($"{item.Key}|{item.Value}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Your steam name: " + gs.Player.Name);
                Console.WriteLine("Your dota account id: " + gs.Player.AccountID);

                Console.WriteLine("Your hero healing: " + gs.Player.HeroHealing);
                Console.WriteLine("Your hero damage: " + gs.Player.HeroDamage);
                Console.WriteLine("Your hero tower damage: " + gs.Player.TowerDamage);

                Console.WriteLine("Radiant Score: " + gs.Map.RadiantScore);
                Console.WriteLine("Dire Score: " + gs.Map.DireScore);
                Console.WriteLine("Is game paused: " + gs.Map.IsPaused);

                Console.WriteLine("Hero ID: " + gs.Hero.ID);
                Console.WriteLine("Hero XP: " + gs.Hero.Experience);
                Console.WriteLine("Hero has Aghanims Shard upgrade: " + gs.Hero.HasAghanimsShardUpgrade);

                Console.WriteLine("Hero Health: " + gs.Hero.Health);
                for (int i = 0; i < gs.Abilities.Count; i++)
                {
                    Console.WriteLine("Ability {0} = {1}", i, gs.Abilities[i].Name);
                }
                Console.WriteLine("First slot inventory: " + gs.Items.GetInventoryAt(0).Name);
                Console.WriteLine("Second slot inventory: " + gs.Items.GetInventoryAt(1).Name);
                Console.WriteLine("Third slot inventory: " + gs.Items.GetInventoryAt(2).Name);
                Console.WriteLine("Fourth slot inventory: " + gs.Items.GetInventoryAt(3).Name);
                Console.WriteLine("Fifth slot inventory: " + gs.Items.GetInventoryAt(4).Name);
                Console.WriteLine("Sixth slot inventory: " + gs.Items.GetInventoryAt(5).Name);

                Console.WriteLine("Teleport item slot: " + gs.Items.Teleport.Name);
                Console.WriteLine("Neutral item slot: " + gs.Items.Neutral.Name);

                if (gs.Items.InventoryContains("item_blink"))
                {
                    Console.WriteLine("You have a blink dagger");
                }
                else
                {
                    Console.WriteLine("You DO NOT have a blink dagger");
                }

                Console.WriteLine("Talent Tree:");
                for (int i = gs.Hero.TalentTree.Length - 1; i >= 0; i--)
                {
                    var level = gs.Hero.TalentTree[i];
                    Console.WriteLine($"{level}");
                }
            }
        }

        private static void CreateGsifile()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");

            if (regKey != null)
            {
                string gsifolder = regKey.GetValue("SteamPath") +
                                   @"\steamapps\common\dota 2 beta\game\dota\cfg\gamestate_integration";
                Directory.CreateDirectory(gsifolder);
                string gsifile = gsifolder + @"\gamestate_integration_testGSI.cfg";
                if (File.Exists(gsifile))
                    return;

                string[] contentofgsifile =
                {
                    "\"Dota 2 Integration Configuration\"",
                    "{",
                    "    \"uri\"           \"http://localhost:4000\"",
                    "    \"timeout\"       \"5.0\"",
                    "    \"buffer\"        \"0.1\"",
                    "    \"throttle\"      \"0.1\"",
                    "    \"heartbeat\"     \"30.0\"",
                    "    \"data\"",
                    "    {",
                    "        \"provider\"       \"1\"",
                    "        \"map\"            \"1\"",
                    "        \"player\"         \"1\"",
                    "        \"hero\"           \"1\"",
                    "        \"abilities\"      \"1\"",
                    "        \"items\"          \"1\"",
                    "        \"events\"         \"1\"",
                    "        \"buildings\"      \"1\"",
                    "        \"league\"         \"1\"",
                    "        \"draft\"          \"1\"",
                    "        \"wearables\"      \"1\"",
                    "        \"minimap\"        \"1\"",
                    "        \"roshan\"         \"1\"",
                    "        \"couriers\"       \"1\"",
                    "        \"neutralitems\"   \"1\"",
                    "    }",
                    "}",

                };

                File.WriteAllLines(gsifile, contentofgsifile);
            }
            else
            {
                Console.WriteLine("Registry key for steam not found, cannot create Gamestate Integration file");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
