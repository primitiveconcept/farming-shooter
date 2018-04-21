namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public class Score : ObservableRangeInt
	{
		[SerializeField]
		private UnityEvent onMaxedOut;


		public override void Increase(int amount)
		{
			base.Increase(amount);

			if (this.Current == this.Max)
				this.onMaxedOut.Invoke();
		}
	}
}