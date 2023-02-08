using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    public class CountDownChoice : MonoBehaviour
    {
        public static float curTime;
        float currentTime = 0f;
        public float startingtime = 10;
        public TMPro.TextMeshProUGUI timeTxt;
        public GameManager choice;
        // Start is called before the first frame update
        void Start()
        {
            currentTime = startingtime;
            choice.GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            curTime = currentTime;


            if (CountDownTimer.fade2)
            {                    
                if (!QizeManager.isTimeUp)
                {
                    currentTime -= 1 * Time.deltaTime;
                    timeTxt.text = currentTime.ToString("0");
                    
                    if (curTime <= 0)
                    {
                        currentTime = 0;
                        //GameManager.scoreNum -= 1;
                        QizeManager.isTimeUp = true;
                        
                    }
                }
                
                if (QizeManager.isTimeUp)
                {
                    ResetTimeChoice();
                }
            
            }


        }

        private void OnEnable()
        {
            ResetTimeChoice();
        }

        public void ResetTimeChoice()
        {
            currentTime = startingtime;

        }
    }
}
