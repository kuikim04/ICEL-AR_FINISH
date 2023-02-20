using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script {
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager questManager;

        public List<Quest> questsList = new List<Quest>();
        public List<Quest> currentQuestsList = new List<Quest>();

        public List<int> doneQuestlist = new List<int>();
        private int SaveListCount;
        public QuestDatScriptable[] questDaylist;

        private void Awake()
        {
            if (questManager == null)
            {
                questManager = this;
            }
            else if (questManager != null)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {

            #region CheckQuestDay

            //day 1
            if (Singleton.Instance.numQuest == 1)
            {
                questsList = new List<Quest>(questDaylist[0].quests);
            }
            //day 2
            else if(Singleton.Instance.numQuest == 2)
            {
                questsList = new List<Quest>(questDaylist[1].quests);
            }
            //day 3
            else if (Singleton.Instance.numQuest == 3)
            {
                questsList = new List<Quest>(questDaylist[2].quests);
            }
            //day 4
            else if (Singleton.Instance.numQuest == 4)
            {
                questsList = new List<Quest>(questDaylist[3].quests);
            }
            //day 5
            else if (Singleton.Instance.numQuest == 5)
            {
                questsList = new List<Quest>(questDaylist[4].quests);
            }
            //day 6
            else if (Singleton.Instance.numQuest == 6)
            {
                questsList = new List<Quest>(questDaylist[5].quests);
            }
            //day 7
            else if (Singleton.Instance.numQuest == 7)
            {
                questsList = new List<Quest>(questDaylist[6].quests);
            }
            //day 8
            else if (Singleton.Instance.numQuest == 8)
            {
                questsList = new List<Quest>(questDaylist[7].quests);
            }
            //day 9
            else if (Singleton.Instance.numQuest == 9)
            {
                questsList = new List<Quest>(questDaylist[8].quests);
            }
            //day 10
            else if (Singleton.Instance.numQuest == 10)
            {
                questsList = new List<Quest>(questDaylist[9].quests);
            }
            //day 11
            else if (Singleton.Instance.numQuest == 11)
            {
                questsList = new List<Quest>(questDaylist[10].quests);
            }
            //day 12
            else if (Singleton.Instance.numQuest == 12)
            {
                questsList = new List<Quest>(questDaylist[11].quests);
            }


            #endregion
        }


        public void QuestRequest(QuestObject questObject)
        {
            if(questObject.availableQuestIDs.Count > 0)
            {
                for(int i = 0; i < questsList.Count; i++)
                {
                    
                    for(int j = 0; j < questObject.availableQuestIDs.Count; j++)
                    {
                        if(questsList[i].id == questObject.availableQuestIDs[j]
                            && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                        {
                           
                            //AcceptQuest(questObject.availableQuestIDs[j]);
                            QuestUI.uiManager.questAvailable = true;
                            QuestUI.uiManager.availableQuests.Add(questsList[i]);
                        }
                    }
                }
            }

            for(int i = 0; i < currentQuestsList.Count; i++)
            {
                for(int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if(currentQuestsList[i].id == questObject.recivableQuestIDs[j]
                        && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED
                        || currentQuestsList[i].progess == Quest.QuestProgess.COMPLETE)
                    {
                        //CompleteQuest(questObject.recivableQuestIDs[j]);
                        QuestUI.uiManager.questRunning = true;
                        QuestUI.uiManager.activeQuests.Add(currentQuestsList[i]);
                        Debug.Log(currentQuestsList[i].id);
                    }
                }
            }
        }

        public void AddQuestItem(string questObjective, int itemAmount)
        {
            for(int i = 0; i < currentQuestsList.Count; i++)
            {
                if(currentQuestsList[i].questObjective == questObjective
                    && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
                {
                    currentQuestsList[i].questObjectiveCount += itemAmount;
                }
                if (currentQuestsList[i].questObjectiveCount >= currentQuestsList[i].questObjectiveRequirment
                    && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
                {
                    currentQuestsList[i].progess = Quest.QuestProgess.COMPLETE;
                }
            }
        }

        public void AcceptQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID &&
                    questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                {
                    currentQuestsList.Add(questsList[i]);
                    questsList[i].progess = Quest.QuestProgess.ACCEPTED;               

                }
            }
        }

        public void CompleteQuest(int questID)
        {
            for (int i = 0; i < currentQuestsList.Count; i++)
            {
                if (currentQuestsList[i].id == questID &&
                    currentQuestsList[i].progess == Quest.QuestProgess.COMPLETE)
                {
                    currentQuestsList[i].progess = Quest.QuestProgess.DONE;
                    doneQuestlist.Add(currentQuestsList[i].id);
                    currentQuestsList.Remove(currentQuestsList[i]);
                    SavelistDone();
                    
                }

            }

            CheckChainQuest(questID);
        }

        public void CheckChainQuest(int questID)
        {
            int tempID = 0;

            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID
                    && questsList[i].nextQuest > 0)
                {
                    tempID = questsList[i].nextQuest;
                }
            }

            if (tempID > 0)
            {
                for (int i = 0; i < questsList.Count; i++)
                {
                    if (questsList[i].id == tempID &&
                        questsList[i].progess == Quest.QuestProgess.NOT_AVAILABLE)
                    {
                        questsList[i].progess = Quest.QuestProgess.AVAILABLE;
                        
                    }
                }
            }
        }

        //BOOL

        public bool RequestAvailableQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.AVAILABLE )
                {
                    return true;
                }
            }
            return false;
        }

        public bool RequestAcceptedQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.ACCEPTED)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RequestCompleteQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.COMPLETE)
                {
                    return true;
                }
            }
            return false;
        }


        public bool CheckAvailableQuest(QuestObject questObject)
        {
            for(int i =0; i < questsList.Count; i++)
            {
                for(int j = 0; j < questObject.availableQuestIDs.Count; j++)
                {
                    if(questsList[i].id == questObject.availableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public bool CheckAcceptQuest(QuestObject questObject)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.recivableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.ACCEPTED)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public bool CheckCompleteQuest(QuestObject questObject)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.recivableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.COMPLETE)
                    {
                        return true;
                    }
                }

            }
            return false;
        }


        public void SavelistDone()
        {
            for(int i = 0; i < doneQuestlist.Count; i++)
            {
                PlayerPrefs.SetInt("Quests" + i, doneQuestlist[i]);
            }
            PlayerPrefs.SetInt("Count", doneQuestlist.Count);
        }

        public void LoadQuestList()
        {
            doneQuestlist.Clear();
            SaveListCount = PlayerPrefs.GetInt("Count");

            for(int i = 0; i < SaveListCount; i++)
            {
                int quests = PlayerPrefs.GetInt("Quests" + i);
                doneQuestlist.Add(quests);
            }
        }
    }
}