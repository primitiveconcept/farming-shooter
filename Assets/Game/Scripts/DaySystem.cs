namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;


	public class DaySystem : MonoBehaviour
	{
		[SerializeField]
		private int currentDay;

		[SerializeField]
		private ItemData ascensionItem;

		[SerializeField]
		private Spawner playerSpawn;

		[SerializeField]
		private string[] dayMessages;

		[SerializeField]
		private GameObject dayTransitionUI;

		[SerializeField]
		private GameObject endgameScreen;

		[SerializeField]
		private Text dayCounterText;

		[SerializeField]
		private Text dayMessageText;

		[SerializeField]
		private UIPlayerHud playerHud;

		private Vector2 levelStartPosition;


		public void Awake()
		{
			this.levelStartPosition = Camera.main.transform.parent.position;
		}


		public void EndGame()
		{
			this.endgameScreen.SetActive(true);
		}


		public void RestartGame()
		{
			SceneManager.LoadScene(0);
		}


		public void BeginDay()
		{
			GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
			if (player == null)
			{
				player = this.playerSpawn.Spawn();
			}
			player.transform.position = this.playerSpawn.transform.position;
			Health playerHealth = player.GetComponent<Health>();
			playerHealth.SetCurrent(playerHealth.Max);
			this.playerHud.ObservePlayer(player);

			Spawner[] spawners = FindObjectsOfType<Spawner>();
			foreach (Spawner spawner in spawners)
			{
				if (!spawner.Prefab.CompareTag(Tags.Player))
				{
					spawner.DespawnAll();
					spawner.Spawn();
				}
			}

			this.dayTransitionUI.SetActive(false);
			GameTime.Unpause();
		}


		public void MoveToNextDay()
		{
			GameTime.Pause();
			Camera.main.transform.parent.position = this.levelStartPosition;

			GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
			if (player != null)
			{
				Inventory playerInventory = player.GetComponent<Inventory>();
				if (playerInventory.CanCraft(this.ascensionItem))
				{
					EndGame();
				}
			}

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

			this.currentDay++;
			this.dayTransitionUI.SetActive(true);
			this.dayCounterText.text = "DAY " + this.currentDay;
			if (this.currentDay < this.dayMessages.Length)
			{
				this.dayMessageText.text = this.dayMessages[this.currentDay];
			}
		}
	}
}