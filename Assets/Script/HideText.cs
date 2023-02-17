using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script {
    public class HideText : MonoBehaviour
    {
        public Button btn;


        private void Update()
        {
            if (QuestUI.uiManager.isCompleteAllQuest)
            {
                btn.enabled = false;
            }
    }
    }
}