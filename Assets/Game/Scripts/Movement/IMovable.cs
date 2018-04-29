namespace FarmingShooter
{
	using UnityEngine;


	public interface IMovable
	{
		float Speed { get; set; }
		Vector2 MoveDirection { get; set; }
		Vector2 AdditionalVelocity { get; set; }

		Rigidbody2D Rigidbody2D { get; }

		void Move();
	}


	public static class MovableExtensions
	{
		public static Vector2 ClampToIntegerDirection(this Vector2 vector2)
		{
			return new Vector2(
				Mathf.Clamp((int)vector2.x, -1, 1),
				Mathf.Clamp((int)vector2.y, -1, 1));
		}

		public static Vector2 ClampToDirection(this Vector2 vector2)
		{
			return new Vector2(
				Mathf.Clamp(vector2.x, -1, 1),
				Mathf.Clamp(vector2.y, -1, 1));
		}
	}
}
