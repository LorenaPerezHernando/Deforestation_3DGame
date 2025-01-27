using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deforestation
{

	public class Tree : MonoBehaviour
	{
		#region Fields
		[SerializeField] private GameObject _fire;
		private HealthSystem _health;
		#endregion

		#region Properties
	public int Index { get; set; }
		#endregion

		#region Unity Callbacks	
		private void Awake()
		{
			_health = GetComponent<HealthSystem>();
			if (_health!= null)
				_health.OnDeath += Die;
		}

		private void Die()
		{
			Destroy(gameObject);

			int veces = Random.Range(1, 5); // Genera un número aleatorio entre 1 y 4
			for (int i = 0; i < veces; i++)
			{
				Vector3 destinoAleatorio = Random.insideUnitSphere * 5;
				destinoAleatorio += transform.position;
				UnityEngine.AI.NavMeshHit hit;
				if (UnityEngine.AI.NavMesh.SamplePosition(destinoAleatorio, out hit, 5, 1))
				{
					Instantiate(_fire, hit.position, Quaternion.identity);
				}
			}
		}

		#endregion

		#region Private Methods
		#endregion

		#region Public Methods
		#endregion

	}
}
