using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Deforestation.Network
{

    public class NetworkController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UINetwork _ui;
        [SerializeField] private Transform[] _spawnPoints;
        private static bool[] _spawnPointsTaken;
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
                _spawnPointsTaken = new bool[_spawnPoints.Length];
                photonView.RPC("RPC_SpawnPlayer", RpcTarget.MasterClient);
            }
            //Player tiene que estar en la carpeta llamada "Resources"
            
            
            _ui.LoadingComplete();
        }

        [PunRPC]
        void RPC_SpawnPlayer()
        {
            int index = -1;
            for(int i = 0;  i < _spawnPoints.Length; i++)
            {
                if (!_spawnPointsTaken[i])
                {
                    //Cogemos la primera pos no cogida
                    index = i;
                    _spawnPointsTaken[i] = true;
                    break;
                }
            }
            if(index != -1)
            {
                Transform spawnPoint = _spawnPoints[index];
                PhotonNetwork.Instantiate("PlayerMultiplayer", spawnPoint.position, Quaternion.identity);
                PhotonNetwork.Instantiate("TheMachineMultiplayer", spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("No hay puntos de spawn disponibles");
            }
        }
        

        
    }

}