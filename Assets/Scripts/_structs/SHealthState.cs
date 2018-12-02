using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sacrifice
{
    public struct SHealthState
    {

        public float CurrentHealth { get; private set; }

        public float TotalHealth { get; private set; }

        public float NonSacrifisedPercentage { get; private set; }


        public SHealthState(float cur_health, float tot_health, float non_sacrifised_perc)
        {
            CurrentHealth = cur_health;
            TotalHealth = tot_health;
            NonSacrifisedPercentage = non_sacrifised_perc;
        }

    }
}