namespace FarmingShooter
{
	using UnityEngine;


	public class MoveTowardTarget : MonoBehaviour
	{
		[SerializeField]
		private TargetObject targeting;

		[SerializeField]
		private Transform targetTransform;

		private IMovable movement;


		public Transform AcquireTarget()
		{
			if (this.targetTransform != null)
				return this.targetTransform;

			switch (this.targeting)
			{
				default:
				case TargetObject.Custom:
					return this.targetTransform;

				case TargetObject.NearestPlayer:
					Player nearestPlayer = Player.GetClosest(this.transform);
					if (nearestPlayer != null)
						return nearestPlayer.transform;
					return null;
			}
		}


		public void Awake()
		{
			this.movement = GetComponent<IMovable>();
		}


		public void Update()
		{
			this.targetTransform = AcquireTarget();
			if (this.targetTransform != null)
			{
				this.movement.MoveDirection =
					(this.targetTransform.position
					- this.transform.position)
					.normalized;
			}
		}
	}
}