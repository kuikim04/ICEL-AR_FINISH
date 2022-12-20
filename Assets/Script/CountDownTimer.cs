using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CountDownTimer : MonoBehaviour
    {
        public float timeValue = 300;
        public TMPro.TextMeshProUGUI timeTxt;
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                Singleton.Instance.gameOver = true;
                timeValue = 0;
            }


            if(Singleton.Instance.readyPlay)
               DisplayTime(timeValue);
        }

        void DisplayTime(float timeToDisplay)
        {
            if(timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float second = Mathf.FloorToInt(timeToDisplay % 60);
            timeTxt.text = string.Format("{0:00} : {1:00}" , minutes, second);
        }

        public void Useitem(float upTime)
        {
            timeValue += upTime;
        }
    }
   
}
