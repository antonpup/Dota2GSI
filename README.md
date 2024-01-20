Dota 2 GSI (Game State Integration)
===================================

[![Dota 2](https://img.shields.io/badge/Dota_2-9f1d1c?style=flat&label=Game&labelColor=black)](https://store.steampowered.com/app/570/)
[![.NET](https://github.com/antonpup/Dota2GSI/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/antonpup/Dota2GSI/actions/workflows/dotnet.yml)
[![GitHub Release](https://img.shields.io/github/v/release/antonpup/Dota2GSI)](https://github.com/antonpup/Dota2GSI/releases/latest)
[![NuGet Version](https://img.shields.io/nuget/v/Dota2GSI)](https://www.nuget.org/packages/Dota2GSI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Dota2GSI?label=nuget%20downloads)](https://www.nuget.org/packages/Dota2GSI)

A C# library to interface with the Game State Integration found in Dota 2.

## About Dota 2 GSI

This library provides an easy way to implement Game State Integration from Dota 2 into C# applications through exposing a number of events.

Underneath the hood, once the library is started, it continuously listens for HTTP POST requests made by the game on a specific address and port. When a request is received, the JSON data is parsed into [GameState object](#game-state-structure) and is offered to your C# application through the `NewGameState` event. The library also subscribes to `NewGameState` to determine more granular changes to raise more specific events _(such as `TimeOfDayChanged`, `InventoryItemAdded`, or `TowerDestroyed` to name a few)_. A full list of exposed Game Events can be found in the [Implemented Game Events](#implemented-game-events) section.

## About Game State Integration

Game State Integration is Valve's implementation for exposing current game state _(such as player health, mana, ammo, etc.)_ and game events without the need to read game memory or risking anti-cheat detection. The information exposed by GSI is limited to what Valve has determined to expose. For example, the game can expose information about all players in the game while spectating a match, but will only expose local player's information when playing a game. While the information is limited, there is enough information to create a live game analysis tool, create custom RGB lighting effects, or create a live streaming plugin to show additional game information. For example, GSI can be seen used during competitive tournament live streams to show currently spectated player's information and in-game statistics.

You can read about Game State Integration for Counter-Strike: Global Offensive [here](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration).

## Installation

Install from [nuget](https://www.nuget.org/packages/Dota2GSI).

## Building Dota 2 GSI

1. Make sure you have Visual Studio installed with `.NET desktop development` workload and `.Net 8.0 Runtime` individual component.
2. Make sure you have CMake 3.26 or later installed from [https://cmake.org/](https://cmake.org/).
3. In the repository root directory run: `cmake -B build/ .` to generate the project solution file.
4. Open the project solution located in `build/Dota2GSI.sln`.

## How to use

1. After installing the [Dota2GSI nuget package](https://www.nuget.org/packages/Dota2GSI) in your project, create an instance of `GameStateListener`, providing a custom port or custom URI.
```C#
GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/
```
or
```C#
GameStateListener gsl = new GameStateListener("http://127.0.0.1:1234/");
```
> **Please note**: If your application needs to listen to a URI other than `http://localhost:*/` (for example `http://192.168.0.2:100/`), you will need to run your application with administrator privileges.

2. Create a Game State Integration configuration file. This can either be done manually by creating a file `<PATH TO GAME DIRECTORY>/game/dota/cfg/gamestate_integration/gamestate_integration_<CUSTOM NAME>.cfg` where `<CUSTOM NAME>` should be the name of your application (it can be anything). Or you can use the built-in function `GenerateGSIConfigFile()` to automatically locate the game directory and generate the file. The function will automatically take into consideration the URI or port specified when a `GameStateListener` instance was created in the previous step.
```C#
if (!gsl.GenerateGSIConfigFile("Example"))
{
    Console.WriteLine("Could not generate GSI configuration file.");
}
```
The function `GenerateGSIConfigFile` takes a string `name` as the parameter. This is the `<CUSTOM NAME>` mentioned earlier. The function will also return `True` when file generation was successful, and `False` otherwise. The resulting file from the above code should look like this:
```
"Example Integration Configuration"
{
    "uri"          "http://localhost:3000/"
    "timeout"      "5.0"
    "buffer"       "0.1"
    "throttle"     "0.1"
    "heartbeat"    "10.0"
    "data"
    {
        "auth"            "1"
        "provider"        "1"
        "map"             "1"
        "player"          "1"
        "hero"            "1"
        "abilities"       "1"
        "items"           "1"
        "events"          "1"
        "buildings"       "1"
        "league"          "1"
        "draft"           "1"
        "wearables"       "1"
        "minimap"         "1"
        "roshan"          "1"
        "couriers"        "1"
        "neutralitems"    "1"
    }
}
```

3. Create handlers and subscribe for events your application will be using. (A full list of exposed Game Events can be found in the [Implemented Game Events](#implemented-game-events) section.)
If your application just needs `GameState` information, this is done by subscribing to `NewGameState` event and creating a handler for it:
```C#
...
gsl.NewGameState += OnNewGameState;
...

void OnNewGameState(GameState gs)
{
    // Read information from the game state.
}
```
If you would like to utilize `Game Events` in your application, this is done by subscribing to an event from the [Implemented Game Events](#implemented-game-events) list and creating a handler for it:
```C#
...
gsl.GameEvent += OnGameEvent; // Will fire on every GameEvent
gsl.TimeOfDayChanged += OnTimeOfDayChanged; // Will only fire on TimeOfDayChanged events.
gsl.InventoryItemAdded += OnInventoryItemAdded; // Will only fire on InventoryItemAdded events.
...

void OnGameEvent(DotaGameEvent game_event)
{
    // Read information from the game event.
    
    if (game_event is TimeOfDayChanged tod_changed)
    {
        Console.WriteLine($"Is daytime: {tod_changed.IsDaytime} Is Nightstalker night: {tod_changed.IsNightstalkerNight}");
    }
}

void OnTimeOfDayChanged(TimeOfDayChanged game_event)
{
    Console.WriteLine($"Is daytime: {game_event.IsDaytime} Is Nightstalker night: {game_event.IsNightstalkerNight}");
}

void OnInventoryItemAdded(InventoryItemAdded game_event)
{
    Console.WriteLine($"Player {game_event.Player.Details.Name} gained an item in their inventory: {game_event.Value.Name}");
}
```
Both `NewGameState` and `Game Events` can be used alongside one another. The `Game Events` are generated based on the `GameState`, and are there to provide ease of use.

4. Finally you want to start the `GameStateListener` to begin capturing HTTP POST requests. This is done by calling the `Start()` method of `GameStateListener`. The method will return `True` if started successfully, or `False` when failed to start. Often the failure to start is due to insufficient permissions or another application is already using the same port.
```C#
if (!gsl.Start())
{
    // GameStateListener could not start.
}
// GameStateListener started and is listening for Game State requests.
```

## Implemented Game Events

* `GameEvent` The base game event, will fire for all other listed events.

### Abilities Events

* `AbilitiesUpdated`
* `AbilityDetailsChanged`
* `AbilityAdded`
* `AbilityRemoved`
* `AbilityUpdated`

### Auth Events

* `AuthUpdated`

### Buildings Events

* `BuildingsUpdated`
* `BuildingsLayoutUpdated`
* `BuildingUpdated`
* `TeamBuildingUpdated`
* `TeamBuildingDestroyed`
* `TowerUpdated`
* `TowerDestroyed`
* `RacksUpdated`
* `RacksDestroyed`
* `AncientUpdated`
* `AncientDestroyed`

### Couriers Events

* `CouriersUpdated`
* `CourierUpdated`
* `TeamCourierUpdated`
* `CourierItemAdded`
* `CourierItemRemoved`
* `CourierItemUpdated`

### Draft Events

* `DraftUpdated`
* `TeamDraftDetailsUpdated`

### Gameplay Events

* `EventsUpdated`
* `GameplayEvent`
* `TeamGameplayEvent`
* `PlayerGameplayEvent`

### Hero Events

* `HeroUpdated`
* `HeroDetailsChanged`
* `HeroLevelChanged`
* `HeroHealthChanged`
* `HeroDied`
* `HeroRespawned`
* `HeroTookDamage`
* `HeroManaChanged`
* `HeroStateChanged`
* `HeroMuteStateChanged`
* `HeroSelectedChanged`
* `HeroTalentTreeChanged`
* `HeroAttributesLevelChanged`

### Items Events

* `ItemsUpdated`
* `ItemDetailsChanged`
* `ItemUpdated`
* `InventoryItemAdded`
* `InventoryItemRemoved`
* `InventoryItemUpdated`
* `StashItemAdded`
* `StashItemRemoved`
* `StashItemUpdated`

### League Events

* `LeagueUpdated`

### Map Events

* `MapUpdated`
* `TimeOfDayChanged`
* `TeamScoreChanged`
* `GameStateChanged`
* `PauseStateChanged`
* `GamePaused`
* `GameResumed`
* `TeamVictory`
* `TeamDefeat`
* `RoshanStateChanged`

### Minimap Events

* `MinimapUpdated`
* `MinimapElementAdded`
* `MinimapElementUpdated`
* `MinimapElementRemoved`
* `TeamMinimapElementUpdated`

### NeutralItems Events

* `NeutralItemsUpdated`
* `TeamNeutralItemsUpdated`

### Player Events

* `PlayerUpdated`
* `PlayerDetailsChanged`
* `PlayerKillsChanged`
* `PlayerDeathsChanged`
* `PlayerAssistsChanged`
* `PlayerLastHitsChanged`
* `PlayerDeniesChanged`
* `PlayerKillStreakChanged`
* `PlayerGoldChanged`
* `PlayerWardsPurchasedChanged`
* `PlayerWardsPlacedChanged`
* `PlayerWardsDestroyedChanged`
* `PlayerRunesActivatedChanged`
* `PlayerCampsStackedChanged`

### Provider Events

* `ProviderUpdated`

### Roshan Events

* `RoshanUpdated`

### Wearables Events

* `RoshanUpdated`
* `WearablesUpdated`
* `PlayerWearablesUpdated`

## Game State Structure

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
|   +-- WardPurchaseCooldown
|   +-- RadiantWardPurchaseCooldown
|   +-- DireWardPurchaseCooldown
|   +-- RoshanState
|   +-- RoshanStateEndTime
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
|   |   |   \
|   |   |   (Map of kill id to player id)
|   |   |   ...
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
|   |   +-- SupportGoldSpent
|   |   +-- ConsumableGoldSpent
|   |   +-- ItemGoldSpent
|   |   +-- GoldLostToDeath
|   |   +-- GoldSpentOnBuybacks
|   |   +-- WardsPurchased
|   |   +-- WardsPlaced
|   |   +-- WardsDestroyed
|   |   +-- RunesActivated
|   |   +-- CampsStacked
|   +-- Teams
|   |   \
|   |   (Multi-map of team to player id to Player Details)
|   |   ...
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
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
|   |   +-- HeroState
|   |   +-- IsSilenced
|   |   +-- IsStunned
|   |   +-- IsDisarmed
|   |   +-- IsMagicImmune
|   |   +-- IsHexed
|   |   +-- IsBreak
|   |   +-- IsSmoked
|   |   +-- HasDebuff
|   |   +-- IsMuted
|   |   +-- HasAghanimsScepterUpgrade
|   |   +-- HasAghanimsShardUpgrade
|   |   +-- SelectedUnit
|   |   +-- TalentTree[]
|   |   +-- AttributesLevel
|   +-- Teams
|   |   \
|   |   (Multi-map of team to player id to Hero Details)
|   |   ...
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
+-- Abilities
|   +-- LocalPlayer
|   |   +-- Count
|   |   +-- Ability[]
|   |   |   \
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
|   |   \
|   |   (Multi-map of team to player id to Ability Details)
|   |   ...
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
+-- Items
|   +-- LocalPlayer
|   |   +-- Inventory[]
|   |   |   \
|   |   |   +-- Name
|   |   |   +-- Purchaser
|   |   |   +-- ItemLevel
|   |   |   +-- ContainsRune
|   |   |   +-- CanCast
|   |   |   +-- Cooldown
|   |   |   +-- IsPassive
|   |   |   +-- ItemCharges
|   |   |   +-- AbilityCharges
|   |   |   +-- MaxCharges
|   |   |   +-- ChargeCooldown
|   |   |   +-- Charges
|   |   +-- Stash[]
|   |   |   ...
|   |   +-- CountInventory
|   |   +-- CountStash
|   |   +-- Teleport
|   |   |   ...
|   |   +-- Neutral
|   |   |   ...
|   |   +-- GetInventoryAt( index )
|   |   +-- GetInventoryItem( item_name )
|   |   +-- InventoryContains( item_name )
|   |   +-- InventoryIndexOf( item_name )
|   |   +-- GetStashAt( index )
|   |   +-- GetStashItem( item_name )
|   |   +-- StashContains( item_name )
|   |   +-- StashIndexOf( item_name )
|   +-- Teams
|   |   \
|   |   (Multi-map of team to player id to Item Details)
|   |   ...
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
+-- Events[]
|   |   \
|   |   +-- GameTime
|   |   +-- EventType
|   |   +-- Team
|   |   +-- KillerPlayerID
|   |   +-- PlayerID
|   |   +-- WasSnatched
|   |   +-- TipReceiverPlayerID
|   |   +-- TipAmount
|   |   +-- BountyValue
|   |   +-- TeamGold
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
+-- Buildings
|   +-- RadiantBuildings
|   |   +-- TopTowers
|   |   |   \
|   |   |   (Map of tower id to Building)
|   |   |   +-- Health
|   |   |   +-- MaxHealth
|   |   +-- MiddleTowers
|   |   |   \
|   |   |   (Map of tower id to Building)
|   |   |   ...
|   |   +-- BottomTowers
|   |   |   \
|   |   |   (Map of tower id to Building)
|   |   |   ...
|   |   +-- TopRacks
|   |   |   \
|   |   |   (Map of Racks Type to Building)
|   |   |   ...
|   |   +-- MiddleRacks
|   |   |   \
|   |   |   (Map of Racks Type to Building)
|   |   |   ...
|   |   +-- BottomRacks
|   |   |   \
|   |   |   (Map of Racks Type to Building)
|   |   |   ...
|   |   +-- Ancient
|   |   |   ...
|   |   +-- OtherBuildings
|   |   |   \
|   |   |   (Map of building id to Building)
|   |   |   ...
|   +-- DireBuildings
|   |   ...
|   +-- AllBuildings
|   |   \
|   |   (Map of team to Building Layout)
|   |   ...
|   +-- GetForTeam( team )
+-- League
|   +-- SeriesType
|   +-- SelectionPriority
|   |   +-- Rules
|   |   +-- PreviousPriorityTeamID
|   |   +-- CurrentPriorityTeamID
|   |   +-- PriorityTeamChoice
|   |   +-- NonPriorityTeamChoice
|   |   +-- UsedCoinToss
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
|   +-- Streams[]
|   |   \
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
|   |   |   \
|   |   |   (Map of team to Draft Details)
|   |   +-- IsHomeTeam
|   |   +-- PickIDs
|   |   |   \
|   |   |   (Map of slot number to picked hero ID)
|   |   +-- PickHeroIDs
|   |   |   \
|   |   |   (Map of slot number to picked hero name)
|   +-- GetForTeam( team )
+-- Wearables
|   +-- LocalPlayer
|   |   +-- Wearables
|   |   |   \
|   |   |   (Map of slot number to Wearable Item)
|   |   |   +-- ID
|   |   |   +-- Style
|   +-- Teams
|   |   \
|   |   (Multi-map of team to player id to Item Wearable Item)
|   |   ...
|   +-- GetForTeam( team )
|   +-- GetForPlayer( player_id )
+-- Minimap
|   +-- Elements
|   |   |   \
|   |   |   (Map of element ID to Minimap Element)
|   |   +-- Location
|   |   +-- RemainingTime
|   |   +-- EventDuration
|   |   +-- Image
|   |   +-- Team
|   |   +-- Name
|   |   +-- Rotation
|   |   +-- UnitName
|   |   +-- VisionRange
|   +-- GetForTeam( team )
|   +-- GetByUnitName( unit_name )
+-- Roshan
|   +-- Health
|   +-- MaxHealth
|   +-- IsAlive
|   +-- SpawnPhase
|   +-- PhaseTimeRemaining
|   +-- Location
|   +-- Rotation
|   +-- Drops
|   |   +-- Items
|   |   |   \
|   |   |   (Map of item index to item name)
+-- Couriers
|   +-- CouriersMap
|   |   \
|   |   (Map of courier ID to Courier)
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
|   |   |   \
|   |   |   (Map of item index to Courier Item)
|   |   |   +-- OwnerID
|   |   |   +-- Name
|   |   +-- GetItemAt( index )
|   |   +-- GetInventoryItem( item_name )
|   |   +-- InventoryContains( item_name )
|   |   +-- InventoryIndexOf( item_name )
|   +-- GetForPlayer( player_id )
+-- NeutralItems
|   +-- TierInfos
|   |   |   \
|   |   |   (Map of tier index to Neutral Tier Info)
|   |   +-- Tier
|   |   +-- MaxCount
|   |   +-- DropAfterTime
|   +-- TeamItems
|   |   |   \
|   |   |   (Map of team to Team Neutral Items)
|   |   +-- ItemsFound
|   |   +-- TeamItems
|   |   |   \
|   |   |   (Map of item index to Neutral Item)
|   |   |   +-- Name
|   |   |   +-- Tier
|   |   |   +-- Charges
|   |   |   +-- State
|   |   |   +-- PlayerID
|   +-- GetForTeam( team )
+-- Previously (Previous information from Game State)
+-- LocalPlayer
|   +-- PlayerID
|   +-- IsLocalPlayer
|   +-- Details
|   +-- Hero
|   +-- Abilities
|   +-- Items
|   +-- Wearables
|   +-- Courier
|   +-- MinimapElements
+-- RadiantTeamDetails
|   +-- Team
|   +-- Players
|   +-- Draft
|   +-- NeutralItems
|   +-- Buildings
|   +-- MinimapElements
|   +-- Events
|   +-- IsWinner
+-- DireTeamDetails
|   ...
+-- NeutralTeamDetails
|   ...
+-- IsSpectating
+-- IsLocalPlayer
```

## Null value handling

In the event that the game state is missing information, a default value will be returned:

| Type     | Default value  |
|:---------|:---------------|
| `bool`   | `False`        |
| `int`    | `-1`           |
| `long`   | `-1`           |
| `float`  | `-1`           |
| `string` | `String.Empty` |
| `enum`   | `Undefined`    |

## Example program

An example program for `Dota2GSI` can be found in the `Dota2GSI Example program` directory in the root of the repository. It was initially created by [judge2020](https://github.com/judge2020).

### Item and hero names

A full list of item names can be found [here](https://dota2.fandom.com/wiki/Cheats#Item_names).

A full list of hero names can be found [here](https://dota2.fandom.com/wiki/Cheats#Hero_Names).

## Credits

Thank you to [rakijah](https://github.com/rakijah) for his work on the CSGO Game State Integration library.

Thank you to [judge2020](https://github.com/judge2020) for providing an example program.