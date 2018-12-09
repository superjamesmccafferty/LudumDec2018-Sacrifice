using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    [CreateAssetMenu(fileName = "SOGameSettings", menuName = "Sacrifice/SOGameSettings")]
    public class SOGameSettings : ScriptableObject
    {

        public Dictionary<EPlayerColor, string> Players { get; } = new Dictionary<EPlayerColor, string>();

    }

}