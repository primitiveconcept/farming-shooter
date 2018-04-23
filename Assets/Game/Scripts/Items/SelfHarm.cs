namespace FarmingShooter
{
	using UnityEngine;


	public class SelfHarm : UsableItem
	{
		[SerializeField]
		private int healthCost = 5;

		[SerializeField]
		private Blood bloodPrefab;


		public void Bleed()
		{
			PoolManager.Spawn(this.bloodPrefab.gameObject, this.transform.position);
		}


		public override void Use(Actor origin, Vector2 direction)
		{
			Health health = this.transform.parent.GetComponentInParent<Health>();

			if (health.Current < this.healthCost)
				return;

			health.Reduce(this.healthCost);
			Bleed();
		}
	}
}