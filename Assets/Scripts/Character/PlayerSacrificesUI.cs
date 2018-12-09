using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Sacrifice
{
    public class PlayerSacrificesUI : MonoBehaviour
    {

        [SerializeField]
        TextMeshProUGUI _text_mesh;

        public void SetCharacterName(string name)
        {
            _text_mesh.text = name;
        }
    }
}