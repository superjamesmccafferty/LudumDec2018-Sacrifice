using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{

    public class ScaleForcer : MonoBehaviour
    {

        [SerializeField]
        GameObject ScaleCheck;

        float _changeScale;

        private void Start()
        {
            _changeScale = gameObject.transform.localScale.x;
        }

        void Update()
        {
            Vector3 semi_complete = new Vector3(
                0,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z
            );

            float flip = 1;

            if (ScaleCheck.transform.localScale.x < 1) flip = -1;

            gameObject.transform.localScale = semi_complete + new Vector3(_changeScale * flip, 0, 0);

        }
    }
}