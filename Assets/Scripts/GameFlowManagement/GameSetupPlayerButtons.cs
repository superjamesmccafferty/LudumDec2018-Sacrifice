using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public class GameSetupPlayerButtons : MonoBehaviour
    {

        [SerializeField]
        GameSetupManager _manager;

        [SerializeField]
        EPlayerColor _player;

        [SerializeField]
        string _default_name;

        public void AddPlayer()
        {
            _manager.AddPlayer(_player);
            UpdatePlayerName(_default_name);
        }

        public void RemovePlayer()
        {
            _manager.RemovePlayer(_player);
        }

        public void UpdatePlayerName(string name)
        {
            _manager.UpdateName(_player, name);
        }

    }
}