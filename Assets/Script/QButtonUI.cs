using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script {
    public class QButtonUI : MonoBehaviour
    {
        public int questID;
        public TextMeshProUGUI questTitle;


        // Update is called once per frame
        void Update()
        {
            ShowQuest();
        }

        public void ShowQuest()
        {
          

            QuestUI.uiManager.ShowSelectQuest(questID);

            Debug.Log(questID);

            if (QuestManager.questManager.RequestAvailableQuest(questID))
            {
                QuestUI.uiManager.acceptBtn.SetActive(true);
                QuestUI.uiManager.acceptBtnScript.questID = questID;

            }
            else
            {
                QuestUI.uiManager.acceptBtn.SetActive(false);
            }


            if (QuestManager.questManager.RequestCompleteQuest(questID))
            {
                QuestUI.uiManager.completeBtn.SetActive(true);
                QuestUI.uiManager.completeBtnScript.questID = questID;

            }
            else
            {
                QuestUI.uiManager.completeBtn.SetActive(false);
            }
        }

        public void AcceptQuest()
        {
            QuestManager.questManager.AcceptQuest(questID);
            QuestUI.uiManager.HideQuest();

           
        }

        public void CompleteQuest()
        {
            QuestManager.questManager.CompleteQuest(questID);

            #region Check Quest

            if (Singleton.Instance.numQuest == 1)
            {
                Singleton.Instance.numQuest = 2;
            }

            else if (Singleton.Instance.numQuest == 2)
            {
                Singleton.Instance.numQuest = 3;
            }

            else if (Singleton.Instance.numQuest == 3)
            {
                Singleton.Instance.numQuest = 4;
            }

            #endregion

            QuestUI.uiManager.HideQuest();

        }


    }
}
