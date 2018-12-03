using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sacrifice 
{
    public class Main_Menu : MonoBehaviour
    {

        private int _nbPlayer = 1;
        private GameSessionManager _GSManager;

        void Start()
        {
            _GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();
        }

        public void AddPlayer() 
        {
            _nbPlayer = 1;
        }

        public void StartSession(int nbPlayer)
        {
            _GSManager.StartNewSession(nbPlayer);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
