namespace FarmingShooter
{
	using UnityEngine;
	using FarmingShooter.Exensions.Physics;

	public class ItemPickup : MonoBehaviour,
							IHasSprite
	{
		private static GameObject poolObject;

		[SerializeField]
		private ItemEntry item;

		[SerializeField]
		private SpriteRenderer spriteRenderer;


		#region Properties
		public SpriteRenderer SpriteRenderer
		{
			get { return this.spriteRenderer; }
			set { this.spriteRenderer = value; }
		}
		#endregion


		public void Awake()
		{
			this.spriteRenderer = GetComponent<SpriteRenderer>();
		}


		public static ItemPickup CreateFromItemEntry(ItemEntry itemEntry)
		{
			if (poolObject == null)
			{

				poolObject = new GameObject("Item Pickup");
				poolObject.AddComponent<SpriteRenderer>();
				poolObject.SetupRigidbody();
				poolObject.AddComponent<ItemPickup>();
				poolObject.SetActive(false);
			}

			GameObject itemPickupObject = PoolManager.Spawn(poolObject);
			ItemPickup itemPickup = itemPickupObject.GetComponent<ItemPickup>();
			itemPickup.item = itemEntry;
			itemPickup.SpriteRenderer.sprite = itemEntry.ItemData.Icon;
			Destroy(itemPickupObject.GetComponent<Collider2D>());
			BoxCollider2D collider = itemPickupObject.AddComponent<BoxCollider2D>();
			collider.isTrigger = true;

			return itemPickup;
		}

		
		public void OnTriggerEnter2D(Collider2D other)
		{
			Actor actor = other.GetComponent<Actor>();
			if (actor == null)
				return;

			Inventory actorInventory = actor.Inventory;
			if (actorInventory == null)
				return;

			if (actorInventory.AcquireItem(this.item))
			{
				Debug.Log("Item Pickup: " + this.item.ItemData.DisplayName);
				PoolManager.Despawn(this.gameObject);
			}
		}
	}
}