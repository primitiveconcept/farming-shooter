namespace FarmingShooter
{
	using UnityEngine;


	[RequireComponent(typeof(Collider2D))]
	public class ItemPickup : MonoBehaviour
	{
		[SerializeField]
		private ItemEntry item;

		[SerializeField]
		private Sprite sprite;


		public void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log("ItemPickup collision");

			Actor actor = other.GetComponent<Actor>();
			if (actor == null)
				return;

			Inventory actorInventory = actor.Inventory;
			if (actorInventory == null)
				return;

			if (actorInventory.AcquireItem(item))
			{
				// TODO: Use pooling instead.
				Destroy(this.gameObject);
			}
		}


		public static ItemPickup CreateFromItemEntry(ItemEntry itemEntry)
		{
			GameObject pickupObject = new GameObject(itemEntry.ItemData.name);
			ItemPickup itemPickup = pickupObject.AddComponent<ItemPickup>();
			itemPickup.item = itemEntry;

			return itemPickup;
		}
	}
}