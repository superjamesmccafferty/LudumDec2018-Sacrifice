using System.Collections;
using System.Collections.Generic;
using TobiasUN.Core.Events;
using TobiasUN.Core.Utilities.Code.Collections;
using UnityEngine;

namespace Sacrifice
{
    public class LevelInitializer : MonoBehaviour
    {

        [SerializeField]
        SOGameSettings _game_settings;


        // We pass use the enum array to represent the index of each player color by default so we can set up each player.

        [SerializeField]
        EPlayerColor[] _player_colors = { EPlayerColor.RED, EPlayerColor.BLUE, EPlayerColor.GREEN, EPlayerColor.WHITE };

        [SerializeField]
        GameObject[] _player_prefabs = new GameObject[4];

        [SerializeField]
        PlayerSacrificesUI[] _player_uis = new PlayerSacrificesUI[4];

        [SerializeField]
        GameObject[] _player_spawns = new GameObject[4];


        [SerializeField]
        ReliableEvent_SPlayerWin _player_win_event;



        void Start()
        {

            foreach (EPlayerColor col in _game_settings.Players.Keys)
            {

                int index = UTArray.GetFirstIndex<EPlayerColor>(col, _player_colors);

                if (index == -1)
                {
                    Debug.LogError("Player type index was not found. Is the player missing from the array?");
                }

                GameObject player = Instantiate(
                      _player_prefabs[index],
                      _player_spawns[index].transform.position,
                      new Quaternion());

                _player_uis[index].gameObject.SetActive(true);
                _player_uis[index].SetCharacterName(_game_settings.Players[col]);

            }
        }





    }
}
