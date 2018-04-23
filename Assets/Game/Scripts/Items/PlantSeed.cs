namespace FarmingShooter
{
	using UnityEngine;


	public class PlantSeed : MonoBehaviour
	{
		[SerializeField]
		private Crop cropToPlant;

		[SerializeField]
		private GameObject[] randomBlockSpawns;


		public void SpawnPlant(Collider2D other)
		{
			Vector3 spawnPoint = this.transform.position;

			if (other.CompareTag(Tags.Enemy))
			{
				spawnPoint = other.transform.position;
				Health otherHealth = other.GetComponent<Health>();
				otherHealth.Reduce(otherHealth.Current);
			}

			int randomBlockIdex = Random.Range(0, this.randomBlockSpawns.Length);
			GameObject blockPrefab = this.randomBlockSpawns[randomBlockIdex];
			GameObject cropBlockObject = PoolManager.Spawn(blockPrefab, spawnPoint);
			GameObject cropObject = PoolManager.Spawn(this.cropToPlant.gameObject, spawnPoint);

			Crop crop = cropObject.GetComponent<Crop>();
			

			CropBlock cropBlock = cropBlockObject.GetComponent<CropBlock>();
			cropBlock.Crop = crop;
			crop.CropBlock = cropBlock;

			Destroyable destroyable = GetComponent<Destroyable>();
			if (destroyable != null)
				destroyable.DestroyImmediately();
		}
	}
}