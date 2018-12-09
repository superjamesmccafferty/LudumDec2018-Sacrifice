using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sacrifice
{
    public class GameSetupManager : MonoBehaviour
    {

        [SerializeField]
        SOGameSettings settings;


        LevelManager _levelManager;

        private void Start()
        {
            _levelManager = new LevelManager();
        }


        public void AddPlayer(EPlayerColor player)
        {
            settings.Players.Add(player, $"Player {player.ToString()}");
        }

        public void RemovePlayer(EPlayerColor player)
        {
            settings.Players.Remove(player);
        }

        public void UpdateName(EPlayerColor player, string name)
        {
            settings.Players[player] = name;
        }


        public void StartSession()
        {
            _levelManager.GoToNextLevel();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
