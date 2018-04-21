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
				foreach (ItemEntry itemEntry in this.items)
				{
					if (itemEntry.ItemData == itemData)
						return itemEntry;
				}

				return null;
			}
		}


		public ItemEntry this[int index]
		{
			get { return this.items[index]; }
		}
		#endregion


		public bool AcquireItem(ItemEntry itemEntry)
		{
			if (this.locked)
				return false;

			ItemEntry existingEntry = this[itemEntry.ItemData];

			if (existingEntry != null)
				return IncreaseItemCount(existingEntry, itemEntry.Count);

			return AddNewItemEntry(itemEntry);
		}


		public void ConsumeItem(int index)
		{
			ItemEntry item = this.items[index];
			item.Count--;
			if (item.Count < 1)
				this.items.RemoveAt(index);
		}


		private static bool IncreaseItemCount(ItemEntry existingEntry, int amount)
		{
			if (existingEntry.Count < existingEntry.ItemData.MaxCount)
			{
				existingEntry.Count += amount;
				return true;
			}

			// Item already maxed out.
			return false;
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
	}
}