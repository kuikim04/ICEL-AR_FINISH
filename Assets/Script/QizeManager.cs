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
        public GameObject choicePanel;
        public TextMeshProUGUI questionTxt;
        public static bool isTimeUp;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GenerateQuestion());
        }
        private void Update()
        {
            if (CountDownTimer.fade2)
            {
                if (CountDownChoice.curTime <= 0f)
                {
                    GameManager.scoreNum -= 1;
                    if (isTimeUp)
                    {
                        StartCoroutine(GenerateQuestion());
                        isTimeUp = false;
                    }
                    Debug.Log("TimeUp");
                }
            }
        }
        public void Corect()
        {
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
            currentQuestion = Random.Range(0, QnA.Count);
            yield return new WaitForSeconds(0.5f);
            questionTxt.text = QnA[currentQuestion].question;
           // yield return new WaitForSeconds(0.5f);
            SetAnswer();
        }

        IEnumerator WaitChange()
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(GenerateQuestion());
        }
    }
}
