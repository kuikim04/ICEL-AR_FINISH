using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class QuestObject : MonoBehaviour
    {
        public List<int> availableQuestIDs = new List<int>();
        public List<int> recivableQuestIDs = new List<int>();

       
        public void AcceptQuest()
        {
            //QuestManager.questManager.QuestRequest(this);
            QuestUI.uiManager.CheckQuests(this);
            
           
        }
    }
}