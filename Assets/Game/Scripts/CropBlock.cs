namespace FarmingShooter
{
	using UnityEngine;


	public class CropBlock : MonoBehaviour
	{
		[SerializeField]
		private Color wateredColor = new Color(0.7f, 0.4f, 0.4f);

		private Crop crop;
		private SpriteRenderer spriteRenderer;


		#region Properties
		public Crop Crop
		{
			get { return this.crop; }
			set { this.crop = value; }
		}


		public SpriteRenderer SpriteRenderer
		{
			get { return this.spriteRenderer; }
		}
		#endregion


		public void Awake()
		{
			this.spriteRenderer = GetComponent<SpriteRenderer>();
		}


		public void Unwater()
		{
			this.spriteRenderer.color = Color.white;
		}


		public void Water()
		{
			if (this.crop != null
				&& !this.crop.IsReadyForHarvest
				&& !this.crop.IsWatered)
			{
				this.spriteRenderer.color = this.wateredColor;
				this.crop.Water();
			}
		}
	}
}