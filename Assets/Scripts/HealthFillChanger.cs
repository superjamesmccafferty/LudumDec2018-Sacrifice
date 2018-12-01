using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sacrifice
{
    public class HealthFillChanger : MonoBehaviour
    {

        [SerializeField]
        GameObject _image_object;

        Image _image;

        void Start()
        {
            _image = _image_object.GetComponent<Image>();
        }


        public void ChangeImageFill(SHealthState health)
        {
            _image.fillAmount = health.PercentageHealth;
        }




    }
}