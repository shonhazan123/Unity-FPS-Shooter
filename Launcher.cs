using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Com.Shon.YallaBalagan
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        PlayerName playername;
        Mainmenu mainmenu;
        bool Enterd;
        public void Awake()
        {
             playername = GetComponent<PlayerName>();
             PhotonNetwork.NickName = playername.savename;
           PhotonNetwork.AutomaticallySyncScene = true;
        }
     
        // 1 ) Once were connected //
        public override void OnConnectedToMaster()
        {
            Debug.Log("CONNECTED !");
            // 2 ) Try to Join a room //
            Join();
            base.OnConnectedToMaster();
            
        }
        public override void OnJoinedRoom()
        {
            // if it sDeceeded [ (2) ]  we will start the game //
            StartGame();

            base.OnJoinedRoom();
        }
        // If Join room was not secsesfull ( 2 ) //
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            // we will create a room
            Creat();

            base.OnJoinRandomFailed(returnCode, message);
        }
        public void Connect()
        {
            Debug.Log("Tryin to connect...");
            PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
            
        }

        public void Join()
        {
            PhotonNetwork.JoinRandomRoom();

        }

        public void Creat()
        {
            PhotonNetwork.CreateRoom("");
        }

        public void StartGame()
        {
            // Check if there is more than one player //
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                //Loading the wanted map//
                PhotonNetwork.LoadLevel(2);
            }

        }

    }
}
