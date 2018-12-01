using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacrifice;
using UnityEngine.Events;

namespace TobiasUN.Core.Events
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "ScriptableEvent_SHealthState", menuName = "Events/SHealthState")]
    public class ScriptableEvent_SHealthState : ScriptableEvent<SHealthState, CoreEvent_SHealthState>
    {
    }
}
