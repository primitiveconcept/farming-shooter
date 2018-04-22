namespace FarmingShooter
{
	using UnityEngine;


	public interface IMovable
	{
		Vector2 MoveDirection { get; set; }
		Rigidbody2D Rigidbody2D { get; }

		void Move();
	}
}
