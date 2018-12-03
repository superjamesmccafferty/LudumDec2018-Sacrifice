using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Sacrifice 
{
    public class GameSessionManager : MonoBehaviour
    {

        public int NbPlayer;
        public bool EscapeEnable;

        private LevelManager _levelManager;
        private int[] _winCount = new int[4] { 0, 0, 0, 0 };

        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            _levelManager = new LevelManager();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && EscapeEnable)
            {
                Debug.Log("Escape Menu");
            }
        }

        public void StartNewSession(int nbPlayer)
        {
            this.NbPlayer = nbPlayer;
            this.EscapeEnable = true;

            _nextRound(nbPlayer);
        }

        private void _nextRound(int nbPlayer) 
        {
            _levelManager.GoToNextLevel(nbPlayer);
        }

        public void RoundWon(SPlayerWin winner)
        {
            // Each player will have an ID
            // For now I'll only use the first index
            _winCount[0] += 1;

            if (_winCount[0] == 3)
            {
                _resetCounter();
                GameWon();
            }
            else 
            {
                _nextRound(this.NbPlayer);
            }
        }

        public void GameWon()
        {
            _resetCounter();
            _levelManager.ReturnToMenu();

            Destroy(gameObject);
        }

        private void _resetCounter() 
        {
            for (int i = 0; i < 4; i++)
            {
                _winCount[i] = 0;
            }
        }
    }
}
