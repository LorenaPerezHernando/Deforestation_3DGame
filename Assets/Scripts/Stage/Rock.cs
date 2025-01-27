using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Deforestation
{

	[RequireComponent(typeof(HealthSystem))]
	public class Rock : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[Header("Prefabs")]
		[SerializeField]
		private GameObject [] _crystalPrefab;
		[SerializeField]
		private GameObject [] _rockPrefab;

		[Header("Configuraion")]
		[SerializeField]
		private int _maxSpawnCount = 5; // Máximo de objetos a instanciar
		[SerializeField]
		private float _spawnRadius = 5f; // Radio de instanciación

		private HealthSystem _health;
		#endregion

		#region Unity Callbacks
		// Start is called before the first frame update
		void Awake()
		{
			_health = GetComponent<HealthSystem>();
			_health.OnDeath += DestroyRock;
		}


		#endregion

		#region Private Methods
		private void DestroyRock()
		{
			SpawnObjects(_crystalPrefab);
			SpawnObjects(_rockPrefab);
			Destroy(gameObject);
		}

		private void SpawnObjects(GameObject[] prefabs)
		{
			int spawnCount = Random.Range(1, _maxSpawnCount + 1);
			for (int i = 0; i < spawnCount; i++)
			{
				GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
				Instantiate(prefab, RandomPositionAbove(), Random.rotation);
			}
		}

		private Vector3 RandomPositionAbove()
		{
			Vector3 randomDirection = Random.insideUnitSphere;
			randomDirection.y = Mathf.Abs(randomDirection.y); // Asegura que la dirección es hacia arriba
			float spawnRadiusRandom = Random.Range(0, _spawnRadius);
			return transform.position + randomDirection * spawnRadiusRandom;
		}
		#endregion

		#region Public Methods
		#endregion
	}
}
