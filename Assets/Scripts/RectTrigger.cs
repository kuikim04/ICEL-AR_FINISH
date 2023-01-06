using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script {
    public class RectTrigger : MonoBehaviour
    {
        [Header("ITEM")]
        public ItemUptime upTime;
        public float time;
        [Range(0, 10)]
        public int mSensitivity = 5;
        public bool mIsTriggered = false;

        private Camera mCamera = null;
        private RectTransform mRectTranform = null;
        private Image mImage = null;
       
        private void Awake()
        {
            MeasureDepth.OnTriggerPoints += OnTriggerPoints;
            mRectTranform = GetComponent<RectTransform>();
            mImage = GetComponent<Image>();
            mCamera = Camera.main;
        }
        private void OnDestroy()
        {
            MeasureDepth.OnTriggerPoints -= OnTriggerPoints;

        }
        private void OnTriggerPoints(List<Vector2> triggerPoints)
        {
            if (!enabled)
                return;

            int count = 0;

            foreach (Vector2 point in triggerPoints)
            {
                Vector2 flipedY = new Vector2(point.x, mCamera.pixelHeight - point.y);

                if (RectTransformUtility.RectangleContainsScreenPoint(mRectTranform, flipedY))
                    count++;
            }
            if (count > mSensitivity)
            {
                
                    mIsTriggered = true;
                    gameObject.GetComponent<AnswerScripts>().Answer();        
                
                /*if (gameObject.CompareTag("upTime"))
                {
                    mIsTriggered = true;
                    upTime.UseItem(time);
                }*/

                /*if (gameObject.CompareTag("PauseUI"))
                {

                }*/
            }

        }
    } 
}
