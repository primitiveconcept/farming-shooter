namespace FarmingShooter
{
	using UnityEngine;


	public class AutoGrow : MonoBehaviour
	{
		[SerializeField]
		private float growSpeed;

		[SerializeField]
		private float maxScaleX;

		[SerializeField]
		private float maxScaleY;

		private Vector2 originalScale;


		public void Awake()
		{
			this.originalScale = this.transform.localScale;
		}


		public void OnSpawn()
		{
			this.transform.localScale = this.originalScale;
		}


		public void Update()
		{
			Vector2 newScale = this.transform.localScale;

			if (this.transform.localScale.x < this.maxScaleX)
			{
				newScale.x += Time.deltaTime * this.growSpeed;
				if (newScale.x > this.maxScaleX)
					newScale.x = this.maxScaleX;
			}

			if (this.transform.localScale.y < this.maxScaleY)
			{
				newScale.y += Time.deltaTime * this.growSpeed;
				if (newScale.y > this.maxScaleY)
					newScale.y = this.maxScaleY;
			}

			this.transform.localScale = newScale;
		}
	}
}