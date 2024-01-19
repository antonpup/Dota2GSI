using Dota2GSI;
using Dota2GSI.EventMessages;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

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

            // There are many callbacks that can be subscribed.
            // This example shows a few.
            // _gsl.NewGameState += OnNewGameState; // `NewGameState` can be used alongside `GameEvent`. Just not in this example.
            _gsl.GameEvent += OnGameEvent; // `GameEvent` can be used alongside `NewGameState`.
            _gsl.TimeOfDayChanged += OnTimeOfDayChanged;
            _gsl.TeamScoreChanged += OnTeamScoreChanged;
            _gsl.PauseStateChanged += OnPauseStateChanged;
            _gsl.PlayerGameplayEvent += OnPlayerGameplayEvent;
            _gsl.TeamGameplayEvent += OnTeamGameplayEvent;
            _gsl.InventoryItemAdded += OnInventoryItemAdded;
            _gsl.InventoryItemRemoved += OnInventoryItemRemoved;

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

        private static void OnTimeOfDayChanged(TimeOfDayChanged game_event)
        {
            Console.WriteLine($"Is daytime: {game_event.IsDaytime} Is Nightstalker night: {game_event.IsNightstalkerNight}");
        }
        private static void OnTeamScoreChanged(TeamScoreChanged game_event)
        {
            Console.WriteLine($"New score for {game_event.Team} is {game_event.New}");
        }

        private static void OnPauseStateChanged(PauseStateChanged game_event)
        {
            Console.WriteLine($"New pause state is {(game_event.New ? "paused" : "not paused")}");
        }

        private static void OnPlayerGameplayEvent(PlayerGameplayEvent game_event)
        {
            Console.WriteLine($"Player {game_event.Player.Details.Name} did a thing: " + game_event.Value.EventType);
        }

        private static void OnTeamGameplayEvent(TeamGameplayEvent game_event)
        {
            Console.WriteLine($"Team {game_event.Team} did a thing: " + game_event.Value.EventType);
        }

        private static void OnInventoryItemAdded(InventoryItemAdded game_event)
        {
            Console.WriteLine($"Player {game_event.Player.Details.Name} gained an item in their inventory: " + game_event.Value.Name);
        }

        private static void OnInventoryItemRemoved(InventoryItemRemoved game_event)
        {
            Console.WriteLine($"Player {game_event.Player.Details.Name} lost an item from their inventory: " + game_event.Value.Name);
        }

        private static void OnGameEvent(DotaGameEvent game_event)
        {
            if (game_event is ProviderUpdated provider)
            {
                Console.WriteLine($"Current Game version: {provider.New.Version}");
                Console.WriteLine($"Current Game time stamp: {provider.New.TimeStamp}");
            }
            else if (game_event is PlayerDetailsChanged player_details)
            {
                Console.WriteLine($"Player Name: {player_details.New.Name}");
                Console.WriteLine($"Player Account ID: {player_details.New.AccountID}");
            }
            else if (game_event is HeroDetailsChanged hero_details)
            {
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero ID: " + hero_details.New.ID);
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero XP: " + hero_details.New.Experience);
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero has Aghanims Shard upgrade: " + hero_details.New.HasAghanimsShardUpgrade);
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero Health: " + hero_details.New.Health);
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero Mana: " + hero_details.New.Mana);
                Console.WriteLine($"Player {hero_details.Player.Details.Name} Hero Location: " + hero_details.New.Location);
            }
            else if (game_event is AbilityUpdated ability)
            {
                Console.WriteLine($"Player {ability.Player.Details.Name} updated their ability: " + ability.New);
            }
            else if (game_event is TowerUpdated tower_updated)
            {
                if (tower_updated.New.Health < tower_updated.Previous.Health)
                {
                    Console.WriteLine($"{tower_updated.Team} {tower_updated.Location} tower is under attack! Health: " + tower_updated.New.Health);
                }
                else if (tower_updated.New.Health > tower_updated.Previous.Health)
                {
                    Console.WriteLine($"{tower_updated.Team} {tower_updated.Location} tower is being healed! Health: " + tower_updated.New.Health);
                }
            }
            else if (game_event is TowerDestroyed tower_destroyed)
            {
                Console.WriteLine($"{tower_destroyed.Team} {tower_destroyed.Location} tower is destroyed!");
            }
            else if (game_event is RacksUpdated racks_updated)
            {
                if (racks_updated.New.Health < racks_updated.Previous.Health)
                {
                    Console.WriteLine($"{racks_updated.Team} {racks_updated.Location} {racks_updated.RacksType} racks are under attack! Health: " + racks_updated.New.Health);
                }
                else if (racks_updated.New.Health > racks_updated.Previous.Health)
                {
                    Console.WriteLine($"{racks_updated.Team} {racks_updated.Location} {racks_updated.RacksType} tower are being healed! Health: " + racks_updated.New.Health);
                }
            }
            else if (game_event is RacksDestroyed racks_destroyed)
            {
                Console.WriteLine($"{racks_destroyed.Team} {racks_destroyed.Location} {racks_destroyed.RacksType} racks is destroyed!");
            }
            else if (game_event is AncientUpdated ancient_updated)
            {
                if (ancient_updated.New.Health < ancient_updated.Previous.Health)
                {
                    Console.WriteLine($"{ancient_updated.Team} ancient is under attack! Health: " + ancient_updated.New.Health);
                }
                else if (ancient_updated.New.Health > ancient_updated.Previous.Health)
                {
                    Console.WriteLine($"{ancient_updated.Team} ancient is being healed! Health: " + ancient_updated.New.Health);
                }
            }
            else if (game_event is TeamNeutralItemsUpdated team_neutral_items_updated)
            {
                Console.WriteLine($"{team_neutral_items_updated.Team} neutral items updated: {team_neutral_items_updated.New}");
            }
            else if (game_event is CourierUpdated courier_updated)
            {
                Console.WriteLine($"Player {courier_updated.Player.Details.Name} courier updated: {courier_updated.New}");
            }
            else if (game_event is TeamDraftDetailsUpdated draft_details_updated)
            {
                Console.WriteLine($"{draft_details_updated.Team} draft details updated: {draft_details_updated.New}");
            }
            else if (game_event is TeamDefeat team_defeat)
            {
                Console.WriteLine($"{team_defeat.Team} lost the game.");
            }
            else if (game_event is TeamVictory team_victory)
            {
                Console.WriteLine($"{team_victory.Team} won the game!");
            }
        }

        // NewGameState example

        static void OnNewGameState(GameState gs)
        {
            Console.Clear();

            Console.WriteLine("Current Dota version: " + gs.Provider.Version);
            Console.WriteLine("Your steam name: " + gs.Player.LocalPlayer.Name);
            Console.WriteLine("Your dota account id: " + gs.Player.LocalPlayer.AccountID);

            Console.WriteLine("Current time as displayed by the clock (in seconds): " + gs.Map.ClockTime);

            Console.WriteLine("Radiant Score: " + gs.Map.RadiantScore);
            Console.WriteLine("Dire Score: " + gs.Map.DireScore);
            Console.WriteLine("Is game paused: " + gs.Map.IsPaused);

            Console.WriteLine("Hero ID: " + gs.Hero.LocalPlayer.ID);
            Console.WriteLine("Hero XP: " + gs.Hero.LocalPlayer.Experience);
            Console.WriteLine("Hero has Aghanims Shard upgrade: " + gs.Hero.LocalPlayer.HasAghanimsShardUpgrade);

            Console.WriteLine("Hero Health: " + gs.Hero.LocalPlayer.Health);
            for (int i = 0; i < gs.Abilities.LocalPlayer.Count; i++)
            {
                Console.WriteLine("Ability {0} = {1}", i, gs.Abilities.LocalPlayer[i].Name);
            }

            Console.WriteLine("First slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(0).Name);
            Console.WriteLine("Second slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(1).Name);
            Console.WriteLine("Third slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(2).Name);
            Console.WriteLine("Fourth slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(3).Name);
            Console.WriteLine("Fifth slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(4).Name);
            Console.WriteLine("Sixth slot inventory: " + gs.Items.LocalPlayer.GetInventoryAt(5).Name);

            Console.WriteLine("Teleport item slot: " + gs.Items.LocalPlayer.Teleport.Name);
            Console.WriteLine("Neutral item slot: " + gs.Items.LocalPlayer.Neutral.Name);

            if (gs.Items.LocalPlayer.InventoryContains("item_blink"))
            {
                Console.WriteLine("You have a blink dagger");
            }
            else
            {
                Console.WriteLine("You DO NOT have a blink dagger");
            }

            Console.WriteLine("Talent Tree:");
            for (int i = gs.Hero.LocalPlayer.TalentTree.Length - 1; i >= 0; i--)
            {
                var level = gs.Hero.LocalPlayer.TalentTree[i];
                Console.WriteLine($"{level}");
            }

            foreach (var game_event in gs.Events)
            {
                if (game_event.EventType == Dota2GSI.Nodes.EventsProvider.EventType.Bounty_rune_pickup)
                {
                    Console.WriteLine("Bounty rune was picked up!");
                    break;
                }
                else if (game_event.EventType == Dota2GSI.Nodes.EventsProvider.EventType.Roshan_killed)
                {
                    Console.WriteLine("Roshan was brutally killed!");
                    break;
                }
            }

            Console.WriteLine("Press ESC to quit");
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
                    "        \"auth\"           \"1\"",
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
