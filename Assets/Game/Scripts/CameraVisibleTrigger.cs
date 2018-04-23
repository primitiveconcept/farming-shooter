namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public class CameraVisibleTrigger : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent whileVisible;

		[SerializeField]
		private UnityEvent whileInvisible;

		private Renderer renderer;
		

		public void Start()
		{
			this.renderer = GetComponent<Renderer>();
			if (this.renderer == null)
				this.renderer = GetComponentInChildren<Renderer>(true);
		}

		public void Update()
		{
			if (this.renderer.isVisible)
			{
				if (this.whileVisible != null)
					this.whileInvisible.Invoke();
				return;
			}
			
			if (this.whileInvisible != null)
				this.whileInvisible.Invoke();
		}
	}
}
