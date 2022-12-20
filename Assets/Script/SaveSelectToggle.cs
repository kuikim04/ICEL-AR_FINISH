using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Script
{
    public class SaveSelectToggle : MonoBehaviour
    {
        [SerializeField] Toggle togglesR1, togglesR2, togglesR3;
        public ToggleGroup allowSwith;

        private void Awake()
        {
            if (Singleton.Instance.numQulity == 1)
            {
                togglesR1.isOn = true;
                togglesR2.isOn = false;
                togglesR3.isOn = false;
            }
            if (Singleton.Instance.numQulity == 2)
            {
                togglesR1.isOn = false;
                togglesR2.isOn = true;
                togglesR3.isOn = false;
            }
            if (Singleton.Instance.numQulity == 3)
            {
                togglesR1.isOn = false;
                togglesR2.isOn = false;
                togglesR3.isOn = true;
            }

        }
        private void Update()
        {
            
        }
        private void Start()
        {
            allowSwith = GetComponent<ToggleGroup>();

        }
        // Update is called once per frame


        public void Submit()
        {

        }
        public void ToggleResolution1Selected()
        {
            
            Singleton.Instance.numQulity = 1;
        }
        public void ToggleResolution2Selected()
        {
            
            Singleton.Instance.numQulity = 2;
        }
        public void ToggleResolution3Selected()
        {
            
            Singleton.Instance.numQulity = 3;

        }

        public void SetLowQuality (bool quality)
        {
            if (quality)
                QualitySettings.SetQualityLevel(1);
        }
        public void SetMediumQuality(bool quality)
        {
            if (quality)
                QualitySettings.SetQualityLevel(2);
        }
        public void SetHighQuality(bool quality)
        {
            if(quality)
                QualitySettings.SetQualityLevel(3);
        }
    }
}