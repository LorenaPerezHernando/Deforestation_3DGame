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
        public void InitializePlayer(HealthSystem health, CharacterController player, Inventory inventory, InteractionSystem interaction, Transform playerFollow, DaggerHurts dagger)
        {
            _playerHealth = health;
            _player = player;
            _inventory = inventory;
            _interactionSystem = interaction;
            _playerFollow = playerFollow;
            _dagger = dagger;
        }
        public void InitializeMachine(Transform follow, MachineController machine)
        {
            _machineFollow = follow;
            _machine = machine;
        }
    }
}
