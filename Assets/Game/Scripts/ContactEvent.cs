namespace FarmingShooter
{
	using UnityEngine;
	using UnityEngine.Events;


	[RequireComponent(typeof(Collider2D))]
	public class ContactEvent : MonoBehaviour
	{
		[SerializeField]
		private string[] onlyAffectsTags;

		[SerializeField]
		private UnityEvent onTriggerEnter;

		[SerializeField]
		private UnityEvent onTriggerStay;

		[SerializeField]
		private UnityEvent onTriggerExit;


		public void OnTriggerEnter2D(Collider2D other)
		{
			if (!Tags.HasTag(other.gameObject, this.onlyAffectsTags))
				return;

			if (this.onTriggerEnter != null)
				this.onTriggerEnter.Invoke();
		}


		public void OnTriggerExit2D(Collider2D other)
		{
			if (!Tags.HasTag(other.gameObject, this.onlyAffectsTags))
				return;

			if (this.onTriggerExit != null)
				this.onTriggerExit.Invoke();
		}


		public void OnTriggerStay2D(Collider2D other)
		{
			if (!Tags.HasTag(other.gameObject, this.onlyAffectsTags))
				return;

			if (this.onTriggerStay != null)
				this.onTriggerStay.Invoke();
		}
	}
}