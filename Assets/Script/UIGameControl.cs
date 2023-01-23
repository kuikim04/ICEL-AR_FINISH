using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class UIGameControl : MonoBehaviour
    {
        public GameObject pausePanel;

        public void Pause()
        {
            pausePanel.SetActive(true);
            GameManager.isPause = true;

        }

        public void Continue()
        {
            pausePanel.SetActive(false);
            GameManager.isPause = false;
        }
        public void ExitGamePlay()
        {
            SceneManager.LoadScene(0);
            GameManager.isPause = false;
            Singleton.Instance.levelNumSelect = 0;
            Singleton.Instance.readyPlay = false;
            CountDownTimer.fade1 = true;
            CountDownTimer.fade2 = false;
        }
    }
}