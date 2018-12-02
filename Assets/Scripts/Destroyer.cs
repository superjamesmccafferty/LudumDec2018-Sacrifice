using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public class Destroyer : MonoBehaviour
    {

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}