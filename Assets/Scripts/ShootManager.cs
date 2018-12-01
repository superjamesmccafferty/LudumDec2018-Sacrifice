using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public class ShootManager : MonoBehaviour
    {

        [SerializeField]
        GameObject _projectile;

        [SerializeField]
        Transform _direction_start;

        [SerializeField]
        Transform _direction_end;

        [SerializeField]
        float force = 1000;


        Vector3 _direction;

        void Start()
        {
            _direction = (_direction_end.position - _direction_start.position).normalized;
        }

        public void Shoot(bool is_right)
        {

            GameObject ob = Instantiate(
                _projectile,
                _direction_start.position + _direction / 2,
                Quaternion.identity);

            Rigidbody2D rb2 = ob.GetComponent<Rigidbody2D>();

            float direction_mod = is_right ? 1 : -1;
            rb2.AddForce(new Vector2(force * direction_mod, 0), ForceMode2D.Force);


        }


    }
}