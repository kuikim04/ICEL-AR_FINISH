using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Script
{
    public class LoginManager : MonoBehaviour
    {
        public TMPro.TMP_InputField usernameInput;
        public TMPro.TMP_InputField passwordInput;
        public TMPro.TMP_InputField dateInput;

        public Button loginButton;

        public GameObject mainMenuPage;
        public GameObject loginSuccess;
        public GameObject failedLogin;

        public ArrayList credentials;
        ArrayList LoginHistory;


        // Start is called before the first frame update
        void Start()
        {
            loginButton.onClick.AddListener(login);

            if (File.Exists(Application.dataPath + "/credentials.txt"))
            {
                credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
            }
            else
            {
                Debug.Log("Credential file doesn't exist");
            }

            if (File.Exists(Application.dataPath + "/LoginHistory.txt"))
            {
                LoginHistory = new ArrayList(File.ReadAllLines(Application.dataPath + "/LoginHistory.txt"));
            }
            else
            {
                File.WriteAllText(Application.dataPath + "/LoginHistory.txt", "");
            }

        }

        private void Update()
        {
            if(Singleton.Instance.isRegister)
            {
                usernameInput.text = "";
                passwordInput.text = "";
                dateInput.text = "";
            }
        }

        // Update is called once per frame
        void login()
        {
            bool isExists = false;

            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

            foreach (var i in credentials)
            {
                string line = i.ToString();

                //substring 0-indexof(:) - indexof(:)+1 - i.length-1

                if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                    i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text))
                {
                    isExists = true;

                    SavePlayerDetail.savePlayerDetail.nameFile = usernameInput.text;

                    break;
                }
            }

            if (isExists)
            {

                Debug.Log($"Logging in '{usernameInput.text}'");

                LoginHistory.Add("Username: " + usernameInput.text + " Password: " + passwordInput.text + " is Login" + System.DateTime.Now);
                File.WriteAllLines(Application.dataPath + "/LoginHistory.txt", (String[])LoginHistory.ToArray(typeof(string)));


                loadWelcomeScreen();
            }
            else
            {
                failedLogin.SetActive(true);
                Debug.Log("Incorrect credentials");
            }

            QuestManager.questManager.LoadQuestList();
        }
        void loadWelcomeScreen()
        {
            Singleton.Instance.isLogin = true;
            loginSuccess.SetActive(true);
            StartCoroutine(GoToMainMenu());
        }

        IEnumerator GoToMainMenu()
        {
            yield return new WaitForSeconds(3);
            mainMenuPage.SetActive(true);

        }
    }
}
