using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "Scriptable Objects/GameStateSO")]


public class GameStateSO : ScriptableObject
{
    //state of counter
    public int currentScore;
    public int highScore;

    public float waitingToStartTimer = 1f;
    public float countdownToStartTimer = 3f;
    public float gamePlayingTimer = 20f;



    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    public State currentGameState;



}
