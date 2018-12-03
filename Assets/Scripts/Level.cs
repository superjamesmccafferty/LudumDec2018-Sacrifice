using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Events;
using UnityEngine;

namespace Sacrifice 
{
    public class Level : MonoBehaviour
    {

        public GameObject PlayerPrefab;
        public ReliableEvent_SPlayerWin PlayerWinEvent;

        private int _nbPlayer;
        private GameSessionManager _GSManager;

        void Start()
        {
            _GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();
            int nbPlayer = _GSManager.NbPlayer;

            // Instantiate the selected amount of player
            for (int i = 1; i <= nbPlayer; i++)
            {
                // Will get the next spawn available and instantiate the player on it
                Instantiate(
                  PlayerPrefab,
                  GameObject.Find("Spawn" + i).transform.position,
                  new Quaternion());
            }
        }

        // Verifie if there's only one player left on the screen
        public void PlayerDied() 
        {
            GameObject[] players  = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length.ToString());

            if (players.Length != _nbPlayer) // Will replace this by <= 0
            {
                _GSManager.RoundWon(new SPlayerWin("Nobody", new Color(0, 0, 0)));
                // PlayerWinEvent.Raise(new SPlayerWin("Nobody", new Color(0, 0, 0)));
            }
            else if (players.Length == _nbPlayer) // Will replace 3 by 1 in the integration process
            {
                _GSManager.RoundWon(new SPlayerWin("Domingo", new Color(0, 0, 255)));
                // PlayerWinEvent.Raise(new SPlayerWin("Domingo", new Color(0, 0, 255)));
            }
        }
    }
}
