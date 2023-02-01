using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CountDownTimer : MonoBehaviour
    {
        public float timeValue;
        public TMPro.TextMeshProUGUI timeTxt;
        public static float curtime;

        public float timeFade1;
        public float timeFade2;

        public static bool fade1;
        public static bool fade2;
       
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(fade2);

            if(Singleton.Instance.isWarmUp)
                timeValue = timeFade1;

            if (!Singleton.Instance.isWarmUp)
                timeValue = timeFade2;

            if (Singleton.Instance.isWarmUp)
            {
                fade1 = true;
                fade2 = false;
            }
            if (!Singleton.Instance.isWarmUp)
            {
                fade1 = false;
                fade2 = true;
            }

        }

        // Update is called once per frame
        void Update()
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

            if (Singleton.Instance.readyPlay)
               DisplayTime(timeValue);
        }

        void DisplayTime(float timeToDisplay)
        {
           
               if (timeToDisplay < 0 && !Singleton.Instance.gameOver)
                {
                    if (fade1)
                    {
                        fade1 = false;
                        fade2 = true;
                        timeToDisplay = 0;
                        Singleton.Instance.finishWarmup = true;
                    }
                    if (fade2)
                        timeValue = timeFade2;
                }

                if (fade2 && timeToDisplay < 0)
                {
                    timeToDisplay = 0;
                    Singleton.Instance.gameOver = true;
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
