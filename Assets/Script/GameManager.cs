using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        [Header("Start Game Interaction")]
        public GameObject[] Map;
        public TextMeshProUGUI textNameMap;
        public GameObject uiPlayer;
        public GameObject startPanel;
        public GameObject uiTrigger;

        [Header("Game Play")]
        public GameObject dialogue;
        public static bool isPause;
        public TextMeshProUGUI scoreText;
        private int score;
        [HideInInspector] public static int scoreNum;
        // Start is called before the first frame update
        void Start()
        {
            scoreNum = 0;
        
            startPanel.SetActive(true);
           // Map[Map.Length].SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            score = scoreNum;
            scoreText.text = score.ToString();
            if (scoreNum <= 0)
                scoreNum = 0;

            #region check map
            if (Singleton.Instance.mapNumSelect == 1)
            {
                textNameMap.text = "Airport";
                Map[0].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 2)
            {
                textNameMap.text = "Technology";
                Map[1].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 3)
            {
                textNameMap.text = "Shopping";
                Map[2].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 4)
            {
                textNameMap.text = "Holiday";
                Map[3].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 5)
            {
                textNameMap.text = "Dining";
                Map[4].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 6)
            {
                textNameMap.text = "Forest";
                Map[5].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 7)
            {
                textNameMap.text = "Health";
                Map[6].SetActive(true);
            }
            else if (Singleton.Instance.mapNumSelect == 8)
            {
                textNameMap.text = "Work";
                Map[7].SetActive(true);
            }
            #endregion

            if (!startPanel.activeSelf)
            {
                uiPlayer.SetActive(true);           
            }

            if (CountDownTimer.fade2)
            {
                uiTrigger.SetActive(true);
            }

            if(isPause)
            {
                Time.timeScale = 0;
            }else if (!isPause)
            {
                Time.timeScale = 1;
            }
        }
    }


}