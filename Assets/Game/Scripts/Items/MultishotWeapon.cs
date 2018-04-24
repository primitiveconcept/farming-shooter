namespace FarmingShooter
{
	using UnityEngine;


	public class MultishotWeapon : Weapon
	{
		[SerializeField]
		private GameObject projectilePrefab;

		[SerializeField]
		private Vector2[] directions;

		public override void Attack(Vector2 direction)
		{
			base.Attack(direction);

			foreach (var shotDirection in this.directions)
			{
				GameObject projectile = PoolManager.Spawn(this.projectilePrefab, this.transform.position);
				IMovable projectileMovement = projectile.GetComponent<IMovable>();
				if (projectileMovement != null)
				{
					projectileMovement.MoveDirection = direction + shotDirection;
					projectile.GetComponent<SpriteRenderer>().flipX = projectileMovement.MoveDirection.x < 0;
					
				}
			}
		}
	}
}
