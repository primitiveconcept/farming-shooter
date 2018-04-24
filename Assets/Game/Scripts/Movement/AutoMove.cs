namespace FarmingShooter
{
	using UnityEngine;


	public class AutoMove : MonoBehaviour
	{
		private IMovable movementComponent;


		public void Awake()
		{
			this.movementComponent = GetComponent<IMovable>();
		}

		public void Update()
		{
			this.movementComponent.Move();
		}
	}
}