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
        public QizeLvScene[] qizeLvScenes;
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
            chageChoice = true;
            QnA.RemoveAt(currentQuestion);
            StartCoroutine(GenerateQuestion());
        }

        public void Wrong()
        {
            chageChoice = true;
            StartCoroutine(GenerateQuestion());

        }
        void SetAnswer()
        {
            #region CheckQuizMap

            if (Singleton.Instance.mapNumSelect == 1)
            {
                QnA = new List<QnA>(qizeLvScenes[0].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 2)
            {
                QnA = new List<QnA>(qizeLvScenes[1].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 3)
            {
                QnA = new List<QnA>(qizeLvScenes[2].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 4)
            {
                QnA = new List<QnA>(qizeLvScenes[3].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 5)
            {
                QnA = new List<QnA>(qizeLvScenes[4].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 6)
            {
                QnA = new List<QnA>(qizeLvScenes[5].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 7)
            {
                QnA = new List<QnA>(qizeLvScenes[6].QnA);
            }

            if (Singleton.Instance.mapNumSelect == 8)
            {
                QnA = new List<QnA>(qizeLvScenes[7].QnA);
            }

            #endregion

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
                RandomPosChoice.RandomChoice();
                currentQuestion = Random.Range(0, QnA.Count);
                yield return new WaitForSeconds(3f);
                chageChoice = false;

                questionTxt.text = QnA[currentQuestion].question;
                SetAnswer();
            }
            else
            {
                Singleton.Instance.gameOver = true;
                Debug.Log("Out of Q&A");
            }
        }
    }
}
