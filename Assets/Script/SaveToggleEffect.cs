using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class SaveToggleEffect : MonoBehaviour
    {
        [SerializeField]
        Toggle togglesOn, togglesOff;
        // Start is called before the first frame update
        private void Awake()
        {
            if (Singleton.Instance.isOnEffect)
            {
                togglesOn.isOn = true;
                togglesOff.isOn = false;
                
            }
            if (!Singleton.Instance.isOnEffect)
            {
                togglesOn.isOn = false;
                togglesOff.isOn = true;
               
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ToggleOnSelected()
        {

            Singleton.Instance.isOnEffect = true;
        }
        public void ToggleOffSelected()
        {

            Singleton.Instance.isOnEffect = false;
        }
    }
}
