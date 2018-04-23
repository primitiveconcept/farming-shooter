namespace FarmingShooter
{
	using UnityEngine;


	[ExecuteInEditMode]
	public class ParallaxElement : MonoBehaviour
	{
		[SerializeField]
		private float horizontalSpeed;

		[SerializeField]
		private float verticalSpeed;

		[SerializeField]
		private bool moveInOppositeDirection;

		private Vector3 previousCameraPosition;
		private bool previousMoveParallax;
		private Camera mainCamera;
		private Transform cameraTransform;
		private bool moveParallax;


		public void OnEnable()
		{
			this.mainCamera = Camera.main;
			this.cameraTransform = this.mainCamera.transform;
			this.previousCameraPosition = this.cameraTransform.position;
		}


		public void Update()
		{
			if (this.moveParallax
				&& !this.previousMoveParallax)
				this.previousCameraPosition = this.cameraTransform.position;

			this.previousMoveParallax = this.moveParallax;

			if (!Application.isPlaying
				&& !this.moveParallax)
				return;

			Vector3 distance = this.cameraTransform.position - this.previousCameraPosition;
			float direction = (this.moveInOppositeDirection) ? -1f : 1f;
			this.transform.position += Vector3.Scale(distance, new Vector3(this.horizontalSpeed, this.verticalSpeed)) * direction;

			this.previousCameraPosition = this.cameraTransform.position;
		}
	}
}