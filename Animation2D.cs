using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


namespace Com.Shon.YallaBalagan { 
    public class Animation2D : MonoBehaviourPunCallbacks
    {
        Animator animator;
        PlayerMovment isgrounded;
        float MaxRunBack = -0.5f;
        float acceloration = 2f;
        float deceloration = 1.5f;
        float velocityX = 0.0f;
        float velocityZ = 0.0f;

        // Start is called before the first frame update

        void Start()
        {
            if(photonView.IsMine)
            {
                isgrounded = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
                animator = GetComponent<Animator>();   
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine)
                return;
            bool pressW = Input.GetKey("w");
            bool pressA = Input.GetKey("a");
            bool pressD = Input.GetKey("d");
            bool pressS = Input.GetKey("s");
            bool pressShift = Input.GetKey("left shift");
            if(Input.GetKey(KeyCode.Space)&& isgrounded.isGrounded)
            {
                animator.SetBool("IsJump", true);
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("IsJump", false);
            }
            if (pressW && velocityZ <0.5)
            {
                velocityZ += Time.deltaTime * acceloration;
            }
            if(!pressW && velocityZ > 0)
            {
                velocityZ -= (deceloration/2) * Time.deltaTime;
            }
            if(!pressW && !pressS && velocityZ < 0)
            {
                velocityZ += acceloration *Time.deltaTime;
            }
            if (!pressW && !pressS && velocityZ > 0)
            {
                velocityZ -= (acceloration/2) * Time.deltaTime;
            }
            if(velocityZ>0 && pressShift && velocityZ < 1)
            {
                velocityZ += acceloration * Time.deltaTime;
            }
            if (velocityZ > 0.5 && !pressShift)
            {
                velocityZ -= deceloration * Time.deltaTime;
            }

            if (pressS && velocityZ > MaxRunBack)
            {
                velocityZ-= acceloration * Time.deltaTime;
            }
            if(!pressS && velocityZ < 0)
            {
                velocityZ += (deceloration/2) * Time.deltaTime;
            }

            if (pressD && velocityX < 1)
            {
                velocityX += acceloration * Time.deltaTime;
            }
            if(!pressD && velocityX > 0)
            {
                velocityX -= (deceloration*2) * Time.deltaTime;
            }
            if (pressA && velocityX > -1)
            {
                velocityX -= acceloration * Time.deltaTime;
            }
            if (!pressA && velocityX < 0)
            {
                velocityX += (deceloration*2) * Time.deltaTime;
            }

           // if (!pressA && !pressD && !pressS && !pressW && !pressShift && (velocityZ < 0.002 || velocityZ > -0.002))
            //    velocityZ = 0;

            animator.SetFloat("Velocity Z",velocityZ);
            animator.SetFloat("Velocity X",velocityX);
        }
    }

}