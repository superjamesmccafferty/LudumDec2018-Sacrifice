using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice 
{
    public class GameSessionManager : MonoBehaviour
    {

        public int NbPlayer;
        public LevelManager Levels;

        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            Levels = new LevelManager();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Escape Menu");
            }
        }

        public void StartNewSession(int nbPlayer)
        {
            this.NbPlayer = nbPlayer;
            Levels.GoToNextLevel(nbPlayer);
        }
    }
}
