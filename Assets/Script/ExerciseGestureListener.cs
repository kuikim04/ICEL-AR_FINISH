using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;
using System.Collections;
using Script;

[System.Serializable]
public struct ExerciseGoal
{
    public string exerciseName;
    public KinectGestures.Gestures exerciseGesture;
    [Tooltip("How many time the player have to perform this exercise.")] public int exerciseTime;
    public bool restAfterFinish;
}

public class ExerciseGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    
    [SerializeField] int playerIndex;
    long playerId;

    private ExerciseGoal currentExerciseGoal;
    [SerializeField] ExerciseSet exerciseSet;
    int count = 0;
    int index = 0;
    bool isOnBreak;

    KinectGestures.Gestures currentGestureGoal;
    [SerializeField] UnityEvent onExerciseComplete;
    [SerializeField] UnityEvent onBreak;
    [SerializeField] UnityEvent onSetComplete;

    [Header("UI")]
    [SerializeField] UnityEngine.UI.Image exerciseStatusImage;
    [SerializeField] TMPro.TextMeshProUGUI exerciseTxt;
    [SerializeField] TMPro.TextMeshProUGUI exerciseCount;


    [SerializeField] GameObject exerciseCountOrbParent;

    [Space]
    [Tooltip("For Debugging")][SerializeField] TextMeshProUGUI progressDebugTxt;
    [SerializeField] UnityEngine.UI.Slider progressDebugBar;

    [Header("UI Resources")]
    [SerializeField] GameObject exerciseOrbPrefab;
    [SerializeField] Sprite exerciseSucceedImage;
    [SerializeField] Sprite exerciseFailedImage;

    //

    private void Start()
    {
        GameManager.totalExerciseCount = 0;

        ChooseExerciseInOrder(exerciseSet.Exercises);
    }

    //

    public void SetExerciseSet(ExerciseSet set)
    {
        exerciseSet = set;
    }

    public void ChooseExerciseInOrder(List<ExerciseGoal> exercises)
    {
        print("choosing exercise");

        if (index >= exercises.Count)
        {
            onSetComplete.Invoke();

            KinectManager manager = KinectManager.Instance;

            index = 0;
            //Singleton.Instance.gameOver = true;
        }

        bool breakAfterLastOne = exercises[index].restAfterFinish;

        currentExerciseGoal = exercises[index];
        SetGestureGoal(currentExerciseGoal);
    }

    //

    public void CallCountCheck()
    {
        StartCoroutine(ExerciseCountCheck());
    }

    public IEnumerator ExerciseCountCheck()
    {
        if (isOnBreak)
            yield break;

        count++;

        UpdateExerciseOrb();

        //Delay here
        yield return new WaitForSecondsRealtime(0.5f);

        if (exerciseCount != null)
        {
            exerciseCount.text = count.ToString() + "/" + currentExerciseGoal.exerciseTime.ToString();
        }

        if (count >= currentExerciseGoal.exerciseTime)
        {

            onExerciseComplete.Invoke();
            count = 0;

            index++;

            if (!currentExerciseGoal.restAfterFinish)
            {
                ChooseExerciseInOrder(exerciseSet.Exercises);
            }
            else
            {
                ExerciseBreak();
                onBreak.Invoke();
            }
        }
    }

    //

    public void ExerciseResume()
    {
        print("Resume");
        
        isOnBreak = false;

        if (Singleton.Instance.gameOver)
            return;

        ChooseExerciseInOrder(exerciseSet.Exercises);
    }

    public void ExerciseBreak()
    {
        isOnBreak = true;

        ClearGesture();

        exerciseStatusImage.sprite = null;
        exerciseTxt.text = "���͡�ӵͺ";
        exerciseCount.text = String.Empty;
    }

    //

    void KinectGestures.GestureListenerInterface.UserDetected(long userId, int userIndex)
    {
        if (userIndex == playerIndex)
            playerId = userId;

        SetGestureGoal(currentGestureGoal);
    }

    void KinectGestures.GestureListenerInterface.UserLost(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;
    }
    
    // SetGesture overloads

    public void SetGestureGoal(KinectGestures.Gestures gestures)
    {
        KinectManager manager = KinectManager.Instance;

        currentGestureGoal = gestures;


        // detect these user specific gestures
        manager.DetectGesture(playerId, currentGestureGoal);
    }

    public void SetGestureGoal(ExerciseGoal exerciseGoal)
    {
        KinectManager manager = KinectManager.Instance;

        currentGestureGoal = exerciseGoal.exerciseGesture;

        // detect these user specific gestures
        manager.DetectGesture(playerId, currentGestureGoal);

        var tempname = exerciseGoal.exerciseName;
        if (exerciseGoal.exerciseName != string.Empty)
            tempname = exerciseGoal.exerciseGesture.ToString();

        exerciseTxt.text = exerciseGoal.exerciseName;

        SetUpExerciseOrb();

        exerciseCount.text = count.ToString() + "/" + currentExerciseGoal.exerciseTime.ToString();
    }

    public void ClearGesture()
    {
        KinectManager manager = KinectManager.Instance;
        manager.ClearGestures(playerId);
    }

    //

    void SetUpExerciseOrb()
    {
        // Spawn Orb if it doesn't match the exerciseTime
        if (exerciseCountOrbParent.transform.childCount < currentExerciseGoal.exerciseTime)
        {
            for (int i = exerciseCountOrbParent.transform.childCount ; i < currentExerciseGoal.exerciseTime; i++)
            {
                var orb = Instantiate(exerciseOrbPrefab, exerciseCountOrbParent.transform);
                orb.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
            }
        }

        // Turn off all Fills and unused Orbs
        for (int i = 0; i < exerciseCountOrbParent.transform.childCount; i++)
        {
            if (i+1 > currentExerciseGoal.exerciseTime)
            {
                exerciseCountOrbParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                exerciseCountOrbParent.transform.GetChild(i).gameObject.SetActive(true);
            }

            exerciseCountOrbParent.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }

    }

    void UpdateExerciseOrb()
    {
        exerciseCountOrbParent.transform.GetChild(count - 1).GetChild(0).gameObject.SetActive(true);
    }

    //

    void KinectGestures.GestureListenerInterface.GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        print("gesture in progress");

        // the gestures are allowed for the primary user only
        if (userIndex != playerIndex)
            return;

        if ((gesture == currentGestureGoal) && progress > 0.5f)
        {

            if (progressDebugTxt != null)
            {
                string sGestureText = string.Format("{0}%", progress * 100f);
                progressDebugTxt.text = sGestureText;
                progressDebugBar.value = progress;
            }
        }
    }

    bool KinectGestures.GestureListenerInterface.GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        print("gesture cancelled");

        // the gestures are allowed for the primary user only
        if (userIndex != playerIndex)
            return false;

        if (progressDebugTxt != null)
        {
            progressDebugTxt.text = "canceled";
            progressDebugBar.value = 0;
        }

        //StartCoroutine(displayStatus(exerciseFailedImage, string.Empty, 1f));

        return true;
    }

    bool KinectGestures.GestureListenerInterface.GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        print("gesture completed");

        // the gestures are allowed for the primary user only
        if (userIndex != playerIndex)
            return false;

            if (progressDebugTxt != null)
            {
                progressDebugBar.value = 0;
                progressDebugTxt.text = "Completed!";
            }

            if (gesture == currentGestureGoal)
            {
                StartCoroutine(displayStatus(exerciseSucceedImage, string.Empty, 1f));
                StartCoroutine(ExerciseCountCheck());
                GameManager.totalExerciseCount += 1;
            }
            else
            {
            StartCoroutine(displayStatus(exerciseFailedImage, string.Empty, 1f));
        }

        return true;

    }

    //

    IEnumerator displayStatus(Sprite sprite, string text, float time)
    {
        exerciseStatusImage.sprite = sprite;
        if (text != string.Empty)
            exerciseTxt.text = text;
        exerciseStatusImage.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(time);

        exerciseTxt.text = currentExerciseGoal.exerciseName;
        exerciseStatusImage.gameObject.SetActive(false);
    }
}
