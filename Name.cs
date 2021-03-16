
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

namespace Com.Shon.YallaBalagan
{

    public class Name : MonoBehaviourPunCallbacks
    {

        public TextMeshProUGUI nameText;
        private PlayerName savedName;
        private string Playername;
        // Start is called before the first frame update
        void Start()
        {
            
            SetName();
        }
       
        public void SetName()
        {
         nameText.text = photonView.Owner.NickName;   

        }
    }

}

            
            
            
                
