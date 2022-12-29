using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Script {
    public class AnswerScripts : MonoBehaviour
    {
        public bool isCorrect = false;
        public QizeManager qize;
        public void Answer()
        {
            if (isCorrect)
            {
                Debug.Log("isCorrect");
                qize.Corect();
                GameManager.scoreNum += 1;
            }
            else
            {
                Debug.Log("isNotCorrect");
                qize.Corect();
                GameManager.scoreNum -= 1;
            }
        }
    }
}