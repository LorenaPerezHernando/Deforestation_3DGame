using UnityEngine;
using System;

namespace Deforestation.Machine.Weapon
{
	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : MonoBehaviour
	{

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
            if (other.CompareTag("Player") || other.CompareTag("Machine"))
                return;


            Debug.Log("Hurt: " + other.name);
			HealthSystem health = other.GetComponent<HealthSystem>();

            if (health != null)
				health.TakeDamage(_damage); Debug.Log("Health SYs: "+ other.name);


            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject, 1);
			GetComponent<Collider>().enabled = false;
		}

        
        #endregion

    }
}