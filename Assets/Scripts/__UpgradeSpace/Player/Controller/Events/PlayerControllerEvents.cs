using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TobiasUN.Core.Events;

namespace Sacrifice
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "PlayerControllerEvents", menuName = "Sacrifice/PlayerControllerEvents")]
    public class PlayerControllerEvents : ScriptableObject
    {

        [SerializeField]
        ReliableEvent_int _on_land;

        [SerializeField]
        ReliableEvent_int _on_jump_start;

        [SerializeField]
        ReliableEvent_int _on_jump_stop;

        [SerializeField]
        ReliableEvent_int _on_run_start;

        [SerializeField]
        ReliableEvent_int _on_run_stop;

        [SerializeField]
        ReliableEvent_int _on_attack_start;

        [SerializeField]
        ReliableEvent_int _on_attack_stop;


        public ReliableEvent_int OnLand { get { return _on_land; } }
        public ReliableEvent_int OnJumpStart { get { return _on_jump_start; } }
        public ReliableEvent_int OnJumpStop { get { return _on_jump_stop; } }
        public ReliableEvent_int OnRunStart { get { return _on_run_start; } }
        public ReliableEvent_int OnRunStop { get { return _on_run_stop; } }
        public ReliableEvent_int OnAttackStart { get { return _on_attack_start; } }
        public ReliableEvent_int OnAttackStop { get { return _on_attack_stop; } }

    }
}
