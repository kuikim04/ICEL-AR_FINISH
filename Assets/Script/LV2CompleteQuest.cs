using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class LV2CompleteQuest : MonoBehaviour
    {
        public void CompleteQuestLV2()
        {
            if (Singleton.Instance.mapNumSelect == 1)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Airport Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 2)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Technology Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 3)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Shopping Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 4)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Holiday Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 5)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Food/Dining Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 6)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Forest Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 7)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Hospital Success", 1);
            }
            if (Singleton.Instance.mapNumSelect == 8)
            {
                QuestManager.questManager.AddQuestItem("Play Level2 Office Success", 1);
            }

        }
    }
}
