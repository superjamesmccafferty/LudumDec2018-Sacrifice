using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Utilities.Code.Types;
using UnityEngine;

namespace Sacrifice
{

    public class PowerUpSpawner : MonoBehaviour
    {

        [SerializeField]
        GameObject _pickup_prefab;


        public bool is_populated { get; private set; }

        PowerUp _current_powerup;


        public void Spawn()
        {

            if (is_populated) return;

            GameObject ob = Instantiate(_pickup_prefab, gameObject.transform);
            _current_powerup = _pickup_prefab.GetComponent<PowerUp>();
            is_populated = true;

        }

    }
}