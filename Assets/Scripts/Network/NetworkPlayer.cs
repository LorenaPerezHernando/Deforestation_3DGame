using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Deforestation;
using Deforestation.Recolectables;
using Deforestation.Interaction;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

namespace Deforestation.Network
{
    public class NetworkPlayer : MonoBehaviourPun
    {
        [Header("Avatar")]
        [SerializeField] private GameObject _3DAvatar;
        [Header("Scripts in Player")]
        private NetworkGameController _gameController;
        [SerializeField] private HealthSystem _health;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InteractionSystem _interactionSystem;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private StarterAssetsInputs _inputs;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _playerFollow;
        [SerializeField] private DaggerHurts _dagger;

        private Animator _anim;

        private void Awake()
        {
            _gameController = FindObjectOfType<NetworkGameController>(true);

            _anim = _3DAvatar.GetComponent<Animator>();
            _dagger = GetComponentInChildren<DaggerHurts>();

            _health = GetComponent<HealthSystem>();
            _inventory = GetComponent<Inventory>();
            _interactionSystem = GetComponent<InteractionSystem>();
            _characterController = GetComponent<CharacterController>();
            _firstPersonController = GetComponent<FirstPersonController>();
            _inputs = GetComponent<StarterAssetsInputs>();
            _playerInput = GetComponent<PlayerInput>();

        }
        void Start()
        {
            if (photonView.IsMine)
            {
               _gameController.InitializePlayer(_health, _characterController, _inventory, _interactionSystem, _playerFollow, _dagger );
                _health.OnHealthChanged += Hit;
                _health.OnDeath += Die;
            }
            else
            {
               DisconectPlayer();

            }
        }
        void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    _anim.SetBool("Run", true);
                }
                else
                {
                    _anim.SetBool("Run", false);
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    _anim.SetTrigger("Jump");
                }
            }
        }
        private void DisconectPlayer()
        {
            //los otros players 
            Destroy(_health); Destroy(_inventory); Destroy(_interactionSystem); Destroy(_characterController);
            Destroy(_firstPersonController); Destroy(_inputs); Destroy(_playerInput); Destroy(_dagger);
        }

        private void Die()
        {
            _anim.SetTrigger("Die");
            DisconectPlayer();
            enabled = false;
        }

        private void Hit(float obj)
        {
            _anim.SetTrigger("Hit");
        }

        
    }
}
