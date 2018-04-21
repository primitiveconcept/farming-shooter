namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	public abstract class ObservableRangeInt : MonoBehaviour
	{
		[SerializeField]
		private int current;

		[SerializeField]
		private int min = 0;

		[SerializeField]
		private int max = 100;

		[SerializeField]
		private bool setToMaxOnStart = true;

		[SerializeField]
		private ChangedEvent onHealthChanged;


		#region Properties
		public int Current
		{
			get { return this.current; }
		}


		public int Max
		{
			get { return this.max; }
		}


		public int Min
		{
			get { return this.min; }
		}


		public bool SetToMaxOnStart
		{
			get { return this.setToMaxOnStart; }
			set { this.setToMaxOnStart = value; }
		}
		#endregion


		public virtual void Increase(int amount)
		{
			int newValue = this.current + amount;
			if (newValue > this.max)
				newValue = this.max;

			if (newValue == this.current)
				return;

			this.current = newValue;
			this.onHealthChanged.Invoke(this);
		}


		public virtual void Reduce(int amount)
		{
			int newValue = this.current - amount;
			if (newValue < 0)
				newValue = 0;

			if (newValue == this.current)
				return;

			this.current = newValue;
			this.onHealthChanged.Invoke(this);
		}


		public virtual void SetCurrent(int value)
		{
			if (value == this.current)
				return;

			this.current = value;
			this.onHealthChanged.Invoke(this);
		}


		public virtual void SetMax(int value)
		{
			if (value < this.min)
				value = this.min;

			if (value == this.max)
				return;

			this.max = value;
			this.onHealthChanged.Invoke(this);
		}


		public virtual void SetMin(int value)
		{
			if (value > this.max)
				value = this.max;

			if (value == this.min)
				return;

			this.min = value;
			this.onHealthChanged.Invoke(this);
		}


		public virtual void Start()
		{
			if (this.setToMaxOnStart)
				SetCurrent(this.max);
		}


		public class ChangedEvent : UnityEvent<ObservableRangeInt>
		{
		}
	}
}