using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sacrifice
{
    public class Projectile : MonoBehaviour
    {

        public float Damage { get; set; } = 10;

        int _bounce_count = 0;

        void OnCollisionEnter2D(Collision2D col)
        {
            _bounce_count++;

            IDamagable dam = col.gameObject.GetComponent<IDamagable>();

            dam?.Damage(Damage);

            if (_bounce_count >= 5)
            {
                Destroy(gameObject);
            }


        }

    }
}