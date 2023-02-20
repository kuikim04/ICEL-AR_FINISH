using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class GameOverPanel : MonoBehaviour
    {
        public TextMeshProUGUI timeToUse;
        public TextMeshProUGUI numPoint;
        public TextMeshProUGUI numEx;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GameOver();
        }
        void GameOver()
        {
            float minutes = Mathf.FloorToInt(CountDownTimer.curtime / 60);
            float second = Mathf.FloorToInt(CountDownTimer.curtime % 60);

            timeToUse.text = string.Format("{0:00} : {1:00}", minutes, second);
            numEx.text = GameManager.totalExerciseCount.ToString();
            numPoint.text = GameManager.scoreNum.ToString();
            //numEx.text =

        }
    }
}
