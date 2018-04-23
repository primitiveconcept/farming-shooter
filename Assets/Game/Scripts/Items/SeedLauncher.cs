namespace FarmingShooter
{
	using UnityEngine;


	public class SeedLauncher : UsableItem
	{
		[SerializeField]
		private GameObject seedPrefab;

		public override void Use(Actor origin, Vector2 direction)
		{
			GameObject seed = PoolManager.Spawn(this.seedPrefab, this.transform.position);
			IMovable projectileMovement = seed.GetComponent<IMovable>();
			if (projectileMovement != null)
				projectileMovement.MoveDirection = direction;
		}
	}
}
