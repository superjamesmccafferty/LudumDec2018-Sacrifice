using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacrifice;
using UnityEngine.Events;

namespace TobiasUN.Core.Events
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "ScriptableEvent_SSacrificeStats", menuName = "Events/SSacrificeStats")]
    public class ScriptableEvent_SSacrificeStats : ScriptableEvent<SSacrificeStats, CoreEvent_SSacrificeStats>
    {
    }
}
