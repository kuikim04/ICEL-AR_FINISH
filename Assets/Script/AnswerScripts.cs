using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Script {
    public class AnswerScripts : MonoBehaviour
    {
        public bool isCorrect = false;
        public QizeManager qize;

        [Header("Effect")]
        public GameObject correctFx;
        public GameObject wrongFx;
        public void Answer()
        {
            if (isCorrect)
            {
                Debug.Log("isCorrect");
                qize.Corect();
                correctFx.SetActive(true);
                GameManager.scoreNum += 1;
            }
            else
            {
                Debug.Log("isNotCorrect");
                wrongFx.SetActive(true);
                qize.Corect();
                //GameManager.scoreNum -= 1;
            }
        }
    }
}