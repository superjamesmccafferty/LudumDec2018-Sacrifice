﻿using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrifice
{
    public class PlayerStateManager : MonoBehaviour, IDamagable
    {

        [SerializeField]
        float _inital_total_health = 100;

        [Header("Events")]
        [SerializeField]
        ReliableEvent_float OnDamage = new ReliableEvent_float();

        [SerializeField]
        ReliableEvent OnDeath = new ReliableEvent();

        [SerializeField]
        ReliableEvent_SHealthState OnHealthStateChange = new ReliableEvent_SHealthState();



        public float Health { get; private set; }

        public float TotalHealth { get; private set; }



        void Start()
        {
            Health = _inital_total_health;
            TotalHealth = _inital_total_health;
        }

        void RaiseHealthChange()
        {
            OnHealthStateChange.Raise(new SHealthState(Health, TotalHealth, Health / TotalHealth));
        }

        /// <summary>
        /// Change the total health of the player. Cannot go below 0
        /// </summary>
        /// <param name="change">Amount to change health by</param>
        public void ChangeTotalHealth(float change)
        {

            TotalHealth += change;
            if (TotalHealth < 0) TotalHealth = 0;

            if (Health >= TotalHealth) Health = TotalHealth;

        }

        /// <summary>
        /// Removes damage from the health of the player. Damage must be positive. 
        /// </summary>
        /// <param name="damage">Positive value representing damage</param>
        public void Damage(float damage)
        {
            if (damage <= 0) return;

            Health -= damage;
            if (Health <= 0) Health = 0;

            OnDamage.Raise(damage);
            OnDeath.Raise();

        }


        // EVENT SUBSCRIPTIONS
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
    }
}