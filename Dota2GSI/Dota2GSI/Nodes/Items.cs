using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item details.
    /// </summary>
    public class ItemDetails : Node
    {
        /// <summary>
        /// List of the inventory items.
        /// </summary>
        public readonly List<Item> Inventory = new List<Item>();

        /// <summary>
        /// List of the stash items.
        /// </summary>
        public readonly List<Item> Stash = new List<Item>();

        /// <summary>
        /// Number of items in the inventory.
        /// </summary>
        public int CountInventory { get { return Inventory.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Number of items in the stash.
        /// </summary>
        public int CountStash { get { return Stash.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Gets the teleport item.
        /// </summary>
        public readonly Item Teleport;

        /// <summary>
        /// Gets the neutral item.
        /// </summary>
        public readonly Item Neutral;

        internal ItemDetails(JObject parsed_data = null) : base(parsed_data)
        {
            if (_ParsedData != null)
            {
                List<string> slots = _ParsedData.Properties().Select(p => p.Name).ToList();
                foreach (string item_slot in slots)
                {
                    Item item = new Item(_ParsedData[item_slot] as JObject);

                    if (item_slot.StartsWith("slot"))
                    {
                        Inventory.Add(item);
                    }
                    else if (item_slot.StartsWith("stash"))
                    {
                        Stash.Add(item);
                    }
                    else if (item_slot.Equals("teleport0"))
                    {
                        Teleport = item;
                    }
                    else if (item_slot.Equals("neutral0"))
                    {
                        Neutral = item;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the inventory item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The inventory item.</returns>
        public Item GetInventoryAt(int index)
        {
            if (index < 0 || index > Inventory.Count - 1)
            {
                return new Item();
            }

            return Inventory[index];
        }

        /// <summary>
        /// Gets the inventory item by item name.
        /// </summary>
        /// <param name="item_name">The item name to look for.</param>
        /// <returns>The inventory item.</returns>
        public Item GetInventoryItem(string item_name)
        {
            foreach (var item in Inventory)
            {
                if (item.Name.Equals(item_name))
                {
                    return item;
                }
            }

            return new Item();
        }

        /// <summary>
        /// Checks if item exists in the inventory.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>True if item is in the inventory, false otherwise.</returns>
        public bool InventoryContains(string item_name)
        {
            var found_index = InventoryIndexOf(item_name);
            if (found_index > -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets index of the first occurence of the item in the inventory.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>The first index at which item is found, -1 if not found.</returns>
        public int InventoryIndexOf(string item_name)
        {
            for (int x = 0; x < this.Inventory.Count; x++)
            {
                if (this.Inventory[x].Name == item_name)
                {
                    return x;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the stash item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The stash item.</returns>
        public Item GetStashAt(int index)
        {
            if (index < 0 || index > Stash.Count - 1)
            {
                return new Item();
            }

            return Stash[index];
        }

        /// <summary>
        /// Gets the stash item by item name.
        /// </summary>
        /// <param name="item_name">The item name to look for.</param>
        /// <returns>The inventory item.</returns>
        public Item GetStashItem(string item_name)
        {
            foreach (var item in Stash)
            {
                if (item.Name.Equals(item_name))
                {
                    return item;
                }
            }

            return new Item();
        }

        /// <summary>
        /// Checks if item exists in the stash.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>True if item is in the stash, false otherwise.</returns>
        public bool StashContains(string item_name)
        {
            var found_index = StashIndexOf(item_name);
            if (found_index > -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets index of the first occurence of the item in the stash.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>The first index at which item is found, -1 if not found.</returns>
        public int StashIndexOf(string item_name)
        {
            for (int x = 0; x < this.Stash.Count; x++)
            {
                if (this.Stash[x].Name == item_name)
                {
                    return x;
                }
            }

            return -1;
        }
    }

    /// <summary>
    /// Class representing item information.
    /// </summary>
    public class Items : Node
    {
        /// <summary>
        /// The local player's items.
        /// </summary>
        public readonly ItemDetails LocalPlayer;

        /// <summary>
        /// The team players items. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<PlayerTeam, Dictionary<int, ItemDetails>> Teams = new Dictionary<PlayerTeam, Dictionary<int, ItemDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Items(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player items
            LocalPlayer = new ItemDetails(parsed_data);

            // Attempt to parse team players items
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new Dictionary<int, ItemDetails>());
                }

                GetMatchingObjects(parsed_data, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var item_details = new ItemDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, item_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = item_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the item details for a specific team.
        /// </summary>
        /// <param name="team_id">The team.</param>
        /// <returns>A dictionary of player id mapped to their item details.</returns>
        public Dictionary<int, ItemDetails> GetTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new Dictionary<int, ItemDetails>();
        }

        /// <summary>
        /// Gets the item details for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The player item details.</returns>
        public ItemDetails GetPlayer(int player_id)
        {
            foreach(var team in Teams)
            {
                foreach (var player in team.Value)
                {
                    if (player.Key == player_id)
                    {
                        return player.Value;
                    }
                }
            }

            return new ItemDetails();
        }
    }
}
