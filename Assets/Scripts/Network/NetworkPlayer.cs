using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Deforestation;
using Deforestation.Recolectables;
using Deforestation.Interaction;
using StarterAssets;
using UnityEngine.InputSystem;

public class NetworkPlayer : MonoBehaviourPun
{
    [Header("Avatar")]
    [SerializeField] private GameObject _3DAvatar;
    [Header("Scripts in Player")]
    private GameController _gameController;
    [SerializeField] private HealthSystem _health;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InteractionSystem _interactionSystem;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private PlayerInput _playerInput;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>(true);

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
        if(photonView.IsMine)
        {
            //GameController
            _3DAvatar.SetActive(false);
        }
        else
        {
            //los otros players 
            Destroy(_health); Destroy(_inventory); Destroy(_interactionSystem); Destroy(_characterController);
            Destroy(_firstPersonController); Destroy(_inputs); Destroy(_playerInput);
            _3DAvatar.SetActive(true);
        }
    }


    void Update()
    {
        
    }
}
