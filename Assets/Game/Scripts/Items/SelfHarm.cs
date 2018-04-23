namespace FarmingShooter
{
	using UnityEngine;


	public class SelfHarm : UsableItem
	{
		[SerializeField]
		private int healthCost = 5;

		[SerializeField]
		private Blood bloodPrefab;

		
		public override void Use(Actor origin, Vector2 direction)
		{
			var health = this.transform.parent.GetComponentInParent<Health>();
			
			if (health.Current < this.healthCost)
				return;

			health.Reduce(this.healthCost);
			PoolManager.Spawn(this.bloodPrefab.gameObject, this.transform.position);
		}
	}
}