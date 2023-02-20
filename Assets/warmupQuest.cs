using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    public class warmupQuest : MonoBehaviour
    {
        public void CompleteQuestWarmUp()
        {
            QuestManager.questManager.AddQuestItem("Warm Up Success", 1);            
        }

        public void CompleteQuestWarmUpCoolDown()
        {
            QuestManager.questManager.AddQuestItem("Cool Down Success", 1);
        }
    }
}
