using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Shon.YallaBalagan
{

    public class Bilbord : MonoBehaviourPun
    {
        private Transform mainCam;
        private PlayerMovment player;
        bool IsDead;
        private Manager manager;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
            mainCam = Camera.main.transform;
            IsDead = player.isDead;
        }

        // Update is called once per frame
        private void Update()
        {   if(!IsDead)
                transform.LookAt(transform.position + mainCam.rotation * Vector3.forward, mainCam.rotation * Vector3.up);
        }
    }

}