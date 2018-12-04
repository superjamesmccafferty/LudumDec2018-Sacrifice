using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using TMPro;

namespace Sacrifice
{
    public class GameSessionManager : MonoBehaviour
    {

        public TextMeshProUGUI[] tmps;

        public int NbPlayer;

        public bool[] ActivePlayer;

        public bool EscapeEnable;

        private LevelManager _levelManager;
        private int[] _winCount = new int[4] { 0, 0, 0, 0 };
        public string[] pnames = new string[4];

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

        public void StartNewSession(int nbPlayer, bool[] activePlayer)
        {
            this.NbPlayer = nbPlayer;
            this.ActivePlayer = activePlayer;
            this.EscapeEnable = true;

            _nextRound();
        }

        private void _nextRound()
        {
            _levelManager.GoToNextLevel();
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
                _nextRound();
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

        public void pullNames()
        {

            for (int i = 0; i < 4; i++)
            {
                pnames[i] = tmps[i] != null && tmps[i].enabled && tmps[i].text.Length > 1 ? tmps[i].text : $"Player {i + 1}";
            }

        }
    }
}
