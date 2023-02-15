using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Script {
    public class GameManagerWarmUp : MonoBehaviour
    {
        [Header("Start Game Interaction")]
        public GameObject uiPlayer;
        public GameObject startPanel;
        public TextMeshProUGUI textNameMap;

        public GameObject gameOverPanel;
        public static bool isPause;

        // Start is called before the first frame update
        void Start()
        {
            startPanel.SetActive(true);

        }

        // Update is called once per frame
        void Update()
        {
            if (!startPanel.activeSelf)
                uiPlayer.SetActive(true);


            if (isPause)
            {
                Time.timeScale = 0;
            }
            else if (!isPause)
            {
                Time.timeScale = 1;
            }


            if (Singleton.Instance.gameOver)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
        }
    }
}
