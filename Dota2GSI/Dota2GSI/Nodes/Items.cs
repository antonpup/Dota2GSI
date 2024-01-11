using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item information.
    /// </summary>
    public class Items : Node
    {
        private List<Item> inventory = new List<Item>();
        private List<Item> stash = new List<Item>();
        private Item teleport = new Item();
        private Item neutral = new Item();

        /// <summary>
        /// Number of items in the inventory.
        /// </summary>
        public int CountInventory { get { return inventory.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Gets the IEnumerable of the inventory items.
        /// </summary>
        public IEnumerable<Item> Inventory
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted.
                return inventory.ToList();
            }
        }

        /// <summary>
        /// Number of items in the stash.
        /// </summary>
        public int CountStash { get { return stash.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Gets the IEnumerable of the stash items.
        /// </summary>
        public IEnumerable<Item> Stash
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted.
                return stash.ToList();
            }
        }

        /// <summary>
        /// Gets the teleport item.
        /// </summary>
        public Item Teleport
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted.
                return teleport;
            }
        }

        /// <summary>
        /// Gets the neutral item.
        /// </summary>
        public Item Neutral
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted.
                return neutral;
            }
        }

        internal Items(JObject parsed_data = null) : base(parsed_data)
        {
            if (_ParsedData != null)
            {
                List<string> slots = _ParsedData.Properties().Select(p => p.Name).ToList();
                foreach (string item_slot in slots)
                {
                    Item item = new Item(_ParsedData[item_slot] as JObject);

                    if (item_slot.StartsWith("slot"))
                    {
                        inventory.Add(item);
                    }
                    else if (item_slot.StartsWith("stash"))
                    {
                        stash.Add(item);
                    }
                    else if (item_slot.Equals("teleport0"))
                    {
                        teleport = item;
                    }
                    else if (item_slot.Equals("neutral0"))
                    {
                        neutral = item;
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
            if (index < 0 || index > inventory.Count - 1)
            {
                return new Item();
            }

            return inventory[index];
        }

        /// <summary>
        /// Gets the inventory item by item name.
        /// </summary>
        /// <param name="item_name">The item name to look for.</param>
        /// <returns>The inventory item.</returns>
        public Item GetInventoryItem(string item_name)
        {
            foreach(var item in inventory)
            {
                if (item.Name.Equals(item_name))
                {
                    return item;
                }
            }

            return new Item();
        }

        /// <summary>
        /// Gets the stash item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The stash item.</returns>
        public Item GetStashAt(int index)
        {
            if (index < 0 || index > stash.Count - 1)
            {
                return new Item();
            }

            return stash[index];
        }

        /// <summary>
        /// Gets the stash item by item name.
        /// </summary>
        /// <param name="item_name">The item name to look for.</param>
        /// <returns>The inventory item.</returns>
        public Item GetStashItem(string item_name)
        {
            foreach (var item in stash)
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
        /// <param name="itemname">The item name.</param>
        /// <returns>True if item is in the inventory, false otherwise.</returns>
        public bool InventoryContains(string itemname)
        {
            var found_index = InventoryIndexOf(itemname);
            if (found_index > -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if item exists in the stash.
        /// </summary>
        /// <param name="itemname">The item name.</param>
        /// <returns>True if item is in the stash, false otherwise.</returns>
        public bool StashContains(string itemname)
        {
            var found_index = StashIndexOf(itemname);
            if (found_index > -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets index of the first occurence of the item in the inventory.
        /// </summary>
        /// <param name="itemname">The item name.</param>
        /// <returns>The first index at which item is found, -1 if not found.</returns>
        public int InventoryIndexOf(string itemname)
        {
            int index = -1;
            for (int x = 0; x < this.inventory.Count; x++)
            {
                if (this.inventory[x].Name == itemname)
                {
                    index = x;
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// Gets index of the first occurence of the item in the stash.
        /// </summary>
        /// <param name="itemname">The item name.</param>
        /// <returns>The first index at which item is found, -1 if not found.</returns>
        public int StashIndexOf(string itemname)
        {
            int index = -1;
            for (int x = 0; x < this.stash.Count; x++)
            {
                if (this.stash[x].Name == itemname)
                {
                    index = x;
                    break;
                }
            }

            return index;
        }
    }
}
