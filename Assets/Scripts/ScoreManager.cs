using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public int currentScore=0;

    [HideInInspector]
    public int highScore=0;

    readonly string HS_KEY = "hoghscore";

    public Text scoreText;

    void Start()
    {
        scoreText.text = currentScore.ToString();
        highScore = PlayerPrefs.GetInt(HS_KEY);
    }

    public void StartScoreCount()
    {
        StartCoroutine(incraseScore(1));
    }

    public void ResetScore()
    {
        this.currentScore = 0;
    }

    public void UpdateHighscore()
    {
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(HS_KEY, currentScore);
            highScore = currentScore;
        }
    }

    public IEnumerator incraseScore(int value)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            currentScore += value;
            scoreText.text = currentScore.ToString();

        }
    }
}
