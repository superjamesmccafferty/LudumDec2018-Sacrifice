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
        ReliableEvent OnShoot;


        Vector3 _direction;

        void Start()
        {
            _direction = (_direction_end.position - _direction_start.position).normalized;
        }

        public void Init(float force, float damage, ReliableEvent_float force_change_event, ReliableEvent_float bullet_damage_event)
        {
            _force = force;
            _damage = damage;

            force_change_event.Subscribe(f => _force = f);
            bullet_damage_event.Subscribe(f => _damage = f);
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

            Vector2 force_to_add = new Vector2(
                _direction.x * direction_mod * _force,
                _direction.y * _force
            );

            rb2.AddForce(force_to_add, ForceMode2D.Force);

            OnShoot.Raise();

        }

        // --- EVENTS ---
        CoreEventToken Subscribe_OnShoot(UnityAction callback)
        {
            return OnShoot.Subscribe(callback);
        }

    }
}