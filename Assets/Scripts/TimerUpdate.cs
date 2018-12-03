using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TobiasUN.Core.Utilities.Timer;

using TMPro;

namespace Sacrifice
{
    public class TimerUpdate : MonoBehaviour
    {

        TextMeshProUGUI text;

        void Start()
        {
            text = gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void FloatToText(STimerTick tick)
        {

            float time = tick.CurrentTime;

            float sub_seconds = Mathf.Round(time * 100 % 100);
            float seconds = Mathf.Floor(time) % 60;
            float minute = Mathf.Floor(time / 60);

            text.text = $"{PadFloat(minute)}:{PadFloat(seconds)}:{PadFloat(sub_seconds)}";

        }

        string PadFloat(float to_pad)
        {

            if (to_pad == 0)
            {
                return "00";
            }
            else if (to_pad < 10)
            {
                return $"0{to_pad}";
            }
            else
            {
                return $"{to_pad}";
            }

        }

    }
}