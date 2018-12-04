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
        private GameSettings _GSet;


        void Start()
        {
            _GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();
            _GSet = GameObject.Find("GameSettings").GetComponent<GameSettings>();

            GameObject[] Players = new GameObject[] { Player1, Player2, Player3, Player4 };
            GameObject[] PlayersInformation = new GameObject[4];

            for (int i = 0; i < 4; i++)
            {
                PlayersInformation[i] = (GameObject.FindGameObjectWithTag($"player_informations_{i + 1}"));
            }

            bool[] activePlayers = _GSManager.ActivePlayer;
            for (int i = 0; i < 4; i++)
            {
                if (activePlayers[i])
                {
                    // Will get the next spawn available and instantiate the player on it
                    GameObject x = Instantiate(
                      Players[i],
                      GameObject.Find($"Spawn{i + 1}").transform.position,
                      new Quaternion());

                    x.GetComponent<PlayerStateManager>()._game_settings = _GSet;
                }
                else
                {
                    PlayersInformation[i].SetActive(false);
                }
            }
        }

        // Verifie if there's only one player left on the screen
        public void PlayerDied()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length.ToString());

            if (players.Length == 0) // Will replace this by <= 0
            {
                _GSManager.RoundWon(new SPlayerWin("Nobody", new Color(0, 0, 0)));
                // PlayerWinEvent.Raise(new SPlayerWin("Nobody", new Color(0, 0, 0)));
            }
            else if (players.Length == 1) // Will replace 3 by 1 in the integration process
            {
                _GSManager.RoundWon(new SPlayerWin("Domingo", new Color(0, 0, 255)));
                // PlayerWinEvent.Raise(new SPlayerWin("Domingo", new Color(0, 0, 255)));
            }
        }
    }
}
