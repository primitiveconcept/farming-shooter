namespace FarmingShooter
{
	using FarmingShooter.Exensions.Physics;
	using UnityEngine;


	public class SimpleMovement : MonoBehaviour,
								IMovable
	{
		[SerializeField]
		private float speed;

		[SerializeField]
		private Vector2 moveDirection;

		private new Rigidbody2D rigidbody2D;


		#region Properties
		public Vector2 MoveDirection
		{
			get { return this.moveDirection; }
			set { this.moveDirection = value; }
		}


		public Rigidbody2D Rigidbody2D
		{
			get { return this.rigidbody2D; }
		}


		public float Speed
		{
			get { return this.speed; }
			set { this.speed = value; }
		}
		#endregion


		public void Awake()
		{
			this.rigidbody2D = this.gameObject.SetupRigidbody();
		}


		public void Move()
		{
			Vector2 velocity = new Vector2(this.moveDirection.x, this.moveDirection.y) * this.speed;
			this.rigidbody2D.velocity = velocity;
		}
	}
}