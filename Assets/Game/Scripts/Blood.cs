namespace FarmingShooter
{
	using UnityEngine;


	public class Blood : MonoBehaviour
	{
		public void Saturate(Collider2D other)
		{
			CropBlock cropBlock = other.GetComponent<CropBlock>();
			if (cropBlock != null)
			{
				Debug.Log("Watered");
				cropBlock.Water();
				return;
			}

			Crop crop = other.GetComponent<Crop>();
			if (crop != null)
			{
				Debug.Log("Watered");
				crop.Water();
			}
		}
	}
}