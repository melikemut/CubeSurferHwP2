using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    int tempScore;
    public int Score
    {
        get { return tempScore; }
        set
        {
            tempScore = value;
            scoreText.text = Score.ToString();
            PlayerPrefs.SetInt("Score", Score);
        }
    }

    private void Awake()
    {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        Score = PlayerPrefs.GetInt("Score");
    }
    public void SetScore(int variable)
    {
        Score += variable;
        PlayerPrefs.SetInt("Score", Score);
    }
    public void SetScore()
    {
        Score++;
        PlayerPrefs.SetInt("Score", Score);
    }
    public int GetScore()
    {
        return Score;
    }
}
