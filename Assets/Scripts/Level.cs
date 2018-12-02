using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice 
{
    public class Level : MonoBehaviour
    {

        public GameObject playerPrefab;

        void Start()
        {
            var nbPlayer = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>().NbPlayer;

            // Instantiate the selected amount of player
            for (int i = 1; i <= nbPlayer; i++)
            {
                // Will get the next spawn available and instantiate the player on it
                Instantiate(
                  playerPrefab,
                  GameObject.Find("Spawn" + i).transform.position,
                  new Quaternion());
            }
        }

        // Verifie if there's only one player left on the screen
        public void PlayerDied() 
        {
            Debug.Log("A player died");              
        }
    }
}
