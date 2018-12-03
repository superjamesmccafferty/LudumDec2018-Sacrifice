using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Events;
using UnityEngine;

namespace Sacrifice 
{
    public class Level : MonoBehaviour
    {

        public GameObject Player1;

        public GameObject Player2;

        public GameObject Player3;

        public GameObject Player4;

        public ReliableEvent_SPlayerWin PlayerWinEvent;

        private int _nbPlayer;
        private GameSessionManager _GSManager;

        void Start()
        {
            _GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();

            GameObject[] Players = new GameObject[] { Player1, Player2, Player3, Player4 };
            GameObject[] PlayerInfos = GameObject.FindGameObjectsWithTag("player_informations");

            Debug.Log(PlayerInfos.Length);

            // Instantiate the selected amount of player
            for (int i = 1; i <= _GSManager.NbPlayer; i++)
            {
                // Will get the next spawn available and instantiate the player on it
                Instantiate(
                  Players[i - 1],
                  GameObject.Find("Spawn" + i).transform.position,
                  new Quaternion());
            }

            for (int i = 0; i < _GSManager.NbPlayer; i++) 
            {
                PlayerInfos[i].SetActive(false);
            }
        }

        // Verifie if there's only one player left on the screen
        public void PlayerDied() 
        {
            GameObject[] players  = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length.ToString());

            if (players.Length == 0) // Will replace this by <= 0
            {
                _GSManager.RoundWon(new SPlayerWin("Nobody", new Color(0, 0, 0)));
                // PlayerWinEvent.Raise(new SPlayerWin("Nobody", new Color(0, 0, 0)));
            }
            e lse if (players.Length == 1) // Will replace 3 by 1 in the integration process
            {
                _GSManager.RoundWon(new SPlayerWin("Domingo", new Color(0, 0, 255)));
                // PlayerWinEvent.Raise(new SPlayerWin("Domingo", new Color(0, 0, 255)));
            }
        }
    }
}
