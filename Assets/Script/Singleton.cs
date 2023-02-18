using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }

        public int levelNumSelect { get; set; }
        public int mapNumSelect { get; set; }
        public int numQulity { get; set; }
        public int numQuest { get; set; }
        public int curQuest { get; set; }
       
        public float soundVolume { get; set; }
        public float musicVolume { get; set; }
        public bool isMute { get; set; }
        public bool isOnEffect { get; set; }
        public bool readyPlay { get; set; }
        public bool gameOver { get; set; }
       

        public bool isLogin;
        public bool isRegister;
        public bool isInputDay { get; set; }

        public bool isWarmUp { get; set; }
        public bool finishWarmup { get; set; }

        private void Awake()
        {
            if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }


            else
            {
                Destroy(gameObject);
            }


            soundVolume = 1;
            musicVolume = 1;
            mapNumSelect = 0;
            levelNumSelect = 0;
            numQulity = 1;
            numQuest = 0;
            curQuest = 1;

            gameOver = false;
            isMute = false;
            isLogin = false;
            isRegister = false; 
            isOnEffect = true;
            isInputDay = false;

        }


        // Start is called before the first frame update
        void Start()
        {

        }

      
    }
}
