using UnityEngine;
using Photon.Pun;
public class Target : MonoBehaviourPunCallbacks
{
    public float Health  = 50f;

    public void TakeDamage(float amount)
    {
        Health -= amount;

        if(Health <= 0 )
        {
            Die();
        }

        void Die ()
        {
            Destroy(gameObject);
        }

    }
 
     
}

