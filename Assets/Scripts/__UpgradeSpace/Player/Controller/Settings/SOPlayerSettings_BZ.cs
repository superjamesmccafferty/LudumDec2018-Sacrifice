using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TobiasUN.Core.Settings;

namespace Sacrifice
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "PlayerSettings_BZ", menuName = "Sacrifice/PlayerSettings_BZ")]
    public class SOPlayerSettings_BZ : SOImmutableSettings<SPlayerSettings_BZ>
    {

        [SerializeField]
        [Tooltip("Player Settings")]
        private SPlayerSettings_BZ _settings = new SPlayerSettings_BZ
        {
            jump_force = 400,
            movement_smoothing = 0.05f,
            is_air_control_on = true,
            move_speed = 600
        };

        /// <summary>
        /// Player Settings struct. Will only pass value so cannot be used to change settings
        /// </summary>
        public override SPlayerSettings_BZ Settings
        {
            get
            {
                return _settings;
            }
        }

    }
}