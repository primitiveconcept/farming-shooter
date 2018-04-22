namespace FarmingShooter
{
	using System.Collections.Generic;
	using UnityEngine;


	public class Inventory : MonoBehaviour
	{
		[SerializeField]
		private List<ItemEntry> items;

		[SerializeField]
		private int maxSlots = 1;

		[SerializeField]
		private bool locked = false;

		private HashSet<ItemData> discoveredItems = new HashSet<ItemData>();


		#region Properties
		public int Count
		{
			get { return this.items.Count; }
		}


		public List<ItemEntry> Items
		{
			get { return this.items; }
		}


		public bool Locked
		{
			get { return this.locked; }
			set { this.locked = value; }
		}


		public int MaxSlots
		{
			get { return this.maxSlots; }
			set { this.maxSlots = value; }
		}


		public ItemEntry this[ItemData itemData]
		{
			get
			{
				int index = GetIndexOf(itemData);
				if (index < 0)
					return null;

				return this[index];
			}
		}


		public void DropAll()
		{
			for (int i = 0; i < this.Count; i++)
			{
				DropItem(i);
			}
		}


		public void DropItem(int index)
		{
			var itemEntry = this[index];
			if (!itemEntry.IsDroppable)
				return;

			ItemPickup.CreateFromItemEntry(itemEntry);
			ConsumeItem(this[index]);
		}


		public ItemEntry this[int index]
		{
			get { return this.items[index]; }
		}
		#endregion


		public bool AcquireItem(ItemEntry itemEntry)
		{
			return AcquireItem(itemEntry.ItemData, itemEntry.Count);
		}


		public bool AcquireItem(ItemData itemData, int amount)
		{
			if (this.locked)
				return false;

			ItemEntry existingEntry = this[itemData];

			bool collected = false;

			if (existingEntry != null)
				collected = IncreaseItemCount(existingEntry, amount);
			else
				collected = AddNewItemEntry(new ItemEntry() { ItemData = itemData, Count = amount });

			if (collected
				&& !this.discoveredItems.Contains(itemData))
			{
				this.discoveredItems.Add(itemData);
			}

			return collected;
		}


		public bool CanCraft(ItemData itemData)
		{
			foreach (ItemEntry ingredient in itemData.Recipe)
			{
				if (!Contains(ingredient.ItemData, ingredient.Count))
					return false;
			}

			return true;
		}


		public void ConsumeItem(int index, int amount = 1)
		{
			ItemEntry item = this.items[index];
			item.Count -= amount;
			if (item.Count < 1)
				this.items.RemoveAt(index);
		}


		public void ConsumeItem(ItemEntry itemEntry)
		{
			ConsumeItem(itemEntry.ItemData, itemEntry.Count);
		}


		public void ConsumeItem(ItemData itemData, int amount = 1)
		{
			int index = GetIndexOf(itemData);
			ConsumeItem(index, amount);
		}


		public bool Contains(ItemEntry itemEntry)
		{
			return Contains(itemEntry.ItemData, itemEntry.Count);
		}


		public bool Contains(ItemData itemData, int count = 1)
		{
			ItemEntry foundItem = this[itemData];

			return foundItem.Count >= count;
		}


		public bool Craft(ItemData itemData)
		{
			if (!CanCraft(itemData))
				return false;

			foreach (ItemEntry ingredient in itemData.Recipe)
			{
				ConsumeItem(ingredient);
			}

			AcquireItem(itemData, 1);

			return true;
		}


		public int GetIndexOf(ItemData itemData)
		{
			for (int index = 0; index < this.Count; index++)
			{
				ItemEntry itemEntry = this[index];
				if (itemEntry.ItemData == itemData)
					return index;
			}

			return -1;
		}


		private bool AddNewItemEntry(ItemEntry itemEntry)
		{
			if (this.Count < this.maxSlots)
			{
				this.items.Add(itemEntry);
				return true;
			}

			// Inventory slots already maxed out.
			return false;
		}


		private bool IncreaseItemCount(ItemEntry existingEntry, int amount)
		{
			if (existingEntry.Count < existingEntry.ItemData.MaxCount)
			{
				existingEntry.Count += amount;
				return true;
			}

			// Item already maxed out.
			return false;
		}
	}
}