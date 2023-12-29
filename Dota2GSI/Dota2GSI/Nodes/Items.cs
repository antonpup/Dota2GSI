using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item information
    /// </summary>
    public class Items : Node
    {
        private List<Item> inventory = new List<Item>();
        private List<Item> stash = new List<Item>();

        /// <summary>
        /// Number of items in the inventory
        /// </summary>
        public int CountInventory { get { return inventory.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Gets the IEnumerable of the inventory items
        /// </summary>
        public IEnumerable<Item> Inventory
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted
                return this.inventory.ToList();
            }
        }

        /// <summary>
        /// Number of items in the stash
        /// </summary>
        public int CountStash { get { return stash.Where(s => s.Name != "empty").Count(); } }

        /// <summary>
        /// Gets the IEnumerable of the stash items
        /// </summary>
        public IEnumerable<Item> Stash
        {
            get
            {
                // Use ToList to make a copy, so original list is safe even when casted
                return this.stash.ToList();
            }
        }

        internal Items(string json_data) : base(json_data)
        {
            List<string> slots = _ParsedData.Properties().Select(p => p.Name).ToList();
            foreach (string item_slot in slots)
            {
                Item item = new Item(_ParsedData[item_slot].ToString());

                if (item_slot.StartsWith("slot"))
                {
                    this.inventory.Add(item);
                }
                else
                {
                    this.stash.Add(item);
                }
            }
        }

        /// <summary>
        /// Gets the inventory item at the specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns></returns>
        public Item GetInventoryAt(int index)
        {
            if (index < 0 || index > inventory.Count - 1)
            {
                return new Item("");
            }

            return inventory[index];
        }

        /// <summary>
        /// Gets the stash item at the specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns></returns>
        public Item GetStashAt(int index)
        {
            if (index < 0 || index > stash.Count - 1)
            {
                return new Item("");
            }

            return stash[index];
        }

        /// <summary>
        /// Checks if item exists in the inventory
        /// </summary>
        /// <param name="itemname">The item name</param>
        /// <returns>A boolean if item is in the inventory</returns>
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
        /// Checks if item exists in the stash
        /// </summary>
        /// <param name="itemname">The item name</param>
        /// <returns>A boolean if item is in the stash</returns>
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
        /// Gets index of the first occurence of the item in the inventory
        /// </summary>
        /// <param name="itemname">The item name</param>
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
        /// Gets index of the first occurence of the item in the stash
        /// </summary>
        /// <param name="itemname">The item name</param>
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
