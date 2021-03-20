using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  namespace Com.Shon.YallaBalagan { 
    public class gunAnimation : MonoBehaviour
    {
        Animator animator;
        Gun gun;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        }

        // Update is called once per frame
        void Update()
        {
            bool reloading = gun.reloading;
            bool Isreloading = animator.GetBool("Reloading");
            bool Shoot = Input.GetKey(KeyCode.Mouse0);
            if (Shoot)
            {
                animator.SetBool("isFreezed", false);
                animator.SetBool("isShooted", true);
            }
            if(!Shoot)
            {
            
                animator.SetBool("isShooted", false);
                animator.SetBool("isFreezed", false);
          
            }
            if(reloading && !Isreloading )
            {
                animator.SetBool("Reloading", true);
                Invoke("Finish", 2f);
                Finish();
            }
             void Finish ()
            {
                animator.SetBool("Reloading", false);
            }
        }
    }

}