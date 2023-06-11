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
        sessionHighScore = PlayerPrefs.GetInt("Highscore");
        DisplayOutcome();
    }

    public void AddToScore(int gain)
    {
        _score += gain;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScore.text = "Score: " + _score;
    }

    public void DisplayOutcome()
    {
        outcome.enabled = true;
        gameScore.enabled = true;

        outcome.text = "Highscore: "+ sessionHighScore;
        gameScore.text = "Last Score: " + PlayerPrefs.GetInt("Lastscore");
    }

    public void Reload()
    {
        currentScore = _score;
        sessionHighScore = Math.Max(sessionHighScore, currentScore);
        PlayerPrefs.SetInt("Highscore", sessionHighScore);
        PlayerPrefs.SetInt("Lastscore", _score);
    }

    public void StartGame()
    {
        outcome.enabled = false;
        gameScore.enabled = true;
        _score = 0;
        UpdateScoreText();
    }
}