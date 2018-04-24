namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.UI;


	public class AscensionUpgrader : MonoBehaviour
	{
		private const int None = -1;
		private const int Speed = 0;
		private const int Weapon = 1;

		[SerializeField]
		private ItemData scoreItem;

		[SerializeField]
		private int currentSelection;

		[SerializeField]
		private int[] maxUpgrades;

		[ReadOnly]
		[SerializeField]
		private int[] currentUpgrades;

		[SerializeField]
		private Image[] upgradeImages;

		[SerializeField]
		private UnityEvent[] upgradeActions;

		private Actor playerActor;
		private IMovable playerMovement;
		private IMovable levelMovement;

		private float originalPlayerSpeed;
		private float originalLevelSpeed;


		public void ActivateSelection()
		{
			if (this.currentSelection < 0)
				return;

			if (this.currentUpgrades[this.currentSelection] >= this.maxUpgrades[this.currentSelection])
				return;

			this.currentUpgrades[this.currentSelection]++;

			if (this.upgradeActions[this.currentSelection] != null)
				this.upgradeActions[this.currentSelection].Invoke();

			this.upgradeImages[this.currentSelection].color = Color.gray;
			if (this.currentUpgrades[this.currentSelection] >= this.maxUpgrades[this.currentSelection])
			{
				Text text = this.upgradeImages[this.currentSelection].GetComponentInChildren<Text>();
				text.color = Color.clear;
			}

			this.currentSelection = -1;
			// TODO: Update UI.
		}


		public void Awake()
		{
			this.playerActor = GetComponent<Actor>();
			this.playerMovement = GetComponent<IMovable>();
			this.levelMovement = GameObject.FindGameObjectWithTag(Tags.AutoScroll).GetComponent<IMovable>();
			this.originalPlayerSpeed = this.playerMovement.Speed;
			this.originalLevelSpeed = this.levelMovement.Speed;

			UIPlayerHud playerHud = FindObjectOfType<UIPlayerHud>();
			Transform upgradesUI = playerHud.transform.Find("Upgrades");
			this.upgradeImages = upgradesUI.gameObject.GetComponentsInChildren<Image>(true);

			ResetUpgrades();
		}


		public void IncrementSelection()
		{
			if (this.currentSelection > -1)
				this.upgradeImages[this.currentSelection].color = Color.gray;

			this.currentSelection++;
			if (this.currentSelection >= this.maxUpgrades.Length)
				this.currentSelection = 0;

			this.upgradeImages[this.currentSelection].color = Color.yellow;

			//TODO: Update UI.
		}


		public void ResetUpgrades()
		{
			if (this.currentSelection > -1)
				this.upgradeImages[this.currentSelection].color = Color.gray;

			this.playerMovement.Speed = this.originalPlayerSpeed;
			this.playerMovement.AdditionalMovement = new Vector2(this.originalLevelSpeed, 0);
			this.levelMovement.Speed = this.originalLevelSpeed;
			this.playerActor.EquipWeapon(0);

			this.currentSelection = -1;
			this.currentUpgrades = new int[this.maxUpgrades.Length];

			foreach (var upgradeImage in this.upgradeImages)
			{
				Text text = upgradeImage.GetComponentInChildren<Text>();
				text.color = Color.black;
			}
		}


		public void SpeedUp()
		{
			this.playerMovement.Speed++;
			this.playerMovement.AdditionalMovement += new Vector2(0.5f, 0);
			this.levelMovement.Speed += 0.5f;
		}


		public void UpdateIfScoreItem(ItemEntry item)
		{
			if (item.ItemData == this.scoreItem)
				IncrementSelection();
		}


		public void WeaponUp()
		{
			this.playerActor.EquipNextWeapon();
		}
	}
}