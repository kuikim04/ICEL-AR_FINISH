using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class LV3CompleteQuest : MonoBehaviour
    {
        public void CompleteQuestLV3()
        {
            if (Singleton.Instance.mapNumSelect == 1)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Airport Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 2)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Technology Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 3)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Shopping Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 4)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Holiday Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 5)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Food/Dining Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 6)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Forest Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 7)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Hospital Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 8)
            {
                QuestManager.questManager.AddQuestItem("Play Level3 Office Success", 1);
            }

        }
    }
}
