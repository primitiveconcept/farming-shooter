namespace FarmingShooter.Exensions.Physics
{
	using UnityEngine;


	public static class PhysicsExtensions
	{
		public static Rigidbody2D SetupRigidbody(this GameObject gameObject)
		{
			Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
			if (rigidbody2D == null)
			{
				rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
				rigidbody2D.gravityScale = 0;
			}
			
			rigidbody2D.isKinematic = false;
			rigidbody2D.mass = 1;
			rigidbody2D.angularDrag = 0;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

			return rigidbody2D;
		}
	}
}
