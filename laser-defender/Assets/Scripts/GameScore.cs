using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameScore
{
    public delegate void ScoreUpdated();
    public static event ScoreUpdated E_ScoreUpdated;

    public delegate void WavesUpdated();
    public static event WavesUpdated E_WavesUpdated;

    public static int WavesCleared { get; set; }
    public static int CurrentScore { get; private set; }

    public static void ResetScore()
    {
        CurrentScore = 0;
        E_ScoreUpdated?.Invoke();
    }
    
    public static void UpdateScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
        E_ScoreUpdated?.Invoke();
    }

    public static void ResetWaves()
    {
        WavesCleared = 0;
        E_WavesUpdated?.Invoke();
    }

    public static void UpdateWaves(int waveToAdd)
    {
        WavesCleared = waveToAdd;
        E_WavesUpdated?.Invoke();
    }
}
