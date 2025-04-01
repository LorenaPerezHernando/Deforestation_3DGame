using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Deforestation
{

	public class HealthSystem : MonoBehaviour
	{
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;

		[SerializeField]
		private float _maxHealth = 100f;
		private float _currentHealth;

		private void Awake()
		{
			_currentHealth = _maxHealth;
		}

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			OnHealthChanged?.Invoke(_currentHealth);

			if (_currentHealth <= 0)
			{
				print("Died");
				Die();
			}
		}

		public void Heal(float amount)
		{
			_currentHealth += amount;
			_currentHealth = Mathf.Min(_currentHealth, _maxHealth);
			OnHealthChanged?.Invoke(_currentHealth);
		}

		public void SetHealth(float value)
		{
			_currentHealth = value;
			_currentHealth = Mathf.Min(_currentHealth, _maxHealth);
			OnHealthChanged?.Invoke(_currentHealth);
		}

		private void Die()
		{
            Animator _anim = GetComponent<Animator>();
            _anim.SetTrigger("Die");
            OnDeath?.Invoke();
			StartCoroutine(Died());
			
            // Aqu� puedes a�adir l�gica adicional para la muerte, como destruir el objeto.
        }

		IEnumerator Died()
		{
			yield return new WaitForSeconds(3f);
            //TODO Particula de nubes
            NavMeshAgent _agent = GetComponent<NavMeshAgent>();
            
            Destroy(_agent);
            Destroy(this);
            Destroy(gameObject);
        }
		
	}

}