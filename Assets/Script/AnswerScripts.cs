using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Script {
    public class AnswerScripts : MonoBehaviour
    {
        public bool isCorrect = false;
        public QizeManager qize;

        [Header("Effects")]
        public GameObject correctFx;
        public GameObject wrongFx;

        public void Answer()
        {
            if (isCorrect)
            {
                Debug.Log("isCorrect");
                qize.Corect();
                GameManager.scoreNum += 1;

                correctFx.SetActive(true);
            }
            else
            {
                Debug.Log("isNotCorrect");
                qize.Wrong();
                GameManager.scoreNum -= 1;

                wrongFx.SetActive(true);
            }
        }

        private void OnDisable()
        {
            correctFx.SetActive(false);
            wrongFx.SetActive(false);
        }
    }
}