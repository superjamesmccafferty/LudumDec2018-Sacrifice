using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sacrifice
{
    public class Projectile : MonoBehaviour
    {

        void OnCollisionEnter2D(Collision2D col)
        {

            IDamagable dam = col.gameObject.GetComponent<IDamagable>();

            dam.Damage(10f);

            Debug.Log(dam);

            Destroy(gameObject);

        }

    }
}