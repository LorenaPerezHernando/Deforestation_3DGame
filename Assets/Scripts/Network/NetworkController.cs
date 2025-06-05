using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using Deforestation.UI;
using Random = UnityEngine.Random;

namespace Deforestation.Network
{

    public class NetworkController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private UINetwork _ui;
        [SerializeField] private UIGameController _uiGameController;

        //Master
        [SerializeField] private List<Transform> _spawnPoints;
        private int _indexSpawns = 0;

        //Client
        private bool _waitingForSpawn = false;

        private GameObject _machine;
        private GameObject _player;

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("DeforestationRoom", new RoomOptions { MaxPlayers = 10 }, null);
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {

                SpawnMe(_spawnPoints[0].position);
                _spawnPoints.RemoveAt(0);
            }
            else
            {
                _waitingForSpawn = true;
                photonView.RPC("RPC_SpawnPoint", RpcTarget.MasterClient);
                Debug.Log("SpawnPoint)");

            }

            _ui.LoadingComplete();
        }

        private void SpawnMe(Vector3 spawnPoint)
        {
            _player = PhotonNetwork.Instantiate("PlayerMultiplayer", spawnPoint, Quaternion.identity);
            _machine = PhotonNetwork.Instantiate("TheMachineMultiplayer", spawnPoint + Vector3.back * 7, Quaternion.identity);

            //dead control
           // _player.GetComponent<HealthSystem>().OnDeath += PlayerDie;
            //_machine.GetComponent<HealthSystem>().OnDeath += MachineDie;

            _uiGameController.enabled = true;
        }
        [PunRPC]
        public void RPC_DisableObjectByName(string objectName)
        {
            GameObject obj = GameObject.Find(objectName);
            if (obj != null)
            {
                obj.SetActive(false);
                photonView.RPC("RPC_CheckVictory", RpcTarget.All); 
            }
        }
        [PunRPC]
        public void RPC_CheckVictory()
        {
            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] allMachines = GameObject.FindGameObjectsWithTag("Machine");

            int activePlayers = allPlayers.Count(p => p.activeInHierarchy);
            int activeMachines = allMachines.Count(m => m.activeInHierarchy);

            
            Debug.Log("Victoria 1");
            if (activePlayers == 1 || activeMachines == 1)
            {
                _ui.EndGamePanel.SetActive(true);
                
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("VICTORIA - Se ha llamado correctamente desde RPC");
            }
        }


        [PunRPC]
        void RPC_SpawnPoint()
        {
            _indexSpawns++;
            if (_indexSpawns >= _spawnPoints.Count)
                _indexSpawns = 0;
            photonView.RPC("RPC_RecievePoint", RpcTarget.Others, _spawnPoints[_indexSpawns].position);
            _spawnPoints.RemoveAt(0);

           // photonView.RPC("NPC_RecievePoint", RpcTarget.Others, _spawnPoints[0].position);

        }

        [PunRPC]
        void RPC_RecievePoint(Vector3 spawnPos)
        {
            if (_waitingForSpawn)
            {
                _waitingForSpawn = false;
                SpawnMe(spawnPos);
            }
        }

        private void MachineDie()
        {
            if (GameController.Instance.MachineModeOn)
            {
                GameController.Instance.MachineMode(false);
                _player.GetComponent<HealthSystem>().TakeDamage(1000);
            }

            StartCoroutine(CheckForVictoryifNoMachines());
            DestroyImmediate(_machine);
            SpawnExplosions(_machine.transform.position + Vector3.up * 4, 5, 5);
        }

        public void SpawnExplosions(Vector3 centerPoint, int numberOfExplosions = 4, float maxDistance = 5f)
        {
            for (int i = 0; i < numberOfExplosions; i++)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                Vector3 spawnPosition = centerPoint + randomDirection.normalized * Random.Range(0f, maxDistance);
                Instantiate(_explosionPrefab, spawnPosition, Quaternion.identity);
            }
        }
        private void PlayerDie()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _ui.EndGamePanel.SetActive(true);
        }

        private IEnumerator CheckForVictoryifNoMachines()
        {
            yield return null;

            GameObject[] remainingMachines = GameObject.FindGameObjectsWithTag("Machine");

            if (remainingMachines.Length == 1)
            {
                Debug.Log(" VICTORIA");
                _ui.EndGamePanel.SetActive(true);

            }

            GameObject[] remainingPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (remainingPlayers.Length == 1)
            {
                Debug.Log("VICTORIA");
                _ui.EndGamePanel.SetActive(true);
            }

        }

    }

}