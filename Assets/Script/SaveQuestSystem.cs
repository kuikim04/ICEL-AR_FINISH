using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Script
{
    public class SaveQuestSystem : MonoBehaviour
    {
        public static void SaveQuest(QuestManager questManager)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/quest.q";

            FileStream stream = new FileStream(path, FileMode.Create);

            
        }
    }
}
