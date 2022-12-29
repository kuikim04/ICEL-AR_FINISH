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
        // Start is called before the first frame update
        void Start()
        {
            GenerateQuestion();
        }
        private void Update()
        {
            if (choicePanel.activeSelf)
            {
                if (CountDownChoice.curTime <= 0.1f)
                {
                    GameManager.scoreNum -= 1;
                    StartCoroutine(WaitChange());
                    Debug.Log("TimeUp");
                }
            }
        }
        public void Corect()
        {
            QnA.RemoveAt(currentQuestion);
            GenerateQuestion();
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
        public void GenerateQuestion()
        {
            currentQuestion = Random.Range(0, QnA.Count);
            questionTxt.text = QnA[currentQuestion].question;
            SetAnswer();
        }

        IEnumerator WaitChange()
        {
            yield return new WaitForSeconds(0.1f);
            GenerateQuestion();
        }
    }
}
