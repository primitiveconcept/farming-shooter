namespace FarmingShooter
{
	using UnityEngine;


	public abstract class UsableItem : MonoBehaviour
	{
		public abstract void Use(Vector2 direction);
	}
}
