using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Deforestation.Network
{

    public class NetworkController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UINetwork _ui;
        //Server/master
        [SerializeField] private List<Transform> _spawnPoints;
        private bool _waitingforSpawn = false; 

         void Start()
         {
            PhotonNetwork.ConnectUsingSettings();
   
         }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("FPSRoom", new RoomOptions { MaxPlayers = 10 }, null);
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
                _waitingforSpawn = true;
                photonView.RPC("RPC_SpawnPoint", RpcTarget.MasterClient);
            }
            //Player tiene que estar en la carpeta llamada "Resources"
            
            
            _ui.LoadingComplete();
        }

        private void SpawnMe(Vector3 spawnPoint)
        {
            PhotonNetwork.Instantiate("PlayerMultiplayer", spawnPoint, Quaternion.identity);
            PhotonNetwork.Instantiate("TheMachineMultiplayer", spawnPoint, Quaternion.identity);
        }

        [PunRPC]
        void RPC_SpawnPoint()
        {
            photonView.RPC("RPC_ReceivePoint", RpcTarget.Others, _spawnPoints[0].position);
            _spawnPoints.RemoveAt(0);
        }
        [PunRPC]
        void RPC_ReceivePoint(Vector3 spawnPoint)
        {

            if (_waitingforSpawn)
            {
                _waitingforSpawn = false;
                SpawnMe(spawnPoint);
            }
        }



    }

}