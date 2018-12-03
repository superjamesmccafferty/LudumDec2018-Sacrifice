﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice 
{
    public class DelayedStart : MonoBehaviour
    {

        public GameObject CountDown;

        void Start()
        {
            StartCoroutine("StartDelay");
        }

        IEnumerator StartDelay()
        {
            Time.timeScale = 0;
            float pauseTime = Time.realtimeSinceStartup + 3f;

            while (Time.realtimeSinceStartup < pauseTime)
            {
                yield return 0;
            }

            CountDown.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}