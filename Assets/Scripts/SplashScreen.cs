using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

namespace com.euzhene.splashScreen
{
    public class SplashScreen : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text loadingText;

        private const string CONNECTING_TEXT = "Connecting to server";
        private const string RECONNECT_TEXT = "Reconnecting";
        private const string DISCONNECTED_TEXT = "Disconnected";
        private string currentConnectionState = CONNECTING_TEXT;
        private bool tryingToConnect = false;
        private void Start()
        {
            loadingText.text = CONNECTING_TEXT;
            InvokeRepeating("UpdateLoadingText", 0f, 0.4f);
            PhotonNetwork.ConnectUsingSettings();
        }
        private void UpdateLoadingText()
        {
            if (loadingText.text.EndsWith("..."))
            {
                loadingText.text = currentConnectionState;
            }
            else
            {
                loadingText.text += ".";
            }
        }
        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene(1);
        }
        private void WaitForConnection()
        {
            if (!PhotonNetwork.Reconnect())
            {
                currentConnectionState = RECONNECT_TEXT;

                PhotonNetwork.ConnectUsingSettings();
            }

        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            if (!tryingToConnect)
            {
                CancelInvoke("WaitForConnection");
                currentConnectionState = DISCONNECTED_TEXT;
                Debug.Log("Disconnected");
                InvokeRepeating("WaitForConnection", 2.5f, 1f);

                tryingToConnect = true;

            }
        }
    }
}
