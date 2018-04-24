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

		[SerializeField]
		private Vector2 additionalMovement;

		private new Rigidbody2D rigidbody2D;
		private bool shouldMove;


		#region Properties
		public Vector2 AdditionalMovement
		{
			get { return this.additionalMovement; }
			set { this.additionalMovement = value; }
		}


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


		public void FixedUpdate()
		{
			this.rigidbody2D.velocity = Vector2.zero;

			if (!this.shouldMove)
				return;

			this.shouldMove = false;

			if (GameTime.IsPaused)
				this.rigidbody2D.velocity = Vector2.zero; // Hack, dammit.

			if (GameTime.IsPaused)
				return;

			Vector2 velocity = new Vector2(
									this.moveDirection.x,
									this.moveDirection.y)
								* this.speed
								+ this.additionalMovement;
			this.rigidbody2D.velocity = velocity;
		}


		public void Move()
		{
			this.shouldMove = true;
		}
	}
}