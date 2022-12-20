using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    public class ItemUptime : MonoBehaviour
    {
        public GameObject timer;
        public GameObject gainTimer;
        bool isUse;
        // Start is called before the first frame update
        void Start()
        {          
            timer.GetComponent<CountDownTimer>();
        }
        private void Update()
        {
           
          
        }
        public void UseItem(float upTimer)
        {           
            //if (!isUse)
         //   {
                if (timer == null)
                {
                    timer = GameObject.Find("Ui Player/Time/TextNum");
                }

                isUse = true;

                upTimer = 15;
                gainTimer.SetActive(true);
                timer.GetComponent<CountDownTimer>().Useitem(upTimer);
                
          //  }
        } 
    } 
}
