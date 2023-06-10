using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    private static int _score;

    public void ResetScore()
    {
        _score = 0;
    }

    public void AddToScore(int gain)
    {
        _score += gain;
    }

    private void UpdateScoreText()
    {
        tmpText.text = "" + _score;
    }
}