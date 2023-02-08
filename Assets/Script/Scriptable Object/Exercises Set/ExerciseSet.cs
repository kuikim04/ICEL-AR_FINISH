using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Exercises Set")]
public class ExerciseSet : ScriptableObject
{
    public List<ExerciseGoal> Exercises;
}