namespace FarmingShooter
{
	using UnityEngine;


	public class FlightMovement : MonoBehaviour,
		IMoveable
	{
		[SerializeField]
		private float speed;

		private Rigidbody2D rigidBody;


		public void Awake()
		{
			this.rigidBody = GetComponent<Rigidbody2D>();
		}

		public float Speed
		{
			get { return this.speed; }
			set { this.speed = value; }
		}


		public void Move(Vector2 direction)
		{
			rigidBody.velocity = new Vector2(direction.x, direction.y) * this.speed;

			/*
			rigidBody.position = new Vector3(
				Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
				*/
		}
	}
}