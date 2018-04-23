namespace FarmingShooter
{
	using System.Collections;
	using UnityEngine;


	public class Destroyable : MonoBehaviour
	{
		[SerializeField]
		private GameObject destroyEffectPrefab;

		[SerializeField]
		private bool destroyOnStart;

		[SerializeField]
		private float delay;

		[SerializeField]
		private bool isPooled;


		public void Destroy()
		{
			Debug.Log("Destroyed: " + this.gameObject.name);

			if (this.destroyEffectPrefab != null)
			{
				var destroyEffect = PoolManager.Spawn(this.destroyEffectPrefab, this.transform.position);
				destroyEffect.transform.SetParent(this.transform.parent);
			}

			if (this.delay > 0)
				StartCoroutine(DelayedDestroyCoroutine());
			else
				RemoveObject();
		}


		public void DestroyImmediately()
		{
			RemoveObject();
		}


		public void OnSpawn()
		{
			if (this.destroyOnStart)
				Destroy();
		}


		public void Start()
		{
			if (this.destroyOnStart)
				Destroy();
		}


		private IEnumerator DelayedDestroyCoroutine()
		{
			if (GameTime.IsPaused)
				yield return null;

			yield return new WaitForSeconds(this.delay);
			RemoveObject();
		}


		private void RemoveObject()
		{
			if (this.isPooled)
				PoolManager.Despawn(this.gameObject);
			else
				Destroy(this.gameObject);
		}
	}
}