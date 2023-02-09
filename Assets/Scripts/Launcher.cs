using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace com.euzhene.twentyone
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject playObject;
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
           playObject.GetComponent<Button>().interactable = true;

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
