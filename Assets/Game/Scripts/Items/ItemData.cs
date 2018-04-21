namespace FarmingShooter
{
	using UnityEngine;


	[CreateAssetMenu]
	public abstract class ItemData : ScriptableObject
	{
		[SerializeField]
		private string displayName;

		[SerializeField]
		private Sprite icon;

		[SerializeField]
		private int maxCount = 255;

		[SerializeField]
		private bool isUsable = true;

		[SerializeField]
		private bool isWeapon = false;

		[SerializeField]
		private bool isConsumed = true;


		#region Properties
		public string DisplayName
		{
			get { return this.displayName; }
		}


		public Sprite Icon
		{
			get { return this.icon; }
		}


		public bool IsConsumed
		{
			get { return this.isConsumed; }
		}


		public bool IsUsable
		{
			get { return this.isUsable; }
		}


		public bool IsWeapon
		{
			get { return this.isWeapon; }
		}


		public int MaxCount
		{
			get { return this.maxCount; }
		}
		#endregion


		public virtual void Attack(Transform origin)
		{
		}


		public virtual void Use(Transform origin)
		{
		}
	}
}