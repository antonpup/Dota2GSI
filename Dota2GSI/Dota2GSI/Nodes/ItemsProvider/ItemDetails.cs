using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.ItemsProvider
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
        public Item Teleport = new Item();

        /// <summary>
        /// Gets the neutral item.
        /// </summary>
        public Item Neutral = new Item();

        private Regex _slot_regex = new Regex(@"slot(\d+)");
        private Regex _stash_regex = new Regex(@"stash(\d+)");
        private Regex _teleport_regex = new Regex(@"teleport(\d+)");
        private Regex _neutral_regex = new Regex(@"neutral(\d+)");

        internal ItemDetails(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingObjects(parsed_data, _slot_regex, (Match match, JObject obj) =>
            {
                Item item = new Item(obj);
                Inventory.Add(item);
            });

            GetMatchingObjects(parsed_data, _stash_regex, (Match match, JObject obj) =>
            {
                Item item = new Item(obj);
                Stash.Add(item);
            });

            GetMatchingObjects(parsed_data, _teleport_regex, (Match match, JObject obj) =>
            {
                Item item = new Item(obj);
                Teleport = item;
            });

            GetMatchingObjects(parsed_data, _neutral_regex, (Match match, JObject obj) =>
            {
                Item item = new Item(obj);
                Neutral = item;
            });
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Inventory: {Inventory}, " +
                $"Stash: {Stash}, " +
                $"Teleport: {Teleport}, " +
                $"Neutral: {Neutral}, " +
                $"]";
        }
    }
}
