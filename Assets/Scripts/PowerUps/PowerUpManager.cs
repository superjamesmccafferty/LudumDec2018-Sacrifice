using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Utilities.Code.Collections;
using UnityEngine;


namespace Sacrifice
{
    public class PowerUpManager : MonoBehaviour
    {

        [SerializeField]
        PowerUpSpawner[] _powerup_spawners;


        List<PowerUpSpawner> non_populated = new List<PowerUpSpawner>();

        List<PowerUpSpawner> populated = new List<PowerUpSpawner>();


        void Start()
        {

            foreach (PowerUpSpawner p in _powerup_spawners)
            {
                non_populated.Add(p);
            }

        }


        public void SpawnRandomPowerUp()
        {

            // Make sure targeting correct spawners
            List<PowerUpSpawner> no_longer_populated = new List<PowerUpSpawner>();

            foreach (PowerUpSpawner sp in populated)
            {
                if (sp.is_populated == false) no_longer_populated.Add(sp);
            }

            foreach (PowerUpSpawner sp in no_longer_populated)
            {
                populated.Remove(sp);
                non_populated.Add(sp);
            }

            //if (non_populated.Count == 0)
            //{
            //    PowerUpSpawner p = UTList.Random(populated);
            //    p.Spawn();
            //}
            //else
            //{
            //    PowerUpSpawner p = UTList.Random(non_populated);

            //    p.Spawn();
            //    non_populated.Remove(p);
            //    populated.Add(p);
            //}

        }




    }
}