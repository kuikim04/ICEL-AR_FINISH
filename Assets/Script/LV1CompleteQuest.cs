using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class LV1CompleteQuest : MonoBehaviour
    {
        public void CompleteQuestLV1()
        {
            if (Singleton.Instance.mapNumSelect == 1)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Airport Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 2)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Technology Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 3)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Shopping Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 4)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Holiday Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 5)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Food/Dining Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 6)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Forest Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 7)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Hospital Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 8)
            {
                QuestManager.questManager.AddQuestItem("Play Level1 Office Success", 1);
            }
          
        }
    }
}
