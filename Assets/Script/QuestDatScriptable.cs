using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    [CreateAssetMenu(fileName = "DataQuestDay", menuName = "DataQuestDay/QuestList", order = 1)]
    public class QuestDatScriptable : ScriptableObject
    {
        public List<Quest> quests;
    }
}