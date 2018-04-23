namespace FarmingShooter
{
	using UnityEngine;


	public class DaySystem : MonoBehaviour
	{
		[SerializeField]
		private int currentDay;

		[SerializeField]
		private Spawner playerSpawn;

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
			GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
			if (player == null)
			{
				player = this.playerSpawn.Spawn();
			}
			player.transform.position = this.playerSpawn.transform.position;
			Health playerHealth = player.GetComponent<Health>();
			playerHealth.SetCurrent(playerHealth.Max);
			FindObjectOfType<UIPlayerHud>().ObservePlayer(player);


			Spawner[] spawners = FindObjectsOfType<Spawner>();
			foreach (Spawner spawner in spawners)
			{
				if (!spawner.Prefab.CompareTag(Tags.Player))
				{
					spawner.DespawnAll();
					spawner.Spawn();
				}
			}
		}
	}
}