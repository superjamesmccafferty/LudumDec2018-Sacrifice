using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacrifice;
using UnityEngine.Events;
 
namespace TobiasUN.Core.Events
{
    [System.Serializable()]
    [CreateAssetMenu(fileName = "ScriptableEvent_SPlayerWin", menuName = "Events/SPlayerWin")]
    public class ScriptableEvent_SPlayerWin : ScriptableEvent<SPlayerWin, CoreEvent_SPlayerWin>
    {
    }
}
