namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.UI;


	public class UIPlayerHud : MonoBehaviour
	{
		[SerializeField]
		private UIMeter healthMeter;

		[SerializeField]
		private Text scoreText;

		[SerializeField]
		private ItemData scoreItem;

		[SerializeField]
		private Image selectedItemIcon;

		[SerializeField]
		private Text selectedItemCount;

		private GameObject player;


		public void ObservePlayer(GameObject player)
		{
			this.player = player;

			Health health = this.player.GetComponent<Health>();
			health.OnChanged.AddListener(healthState => this.healthMeter.UpdateMeter(healthState));

			Inventory inventory = this.player.GetComponent<Inventory>();
			inventory.OnItemPickup.AddListener(OnItemPickup);
			inventory.OnItemRemoval.AddListener(OnItemRemoval);
		}


		public void ShowSelectedItem(ItemEntry itemEntry)
		{
			this.selectedItemIcon.sprite = itemEntry.ItemData.Icon;
			this.selectedItemCount.text = itemEntry.ItemData.IsConsumed
				? itemEntry.Count.ToString()
				: "";
		}
		

		private void OnItemPickup(ItemEntry itemEntry)
		{
			if (itemEntry.ItemData == this.scoreItem)
				this.scoreText.text = itemEntry.Count.ToString("D4") + " / 1000";
		}


		private void OnItemRemoval(ItemEntry itemEntry)
		{
			if (itemEntry.ItemData == this.scoreItem)
				this.scoreText.text = itemEntry.Count.ToString("D4") + " / 1000";
		}


		
	}
}