using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{

    public class GameSettings : MonoBehaviour
    {

        public string[] names = { "Bob", "Caron", "Jacob", "Toby" };

        public void setName(string name, int id)
        {
            names[id] = name;
        }

        public string getName(int id)
        {
            return names[id];
        }







    }
}