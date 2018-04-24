namespace FarmingShooter
{
	using UnityEngine;


	public interface IMovable
	{
		float Speed { get; set; }
		Vector2 MoveDirection { get; set; }
		Vector2 AdditionalMovement { get; set; }

		Rigidbody2D Rigidbody2D { get; }

		void Move();
	}
}
