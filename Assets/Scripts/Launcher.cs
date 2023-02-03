using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace com.euzhene.twentyone
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        void Update()
        {

        }

        public override void OnConnectedToMaster()
        {
           
        }
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public void Play() {
            PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 5);
        }

    }
}
