namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public class Health : ObservableRangeInt
	{
		[SerializeField]
		private UnityEvent onDepleted;

		private int originalHealth;

		#region Properties
		public UnityEvent OnDepleted
		{
			get { return this.onDepleted; }
		}
		#endregion


		public override void Start()
		{
			if (this.setToMaxOnStart)
				this.current = this.max;

			this.originalHealth = this.Current;
		}

		public void OnSpawn()
		{
			SetCurrent(this.originalHealth);
		}


		public override void Reduce(int amount, bool forceEvent = false)
		{
			base.Reduce(amount, forceEvent);

			if (this.Current == this.Min)
				this.onDepleted.Invoke();
		}
	}
}