using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager questManager;

        public List<Quest> questsList = new List<Quest>();
        public List<Quest> currentQuestsList = new List<Quest>();

        public List<int> doneQuestlist = new List<int>();
        private int SaveListCount;
        public QuestDatScriptable[] questDaylist;

        List<string> questDoneIDs;

        private void Awake()
        {
            if (questManager == null)
            {
                questManager = this;
            }
            else if (questManager != null)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            PlayerPrefs.DeleteAll();
        
        }
        private void Update()
        {
            #region CheckQuestDay

            //day 1
            if (Singleton.Instance.numQuest == 1)
            {
                questsList = new List<Quest>(questDaylist[0].quests);
            }
            //day 2
            else if (Singleton.Instance.numQuest == 2)
            {
                questsList = new List<Quest>(questDaylist[1].quests);           
            }
            //day 3
            else if (Singleton.Instance.numQuest == 3)
            {
                questsList = new List<Quest>(questDaylist[2].quests);
            }
            //day 4
            else if (Singleton.Instance.numQuest == 4)
            {
                questsList = new List<Quest>(questDaylist[3].quests);
            }
            //day 5
            else if (Singleton.Instance.numQuest == 5)
            {
                questsList = new List<Quest>(questDaylist[4].quests);
            }
            //day 6
            else if (Singleton.Instance.numQuest == 6)
            {
                questsList = new List<Quest>(questDaylist[5].quests);
            }
            //day 7
            else if (Singleton.Instance.numQuest == 7)
            {
                questsList = new List<Quest>(questDaylist[6].quests);
            }
            //day 8
            else if (Singleton.Instance.numQuest == 8)
            {
                questsList = new List<Quest>(questDaylist[7].quests);
            }
            //day 9
            else if (Singleton.Instance.numQuest == 9)
            {
                questsList = new List<Quest>(questDaylist[8].quests);
            }
            //day 10
            else if (Singleton.Instance.numQuest == 10)
            {
                questsList = new List<Quest>(questDaylist[9].quests);
            }
            //day 11
            else if (Singleton.Instance.numQuest == 11)
            {
                questsList = new List<Quest>(questDaylist[10].quests);
            }
            //day 12
            else if (Singleton.Instance.numQuest == 12)
            {
                questsList = new List<Quest>(questDaylist[11].quests);
            }


            #endregion


        }


        public void QuestRequest(QuestObject questObject)
        {
            if (questObject.availableQuestIDs.Count > 0)
            {
                for (int i = 0; i < questsList.Count; i++)
                {

                    for (int j = 0; j < questObject.availableQuestIDs.Count; j++)
                    {
                        if (questsList[i].id == questObject.availableQuestIDs[j]
                            && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                        {

                            //AcceptQuest(questObject.availableQuestIDs[j]);
                            QuestUI.uiManager.questAvailable = true;
                            QuestUI.uiManager.availableQuests.Add(questsList[i]);
                        }
                    }
                }
            }

            for (int i = 0; i < currentQuestsList.Count; i++)
            {
                for (int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if (currentQuestsList[i].id == questObject.recivableQuestIDs[j]
                        && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED
                        || currentQuestsList[i].progess == Quest.QuestProgess.COMPLETE)
                    {
                        //CompleteQuest(questObject.recivableQuestIDs[j]);
                        QuestUI.uiManager.questRunning = true;
                        QuestUI.uiManager.activeQuests.Add(currentQuestsList[i]);                 
                    }
                }
            }
        }

        public void AddQuestItem(string questObjective, int itemAmount)
        {
            for (int i = 0; i < currentQuestsList.Count; i++)
            {
                if (currentQuestsList[i].questObjective == questObjective
                    && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
                {
                    currentQuestsList[i].questObjectiveCount += itemAmount;
                }
                if (currentQuestsList[i].questObjectiveCount >= currentQuestsList[i].questObjectiveRequirment
                    && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
                {
                    currentQuestsList[i].progess = Quest.QuestProgess.COMPLETE;
                }
            }
        }

        public void AcceptQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID &&
                    questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                {
                    currentQuestsList.Add(questsList[i]);
                    questsList[i].progess = Quest.QuestProgess.ACCEPTED;

                    AddDataToFile();
                }
            }
        }

        public void CompleteQuest(int questID)
        {
            SavePlayerDetail.savePlayerDetail.playerDetail = new ArrayList(File.ReadAllLines(Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + ".txt"));

            for (int i = 0; i < currentQuestsList.Count; i++)
            {
                if (currentQuestsList[i].id == questID &&
                    currentQuestsList[i].progess == Quest.QuestProgess.COMPLETE)
                {
                    currentQuestsList[i].progess = Quest.QuestProgess.DONE;

                    // doneQuestlist.Add(currentQuestsList[i].id);

                    SaveListDoneInTxt(currentQuestsList[i].id);
                    #region Save .txt

                    string filePath = Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + ".txt";
                    List<string> lines = new List<string>(File.ReadAllLines(filePath));
                    string newProgress = Quest.QuestProgess.DONE.ToString();

                    // วนลูปเข้าไปใน List<string> เพื่อแก้ไขข้อมูล
                    for (int j = 0; j < lines.Count; j++)
                    {
                        if (lines[j].Contains("Quest ID:" + questID))
                        {
                            string oldProgress = GetCurrentProgress(questID);
                            if (oldProgress == newProgress.ToString())
                            {
                                // ถ้า Progress เดิมเหมือน Progress ใหม่ที่ต้องการเปลี่ยนเป็น จะไม่ต้องทำอะไร
                                Debug.Log("Error");
                            }
                            else
                            {
                                // แทนที่ Progress เดิมด้วย Progress ใหม่                              
                                lines[j] = lines[j].Replace("Progress:" + oldProgress, "Progress:" + newProgress.ToString());
                                Debug.Log(newProgress);
                            }
                        }
                    }
                    // เขียน List<string> ลงไฟล์ .txt ใหม่
                    File.WriteAllLines(filePath, lines.ToArray(), Encoding.UTF8);
                    #endregion

                    currentQuestsList.Remove(currentQuestsList[i]);
                    SavelistDone();

                }

            }

            CheckChainQuest(questID);
        }

        public void CheckChainQuest(int questID)
        {
            int tempID = 0;

            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID
                    && questsList[i].nextQuest > 0)
                {
                    tempID = questsList[i].nextQuest;
                }
            }

            if (tempID > 0)
            {
                for (int i = 0; i < questsList.Count; i++)
                {
                    if (questsList[i].id == tempID &&
                        questsList[i].progess == Quest.QuestProgess.NOT_AVAILABLE)
                    {
                        questsList[i].progess = Quest.QuestProgess.AVAILABLE;

                    }
                }
            }
        }

        //BOOL

        public bool RequestAvailableQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.AVAILABLE)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RequestAcceptedQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.ACCEPTED)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RequestCompleteQuest(int questID)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == questID && questsList[i].progess ==
                    Quest.QuestProgess.COMPLETE)
                {
                    return true;
                }
            }
            return false;
        }


        public bool CheckAvailableQuest(QuestObject questObject)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.availableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.availableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public bool CheckAcceptQuest(QuestObject questObject)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.recivableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.ACCEPTED)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public bool CheckCompleteQuest(QuestObject questObject)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.recivableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.recivableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.COMPLETE)
                    {
                        return true;
                    }
                }

            }
            return false;
        }


        public void SavelistDone()
        {
            for (int i = 0; i < doneQuestlist.Count; i++)
            {
                PlayerPrefs.SetInt("Quests" + i, doneQuestlist[i]);
            }
            PlayerPrefs.SetInt("Count", doneQuestlist.Count);
        }
        public void LoadQuestList()
        {
            doneQuestlist.Clear();
            SaveListCount = PlayerPrefs.GetInt("Count");

            for (int i = 0; i < SaveListCount; i++)
            {
                int quests = PlayerPrefs.GetInt("Quests" + i);
                doneQuestlist.Add(quests);
            }
        }
        public void SaveListDoneInTxt(int id)
        {
            if (File.Exists(Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + "QuestListDone" + ".txt"))
            {
                questDoneIDs = new List<string>(File.ReadAllLines(Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + "QuestListDone" + ".txt"));
            }
            else
            {
                questDoneIDs = new List<string>();
            }
            questDoneIDs.Add("Quest Day: " + Singleton.Instance.numQuest + " : " + id.ToString());
            // เขียน List ลงในไฟล์ txt ใหม่
            File.WriteAllLines(Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + "QuestListDone" + ".txt", questDoneIDs.ToArray());

            // อ่านค่าจากไฟล์ txt และแปลงเป็น int
            string filePath = Path.Combine(Application.streamingAssetsPath, Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + "QuestListDone" + ".txt");
            string data = File.ReadAllText(filePath);
            int number = int.Parse(data);

            // ใช้ค่า number ในการดำเนินการต่อไป
            Debug.Log(number);
        }



        public void AddDataToFile()
        {
            string filePath = Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + ".txt";

            // ตรวจสอบว่าไฟล์ว่างเปล่ามั้ย
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {              
                WriteNewDataToFile(filePath);
            }
            else
            {
                // อ่านข้อมูลเดิมจากไฟล์
                string[] lines = File.ReadAllLines(filePath);
                bool headerExists = false;
                int headerIndex = -1;

                // ค้นหาหัวข้อ Quest Day
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("Quest Day:"))
                    {
                        headerExists = true;
                        headerIndex = i;
                        break;
                    }
                }

                // เพิ่มหัวข้อ Quest Day ใหม่ (ถ้ายังไม่มี)

                if (!headerExists)
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        for (int i = 0; i < Singleton.Instance.numQuest; i++)
                        {
                            string newHeader = "Quest Day:" + (i + 1);
                            string newData = questDaylist[i].EncodeDataToString();

                            writer.WriteLine(newHeader);
                            writer.WriteLine(newData);
                            
                        }
                    }                   
                    return;
                }

                // อัพเดทข้อมูลใหม่
                for (int i = 0; i < Singleton.Instance.numQuest; i++)
                {
                    string newHeader = "\n" + "Quest Day:" + (i + 1);
                    string newData = questDaylist[i].EncodeDataToString();
                    string newProgess = Quest.QuestProgess.DONE.ToString();

                    // หาบรรทัดที่ต้องการเขียนข้อมูลใหม่
                    int startIndex = headerIndex + 7;
                    int endIndex = (i == Singleton.Instance.numQuest - 1) ? lines.Length : FindNextHeaderIndex(lines, startIndex);


                    // เขียนข้อมูลใหม่ลงไฟล์
                    if (!IsQuestDayExists(filePath, i + 1))
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, false))
                        {
                            // เขียนข้อมูลเดิมก่อนหัวข้อที่กำหนด
                            for (int j = 0; j < startIndex; j++)
                            {
                                writer.WriteLine(lines[j]);
                            }

                            // เขียนหัวข้อ Quest Day ที่กำหนด
                            writer.WriteLine(newHeader);
/*
                            // เพิ่มแถวว่างและข้อความ "Quest Day"
                            writer.WriteLine("");*/

                            // เขียนข้อมูลใหม่
                            writer.WriteLine(newData);

                            // เขียนข้อมูลเดิมที่หลังหัวข้อที่กำหนด (หากมี)
                            for (int j = startIndex; j < endIndex; j++)
                            {
                                writer.WriteLine(lines[j]);
                            }
                        }

                        // อัพเดท headerIndex ใหม่                    
                        headerIndex = FindHeaderIndex(lines, newHeader);

                    }
                   
                }
            }
        }
        private void WriteNewDataToFile(string filePath)
        {
            string newHeader = $"Quest Day:{Singleton.Instance.numQuest}";
            string newData = questDaylist[Singleton.Instance.numQuest-1].EncodeDataToString();

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(newHeader);
                writer.WriteLine(newData);
            }
        }
        private int FindNextHeaderIndex(string[] lines, int startIndex)
        {
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Quest Day:"))
                {
                    return i;
                }
            }

            return lines.Length;
        }
        private int FindHeaderIndex(string[] lines, string header)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(header))
                {
                    return i;
                }
            }

            return -1; // ไม่พบ header ที่ต้องการ
        }
        private bool IsQuestDayExists(string filePath, int questDay)
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return false;
            }
            else
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("Quest Day:" + questDay))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        private string GetCurrentProgress(int questID)
        {
            string filePath = Application.dataPath + "/" + SavePlayerDetail.savePlayerDetail.nameFile + ".txt";

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.Contains("Quest ID:" + questID))
                {
                    string[] parts = line.Split(' ');
                    return parts[parts.Length - 1];
                }
            }
            return null;
        }

    }
}
