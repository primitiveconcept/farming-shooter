namespace FarmingShooter
{
	using UnityEngine;
	using System.Collections.Generic;


	[ExecuteInEditMode]
	public class InverseKinematics : MonoBehaviour
	{
		public int iterations = 5;

		[Range(0.01f, 1)]
		public float damping = 1;

		public Transform target;
		public Transform endTransform;

		public Node[] angleLimits = new Node[0];

		Dictionary<Transform, Node> nodeCache;


		public static float SignedAngle(Vector3 a, Vector3 b)
		{
			float angle = Vector3.Angle(a, b);
			float sign = Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(a, b)));

			return angle * sign;
		}


		public void LateUpdate()
		{
			if (!Application.isPlaying)
				Start();

			if (this.target == null
				|| this.endTransform == null)
				return;

			int i = 0;

			while (i < this.iterations)
			{
				CalculateIK();
				i++;
			}

			this.endTransform.rotation = this.target.rotation;
		}


		public void OnValidate()
		{
			// min & max has to be between 0 ... 360
			foreach (Node node in this.angleLimits)
			{
				node.min = Mathf.Clamp(node.min, 0, 360);
				node.max = Mathf.Clamp(node.max, 0, 360);
			}
		}


		public void Start()
		{
			// Cache optimization
			this.nodeCache = new Dictionary<Transform, Node>(this.angleLimits.Length);
			foreach (Node node in this.angleLimits)
				if (!this.nodeCache.ContainsKey(node.Transform))
					this.nodeCache.Add(node.Transform, node);
		}


		private void CalculateIK()
		{
			Transform node = this.endTransform.parent;

			while (true)
			{
				RotateTowardsTarget(node);

				if (node == this.transform)
					break;

				node = node.parent;
			}
		}


		private float ClampAngle(float angle, float min, float max)
		{
			angle = Mathf.Abs((angle % 360) + 360) % 360;
			return Mathf.Clamp(angle, min, max);
		}


		private void RotateTowardsTarget(Transform transform)
		{
			Vector2 toTarget = this.target.position - transform.position;
			Vector2 toEnd = this.endTransform.position - transform.position;

			// Calculate how much we should rotate to get to the target
			float angle = SignedAngle(toEnd, toTarget);

			// Flip sign if character is turned around
			angle *= Mathf.Sign(transform.root.localScale.x);

			// "Slows" down the IK solving
			angle *= this.damping;

			// Wanted angle for rotation
			angle = -(angle - transform.eulerAngles.z);

			// Take care of angle limits 
			if (this.nodeCache.ContainsKey(transform))
			{
				// Clamp angle in local space
				Node node = this.nodeCache[transform];
				float parentRotation = transform.parent ? transform.parent.eulerAngles.z : 0;
				angle -= parentRotation;
				angle = ClampAngle(angle, node.min, node.max);
				angle += parentRotation;
			}

			transform.rotation = Quaternion.Euler(0, 0, angle);
		}


		[System.Serializable]
		public class Node
		{
			public Transform Transform;
			public float min;
			public float max;
		}
	}
}