using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{

    public class FireyBumCode : MonoBehaviour
    {

        [SerializeField]
        float force;

        [SerializeField]
        float damage;

        void OnCollisionEnter2D(Collision2D col)
        {
            col.gameObject.GetComponent<PlayerStateManager>()?.BumFire(force, damage);

            if (col.gameObject.GetComponent<Projectile>() != null)
            {
                Destroy(col.gameObject);
            }
        }


    }

}