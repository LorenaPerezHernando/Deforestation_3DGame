using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace Deforestation
{

	public class HealthSystem : MonoBehaviour
	{
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;


        [SerializeField] private ParticleSystem _deathParticle;
		[SerializeField] private ParticleSystem _bloodParticle;
        private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth;

		private void Awake()
		{
			
			_deathParticle = GetComponentInChildren<ParticleSystem>();
			if( _deathParticle != null )
			_deathParticle.gameObject.SetActive(false);

			_bloodParticle = GetComponentInChildren<ParticleSystem>();
			

			
		}

        private void Start()
        {
			
            
        }

        public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			OnHealthChanged?.Invoke(_currentHealth);

			_bloodParticle.Play();
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
			if(gameObject.tag == "Dinosaur")
			{
				Animator _anim = GetComponent<Animator>();
				_anim.SetTrigger("Die");
                if (_deathParticle != null)
                    _deathParticle.gameObject.SetActive(true);
                OnDeath?.Invoke();
				StartCoroutine(Died());

			}
			
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