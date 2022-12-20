using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
namespace Script
{
    public class Register : MonoBehaviour
    {

        public TMPro.TMP_InputField usernameInput;
        public TMPro.TMP_InputField passwordInput;
        public TMPro.TMP_InputField emailInput;
        public TMPro.TMP_InputField dateInput;

        public GameObject warnIsExists;
        public GameObject successRegister;
        public GameObject registerNotDone;

        public Button registerBtn;

        ArrayList credentials;
        ArrayList credentialsDetail;


        // Start is called before the first frame update
        void Start()
        {
            registerBtn.onClick.AddListener(writeStuffToFile);


            if (File.Exists(Application.dataPath + "/credentials.txt"))
            {
                credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
            }
            else
            {
                File.WriteAllText(Application.dataPath + "/credentials.txt", "");
            }

            #region detailID

            if (File.Exists(Application.dataPath + "/credentialsDetail.txt"))
            {
                credentialsDetail = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentialsDetail.txt"));
            }
            else
            {
                File.WriteAllText(Application.dataPath + "/credentialsDetail.txt", "");
            }
            #endregion
        }
        private void Update()
        {

            if (!Singleton.Instance.isRegister)
            {
                usernameInput.text = "";
                passwordInput.text = "";
                emailInput.text = "";
                dateInput.text = "";
            }
        }


        void writeStuffToFile()
        {       
            bool isExists = false;

            Singleton.Instance.isRegister = true;
            //bool isDone = false;

            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

            bool hasAt = emailInput.text.IndexOf('@') > 0 && emailInput.text.IndexOf('.') > 0;
            

            foreach (var i in credentials)
            {
                if (i.ToString().Contains(usernameInput.text) || i.ToString().Contains(emailInput.text))
                {
                    isExists = true;
                    break;
                }
            }

            if (isExists)
            {
                warnIsExists.SetActive(true);
                // Debug.Log($"Username '{usernameInput.text}' already exists");
            }
            else if (usernameInput.text != "" && passwordInput.text != "" && hasAt && dateInput.text != "")
            {
                successRegister.SetActive(true);
                StartCoroutine(RegisterSuccess());
                credentials.Add(usernameInput.text + ":" + passwordInput.text);
                File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));

                #region  เก็บแยกไอดีพาสเวิด อีเมลล์ และวันเกิด เพื่อไม่ให้การค้นหาไอดีล้อกอินสับสน

                if (usernameInput.text != null && passwordInput.text != null && emailInput.text != null && dateInput.text != null)
                {
                    credentialsDetail.Add("username: " + usernameInput.text + " Password: " + passwordInput.text + " Email: " + emailInput.text + " Birth day: " + dateInput.text);
                    credentialsDetail.Add("__________________________________________________________");
                    File.WriteAllLines(Application.dataPath + "/credentialsDetail.txt", (String[])credentialsDetail.ToArray(typeof(string)));
                }
                #endregion

                // Debug.Log("Account Registered");
            }
            else
            {
                registerNotDone.SetActive(true);
            }
     

        }

        IEnumerator RegisterSuccess()
        {
            yield return new WaitForSeconds(2);
            usernameInput.text = "";
            passwordInput.text = "";
            emailInput.text = "";
            dateInput.text = "";
        }
    }
}