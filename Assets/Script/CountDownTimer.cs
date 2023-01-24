using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CountDownTimer : MonoBehaviour
    {
        public float timeValue;
        public TMPro.TextMeshProUGUI timeTxt;

        public static bool fade1;
        public static bool fade2;
       
        // Start is called before the first frame update
        void Start()
        {
            timeValue = 2;
            fade1 = true;
            fade2 = false;
            Debug.Log(fade1);
            Debug.Log(fade2);
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
            if (timeToDisplay < 0)
            {
                if (fade1)
                {
                    fade2 = true;
                    fade1 = false;
                    timeToDisplay = 0;

                }
                if (fade2)
                    timeValue = 300;
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
