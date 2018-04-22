namespace FarmingShooter
{
	using UnityEngine;
	using FarmingShooter.Exensions.Physics;

	[RequireComponent(typeof(Collider2D))]
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


		public static ItemPickup CreateFromItemEntry(ItemEntry itemEntry)
		{
			if (poolObject == null)
			{

				poolObject = new GameObject("Item Pickup Blueprint");
				poolObject.AddComponent<SpriteRenderer>();
				poolObject.AddComponent<ItemPickup>().NestSprite();
				BoxCollider2D collider = poolObject.AddComponent<BoxCollider2D>();
				collider.isTrigger = true;
				poolObject.SetupRigidbody();
				poolObject.SetActive(false);
			}

			GameObject itemPickupObject = PoolManager.Spawn(poolObject);
			ItemPickup itemPickup = itemPickupObject.GetComponent<ItemPickup>();
			itemPickup.item = itemEntry;
			itemPickup.SpriteRenderer.sprite = itemEntry.ItemData.Icon;

			return itemPickup;
		}


		public void Awake()
		{
			this.NestSprite();
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
				// TODO: Use pooling instead.
				PoolManager.Despawn(this.gameObject);
			}
		}
	}
}