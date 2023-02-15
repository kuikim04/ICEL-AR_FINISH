using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    public class CountDownWarmUp : MonoBehaviour
    {
        public float timeValue;
        public TMPro.TextMeshProUGUI timeTxt;
        public static float curtime;


        private void Update()
        {
            curtime = timeValue;

            if (Singleton.Instance.gameOver)
                return;

            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                
            }


            if (Singleton.Instance.gameOver)
                return;

            DisplayTime(timeValue);
        }
       

        void DisplayTime(float timeToDisplay)
        {

            if (timeToDisplay < 0 && !Singleton.Instance.gameOver)
            {
                timeToDisplay = 0;
                Singleton.Instance.gameOver = true;
            }


            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float second = Mathf.FloorToInt(timeToDisplay % 60);
            timeTxt.text = string.Format("{0:00} : {1:00}", minutes, second);
        }
    }
}
