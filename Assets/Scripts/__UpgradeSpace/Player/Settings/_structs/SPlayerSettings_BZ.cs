using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    [System.Serializable]
    public struct SPlayerSettings_BZ
    {
        [Tooltip("Force applied to jump action")]
        public float jump_force;

        [Range(0, 0.3f)]
        [Tooltip("Time is take to smooth between vectors")]
        public float movement_smoothing;

        [Tooltip("Can you contorl the character in the air")]
        public bool is_air_control_on;

        [Tooltip("Speed at which velocity is increased")]
        public float move_speed;
    }
}
