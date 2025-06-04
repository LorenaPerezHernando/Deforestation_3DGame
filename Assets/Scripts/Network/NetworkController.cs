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
            //Player tiene que estar en la carpeta llamada "Resources"
            PhotonNetwork.Instantiate("PlayerMultiplayer",new Vector3(1747f, 126f, 360f), Quaternion.identity);
            
            _ui.LoadingComplete();
        }
    }

}