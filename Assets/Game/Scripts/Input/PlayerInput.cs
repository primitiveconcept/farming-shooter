﻿namespace FarmingShooter
{
	using UnityEngine;
	using UnityStandardAssets.CrossPlatformInput;


	public partial class PlayerInput : MonoBehaviour
	{
		[SerializeField]
		private bool locked;

		private IMovable movement;
		private Actor actor;

		public bool Locked
		{
			get { return this.locked; }
			set { this.locked = value; }
		}


		public void Awake()
		{
			this.movement = GetComponent<IMovable>();
			this.actor = GetComponent<Actor>();
		}


		public void Move(Vector2 direction)
		{
			this.movement.MoveDirection = direction;
			this.movement.Move();
		}


		public void Update()
		{
			if (this.locked
				|| GameTime.IsPaused)
			{
				return;
			}

			float horizontalInput = Input.GetAxisRaw(Controls.HorizontalAxis);
			float verticalInput = Input.GetAxisRaw(Controls.VerticalAxis);
			float mouseWheelInput = Input.GetAxisRaw(Controls.MouseWheelAxis);

			Move(new Vector2(horizontalInput, verticalInput));

			if (mouseWheelInput > 0)
				this.actor.EquipNextWeapon();
			else if (mouseWheelInput < 0)
				this.actor.EquipPreviousWeapon();

			if (CrossPlatformInputManager.GetButtonDown(Controls.Primary))
				this.actor.UseEquippedWeapon();

			
		}
	}
}