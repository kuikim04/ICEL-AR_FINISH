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
        // Start is called before the first frame update
        void Start()
        {
            curTime = startingtime;
        }

        // Update is called once per frame
        void Update()
        {
            curTime = currentTime;

            currentTime -= 1 * Time.deltaTime;
            timeTxt.text = currentTime.ToString("0");

            if (currentTime <= 0)
                currentTime = 10;     

        }


    }
}
