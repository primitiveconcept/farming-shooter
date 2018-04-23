namespace FarmingShooter
{
	using UnityEngine;
	public class SpawnTracker : MonoBehaviour
	{
		private Spawner spawner;


		public void OnDespawn()
		{
			if (this.spawner != null)
				this.spawner.Despawn(this.gameObject);
		}


		public void Respawn()
		{
			if (this.gameObject.activeSelf)
				this.spawner.Despawn(this.gameObject);
			this.spawner.Spawn();
		}

		public Spawner Spawner
		{
			get { return this.spawner; }
			set { this.spawner = value; }
		}
	}
}
