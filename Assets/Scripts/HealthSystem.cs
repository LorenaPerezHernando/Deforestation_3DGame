using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Deforestation.UI;
#if ENABLE_MULTIPLAYER
using Photon.Pun;
using Deforestation.Network;
#endif


namespace Deforestation
{

#if ENABLE_MULTIPLAYER
    public class HealthSystem : MonoBehaviourPun
#else
    public class HealthSystem : MonoBehaviour
#endif
    {
    #if ENABLE_MULTIPLAYER
        [SerializeField] private UINetwork _ui;
    #endif
       
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        public event Action<float> OnHealthChanged;
		public event Action OnDeath;


        [SerializeField] private ParticleSystem _deathParticle;
		[SerializeField] private ParticleSystem _bloodParticle;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth;

		private void Awake()
		{
#if ENABLE_MULTIPLAYER
            _ui = GameObject.FindAnyObjectByType<UINetwork>();
#endif
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
				print("Died"); print("Died" + name + ": " + _currentHealth.ToString());
				//gameObject.SetActive(false);
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

            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Machine"))
            {

#if ENABLE_MULTIPLAYER
				NetworkController network = FindObjectOfType<NetworkController>();
				if (network != null && photonView.IsMine)
{
				network.photonView.RPC("RPC_DisableObjectByName", RpcTarget.All, gameObject.name);
}
#else
                Debug.Log("Modo single-player: se simula la muerte sin red.");
#endif


                OnDeath?.Invoke();
            }

            //if(gameObject.tag == "Player")
            //{
            //             gameObject.SetActive(false);


            //             GameObject[] remainingPlayers = GameObject.FindGameObjectsWithTag("Player");
            //             if (remainingPlayers.Length == 1)
            //             {
            //                 Debug.Log("VICTORIA");
            //                 photonView.RPC("RPC_PlayerDied", RpcTarget.All);

            //             }
            //             OnDeath?.Invoke();
            //	Debug.Log("Invoked Player Death");

            //}

            //if(gameObject.tag == "Machine")
            //{

            //	gameObject.SetActive(false);


            //             GameObject[] remainingMachines = GameObject.FindGameObjectsWithTag("Machine");
            //             if (remainingMachines.Length == 1)
            //             {
            //                 Debug.Log("VICTORIA");
            //                 _ui.EndGamePanel.SetActive(true);

            //             }
            //             OnDeath?.Invoke();
            //             Debug.Log("Invoked Machine Death");
            //         }

        }

#if ENABLE_MULTIPLAYER
        [PunRPC]
        public void RPC_PlayerDied()
        {
            gameObject.SetActive(false);
            _ui.EndGamePanel.SetActive(true);
        }
#endif

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