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
            currentTime = startingtime;
        }

        // Update is called once per frame
        void Update()
        {
            curTime = currentTime;

            if (CountDownTimer.fade2)
            {
                currentTime -= 1 * Time.deltaTime;
                timeTxt.text = currentTime.ToString("0");

                if (curTime <= 0)
                {
                    currentTime = 0;
                    GameManager.scoreNum -= 1;
                    QizeManager.isTimeUp = true;

                    if (QizeManager.isTimeUp)
                        StartCoroutine(Cd());

                }
            }
            
        }
        IEnumerator Cd()
        {
            yield return new WaitForSeconds(0.5f);
            currentTime = 10;
        }

    }
}
