using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

namespace Com.Shon.YallaBalagan
{

    public class Gun : MonoBehaviourPunCallbacks
    {
        [SerializeField]
         Text Amotext;
        private string slash = "/";
        public int damge = 10;
        public int NumofBullet = 30;
        public float recoil;
        public float kickBack;
        public float reloadtime;
        public float range = 100f;
        public float Impactforce = 100;
        public float Firerate = 15;
        public Camera fpscam;
        public GameObject hitimage;
        public GameObject Weapon;
        public ParticleSystem Muzzlflash;
        public ParticleSystem SmokeFromGun;
        public ParticleSystem Shell;
        public GameObject impactEffect;
        public AudioSource GunAs;
        public AudioSource HitAs;
        public Target ShootingAi;
        public LayerMask WhatisEnemy;

        private float Timetofire = 0f;
        public bool reloading = false;
        bool Reload;
        bool Redytoshoot;
        int BulletLeft,BulletsFired = 0 , totalbullet = 90;
        public Animator handsAnimator;
        public GameObject Scar;

        public bool Shooting;
       

        private void Start()
        {
            hitimage.SetActive(false);
            Shooting = false;
            BulletLeft = NumofBullet;
            Redytoshoot = true;
            
        }
        // Update is called once per frame
        void Update()
        {
      
            if (!photonView.IsMine)
                return;

            updateBullet(BulletLeft , totalbullet);
            Reload = Input.GetKey(KeyCode.R);
            Weapon.transform.localPosition = Vector3.Lerp(Weapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
            // asking if we pressed the "Fire" butten.
            if (Input.GetButton("Fire1") && Time.time >= Timetofire)
            {
           

                Timetofire = Time.time + 1.4f / Firerate;
                // if we did , we then use a "shoot" function :

                if (BulletLeft > 0 && totalbullet > 0 && !reloading && Redytoshoot)
                {
                   
                    Shooting = true;
                    if (photonView.IsMine)
                    {
                        photonView.RPC("Shoot", RpcTarget.All);
                    }
                }
                // Mag Empty 
                if (BulletLeft <= 0 && !reloading && !Redytoshoot)
                {
                    if (photonView.IsMine)
                    {
                        Shooting = false;
                        photonView.RPC("ReloadGun", RpcTarget.All);
                    }
                }
                // Self Reloading
          
            }

                if (Reload && !reloading && BulletLeft < NumofBullet)
                {
                    if (photonView.IsMine)
                    {
                         Debug.Log("Is Reloading");
                         Shooting = false;
                         photonView.RPC("ReloadGun", RpcTarget.All);
                    }
                  
                }
        }
        [PunRPC]
        void Shoot()
        {
            Muzzlflash.Play();
            SmokeFromGun.Play();
            Shell.Play();
            // RecoilEffect(recoil, kickBack);
           
            // Raycast is a structure that gets information From what we are pointing to 
            RaycastHit hit = new RaycastHit();

            //                 -from Were -           , To Where                  , Hit info , range
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                if(photonView.IsMine)
                {
                    if (hit.collider.gameObject.layer == 11 )
                    {
                         HitAs.Play();
                         Debug.Log("ENEMY I dentfied!");
                        hitimage.SetActive(true);
                        Invoke("HitOff", 0.1f);
                        hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, damge);

                       
                    }
                   
                }

                Target target = hit.transform.GetComponent<Target>();

            
                if (target != null)
                {

                    target.TakeDamage(damge);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * Impactforce);
                }
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
                
            }
            BulletLeft--;
            BulletsFired++;

            if (BulletLeft <= 0)
                Redytoshoot = false;


        }
        [PunRPC]
        private void TakeDamage(int damage)
        {
            Debug.Log("ENEMY HIT !");
            GetComponent<PlayerMovment>().TakeDamage(damage);
        }
        private void HitOff()
        {
            hitimage.SetActive(false);
        }

        public bool IsShooting(bool shooting )
        {
             shooting = Shooting;
            return shooting;

        }
       [PunRPC]
        void ReloadGun()
        {
            
            reloading = true;
            handsAnimator.SetBool("Reloading", reloading);
            Invoke("ReloadFinished", reloadtime);

        }

        void ReloadFinished()
        {
            totalbullet -= BulletsFired;
            BulletsFired = 0;
            BulletLeft = NumofBullet;
            if (totalbullet < NumofBullet && (totalbullet+BulletLeft) <=30)
            {
                BulletLeft += totalbullet;
            }
            if (totalbullet < 0)
                totalbullet = 0;
            reloading = false;
            handsAnimator.SetBool("Reloading", reloading);
            Redytoshoot = true;
        }
        public void RecoilEffect(float Recoil, float kickBack)
        {
            Weapon.transform.Rotate(-Recoil, 0, 0);
            Weapon.transform.position -= gameObject.transform.forward * kickBack;
        
        }
        public void updateBullet(int Bullets , int Total)
        {
            Amotext.text = Bullets.ToString() + slash + totalbullet.ToString();
        }
    }
    
}
