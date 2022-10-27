using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private ScoreHandler sh;
    public Text highScoreText;
    public Text scoreText;
    int score;
    int highScore;

    // Start is called before the first frame update
    void Start()
    {
        sh = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        score = sh.score;
        highScore = score;
        scoreText.text = highScore.ToString();

        if (PlayerPrefs.GetInt("Score") <= highScore)
        {
            PlayerPrefs.SetInt("Score", highScore);
        }
    }

    public void SetHighScore()
    {
        highScoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
