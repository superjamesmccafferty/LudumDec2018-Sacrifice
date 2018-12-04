using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sacrifice
{
    public class Main_Menu : MonoBehaviour
    {

        private int _nbPlayer = 0;
        private bool[] activePlayers = { false, false, false, false };
        private GameSessionManager _GSManager;

        void Start()
        {
            _GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();
        }

        public void AddPlayer(int playerId)
        {
            Debug.Log($"Add Player {playerId}");
            _nbPlayer++;
            activePlayers[playerId] = true;
        }

        public void RemovePlayer(int playerId)
        {
            Debug.Log($"Removing Player {playerId}");
            _nbPlayer--;
            activePlayers[playerId] = false;
        }

        public void StartSession()
        {
            _GSManager.pullNames();
            _GSManager.StartNewSession(_nbPlayer, activePlayers);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
