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

        [Header("Character Control")]
        [SerializeField]
        CharacterController2D _controller;
        [SerializeField] ShootManager _shot_manager;


        [Header("Settings")]
        [SerializeField]
        float _inital_total_health = 100;
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

        [SerializeField] ESacrificeStats _chosen_sacrifice = ESacrificeStats.HEALTH;



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

        [SerializeField]
        ReliableEvent_ESacrificeStats OnChosenSacrificeChange = new ReliableEvent_ESacrificeStats();





        [SerializeField]
        ReliableEvent_float OnMovementSpeedChange_Perc = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnJumpForceChange_Perc = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnBulletForceChange_Perc = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent_float OnBulletDamageChange_Perc = new ReliableEvent_float();


        // Attributes
        public float Health { get; private set; }

        public float TotalHealth { get; private set; }

        public float MovementSpeed { get; private set; }

        public float JumpForce { get; private set; }

        public float BulletForce { get; private set; }

        public float BulletDamage { get; private set; }

        public ESacrificeStats ChosenSacrifice { get; private set; }


        bool _can_shoot = true;


        void Start()
        {
            Health = _inital_total_health;
            TotalHealth = _inital_total_health;
            MovementSpeed = _initial_movement_speed;
            JumpForce = _initial_jump_force;
            BulletForce = _initial_bullet_force;
            BulletDamage = _initial_bullet_damage;
            ChosenSacrifice = _chosen_sacrifice;


            _shot_manager.Init(BulletForce, BulletDamage, OnBulletForceChange, OnBulletDamageChange);
            _controller.Init(JumpForce, MovementSpeed, OnMovementSpeedChange, OnJumpForceChange);

            OnChosenSacrificeChange.Raise(ChosenSacrifice);
        }


        // Controls

        public void Move(float move, bool jump)
        {
            _controller.Move(move, jump);
        }

        public void Shoot()
        {
            if (_can_shoot)
            {
                _controller.Shoot();
                SacrificeStat(ChosenSacrifice);
                _can_shoot = false;
            }
        }

        public void CanShoot()
        {
            _can_shoot = true;
        }

        public void CycleNextSacrifice()
        {
            ChosenSacrifice = UTEnum.CycleEnumNext<ESacrificeStats>(ChosenSacrifice);
            OnChosenSacrificeChange.Raise(ChosenSacrifice);
        }

        public void ChooseRandomSacrifice()
        {
            ChosenSacrifice = UTEnum.Random<ESacrificeStats>();
            OnChosenSacrificeChange.Raise(ChosenSacrifice);
        }

        public void BumFire(float force, float damage)
        {
            _controller.BumFire(force);
            ChangeHealth(-1 * damage);
        }

        public void PowerUp(EPowerUp power)
        {
            Debug.Log(power);
        }

        // Sacrifice System

        public void SacrificeStat(ESacrificeStats stat)
        {
            SacrificeStat(stat, 0);
        }

        public void SacrificeStat(ESacrificeStats stat, int random_level)
        {
            if (random_level >= 20) return;

            switch (stat)
            {
                case ESacrificeStats.DAMAGE:
                    HandleRandomSacrifice(BulletDamage, _bullet_damage_min, _bullet_damage_sacrifice, random_level, ChangeBulletDamage);
                    return;
                case ESacrificeStats.HEALTH:
                    HandleRandomSacrifice(Health, _min_health, _health_sacrifice, random_level, ChangeTotalHealth);
                    return;
                case ESacrificeStats.JUMP:
                    HandleRandomSacrifice(JumpForce, _jump_force_min, _jump_force_sacrifice, random_level, ChangeJumpForce);
                    return;
                case ESacrificeStats.MOVEMENT:
                    HandleRandomSacrifice(MovementSpeed, _movement_speed_min, _movement_speed_sacrifice, random_level, ChangeMovementSpeed);
                    return;
                case ESacrificeStats.RANGE:
                    HandleRandomSacrifice(BulletForce, _bullet_force_min, _bullet_force_sacrifice, random_level, ChangeBulletForce);
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
            SacrificeStat(UTEnum.Random<ESacrificeStats>(), sacrifice_level + 1);
        }

        // Statistic Changes

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
            OnMovementSpeedChange_Perc.Raise((MovementSpeed - _movement_speed_min) / (_initial_movement_speed - _movement_speed_min));
        }

        public void ChangeJumpForce(float change)
        {
            JumpForce += change;
            OnJumpForceChange.Raise(JumpForce);
            OnJumpForceChange_Perc.Raise((JumpForce - _jump_force_min) / (_initial_jump_force - _jump_force_min));
        }

        public void ChangeBulletForce(float change)
        {
            BulletForce += change;
            OnBulletForceChange.Raise(BulletForce);
            OnBulletForceChange_Perc.Raise((BulletForce - _bullet_force_min) / (_initial_bullet_force - _bullet_force_min));

        }

        public void ChangeBulletDamage(float change)
        {
            BulletDamage += change;
            OnBulletDamageChange.Raise(BulletDamage);
            OnBulletDamageChange_Perc.Raise((BulletDamage - _bullet_damage_min) / (_initial_bullet_damage - _bullet_damage_min));

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


        public CoreEventToken SubscribeOnChosenSacrificeChange(UnityAction<ESacrificeStats> callback)
        {
            return OnChosenSacrificeChange.Subscribe(callback);
        }

    }
}