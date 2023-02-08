using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Script {
    public class CooldownWarmupFate : MonoBehaviour
    {
        public static CooldownWarmupFate Instance;


        public TextMeshProUGUI Time;
        public float msToWait;
        private ulong lastTimeClicked;
        public Button ClickButton;


        float dayHourTime = 86700000.0f;

     
        private void Start()
        { 

            if (PlayerPrefs.HasKey("LastTimeClicked"))
            {
                lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
            }
            else
            {
                lastTimeClicked = (ulong)DateTime.Now.Ticks;
                PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
            }

            if (!Ready())
            {
                ClickButton.interactable = false;
                Singleton.Instance.isWarmUp = false;
                ClickButton.gameObject.SetActive(false);
               // Singleton.Instance.finishWarmup = true;

            }

            if (Singleton.Instance.hasPlayed && !Singleton.Instance.isCoolDown)
            {
                SceneManager.LoadScene("CoolDown");
            }
        }

        private void Update()
        {
            if (!ClickButton.IsInteractable())
            {
                if (Ready())
                {
                    Time.text = "Ready!";
                    return;
                }

                ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
                ulong m = diff / TimeSpan.TicksPerMillisecond;
                float secondsLeft = (float)(msToWait - m) / 1000.0f;

                string r = "";
                //HOURS
                r += ((int)secondsLeft / 3600).ToString() + "h";
                secondsLeft -= ((int)secondsLeft / 3600) * 3600;
                //MINUTES
                r += ((int)secondsLeft / 60).ToString("00") + "m ";
                //SECONDS
                r += (secondsLeft % 60).ToString("00") + "s";
                Time.text = r;

            }

        }


        public void Click()
        {
            lastTimeClicked = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
            ClickButton.interactable = false;

            Singleton.Instance.isWarmUp = false;
            SceneManager.LoadScene("WarmUp");
        }

        private bool Ready()
        {
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;

            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            if (secondsLeft <= 0)
            {
                ClickButton.interactable = true;
                Singleton.Instance.isWarmUp = true;
                ClickButton.gameObject.SetActive(true);

                return true;
            }


            return false;
        }

       
    }
}