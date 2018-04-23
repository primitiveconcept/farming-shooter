namespace FarmingShooter
{
	using UnityEngine;


	public class DaySystem : MonoBehaviour
	{
		[SerializeField]
		private int currentDay;

		[SerializeField]
		private Transform playerSpawn;

		private Vector2 levelStartPosition;


		public void Awake()
		{
			this.levelStartPosition = Camera.main.transform.parent.position;
		}


		public void MoveToNextDay()
		{
			Crop[] crops = FindObjectsOfType<Crop>();
			foreach (Crop crop in crops)
			{
				if (!crop.IsReadyForHarvest
					&& crop.IsWatered)
				{
					crop.IncrementTier();
					crop.Unwater();
				}
			}

			Camera.main.transform.parent.position = this.levelStartPosition;
			PlayerInput player = FindObjectOfType<PlayerInput>();
			player.gameObject.transform.position = this.playerSpawn.position;
			Health playerHealth = player.GetComponent<Health>();
			playerHealth.SetCurrent(playerHealth.Max);
		}
	}
}