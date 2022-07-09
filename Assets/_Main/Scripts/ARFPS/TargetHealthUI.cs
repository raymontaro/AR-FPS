using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI;

namespace ARFPS
{
    public class TargetHealthUI : MonoBehaviour
    {
        private Slider slider;
        private Target target;
        private Follow3dObject follow3DObject;

        
        void Awake()
        {
            slider = GetComponent<Slider>();
            follow3DObject = GetComponent<Follow3dObject>();
        }

        /// <summary>
        /// Initialized slider value.
        /// </summary>               
        public void Init(Target newTarget)
        {
            target = newTarget;
            follow3DObject.target = newTarget.transform;
            slider.maxValue = target.GetMaxHealthValue();
            slider.value = target.GetCurrentHealthValue();
        }

        /// <summary>
        /// Update Slider value.
        /// </summary>               
        public void UpdateUI()
        {
            if (target != null)
            {
                slider.value = target.GetCurrentHealthValue();
            }
        }
    }
}