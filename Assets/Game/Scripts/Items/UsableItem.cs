namespace FarmingShooter
{
	using UnityEngine;


	public abstract class UsableItem : MonoBehaviour
	{
		public abstract void Use(Actor origin, Vector2 direction);
	}
}
