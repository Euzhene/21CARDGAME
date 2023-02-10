using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using com.euzhene.Dialogs;

namespace com.euzhene.twentyone
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject playObject;
        private DialogUI dialogUI;

        private bool tryingToConnect = false;

        void Start()
        {
            dialogUI = DialogUI.Instance;
        }


        public override void OnConnectedToMaster()
        {
            dialogUI.Hide();
            tryingToConnect =false;

        }
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(2);
        }

        public void Play()
        {
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions()
            {
                MaxPlayers = 2,
            });
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            if (tryingToConnect) return;

            tryingToConnect = true;

            dialogUI
            .SetTitle("No internet")
            .SetMessage("Problem with connecting to server. Please try again")
            .SetActionText("RESTART")
            .SetOnClick(() =>
            {
                if (!PhotonNetwork.Reconnect())
                {
                    PhotonNetwork.ConnectUsingSettings();
                }
            })
            .Show();

        }

    }
}
