namespace FarmingShooter
{
	using UnityEngine;


	public class Spawner : MonoBehaviour
	{
		[SerializeField]
		private GameObject prefab;

		[SerializeField]
		private bool spawnOnStart = true;


		public void Start()
		{
			if (this.spawnOnStart)
				PoolManager.Spawn(this.prefab, this.transform.position, this.transform.rotation);
		}

		/*
		public void OnDrawGizmos()
		{
			if (this.prefab == null)
				return;

			SpriteRenderer spriteRenderer = this.prefab.GetComponent<SpriteRenderer>();
			if (spriteRenderer == null)
				return;

			SpriteRenderer sprite = spriteRenderer.sprite;
			if (sprite == null)
				return;

			Rect rect = sprite.rect;

			rect.width /= (sprite.pixelsPerUnit * Camera.main.orthographicSize);
			rect.height /= (sprite.pixelsPerUnit * Camera.main.orthographicSize);
			rect.center = Camera.main.WorldToViewportPoint(this.transform.position);
			
		
			Gizmos.DrawGUITexture(rect, sprite.texture, spriteRenderer.sharedMaterial);
		}
		*/
	}
}
