namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public class Health : ObservableRangeInt
	{
		[SerializeField]
		private UnityEvent onDepleted;


		public override void Reduce(int amount)
		{
			base.Reduce(amount);

			if (this.Current == this.Min)
				this.onDepleted.Invoke();
		}
	}
}