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

		private SpriteRenderer spriteRenderer;
		private Health health;
		private int startingHealth;

		public bool IsReadyForHarvest
		{
			get { return this.currentTier == this.growthTiers.Length - 1; }
		}


		public void DropItemYield()
		{
			ItemPickup.CreateFromItemEntry(this.itemYield);
			PoolManager.Despawn(this.gameObject);
		}


		public void Awake()
		{
			this.spriteRenderer = GetComponent<SpriteRenderer>();
			this.health = GetComponent<Health>();
			this.startingHealth = this.health.Current;
			this.health.OnChanged.AddListener(health => UpdateState());
		}


		public void Start()
		{
			UpdateState();
		}


		public void OnSpawn()
		{
			this.currentTier = 0;
			this.health.SetCurrent(this.startingHealth);
		}

		public void UpdateState()
		{
			int newTier = GetCurrentTier();
			if (this.currentTier != newTier)
			{
				this.spriteRenderer.sprite = this.growthTiers[newTier].Sprite;
				this.currentTier = newTier;

				if (this.onGrowthTierUpdated != null)
					this.onGrowthTierUpdated.Invoke();
			}
		}


		public int GetCurrentTier()
		{
			int currentTier = 0;

			for (int i = 0; i < this.growthTiers.Length; i++)
			{
				GrowthTier growthTier = this.growthTiers[i];
				if (this.health.Current >= growthTier.NecessaryHealth)
					currentTier = i;
			}

			return currentTier;
		}


		[Serializable]
		public class GrowthTier
		{
			[SerializeField]
			private int necessaryHealth;

			[SerializeField]
			private Sprite sprite;


			public int NecessaryHealth
			{
				get { return this.necessaryHealth; }
			}


			public Sprite Sprite
			{
				get { return this.sprite; }
			}
		}
	}
}
