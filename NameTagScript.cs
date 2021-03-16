using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Com.Shon.YallaBalagan
{
    public class NameTagScript : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI playername;
        private Transform maincamera;
        public void Start()
        {

            playername = gameObject.GetComponent<TextMeshProUGUI>();
            maincamera = Camera.main.transform;
        }
        private void LateUpdate()
        {
            

            transform.LookAt(transform.position + maincamera.rotation * Vector3.forward,maincamera.rotation*Vector3.up);
        }
      
    }
}
