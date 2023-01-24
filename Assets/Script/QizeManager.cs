using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class QizeManager : MonoBehaviour
    {
        public List<QnA> QnA;
        public GameObject[] option;
        public int currentQuestion;
        public TextMeshProUGUI questionTxt;
        public static bool isTimeUp;

        public static bool chageChoice;
        public GameManager RandomPosChoice;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GenerateQuestion());
            isTimeUp = false;
            chageChoice = false;
        }
        private void Update()
        {
            if (CountDownTimer.fade2)
            {
                if (CountDownChoice.curTime <= 0f)
                {                    
                    //GameManager.scoreNum -= 1;
                    if (isTimeUp)
                    {
                        isTimeUp = false;
                        chageChoice = true;
                        StartCoroutine(GenerateQuestion());      
                        
                    }
                }              
            }
        }
        public void Corect()
        {
            chageChoice = true;
            QnA.RemoveAt(currentQuestion);
            StartCoroutine(GenerateQuestion());
        }
        void SetAnswer()
        {         

            for (int i = 0; i < option.Length; i++)
            {
                option[i].GetComponent<AnswerScripts>().isCorrect = false;
                option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = QnA[currentQuestion].answers[i];

                if (QnA[currentQuestion].correctAsnwer == i + 1)
                {
                    option[i].GetComponent<AnswerScripts>().isCorrect = true;
                }
            }         
        }
        public IEnumerator GenerateQuestion()
        {
            RandomPosChoice.RandomChoice();
            currentQuestion = Random.Range(0, QnA.Count);
            yield return new WaitForSeconds(3f);
            chageChoice = false;
            questionTxt.text = QnA[currentQuestion].question;         
            SetAnswer();
        }
    }
}
