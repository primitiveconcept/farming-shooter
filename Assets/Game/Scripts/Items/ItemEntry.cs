namespace FarmingShooter
{
	using System;
	using UnityEngine;


	[Serializable]
	public class ItemEntry
	{
		[SerializeField]
		private ItemData itemData;

		[SerializeField]
		private int count;

		public event Action<ItemEntry> Changed;

		public ItemData ItemData
		{
			get { return this.itemData; }
		}


		public int Count
		{
			get { return this.count; }
			set
			{
				if (value == this.count
					|| this.count == this.itemData.MaxCount)
				{
					return;
				}

				if (value < 0)
					this.count = 0;
				else if (value > this.itemData.MaxCount)
					this.count = this.itemData.MaxCount;
				else
					this.count = value;

				Action<ItemEntry> changed = this.Changed;
				if (changed != null)
					changed(this);
			}
		}
	}
}
