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
        [SerializeField] private float _maxHealth = 100f;
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
			
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			OnHealthChanged?.Invoke(_currentHealth);
			if(_bloodParticle != null)
				_bloodParticle.Play();
			if (_currentHealth <= 0)
			{
				print("Died"); print( "Died" +name + ": " + _currentHealth.ToString());
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
				StartCoroutine(DinoDied());

			}
			if(gameObject.tag == "Rock")
				Destroy(gameObject);

			if(gameObject.tag == "Player" || gameObject.tag == "Machine")
			{
				OnDeath?.Invoke();
				Debug.Log("Invoked Player Death");
			}
			
        }

		IEnumerator DinoDied()
		{
			yield return new WaitForSeconds(3f);
            //TODO Particula de nubes
            NavMeshAgent _agent = GetComponent<NavMeshAgent>();
            Destroy(_agent);
            Destroy(this);
            Destroy(gameObject);
        }

		internal void RevivedHealth()
		{
			OnHealthChanged?.Invoke(_maxHealth);
			_currentHealth = _maxHealth;
		}
		
	}

}