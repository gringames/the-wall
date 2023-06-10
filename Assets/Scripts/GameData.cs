using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    private static int _score;

    public void AddToScore(int gain)
    {
        _score += gain;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        tmpText.text = "" + _score;
    }
}