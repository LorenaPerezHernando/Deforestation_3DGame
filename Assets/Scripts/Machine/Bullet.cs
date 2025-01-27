using UnityEngine;
using System;

namespace Deforestation.Machine.Weapon
{
	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private GameObject _explosionPrefab;
		[SerializeField] private float _force = 100;
		[SerializeField] private float _damage = 10;
		private Rigidbody _rb;
		#endregion

		#region Unity Callbacks
		private void Awake()
		{
			_rb = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			_rb.AddForce(transform.forward * _force, ForceMode.Impulse);
		}
		private void OnTriggerEnter(Collider other)
		{
			HealthSystem health = other.GetComponent<HealthSystem>();
			if (health != null)
				health.TakeDamage(_damage);
			
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject, 1);
			GetComponent<Collider>().enabled = false;
		}
		#endregion

		#region Public Methods
		#endregion

		#region Private Methods
		#endregion
	}
}