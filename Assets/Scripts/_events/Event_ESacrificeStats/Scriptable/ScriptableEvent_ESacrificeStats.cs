using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacrifice;
using UnityEngine.Events;

namespace TobiasUN.Core.Events
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "ReliableEvent_ESacrificeStats", menuName = "Events/ESacrificeStats")]
    public class ScriptableEvent_ESacrificeStats : ScriptableEvent<ESacrificeStats, CoreEvent_ESacrificeStats>
    {
    }
}
