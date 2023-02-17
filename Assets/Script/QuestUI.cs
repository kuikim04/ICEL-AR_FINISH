using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class QuestUI : MonoBehaviour
    {
        public static QuestUI uiManager;

        public bool questAvailable = false;
        public bool questRunning = false;



        [Header("UI INTERFACE")]
        public TextMeshProUGUI txtDesCrips;

        private QuestObject currentQuestObject;

        public List<Quest> availableQuests = new List<Quest>();
        public List<Quest> activeQuests = new List<Quest>();

        public GameObject qButton;
        private List<GameObject> qButtons = new List<GameObject>();

        public GameObject acceptBtn;
        public GameObject completeBtn;

        public Transform qBuntonSpacer1, qBuntonSpacer2;

        public QButtonUI acceptBtnScript;
        public QButtonUI completeBtnScript;

        public bool isCompleteAllQuest;
 

        private void Awake()
        {
            if(uiManager == null)
            {
                uiManager = this;
            }
            else if (uiManager != this)
            {
                Destroy(gameObject);
            }
           // DontDestroyOnLoad(gameObject);

            HideQuest();
        }
        // Start is called before the first frame update
        void Start()
        {
            acceptBtn = GameObject.Find("UIManger").
                transform.Find("QuestPanel").
                transform.Find("AcceptBtn").gameObject;

            acceptBtnScript = acceptBtn.GetComponent<QButtonUI>();

            completeBtn = GameObject.Find("UIManger").
                transform.Find("QuestPanel").
                transform.Find("CompleteBtn").gameObject;

            completeBtnScript = completeBtn.GetComponent<QButtonUI>();


            txtDesCrips.text = "CLICK FOR ACCEPT QUEST";
            acceptBtn.SetActive(false);
            completeBtn.SetActive(false);
        }

        private void Update()
        {
            if (Singleton.Instance.numQuest >= 5)
            {
                CompleteAllQuest();
            }                                             
        }

        void CompleteAllQuest()
        {
            isCompleteAllQuest = true;
            txtDesCrips.text = "COMPELTE ALL DIALY QUEST";

        }

        public void CheckQuests(QuestObject questObject)
        {
           
            currentQuestObject = questObject;
            QuestManager.questManager.QuestRequest(questObject);

            if(questRunning || questAvailable && !isCompleteAllQuest)
            {               
                ShowQuestPanel();
            } 
        }

        public void ShowQuestPanel()
        {           
            FillQuestBtn();
        }

        public void ShowQuestLogPanel(Quest activeQuest)
        {
            if (activeQuest.progess == Quest.QuestProgess.ACCEPTED)
            {
                txtDesCrips.text = activeQuest.hint;
            }
            if (activeQuest.progess == Quest.QuestProgess.COMPLETE)
            {
                txtDesCrips.text = activeQuest.congratuation;
            }
        }

        public void HideQuest()
        {

            //txtDesCrips.text = "CLICK FOR CHECK QUEST";

            questAvailable = false;
            questRunning = false;

            availableQuests.Clear();
            activeQuests.Clear();

            for(int i = 0; i < qButtons.Count; i++)
            {
                Destroy(qButtons[i]);
            }

            qButtons.Clear();
        }

        void FillQuestBtn()
        { 
            foreach (Quest availableQuest in availableQuests)
            {
                GameObject questBtn = Instantiate(qButton);
                QButtonUI qBtnScript = questBtn.GetComponent<QButtonUI>();

                qBtnScript.questID = availableQuest.id;
             
                //  qBtn.questTitle.text = availableQuest.title;

                questBtn.transform.SetParent(qBuntonSpacer1, false);
                qButtons.Add(questBtn);
            }

            foreach (Quest activeQuest in activeQuests)
            {
                GameObject questBtn = Instantiate(qButton);
                QButtonUI qBtnScript = questBtn.GetComponent<QButtonUI>();

                qBtnScript.questID = activeQuest.id;
              
                // qBtn.questTitle.text = activeQuest.title;

                questBtn.transform.SetParent(qBuntonSpacer2, false);
                qButtons.Add(questBtn);

            }
        }

        public void ShowSelectQuest(int questID)
        {
            //if (isCompleteAllQuest)         
            //    return;
            

            for (int i = 0; i < availableQuests.Count; i++)
            {
                if (availableQuests[i].id == questID)
                {
                    txtDesCrips.text = availableQuests[i].title;

                    if (availableQuests[i].progess == Quest.QuestProgess.AVAILABLE)
                    {
                        txtDesCrips.text = availableQuests[i].title + " " +
                            availableQuests[i].questObjectiveCount + "/" +
                            availableQuests[i].questObjectiveRequirment; ;
                    }
                }

            }
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].id == questID)
                {
                    txtDesCrips.text = activeQuests[i].title;

                    if (activeQuests[i].progess == Quest.QuestProgess.ACCEPTED)
                    {
                        txtDesCrips.text = activeQuests[i].title + " " +
                            activeQuests[i].questObjectiveCount + "/" +
                            activeQuests[i].questObjectiveRequirment;

                    }
                    else if(activeQuests[i].progess == Quest.QuestProgess.COMPLETE)
                    {
                        txtDesCrips.text = activeQuests[i].title + " " +
                           activeQuests[i].questObjectiveCount + " " + "/" +
                           activeQuests[i].questObjectiveRequirment;

                    }
                }
            }
        }
    }
 }

