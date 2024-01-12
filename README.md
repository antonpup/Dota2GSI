# Dota 2 GSI (Game State Integration)
A C# library to interface the Game State Integration found in Dota 2.

## What is Game State Integration

Game State Integration hasn't been officially released for Dota 2, but it has been available for Counter-Strike: Global Offensive for a few months now. The concept is the same as CSGO's, you can read about [Counter-Strike Game State Integration here](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration).

## About Dota 2 GSI

This library provides easy means of implementing Game State Integration from Dota 2 into C# applications. Library listens for HTTP POST requests made by the game on a specific address and port. Upon receiving a request, the game state is parsed and can be used.

JSON parsing is done though help of Newtonsoft's [JSON.Net Framework](http://www.newtonsoft.com/json).

After starting the `GameStateListener` instance, it will continuously listen for incoming HTTP requests. Upon a received request, the contents will be parsed into a `GameState` object.


## Installation
Via NuGet:

```
Install-Package Dota2GSI
```

Manual installation:

1. Get the [latest binaries](https://github.com/antonpup/Dota2GSI/releases/latest)  
2. Get the [JSON Framework .dll by Newtonsoft](https://github.com/JamesNK/Newtonsoft.Json/releases)  
3. Extract Newtonsoft.Json.dll from `Bin\Net45\Newtonsoft.Json.dll`  
4. Add a reference to both Dota2GSI.dll and Newtonsoft.Json.dll in your project  

## Usage
1. Create a `GameStateListener` instance by providing a port or passing a specific URI:

```C#
GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/
GameStateListener gsl = new GameStateListener("http://127.0.0.1:81/");
```

**Please note**: If your application needs to listen to a URI other than `http://localhost:*/` (for example `http://192.168.2.2:100/`), you need to ensure that it is run with administrator privileges.  
In this case, `http://127.0.0.1:*/` is **not** equivalent to `http://localhost:*/`.

2. Create a handler:

```C#
void OnNewGameState(GameState gs)
{
    //do stuff
}
```

3. Subscribe to the `NewGameState` event:

```C#
gsl.NewGameState += new NewGameStateHandler(OnNewGameState);
```

4. Use `GameStateListener.Start()` to start listening for HTTP POST requests from the game client. This method will return `false` if starting the listener fails (most likely due to insufficient privileges).

## Layout

```
GameState
+-- Auth
|   +-- Token
+-- Provider
|   +-- Name
|   +-- AppID
|   +-- Version
|   +-- TimeStamp
+-- Map
|   +-- Name
|   +-- MatchID
|   +-- GameTime
|   +-- ClockTime
|   +-- IsDaytime
|   +-- IsNightstalkerNight
|   +-- RadiantScore
|   +-- DireScore
|   +-- GameState
|   +-- IsPaused
|   +-- Winningteam
|   +-- CustomGameName
|   +-- RadiantWardPurchaseCooldown
|   +-- DireWardPurchaseCooldown
|   +-- RoshanState
|   +-- RoshanStateEndTime
|   +-- WardPurchaseCooldown
+-- Player
|   +-- LocalPlayer
|   |   +-- SteamID
|   |   +-- AccountID
|   |   +-- Name
|   |   +-- Activity
|   |   +-- Kills
|   |   +-- Deaths
|   |   +-- Assists
|   |   +-- LastHits
|   |   +-- Denies
|   |   +-- KillStreak
|   |   +-- CommandsIssued
|   |   +-- KillList
|   |   +-- Team
|   |   +-- PlayerSlot
|   |   +-- PlayerTeamSlot
|   |   +-- Gold
|   |   +-- GoldReliable
|   |   +-- GoldUnreliable
|   |   +-- GoldFromHeroKills
|   |   +-- GoldFromCreepKills
|   |   +-- GoldFromIncome
|   |   +-- GoldFromShared
|   |   +-- GoldPerMinute
|   |   +-- ExperiencePerMinute
|   |   +-- OnstageSeat
|   |   +-- NetWorth
|   |   +-- HeroDamage
|   |   +-- HeroHealing
|   |   +-- TowerDamage
|   |   +-- WardsPurchased
|   |   +-- WardsPlaced
|   |   +-- WardsDestroyed
|   |   +-- RunesActivated
|   |   +-- CampsStacked
|   |   +-- SupportGoldSpent
|   |   +-- ConsumableGoldSpent
|   |   +-- ItemGoldSpent
|   |   +-- GoldLostToDeath
|   |   +-- GoldSpentOnBuybacks
|   +-- Teams
|   |   ...
|   +-- GetTeam( team )
|   +-- GetPlayer( player_id )
+-- Hero
|   +-- LocalPlayer
|   |   +-- Location
|   |   +-- ID
|   |   +-- Name
|   |   +-- Level
|   |   +-- Experience
|   |   +-- IsAlive
|   |   +-- SecondsToRespawn
|   |   +-- BuybackCost
|   |   +-- BuybackCooldown
|   |   +-- Health
|   |   +-- MaxHealth
|   |   +-- HealthPercent
|   |   +-- Mana
|   |   +-- MaxMana
|   |   +-- ManaPercent
|   |   +-- IsSilenced
|   |   +-- IsStunned
|   |   +-- IsDisarmed
|   |   +-- IsMagicImmune
|   |   +-- IsHexed
|   |   +-- IsMuted
|   |   +-- IsBreak
|   |   +-- HasAghanimsScepterUpgrade
|   |   +-- HasAghanimsShardUpgrade
|   |   +-- IsSmoked
|   |   +-- HasDebuff
|   |   +-- SelectedUnit
|   |   +-- TalentTree[]
|   |   +-- AttributesLevel
|   +-- Teams
|   |   ...
|   +-- GetTeam( team )
|   +-- GetPlayer( player_id )
+-- Abilities
|   +-- LocalPlayer
|   |   +-- Count
|   |   +-- Attributes
|   |   +-- Ability[]
|   |   |   +-- Name
|   |   |   +-- Level
|   |   |   +-- CanCast
|   |   |   +-- IsPassive
|   |   |   +-- IsActive
|   |   |   +-- Cooldown
|   |   |   +-- IsUltimate
|   |   |   +-- Charges
|   |   |   +-- MaxCharges
|   |   |   +-- ChargeCooldown
|   +-- Teams
|   |   ...
|   +-- GetTeam( team )
|   +-- GetPlayer( player_id )
+-- Items
|   +-- LocalPlayer
|   |   +-- Inventory
|   |   +-- Stash
|   |   +-- CountInventory
|   |   +-- CountStash
|   |   +-- Teleport
|   |   +-- Neutral
|   |   +-- GetInventoryAt( index )
|   |   +-- GetInventoryItem( item_name )
|   |   +-- InventoryContains( item_name )
|   |   +-- InventoryIndexOf( item_name )
|   |   +-- GetStashAt( index )
|   |   +-- GetStashItem( item_name )
|   |   +-- StashContains( item_name )
|   |   +-- StashIndexOf( item_name )
|   +-- Teams
|   |   ...
|   +-- GetTeam( team )
|   +-- GetPlayer( player_id )
+-- Events[]
|   +-- GameTime
|   +-- EventType
|   +-- Team
|   +-- KillerPlayerID
|   +-- PlayerID
|   +-- WasSnatched
|   +-- TipReceiverPlayerID
|   +-- TipAmount
|   +-- BountyValue
|   +-- TeamGold
+-- Buildings
|   +-- RadiantBuildings
|   |   +-- TopTowers
|   |   +-- MiddleTowers
|   |   +-- BottomTowers
|   |   +-- TopRacks
|   |   +-- MiddleRacks
|   |   +-- BottomRacks
|   |   +-- Ancient
|   |   +-- OtherBuildings
|   +-- DireBuildings
|   |   ...
|   +-- AllBuildings
|   |   ...
+-- League
|   +-- SeriesType
|   +-- SelectionPriority
|   +-- LeagueID
|   +-- MatchID
|   +-- Name
|   +-- Tier
|   +-- Region
|   +-- Url
|   +-- Description
|   +-- Notes
|   +-- StartTimestamp
|   +-- EndTimestamp
|   +-- ProCircuitPoints
|   +-- ImageBits
|   +-- Status
|   +-- MostRecentActivity
|   +-- RegistrationPeriod
|   +-- BasePrizePool
|   +-- TotalPrizePool
|   +-- LeagueNoteID
|   +-- RadiantTeam
|   |   +-- TeamID
|   |   +-- TeamTag
|   |   +-- TeamName
|   |   +-- SeriesWins
|   +-- DireTeam
|   |   ...
|   +-- SeriesID
|   +-- StartTime
|   +-- FirstTeamID
|   +-- SecondTeamID
|   +-- Stream[]
|   |   +-- StreamID
|   |   +-- Language
|   |   +-- Name
|   |   +-- BroadcastProvider
|   |   +-- StreamURL
|   |   +-- VodURL
+-- Draft
|   +-- ActiveTeam
|   +-- Pick
|   +-- ActiveTeamRemainingTime
|   +-- RadiantBonusTime
|   +-- DireBonusTime
|   +-- Teams
|   |   +-- IsHomeTeam
|   |   +-- PickIDs
|   |   +-- PickHeroIDs
|   +-- GetTeam( team )
+-- Wearables
|   +-- LocalPlayer
|   |   +-- Wearables
|   |   |   +-- ID
|   |   |   +-- Style
|   +-- Teams
|   |   ...
|   +-- GetTeam( team )
|   +-- GetPlayer( player_id )
+-- Minimap
|   +-- Elements
|   |   +-- Location
|   |   +-- RemainingTime
|   |   +-- EventDuration
|   |   +-- Image
|   |   +-- Team
|   |   +-- Name
|   |   +-- Rotation
|   |   +-- UnitName
|   |   +-- VisionRange
+-- Roshan
|   +-- Location
|   +-- Health
|   +-- MaxHealth
|   +-- IsAlive
|   +-- SpawnPhase
|   +-- PhaseTimeRemaining
|   +-- Rotation
|   +-- Drops
|   |   +-- Items
+-- Couriers
|   +-- CouriersMap
|   |   +-- Health
|   |   +-- MaxHealth
|   |   +-- IsAlive
|   |   +-- RemainingRespawnTime
|   |   +-- Location
|   |   +-- Rotation
|   |   +-- OwnerID
|   |   +-- HasFlyingUpgrade
|   |   +-- IsShielded
|   |   +-- IsBoosted
|   |   +-- Items
|   |   |   +-- OwnerID
|   |   |   +-- Name
+-- NeutralItems
|   +-- TierInfos
|   |   +-- Tier
|   |   +-- MaxCount
|   |   +-- DropAfterTime
|   +-- TeamItems
|   |   +-- ItemsFound
|   |   +-- TeamItems
|   |   |   +-- Name
|   |   |   +-- Tier
|   |   |   +-- Charges
|   |   |   +-- State
|   |   |   +-- PlayerID
|   +-- GetTeam( team )
+-- Previously (Previous information from Game State)
```

### Item, and Hero names
Item and hero names are presented in their "internal name" format. A full list of item names can be found [here](http://dota2.gamepedia.com/Cheats#Item_names) and a full list of heroes can be located [here](http://dota2.gamepedia.com/Cheats#Hero_names).

##### Examples:
```C#
int Health = gs.Hero.LocalPlayer.Health; // 560
int MaxHealth = gs.Hero.LocalPlayer.MaxHealth; // 560
string HeroName = gs.Hero.LocalPlayer.Name; //npc_dota_hero_omniknight
int Level = gs.Hero.LocalPlayer.Level; //1

Console.WriteLine("You are playing as " + HeroName + " with " + Health + "/" + MaxHealth + " health and level " + Level);
//You are playing as npc_dota_hero_omniknight with 560/560 health and level 1

```

## Null value handling

In case the JSON did not contain the requested information, these values will be returned:

Type    |Default value
--------|-------------
bool    | false
int     | -1
long    | -1
float   | -1
string  | String.Empty


All Enums have a value `enum.Undefined` that serves the same purpose.

## Example program

A user, [judge2020](https://github.com/judge2020), has created an example program to demonstrate Dota2GSI functionalities. It can be found in the "Dota2GSI Example program" folder.

## Example implementation

Prints "You bought an item" when you buy an item, and "It is night time" when it is night time.

```C#
using Dota2GSI;
using System;

namespace DOTA2GSI_sample
{
    static class Program
    {
        GameStateListener gsl;
        
        static void Main(string[] args)
        {
            gsl = new GameStateListener(4000);
            gsl.NewGameState += new NewGameStateHandler(OnNewGameState);

            if (!gsl.Start())
            {
                System.Windows.MessageBox.Show("GameStateListener could not start. Try running this program as Administrator.\r\nExiting.");
                Environment.Exit(0);
            }
            Console.WriteLine("Listening for game integration calls...");
        }

        static void OnNewGameState(GameState gs)
        {
            if(gs.Map.GameState == DOTA_GameState.DOTA_GAMERULES_STATE_GAME_IN_PROGRESS)
            {
                if(gs.Previously.Items.LocalPlayer.CountInventory > gs.Items.LocalPlayer.CountInventory)
                {
                    Console.WriteLine("You bought an item");
                }
                
                if(!gs.Map.IsDaytime || gs.Map.IsNightstalkerNight)
                {
                    Console.WriteLine("It is night time");
                }
            }
        }
    }
}
```

You will also need to create a custom `gamestate_integration_*.cfg` in `game/dota/cfg/gamestate_integration/`, for example:  
`gamestate_integration_test.cfg`:  
```
"Dota 2 Integration Configuration"
{
    "uri"           "http://localhost:4000/"
    "timeout"       "5.0"
    "buffer"        "0.1"
    "throttle"      "0.1"
    "heartbeat"     "30.0"
    "data"
    {
        "auth"          "1"
        "provider"      "1"
        "map"           "1"
        "player"        "1"
        "hero"          "1"
        "abilities"     "1"
        "items"         "1"
		"events"       	"1"
		"buildings"     "1"
		"league"        "1"
		"draft"         "1"
		"wearables"     "1"
		"minimap"       "1"
		"roshan"       	"1"
		"couriers"      "1"
		"neutralitems"  "1"
    }
}

```

**Please note**: In order to run this test application without explicit administrator privileges, you need to use the URI `http://localhost:<port>` in this configuration file.

## Credits
Special thanks to [rakijah](https://github.com/rakijah) for his CSGO Game State Integration library.

Thanks to [judge2020](https://github.com/judge2020) for providing an example program.
