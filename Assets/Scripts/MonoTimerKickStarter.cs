using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Utilities.Timer;
using UnityEngine;


namespace Sacrifice
{
    public class MonoTimerKickStarter : MonoBehaviour
    {

        public float tick_timer = 1;
        public float timeout_time = 5;
        public bool auto_repeat = true;

        // Use this for initialization
        void Start()
        {
            MonoTimer timer = gameObject.GetComponent<MonoTimer>();

            timer.SetTimerOptions(new STimerOptions(tick_timer, timeout_time, auto_repeat));
            timer.StartTimer();
        }

    }
}