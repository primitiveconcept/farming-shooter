namespace FarmingShooter
{
	using UnityEngine;

	public abstract class Weapon : MonoBehaviour
	{
		public abstract void Attack(Vector2 direction);
	}
}
