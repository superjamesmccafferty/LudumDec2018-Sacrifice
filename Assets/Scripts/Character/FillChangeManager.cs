using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sacrifice
{
    public class FillChangeManager : MonoBehaviour
    {

        [SerializeField]
        GameObject _image_object;

        Image _image;

        void Start()
        {
            _image = _image_object.GetComponent<Image>();
        }


        public void FillWithCurrentHealth(SHealthState health)
        {
            _image.fillAmount = (health.CurrentHealth / health.TotalHealth) * (health.NonSacrifisedPercentage);
        }


        public void FillWithCurrentHealth(float fill)
        {
            _image.fillAmount = fill;
        }


        public void FillWithSacrificedHealth(SHealthState health)
        {
            _image.fillAmount = 1 - health.NonSacrifisedPercentage;
        }




    }
}