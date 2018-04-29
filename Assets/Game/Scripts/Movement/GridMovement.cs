namespace FarmingShooter
{
	using System;
	using FarmingShooter.Exensions.Physics;
	using UnityEngine;


	public class GridMovement : MonoBehaviour,
								IMovable
	{
		[SerializeField]
		private float speed;

		[SerializeField]
		private Vector2 moveDirection;

		[SerializeField]
		private Vector2 additionalVelocity;

		[SerializeField]
		private float pivotInterval = 0.5f;

		[SerializeField]
		private bool autoMove;

		private bool pivotToX = true;
		private float pivotTimer;
		private new Rigidbody2D rigidbody2D;
		private bool shouldMove;


		#region Properties
		public Vector2 AdditionalVelocity
		{
			get { return this.additionalVelocity; }
			set { this.additionalVelocity = value; }
		}


		public bool AutoMove
		{
			get { return this.autoMove; }
			set { this.autoMove = value; }
		}


		public Vector2 MoveDirection
		{
			get { return this.moveDirection; }
			set { this.moveDirection = value; }
		}


		public float PivotInterval
		{
			get { return this.pivotInterval; }
			set { this.pivotInterval = value; }
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
				return;

			Vector2 heading = new Vector2();

			Vector2 velocity = heading
								* this.speed
								+ this.additionalVelocity;

			this.rigidbody2D.velocity = velocity;
		}


		public void Move()
		{
			this.shouldMove = true;
		}


		public void OnValidate()
		{
			this.moveDirection = this.moveDirection.ClampToIntegerDirection();
		}


		public void Update()
		{
			this.pivotTimer += GameTime.DeltaTime;
			if (this.pivotTimer >= this.pivotInterval)
			{
				this.pivotToX = !this.pivotToX;
				this.pivotTimer = 0;
			}
		}
	}
}