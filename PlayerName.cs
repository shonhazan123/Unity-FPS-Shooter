using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

namespace Com.Shon.YallaBalagan
{

    public class PlayerName : MonoBehaviourPunCallbacks
    {
        public string nameOfplayer;
        public string savename;
        public Text inputText;
        public Text loadedName;
        // Start is called before the first frame update
        void Start()
        { 
            nameOfplayer = PlayerPrefs.GetString("name","none");
            loadedName.text = nameOfplayer;
        }


        public void SetName()
        {
           savename = inputText.text;
           PhotonNetwork.NickName = savename;

        }
    }

}