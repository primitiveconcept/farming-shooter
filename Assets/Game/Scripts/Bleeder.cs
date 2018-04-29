namespace FarmingShooter
{
	using UnityEngine;


	public class Bleeder : UsableItem
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
			base.Use(origin, direction);
			Health health = this.transform.parent.GetComponentInParent<Health>();
			health.Reduce(this.healthCost);
			Bleed();
		}
	}
}