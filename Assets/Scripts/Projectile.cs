using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sacrifice
{
    public class Projectile : MonoBehaviour
    {

        PlayerStateManager _shooting_player;

        public void SetPlayerReference(PlayerStateManager reference)
        {
            _shooting_player = reference;
        }

        void OnCollisionEnter2D(Collision2D col)
        {

            if (col.gameObject.tag == "Player") _shooting_player?.Hit();

            IDamagable dam = col.gameObject.GetComponent<IDamagable>();

            dam?.Damage(10f);

            Destroy(gameObject);

        }

    }
}