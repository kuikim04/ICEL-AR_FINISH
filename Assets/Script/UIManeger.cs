using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class UIManeger : MonoBehaviour
    {
        [Header("First Page")]
        public GameObject uiFirst;
        public GameObject uiLogin;

        [Header("Login Page")]
        public GameObject uiMainMenu;
        [Header("Register Page")]
        public GameObject uiRegister;

        [Header("MainMenu")]
        public GameObject uiSetting;
        public GameObject uiCredit;
        public GameObject uiHowtoPlay;

        [Header("Select Level & Select Scene")]
        public GameObject uiSelectLevel;
        public GameObject uiSelectScene;

        public GameObject selectSceneP1;
        public GameObject selectSceneP2;

        [Header("Quest")]
        public GameObject[] questBtn;
        public GameObject inputDayPanel;
        public TMP_InputField inputDay;
        public GameObject dayFalse;
        int day;

        // Start is called before the first frame update
        void Start()
        {
            Singleton.Instance.gameOver = false;   

            if (Singleton.Instance.isLogin)
            {
                uiSelectLevel.SetActive(true);
                uiMainMenu.SetActive(true);
                inputDayPanel.SetActive(true);
            }

            /*if (warmedUp && !cooledDown)
            {
                cooledDown = true;
                SceneManager.LoadScene("CoolDown");
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            #region Quest        
            

            if (Singleton.Instance.isInputDay)
                inputDayPanel.SetActive(false);


            for (int i = 0; i < QuestManager.questManager.doneQuestlist.Count; i++)
            {
                if (QuestManager.questManager.doneQuestlist.Contains(i + 1))
                {
                    questBtn[i].SetActive(false);
                }

               
            }

            #endregion

        }
        public void FirstPage()
        {
            uiFirst.SetActive(false);
            uiLogin.SetActive(true);
        }

        #region Login & Register Page
        public void LoginPage()
        {
            uiLogin.SetActive(false);
            uiMainMenu.SetActive(true);
        }

        public void RegisterPage()
        {
            Singleton.Instance.isRegister = true;

            uiLogin.SetActive(false);
            uiRegister.SetActive(true);
        }
        public void RegisterBtn()
        {
            uiRegister.SetActive(false);
            uiLogin.SetActive(true); 
            Singleton.Instance.isRegister = false;

        }
        #endregion

        #region Main Menu

        public void InputDay()
        {
            if (Singleton.Instance.isInputDay)
                return;

            day = int.Parse(inputDay.text);
            if(day > 12)
            {
                dayFalse.SetActive(true);
                return;
            }
            Singleton.Instance.isInputDay = true;
            Singleton.Instance.numQuest = day;
            inputDayPanel.SetActive(false);


        }
        public void PlayGame()
        {
                uiSelectLevel.SetActive(true);

            /*{
                warmedUp = true;
                SceneManager.LoadScene("WarmUp");
            }*/
        }

        public void Setting()
        {
            uiSetting.SetActive(true);
        }
        public void CloseSetting()
        {
            uiSetting.SetActive(false);
        }

        public void Credit()
        {
            uiCredit.SetActive(true);
        }
        public void CloseCredit()
        {
            uiCredit.SetActive(false);
        }

        public void HowtoPlay()
        {
            uiHowtoPlay.SetActive(true);
        }
        public void CloseHowtoPlay()
        {
            uiHowtoPlay.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Select Level & Scene

        public void SelectLevelBack()
        {
            uiSelectLevel.SetActive(false);
        }
        public void SelectSceneBack()
        {
            uiSelectScene.SetActive(false);
        }

        public void SelectLevel1()
        {
            if (selectSceneP2.activeSelf)
            {
                selectSceneP1.SetActive(true);
                selectSceneP2.SetActive(false);
            }

            Singleton.Instance.levelNumSelect = 1;

            uiSelectScene.SetActive(true);
        }
        public void SelectLevel2()
        {
            if (selectSceneP2.activeSelf)
            {
                selectSceneP1.SetActive(true);
                selectSceneP2.SetActive(false);
            }

            Singleton.Instance.levelNumSelect = 2;

            uiSelectScene.SetActive(true);
        }
        public void SelectLevel3()
        {
            if (selectSceneP2.activeSelf)
            {
                selectSceneP1.SetActive(true);
                selectSceneP2.SetActive(false);
            }

            Singleton.Instance.levelNumSelect = 3;

            uiSelectScene.SetActive(true);
        }

        #region Select Map
        public void SelectMap1()
        {
            if(Singleton.Instance.levelNumSelect == 1)
            {
                //played = true;
                SceneManager.LoadScene("SceneLV1");
            }

            if(Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 1;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap2()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 2;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap3()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 3;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap4()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 4;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap5()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 5;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap6()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 6;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap7()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 7;
            Singleton.Instance.readyPlay = true;
        }
        public void SelectMap8()
        {
            if (Singleton.Instance.levelNumSelect == 1)
            {
                SceneManager.LoadScene("SceneLV1");
            }

            if (Singleton.Instance.levelNumSelect == 2)
            {
                SceneManager.LoadScene("SceneLV2");
            }

            if (Singleton.Instance.levelNumSelect == 3)
            {
                SceneManager.LoadScene("SceneLV3");
            }

            Singleton.Instance.mapNumSelect = 8;
            Singleton.Instance.readyPlay = true;
        }
        #endregion

        public void SLSnextPage()
        {
            selectSceneP1.SetActive(false);
            selectSceneP2.SetActive(true);
        } 
        public void SLSbackPage()
        {
            selectSceneP1.SetActive(true);
            selectSceneP2.SetActive(false);
        }

        #endregion
    }
}