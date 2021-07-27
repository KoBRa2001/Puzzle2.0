using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    public void UpdateScore(int points)
    {
        _scoreText.text = (points + int.Parse(_scoreText.text.ToString())).ToString();
    }

    public void Reset()
    {
        _scoreText.text = "0";
    }
}
