using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Deforestation;
using Deforestation.Recolectables;
using Deforestation.Interaction;
using StarterAssets;
using UnityEngine.InputSystem;
using Deforestation.Machine;

namespace Deforestation.Network
{
    public class NetworkGameController : GameController
    {
       
        public void InitializePlayer(HealthSystem health, CharacterController player, Inventory inventory, InteractionSystem interaction, Transform playerFollow, DaggerHurts dagger) // 
        {
            _playerHealth = health;
            _player = player;
            _inventory = inventory;
            _interactionSystem = interaction;
            _dagger = dagger;
            
            _playerFollow = playerFollow;
            _virtualCamera.Follow = _playerFollow;

            if(_playerFollow == null)
            {
                _playerFollow = transform.Find("PlayerCameraRoot");
                _playerFollow = playerFollow;
            }
        
        }
        public void InitializeMachine(Transform follow, MachineController machine)
        {
            if (_machine != null)
            {
                _machine.HealthSystem.OnHealthChanged -= _uiController.UpdateMachineHealth;
            }

            _machineFollow = follow;
            _machine = machine;
            _virtualCamera.Follow = _machineFollow;

            _machine.HealthSystem.OnHealthChanged += _uiController.UpdateMachineHealth;
            //Para refrescar la UI
            //_machine.HealthSystem.TakeDamage(0);
            
        }
    }
}
