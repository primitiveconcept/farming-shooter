namespace FarmingShooter
{
	using UnityEngine;


	public class SimpleProjectileWeapon : Weapon
	{
		[SerializeField]
		private GameObject projectilePrefab;

		public override void Attack(Vector2 direction)
		{
			GameObject projectile = PoolManager.Spawn(this.projectilePrefab, this.transform.position);
			IMovable projectileMovement = projectile.GetComponent<IMovable>();
			if (projectileMovement != null)
				projectileMovement.MoveDirection = direction;
		}
	}
}
