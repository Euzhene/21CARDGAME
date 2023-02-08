using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace com.euzhene.twentyone
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
           
        }
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public void Play() {
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions(){
                MaxPlayers=2,
            });
        }

    }
}
