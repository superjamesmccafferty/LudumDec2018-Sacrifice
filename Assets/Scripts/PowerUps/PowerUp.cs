using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrifice
{

    public class PowerUp : MonoBehaviour
    {

        public EPowerUp power_up;

        [SerializeField]
        float _speed = 10;

        UnityAction _action;

        void Update()
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * _speed);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            PlayerStateManager man = col.GetComponent<PlayerStateManager>();

            if (man != null)
            {
                man.PowerUp(power_up);
                DestroyObject();
            }
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
            Destroy(this);
        }

    }
}