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
            Singleton.Instance.curQuest += 1;        
            QuestUI.uiManager.HideQuest();

        }


    }
}
