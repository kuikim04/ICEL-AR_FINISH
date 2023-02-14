using System.Collections;
using System.Collections.Generic;
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
        private void Start()
        {
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