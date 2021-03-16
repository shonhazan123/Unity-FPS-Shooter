using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAnimation : MonoBehaviour
{
    Animator animator;
   
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        
    }
}
