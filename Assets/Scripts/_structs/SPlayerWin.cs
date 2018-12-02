using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public struct SPlayerWin
    {

        public string PlayerName { get; private set; }

        public Color PlayerColor { get; private set; }


        public SPlayerWin(string player_name, Color player_color) 
        {
            PlayerName = player_name;
            PlayerColor = player_color;
        }
    }
}

