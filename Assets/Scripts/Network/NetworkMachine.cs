using Deforestation.Machine;
using Deforestation.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Deforestation.Machine.Weapon;


namespace Deforestation.Network
{
    public class NetworkMachine : MonoBehaviourPun
    {
        [SerializeField] private MachineController _machine;
        public Transform machineFollow;
        private NetworkGameController _gameController;


        #region Unity Callbacks	

        private void Awake()
        {
            _machine.WeaponController = GetComponentInChildren<WeaponController>();
        }
        private void Start()
        {
            _gameController = FindObjectOfType<NetworkGameController>(true);
            if (photonView.IsMine)
            {
                
                _gameController.InitializeMachine(machineFollow, _machine);
                _gameController.gameObject.SetActive(true);
                _machine.enabled = true;
                _machine.WeaponController.enabled = true;
                _machine.GetComponent<MachineMovement>().enabled = true;
                //Autoridad de la vida en local
                _machine.HealthSystem.OnHealthChanged += SyncHealth;
                //Autoridad de disparos en local
                _machine.WeaponController.OnMachineShoot += SyncShoot;
            }
            else
            {
                //--
            }
        }


        #endregion

        #region Private Methods
        private void SyncShoot()
        {
            //Capturar la direccion del cañon
            //TODO: refactorizar!
            RaycastHit hit;
            Ray ray = GameController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            //Mandar RPC
            photonView.RPC("OthersShoot", RpcTarget.Others, hit.point);
        }

        [PunRPC]
        private void OthersShoot(Vector3 shootDirection)
        {
            _machine.WeaponController.Shoot(shootDirection);
        }

        private void SyncHealth(float health)
        {
            photonView.RPC("RefreshHealth", RpcTarget.Others, health);
        }

        [PunRPC]
        private void RefreshHealth(float health)
        {
            _machine.HealthSystem.SetHealth(health);
        }
        #endregion

        #region Public Methods
        #endregion


    }


}
