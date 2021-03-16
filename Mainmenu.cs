using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Com.Shon.YallaBalagan
{

    public class Mainmenu : MonoBehaviourPunCallbacks
    { 
        Launcher launcher;
        public void Awake()
        {
            launcher = GameObject.FindObjectOfType<Launcher>();
        }
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void ConnectGame()
        {
            launcher.Connect();
        }

        public void Quitgame()
        {
            Application.Quit();
        }
    


    }
}
