using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    [CreateAssetMenu(fileName = "DataQuestDay", menuName = "DataQuestDay/QuestList", order = 1)]
    public class QuestDatScriptable : ScriptableObject
    {
        public List<Quest> quests = new List<Quest>();

 
        public string EncodeDataToString()
        {
            string encodedData = "";

            // ������ҧ: �� List<int> ����ժ��� dataList ����������� string ��������ͧ���� "," �� "1,2,3,4"
            for (int i = 0; i < quests.Count; i++)
            {
                encodedData += "Quest ID:" + quests[i].id + " " + quests[i].title + " "+ "Progress:"+ quests[i].progess.ToString();
                if (i < quests.Count - 1)
                {
                    encodedData += "\n";
                }
            }

            return encodedData;
        }
    }
}