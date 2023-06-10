using System;
using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private TMP_Text gameScore;
    [SerializeField] private TMP_Text outcome;
    public int sessionHighScore;
    public int currentScore;

    public static GameData Instance;
    private int _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddToScore(int gain)
    {
        _score += gain;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScore.text = "" + _score;
    }

    public void DisplayOutcome()
    {
        outcome.enabled = true;
        gameScore.enabled = false;

        outcome.text = $"Current Score: {currentScore} \n High Score: {sessionHighScore}";
    }

    public void Reload()
    {
        currentScore = _score;
        sessionHighScore = Math.Max(sessionHighScore, currentScore);
    }

    public void StartGame()
    {
        outcome.enabled = false;
        gameScore.enabled = true;
        _score = 0;
    }
}