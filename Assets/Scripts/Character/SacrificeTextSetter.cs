using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Sacrifice
{
    public class SacrificeTextSetter : MonoBehaviour
    {

        TextMeshProUGUI text;

        // Use this for initialization
        void Start()
        {

            text = gameObject.GetComponent<TextMeshProUGUI>();

        }


        public void ChangeSacrificeStat(ESacrificeStats sac)
        {
            text.text = sac.ToString();

        }
    }
}