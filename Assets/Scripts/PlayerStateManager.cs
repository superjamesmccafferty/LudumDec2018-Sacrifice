using System.Collections;
using System.Collections.Generic;

using TobiasUN.Core.Events;
using TobiasUN.Core.Utilities.Code;
using TobiasUN.Core.Utilities.Code.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrifice
{
    public class PlayerStateManager : MonoBehaviour, IDamagable
    {

        [Header("Settings")]
        [SerializeField] float _inital_total_health = 100;
        [SerializeField] float _min_health = 1;
        [SerializeField] float _health_sacrifice = 10;

        [SerializeField] float _initial_movement_speed = 10;
        [SerializeField] float _movement_speed_min = 1;
        [SerializeField] float _movement_speed_sacrifice = 0.5f;

        [SerializeField] float _initial_jump_force = 400;
        [SerializeField] float _jump_force_min = 100;
        [SerializeField] float _jump_force_sacrifice = 100;

        [SerializeField] float _initial_bullet_force = 1000;
        [SerializeField] float _bullet_force_min = 100;
        [SerializeField] float _bullet_force_sacrifice = 100;

        [SerializeField] float _initial_bullet_damage = 10;
        [SerializeField] float _bullet_damage_min = 1;
        [SerializeField] float _bullet_damage_sacrifice = 1;



        [Header("Events")]
        [SerializeField]
        ReliableEvent OnDeath = new ReliableEvent();

        [SerializeField]
        ReliableEvent_float OnDamage = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnMovementSpeedChange = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnJumpForceChange = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnBulletForceChange = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnBulletDamageChange = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_SHealthState OnHealthStateChange = new ReliableEvent_SHealthState();



        // Attributes
        public float Health { get; private set; }

        public float TotalHealth { get; private set; }

        public float MovementSpeed { get; private set; }

        public float JumpForce { get; private set; }

        public float BulletForce { get; private set; }

        public float BulletDamage { get; private set; }




        void Start()
        {
            Health = _inital_total_health;
            TotalHealth = _inital_total_health;
            MovementSpeed = _initial_movement_speed;
            JumpForce = _initial_jump_force;
            BulletForce = _initial_bullet_force;
            BulletDamage = _initial_bullet_damage;

            RaiseOnHealthChange();
            OnMovementSpeedChange.Raise(MovementSpeed);
            OnJumpForceChange.Raise(JumpForce);
            OnBulletForceChange.Raise(BulletForce);
            OnBulletDamageChange.Raise(BulletDamage);
        }


        public void SacrificeStat(SSacrificeStats stat)
        {
            SacrificeStat(stat, 0);
        }

        public void SacrificeStat(SSacrificeStats stat, int random_level)
        {
            if (random_level >= 3) return;

            switch (stat)
            {
                case SSacrificeStats.DAMAGE:
                    HandleRandomSacrifice(BulletDamage, _bullet_damage_min, _bullet_damage_sacrifice, random_level, ChangeBulletDamage);
                    return;
                case SSacrificeStats.HEALTH:
                    HandleRandomSacrifice(Health, _min_health, _health_sacrifice, random_level, ChangeHealth);
                    return;
                case SSacrificeStats.JUMP:
                    HandleRandomSacrifice(JumpForce, _jump_force_min, _jump_force_sacrifice, random_level, ChangeJumpForce);
                    return;
                case SSacrificeStats.MOVEMENT:
                    HandleRandomSacrifice(MovementSpeed, _movement_speed_min, _movement_speed_sacrifice, random_level, ChangeMovementSpeed);
                    return;
                case SSacrificeStats.RANGE:
                    HandleRandomSacrifice(BulletForce, _bullet_force_min, _bullet_damage_sacrifice, random_level, ChangeBulletForce);
                    return;
            }

        }

        void HandleRandomSacrifice(float current, float min, float sacrifice, int random_level, UnityAction<float> change_function)
        {
            float change = ModdedSacrifice(current, min, sacrifice);
            if (change >= 0)
            {
                RandomSacrifice(random_level + 1);
            }
            else
            {
                change_function(change);
            }
        }

        float ModdedSacrifice(float current, float min, float sacrifice)
        {
            float mod = Mathf.Clamp((current - min) - sacrifice, float.MinValue, 0);
            return -1 * (sacrifice + mod);
        }

        void RandomSacrifice(int sacrifice_level)
        {
            SacrificeStat(UTEnum.Random<SSacrificeStats>(), sacrifice_level + 1);
        }

        /// <summary>
        /// Change the total health of the player. Cannot go below 0
        /// </summary>
        /// <param name="change">Amount to change health by</param>
        public void ChangeTotalHealth(float change)
        {
            TotalHealth += change;

            if (TotalHealth < 0)
            {
                TotalHealth = 0;
                OnDeath.Raise();
            }

            RaiseOnHealthChange();

            if (Health >= TotalHealth) Health = TotalHealth;

        }

        public void ChangeHealth(float change)
        {
            Health += change;

            if (Health <= 0) Health = 0;
            if (change < 0) OnDamage.Raise(change * -1);

            RaiseOnHealthChange();

            if (Health == 0) OnDeath.Raise();
        }

        public void ChangeMovementSpeed(float change)
        {
            MovementSpeed += change;
            OnMovementSpeedChange.Raise(MovementSpeed);
        }

        public void ChangeJumpForce(float change)
        {
            JumpForce += change;
            OnJumpForceChange.Raise(JumpForce);
        }

        public void ChangeBulletForce(float change)
        {
            BulletForce += change;
            OnBulletForceChange.Raise(change);
        }

        public void ChangeBulletDamage(float change)
        {
            BulletDamage += change;
            OnBulletDamageChange.Raise(BulletDamage);
        }


        public void Damage(float damage)
        {
            ChangeHealth(damage * -1);
        }

        // EVENT SUBSCRIPTIONS

        void RaiseOnHealthChange()
        {
            OnHealthStateChange.Raise(new SHealthState(Health, TotalHealth, TotalHealth / _inital_total_health));
        }

        public CoreEventToken SubscribeOnHealthStateChange(UnityAction<SHealthState> callback)
        {
            return OnHealthStateChange.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnDeath(UnityAction callback)
        {
            return OnDeath.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnDamage(UnityAction<float> callback)
        {
            return OnDamage.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnMovementSpeedChange(UnityAction<float> callback)
        {
            return OnMovementSpeedChange.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnJumpForceChange(UnityAction<float> callback)
        {
            return OnJumpForceChange.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnBulletForceChange(UnityAction<float> callback)
        {
            return OnBulletForceChange.Subscribe(callback);
        }

        public CoreEventToken SubscribeOnBulletDamageChange(UnityAction<float> callback)
        {
            return OnBulletDamageChange.Subscribe(callback);
        }

    }
}