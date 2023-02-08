using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
using UnityEngine.SceneManagement;
using Photon.Realtime;

namespace com.euzhene.twentyone
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Serializable fields
        public Sprite[] picsOfClubSuit;
        public Sprite[] picsOfDiamondSuit;
        public Sprite[] picsOfHeartSuit;
        public Sprite[] picsOfSpadeSuit;
        [Tooltip("Первым элементом должно быть myHand")]
        public Transform[] handsTransform;

        public GameObject cardPrefab;
        public GameObject backCardPrefab;

        public GameObject StandBtn;
        public GameObject HitBtn;

        public TMP_Text myScoreText;

        #endregion

        #region MasterClient fields
        private Deck deck;
        private Player[] players;
        private byte maxPlayerCount;

        #endregion

        private Hand myHand;
        private int[] playersPosition;
        private int[] playersResult;
        #region Constants
        private const int MAX_SCORE_IN_HAND = 21;
        private const int START_AMOUNT_OF_CARDS = 2;
        #endregion

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

            if (!PhotonNetwork.IsMasterClient) return;
            players = PhotonNetwork.PlayerList;


            if (players.Length == maxPlayerCount)
            {
                int[] playersId = players.Select((x) => x.ActorNumber).ToArray();
                photonView.RPC("GetPlayersId", RpcTarget.All, playersId);
                StartGame();
            }
        }
        #region Network callbacks for every player


        [PunRPC]
        private void GetPlayersId(int[] playersId)
        {
            for (int i = 0; i < playersId.Length; i++)
            {
                playersPosition[i] = playersId[i];

                if (playersId[i] == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    int temp = playersPosition[0];
                    playersPosition[0] = playersId[i];
                    playersPosition[i] = temp;
                }
            }
        }


        [PunRPC]
        private void ReceiveOneCard(string cardJson)
        {
            Card card = Card.Deserialize(cardJson);

            GameObject prefab = cardPrefab;
            GameObject cardObj = Instantiate(prefab, Vector2.zero, Quaternion.identity, myHand.transform);
            cardObj.GetComponent<Image>().sprite = card.GetCardSprite();

            myHand.TakeCard(card);
            photonView.RPC("NotifyPlayersAboutNewCard", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
        }


        [PunRPC]
        private void NotifyPlayersAboutNewCard(int playerId)
        {
            int handIndex = GetIndexOfPlayerById(playerId);
            GameObject prefab = backCardPrefab;
            GameObject cardObj = Instantiate(prefab, Vector2.zero, Quaternion.identity, handsTransform[handIndex]);
        }

        [PunRPC]
        private void NotifyPlayersAboutStartOfGame()
        {
            EnableActionButtons(true);
        }

        [PunRPC]
        private void NotifyAboutStandPlayer(int playerId)
        {
            int handIndex = GetIndexOfPlayerById(playerId);
            Debug.LogFormat("Player {0} is ready", handIndex);
        }

        [PunRPC]
        private void OnGameFinished()
        {
            EnableActionButtons(false);
            photonView.RPC("SendResults", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, myHand.score);
        }


        #endregion

        #region Master Client methods
        private void StartGame()
        {
            deck = new Deck(picsOfClubSuit, picsOfDiamondSuit, picsOfHeartSuit, picsOfSpadeSuit);

            HandOutStartPack();
            photonView.RPC("NotifyPlayersAboutStartOfGame", RpcTarget.All);
        }

        private void HandOutStartPack()
        {
            for (int i = 0; i < START_AMOUNT_OF_CARDS; i++)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    GiveCardToPlayer(players[j].ActorNumber);
                }
            }
        }

        [PunRPC]
        private void GiveCardToPlayer(int playerId)
        {
            Card card = deck.GiveTopCard();
            string json = Card.Serialize(card);
            photonView.RPC("ReceiveOneCard", players.First((x) => x.ActorNumber == playerId), json);
        }

        [PunRPC]
        private void OnNewStandPlayer(int playerId)
        {
            players = PhotonNetwork.PlayerList;
            photonView.RPC("NotifyAboutStandPlayer", RpcTarget.All, playerId);
            CheckStandPlayers();
        }
        private void CheckStandPlayers()
        {
            int standPlayersCount = players.Count((player) =>
               {
                   object stand;
                   player.CustomProperties.TryGetValue("stand", out stand);
                   if (stand == null)
                   {
                       return false;
                   }
                   else
                   {
                       return (bool)stand;
                   }
               });

            if (standPlayersCount == players.Length)
            {
                photonView.RPC("OnGameFinished", RpcTarget.All);
            }
        }
        [PunRPC]
        private void SendResults(int playerId, int score)
        {
            int playerResultIndex = GetIndexOfPlayerById(playerId);
            playersResult[playerResultIndex] = score;
            if (!playersResult.Contains(0))
            {
                Debug.LogFormat("Results are: {0}, {1}", playersResult[0], playersResult[1]);
            }
        }

        #endregion

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        #region Unity Callbacks
        private void Start()
        {
            maxPlayerCount = ((byte)handsTransform.Length);

            playersPosition = new int[maxPlayerCount];

            myHand = new Hand(handsTransform[0]);

            playersResult = new int[maxPlayerCount];

            ObserveChanges();
        }

        #endregion




        public void Hit()
        {
            photonView.RPC("GiveCardToPlayer", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
        }

        public void Stand()
        {
            var table = new ExitGames.Client.Photon.Hashtable();
            table.Add("stand", true);

            if (PhotonNetwork.LocalPlayer.IsMasterClient) //приходится писать из-за бага библиотеки Photon
            {
                PhotonNetwork.LocalPlayer.CustomProperties["stand"] = true;
            }
            else
            {
                PhotonNetwork.SetPlayerCustomProperties(table);
            }
            photonView.RPC("OnNewStandPlayer", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
            EnableActionButtons(false);
        }


        private void EnableActionButtons(bool enable)
        {
            StandBtn.transform.parent.gameObject.SetActive(enable);
        }
        private void ObserveChanges()
        {
            myHand.scoreChanged = (() =>
            {
                myScoreText.text = myHand.score.ToString();
                CheckPlayerScore();
            });
        }
        private void CheckPlayerScore()
        {
            if (myHand.score >= MAX_SCORE_IN_HAND)
            {
                HitBtn.SetActive(false);
            }
        }
        private int GetIndexOfPlayerById(int playerId)
        {
            return playersPosition.ToList().IndexOf(playerId);
        }

    }


}
