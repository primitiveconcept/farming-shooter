namespace FarmingShooter
{
	using System;
	using System.Collections;
	using UnityEngine;
	using UnityEngine.Events;


	public class CameraVisibleTrigger : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent whileVisible;

		[SerializeField]
		private UnityEvent whileInvisible;

		private Renderer renderer;


		#region Properties
		public UnityEvent WhileInvisible
		{
			get
			{
				if (this.whileInvisible == null)
					this.whileInvisible = new UnityEvent();
				return this.whileInvisible;
			}
		}


		public UnityEvent WhileVisible
		{
			get
			{
				if (this.whileVisible == null)
					this.whileVisible = new UnityEvent();
				return this.whileVisible;
			}
		}
		#endregion


		/*
		public void OnBecameInvisible()
		{
			if (this.whileInvisible != null)
				this.whileInvisible.Invoke();
		}


		public void OnBecameVisible()
		{
			if (this.whileVisible != null)
				this.whileVisible.Invoke();
		}
		*/


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
				StartCoroutine(
					ExecuteOnNextFrame(
						() =>
							{
								if (this.whileVisible != null
									&& this.renderer.isVisible)
								{
									this.whileVisible.Invoke();
								}
							}));
				return;
			}

			StartCoroutine(
				ExecuteOnNextFrame(
					() =>
						{
							if (this.whileInvisible != null
								&& !this.renderer.isVisible)
							{
								this.whileInvisible.Invoke();
							}
						}));
		}


		private IEnumerator ExecuteOnNextFrame(Action callback)
		{
			yield return null;
			callback();
		}
	}
}