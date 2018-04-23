namespace FarmingShooter
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;


	public class Crop : MonoBehaviour
	{
		[SerializeField]
		private ItemEntry itemYield;

		[SerializeField]
		private GrowthTier[] growthTiers;

		[SerializeField]
		private int currentTier;

		[SerializeField]
		private UnityEvent onGrowthTierUpdated;

		private bool isWatered;
		private SpriteRenderer spriteRenderer;
		private CropBlock cropBlock;
		private int startingHealth;


		#region Properties
		public CropBlock CropBlock
		{
			get { return this.cropBlock; }
			set { this.cropBlock = value; }
		}


		public int CurrentTier
		{
			get { return this.currentTier; }
		}


		public bool IsReadyForHarvest
		{
			get { return this.currentTier == this.growthTiers.Length - 1; }
		}


		public bool IsWatered
		{
			get { return this.isWatered; }
		}
		#endregion


		public void Awake()
		{
			this.spriteRenderer = GetComponent<SpriteRenderer>();
		}


		public void DropItemYield()
		{
			if (this.IsReadyForHarvest)
			{
				ItemPickup yield = ItemPickup.CreateFromItemEntry(this.itemYield);
				Collider2D collider = GetComponent<Collider2D>();
				yield.transform.position = collider.bounds.max;
			}

			this.cropBlock.Crop = null;
			this.cropBlock.Unwater();

			PoolManager.Despawn(this.gameObject);
		}


		public void IncrementTier()
		{
			if (this.currentTier < this.growthTiers.Length - 1)
				this.currentTier++;
			this.spriteRenderer.sprite = this.growthTiers[this.currentTier].Sprite;
			Destroy(GetComponent<PolygonCollider2D>());
			PolygonCollider2D collider = this.gameObject.AddComponent<PolygonCollider2D>();
			collider.isTrigger = true;
		}


		public void OnSpawn()
		{
			this.currentTier = 0;
			this.spriteRenderer.sprite = this.growthTiers[this.currentTier].Sprite;
		}


		public void Start()
		{
			OnSpawn();
		}


		public void Unwater()
		{
			this.isWatered = false;
			this.cropBlock.Unwater();
		}


		public void Water()
		{
			if (this.IsReadyForHarvest)
				return;

			this.isWatered = true;
			this.cropBlock.Water(); // Can end up being called twice.
		}


		[Serializable]
		public class GrowthTier
		{
			[SerializeField]
			private Sprite sprite;


			#region Properties
			public Sprite Sprite
			{
				get { return this.sprite; }
			}
			#endregion
		}
	}
}