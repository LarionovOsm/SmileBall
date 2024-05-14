using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Control settings")]
    [Range(0,1)] public float TouchTimeScale;
    [Range(0, 100)] public float TouchError;
    [Header("UI Control settings")]
    [Range(0, 5)] public float CanvasGroupFadeTime;
    [Header("Player settings")]
    [Range(1,5)] public int MaxHealthAmount;
    [Range(0, 1)] public float MinSpeedToStartAcceleration;
    [Range(0,15)] public float MaxAcceleration;
    [Header("Level settings")]
    [Range(0, 5)] public float CheckPointDistanceError;
}
