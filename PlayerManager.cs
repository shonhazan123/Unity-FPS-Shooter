using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    public GameObject gun;
    public GameObject charector;
    // Start is called before the first frame update
    void Start()
    {
        if(!photonView.IsMine)
        {
            gun.SetActive(true);
            charector.SetActive(true);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
