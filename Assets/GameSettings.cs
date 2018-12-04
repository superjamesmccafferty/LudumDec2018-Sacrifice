using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Sacrifice
{

    public class GameSettings : MonoBehaviour
    {

        public TextMeshProUGUI[] names_texts;

        public string[] names = { "Bob", "Caron", "Jacob", "Toby" };



        public void Start()
        {
            names = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>().pnames;
        }


        public void setName(string name, int id)
        {
            names[id] = name;
        }

        public string getName(int id)
        {
            return names[id];
        }

        public TextMeshProUGUI getNameText(int id)
        {
            return names_texts[id];
        }







    }
}