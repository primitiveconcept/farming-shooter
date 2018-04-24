namespace FarmingShooter
{
	using UnityEngine;


	public class AscensionAudio : MonoBehaviour
	{
		[SerializeField]
		private ItemData scoreItem;

		private Inventory inventory;
		private AudioSource audioSource;


		public void Awake()
		{
			this.inventory = GetComponent<Inventory>();
			this.audioSource = FindObjectOfType<UIPlayerHud>().GetComponent<AudioSource>();
		}


		public void OnSpawn()
		{
			UpdateChantVolume();
		}


		public void UpdateChantVolume()
		{
			ItemEntry currentScore = this.inventory[this.scoreItem];
			if (currentScore != null)
			{
				float newVolume = (float)currentScore.Count / currentScore.ItemData.MaxCount;
				this.audioSource.volume = newVolume;
			}
			else
				this.audioSource.volume = 0;
		}
	}
}