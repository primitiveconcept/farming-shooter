namespace FarmingShooter
{
	using UnityEngine;


	public class Actor : MonoBehaviour
	{
		[SerializeField]
		private int equippedItemIndex;

		[SerializeField]
		private int equippedWeaponIndex;

		[SerializeField]
		private Transform attackOrigin;

		[SerializeField]
		private Transform useItemOrigin;

		private Inventory inventory;


		#region Properties
		public ItemEntry EquippedItem
		{
			get
			{
				if (this.inventory.Count > this.equippedItemIndex)
					return this.inventory[this.equippedItemIndex];

				return null;
			}
		}


		public ItemEntry EquippedWeapon
		{
			get
			{
				if (this.inventory.Count > this.equippedWeaponIndex)
					return this.inventory[this.equippedWeaponIndex];

				return null;
			}
		}


		public Inventory Inventory
		{
			get { return this.inventory; }
		}
		#endregion


		public void Awake()
		{
			this.inventory = GetComponent<Inventory>();
		}


		public void UseEquippedItem()
		{
			if (this.EquippedItem == null)
				return;

			if (!this.EquippedItem.ItemData.IsUsable)
				return;

			this.EquippedItem.ItemData.Use(this.attackOrigin);

			if (!this.EquippedItem.ItemData.IsConsumed)
				return;

			this.inventory.ConsumeItem(this.equippedItemIndex);
		}


		public void UseEquippedWeapon()
		{
			if (this.EquippedWeapon == null)
				return;

			if (!this.EquippedWeapon.ItemData.IsWeapon)
				return;

			this.EquippedWeapon.ItemData.Attack(this.attackOrigin);

			if (!this.EquippedWeapon.ItemData.IsConsumed)
				return;

			this.inventory.ConsumeItem(this.equippedWeaponIndex);
		}
	}
}