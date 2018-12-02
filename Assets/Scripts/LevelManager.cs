using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sacrifice 
{
    public class LevelManager : MonoBehaviour
    {
        private List<string> _levels;
        private List<string> _availableLevels;

        public LevelManager()
        {
            _levels = new List<string>();
            _levels.Add("level_01");

            ResetAvailableLevels();
        }

        /// <summary>
        /// Load a level randomly from the available 
        /// level and then substract it from the list
        /// </summary>
        public void GoToNextLevel(int nbPlayer)
        {
            if (_availableLevels.Count == 0)
            {
                ResetAvailableLevels();
            }

            int levelIndex = Random.Range(0, _availableLevels.Count);
            SceneManager.LoadScene(_availableLevels[levelIndex]);
            _availableLevels.RemoveAt(levelIndex);
        }

        /// <summary>
        /// Reset the list of available level
        /// </summary>
        void ResetAvailableLevels()
        {
            _availableLevels = new List<string>(_levels);
        }
    }
}
