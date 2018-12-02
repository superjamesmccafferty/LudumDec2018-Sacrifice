using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Utilities.Timer;
using UnityEngine;


namespace Sacrifice
{
    public class MonoTimerKickStarter : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            MonoTimer timer = gameObject.GetComponent<MonoTimer>();

            timer.SetTimerOptions(new STimerOptions(1f, 5f, true));
            timer.StartTimer();
        }

    }
}