using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Events;
using UnityEngine;
using UnityEngine.Events;

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

        //[SerializeField]
        float _force = 1000;

        float _damage = 10;

        [Header("Events")]
        [SerializeField]
        ReliableEvent_SSacrificeStats OnShoot;


        Vector3 _direction;

        PlayerStateManager _manager;

        void Start()
        {
            _manager = gameObject.GetComponent<PlayerStateManager>();

            _force = _manager.BulletForce;
            _damage = _manager.BulletDamage;

            _manager.SubscribeOnBulletForceChange(f => _force = f);
            _manager.SubscribeOnBulletDamageChange(f => _damage = f);

            _direction = (_direction_end.position - _direction_start.position).normalized;
        }

        public void Shoot(bool is_right)
        {
            float direction_mod = is_right ? 1 : -1;

            GameObject ob = Instantiate(
                _projectile,
                _direction_start.position + _direction * direction_mod / 2,
                Quaternion.identity);

            Projectile proj = ob.GetComponent<Projectile>();
            proj.Damage = _damage;

            Rigidbody2D rb2 = ob.GetComponent<Rigidbody2D>();
            rb2.AddForce(new Vector2(_force * direction_mod, 0), ForceMode2D.Force);

            OnShoot.Raise(_manager.ChosenSacrifice);

        }

        // --- EVENTS ---
        CoreEventToken Subscribe_OnShoot(UnityAction<SSacrificeStats> callback)
        {
            return OnShoot.Subscribe(callback);
        }

    }
}