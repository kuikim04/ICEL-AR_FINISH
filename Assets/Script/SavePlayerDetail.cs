using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Script
{
    public class SavePlayerDetail : MonoBehaviour
    {
        public static SavePlayerDetail savePlayerDetail;
        public ArrayList playerDetail;
        public string nameFile { get; set; }

        private void Awake()
        {
            if(savePlayerDetail == null)
            {
                savePlayerDetail = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }
        public void CreateFile(string nameFile)
        {

            if (File.Exists(Application.dataPath + "/" + nameFile))
            {
                playerDetail = new ArrayList(File.ReadAllLines(Application.dataPath + "/" + nameFile + ".txt"));
            }
            else
            {
                File.WriteAllText(Application.dataPath + "/" + nameFile + ".txt", "");
            }
        }
        /*
                public void AddQuiz(string Quiz)
                {
                    string path = Application.dataPath + "/" + nameFile +".txt";

                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path, "Quest Day:" + Singleton.Instance.numQuest);;
                    }
                    string content = "\n" + Quiz;

                    File.AppendAllText(path, content);

                }*/

        /* public string ConvertDataToString(List<string> data)
         {
             string dataString = "";
             for (int i = 0; i < data.Count; i++)
             {
                 dataString += data[i].ToString();
                 if (i < data.Count - 1)
                 {
                     dataString += "\n";
                 }
             }
             return dataString;
         }*/

        /*  public void SaveDataToFile(List<string> data)
          {
              string filePath = Application.dataPath + "/"+ nameFile +".txt";
              string dataString = ConvertDataToString(data);
              using (StreamWriter writer = new StreamWriter(filePath))
              {
                  writer.Write(dataString);
              }
          }*/


        
    }
}
