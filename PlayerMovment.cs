using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Com.Shon.YallaBalagan
{

    public class PlayerMovment : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        Text showHeallth;
        public CharacterController controller;
        public GameObject cameraParent;
        public GameObject charector;
        public float MaxHealth = 100f;
        private float pouse = 3.5f;
        private float Currenthealth;
        public float speed = 10f;
        public float sprint = 13f;
        public float gravity = -55;
        public float jumpHight = 3f;
        Vector3 velocity;
        public Transform GroundCheck;
        public float grounddis = 1f;
        public LayerMask groundMask;
        public bool isGrounded, isDead;

        private Manager manager;
        

        private void Start()
        {
            isDead = false;
            manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
            cameraParent.SetActive(photonView.IsMine);
            charector.SetActive(false);

            if (!photonView.IsMine)
            {
                 gameObject.layer = 11;
                Debug.Log("Layer set to 11 ");
                charector.SetActive(true);
                
            }
            Currenthealth = MaxHealth;
        }
        // Update is called once per frame
        private void Update()
        {

            if (!photonView.IsMine)
                return;
            isDead = false;
            if (Input.GetKey(KeyCode.Escape))
            {
                PhotonNetwork.Destroy(gameObject);
                Application.Quit();
            }

            ShowHeallth(Currenthealth);
            isGrounded = Physics.CheckSphere(GroundCheck.position, grounddis, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHight * - 2f * gravity);
            }
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                controller.Move(move * ( 0.3f * speed) * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

           
        }

    
        public void TakeDamage(int damage)
        {
            if (photonView.IsMine)
            {

                Currenthealth -= damage;
                ShowHeallth(Currenthealth);
                Debug.Log(Currenthealth);
                if(Currenthealth <= 0 )
                {
                    isDead = true;
                    manager.Spawn();
                    Currenthealth = MaxHealth;
                    Invoke("Kill", 0.1f);
                    Kill();
               
                }
                if (Currenthealth < MaxHealth)
                {
                    Invoke("Addhealth", pouse);
                }
            }
        }
        public void Addhealth()
        {
            if(photonView.IsMine)
                if(Currenthealth< MaxHealth)
                 Currenthealth += 10f * Time.deltaTime;
        }
        
        public void Kill()
        {
            PhotonNetwork.Destroy(gameObject);
        }
        public void ShowHeallth(float Heallth)
        {
            if(photonView.IsMine)
              showHeallth.text = Heallth.ToString();
        }

    }
}
