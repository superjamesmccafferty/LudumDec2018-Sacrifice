using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TobiasUN.Core.Events;
using UnityEngine.Events;

namespace Sacrifice
{
    public interface IDamagable
    {

        void Damage(float damage);

        CoreEventToken SubscribeOnDamage(UnityAction<float> callback);

    }

}