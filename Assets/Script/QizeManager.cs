using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script
{
    public class QizeManager : MonoBehaviour
    {
        public List<QnA> QnA;
        public GameObject[] option;
        List<Button> optionBtn;
        public int currentQuestion;
        public TextMeshProUGUI questionTxt;
        public static bool isTimeUp;
        
        public int answersBeforeChangeToExercise;
        int answeredCount;

        public UnityEvent OnQuizBreak;

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
            if (Singleton.Instance.gameOver)
                return;        
                      

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
            QnA.RemoveAt(currentQuestion);

            answeredCount++;

            StartCoroutine(GenerateQuestion());
        }

        public void Wrong()
        {

            answeredCount++;

            StartCoroutine(GenerateQuestion());

        }
        
        void SetAnswer()
        {
            for (int i = 0; i < option.Length; i++)
            {
                option[i].GetComponent<AnswerScripts>().isCorrect = false;
                option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = QnA[currentQuestion].answers[i];

                if (QnA[currentQuestion].correctAsnwer1 == i + 1)
                {
                    option[i].GetComponent<AnswerScripts>().isCorrect = true;
                }

                if (QnA[currentQuestion].correctAsnwer2 == i + 1)
                {
                    option[i].GetComponent<AnswerScripts>().isCorrect = true;
                }
            }
        }

        public IEnumerator GenerateQuestion()
        {
            if (QnA.Count > 0)
            {
				yield return new WaitForSeconds(.5f);

                chageChoice = true;

                RandomPosChoice.RandomChoice();
                currentQuestion = Random.Range(0, QnA.Count);

                yield return new WaitForSeconds(.5f);

                chageChoice = false;

                questionTxt.text = QnA[currentQuestion].question;
                SetAnswer();

                if (answeredCount >= answersBeforeChangeToExercise)
            	{
                	OnQuizBreak.Invoke();
                	answeredCount = 0;
            	}
            
            }
            else
            {
                Singleton.Instance.gameOver = true;
                Debug.Log("Out of Q&A");
            }
        }
    }
}
