namespace FarmingShooter
{
	using UnityEngine;


	public class PlayerInput : MonoBehaviour
	{
		private IMoveable movement;


		public void Awake()
		{
			this.movement = GetComponent<IMoveable>();
		}


		public void Update()
		{
			var horizontalInput = Input.GetAxis(Controls.HorizontalAxis);
			var verticalInput = Input.GetAxis(Controls.VerticalAxis);

			Move(new Vector2(horizontalInput, verticalInput));
		}

		public void Move(Vector2 direction)
		{
			this.movement.Move(direction);
		}
	}
}