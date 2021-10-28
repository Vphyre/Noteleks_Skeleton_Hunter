using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : Projectile
{
    [SerializeField] private LayerMask layer;

    protected void OnCollisionEnter(Collision collisionInfo)
    {
        //Comparing the bit flag between the layers
        if(((1 << collisionInfo.gameObject.layer) & layer) != 0)
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Enemy e = collisionInfo.gameObject.GetComponent<Enemy>();
            if(e != null)
            {
                e.TakeDamage(15);
            }
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GetComponent<Collider>().enabled = true;
    }
}
