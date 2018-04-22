namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public class Health : ObservableRangeInt
	{
		[SerializeField]
		private UnityEvent onDepleted;


		#region Properties
		public UnityEvent OnDepleted
		{
			get { return this.onDepleted; }
		}
		#endregion


		public override void Reduce(int amount, bool forceEvent = false)
		{
			base.Reduce(amount, forceEvent);

			if (this.Current == this.Min)
				this.onDepleted.Invoke();
		}
	}
}