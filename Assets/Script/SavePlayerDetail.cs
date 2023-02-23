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

        public void AddQuiz(string Quiz)
        {
            string path = Application.dataPath + "/" + nameFile +".txt";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
            string content = Quiz;

            File.AppendAllText(path, content);
            
        }
    }
}
