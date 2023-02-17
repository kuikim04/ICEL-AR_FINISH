using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class ExerciseDemoManager : MonoBehaviour
    {
        [SerializeField] ExerciseSet[] DemoSet;

        private void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChange;
        }

        private void OnSceneChange(Scene arg0, Scene arg1)
        {
            var ActiveSceneName = SceneManager.GetActiveScene().name;

            if (ActiveSceneName != "WarmUp")
            {
                KinectManager kinectManager = KinectManager.Instance;
                ExerciseGestureListener listeners = FindObjectOfType(typeof(ExerciseGestureListener)) as ExerciseGestureListener;

                int mapIndex = Singleton.Instance.mapNumSelect - 1;

                listeners?.SetExerciseSet(DemoSet[mapIndex]);
            }
        }
    }
}