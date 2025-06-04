using Deforestation.Machine;
using Deforestation.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Deforestation.Network
{
    public class NetworkMachine : MonoBehaviourPun
    {
        [SerializeField] private MachineController _machine;
        [SerializeField] private Transform _machineFollow;
        private NetworkGameController _gameController;

        private void Awake()
        {
            _machine = GetComponent<MachineController>();
            _machineFollow = GetComponent<Transform>();
            _gameController = FindObjectOfType<NetworkGameController>(true);
        }
        void Start()
        {
            if (photonView.IsMine)
            {
                _gameController.InitializeMachine(_machineFollow, _machine);
                _gameController.gameObject.SetActive(true);
            }
            else
            {
                
            }

        }
    }

    
}
