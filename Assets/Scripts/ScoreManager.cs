using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private float score = 0;
    public static float highScore;

    public GameObject gameUI;
    public DangerManager dangerManagerScript;
    public float[] additionByDangerLevel = { 1f, 2f, 4f, 10f }; // Addition based on danger level

    void Start()
    {
        gameUI.SetActive(false); // Game HUD is hidden before player starts running, it turns active via the start button

        // Highscore is disabled, enabled if there is a highscore
        highScoreText.enabled = false;
        if (highScore > 0)
        {
            highScoreText.enabled = true;
            highScore = PlayerPrefs.GetFloat("HighScore");
        }
    }

    void IncreaseScore()
    {
        // Calculate the score increase based on the current danger level
        float addition = GetDangerLevelMultiplier(dangerManagerScript.dangerLevel);

        // Increase the score based on the multiplier and the base increase rate
        score += addition * 100 * Time.deltaTime;
    }

    void Update()
    {
        // Update UI Text
        UpdateScoreText();
        UpdateHighScoreText();

        // Update highscore
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }

    public void StartScoreCounting()
    {
        // Start increasing score over time
        InvokeRepeating(nameof(IncreaseScore), 1f, 1f); // Invoke every second
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + ((int)score).ToString();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "Highscore: " + ((int)highScore).ToString();
    }

    float GetDangerLevelMultiplier(int dangerLevel)
    {
        // Ensure dangerLevel is within bounds
        if (dangerLevel < 0 || dangerLevel >= additionByDangerLevel.Length)
        {
            Debug.LogError("Invalid danger level!");
            return 1.0f; // Return default multiplier
        }

        return additionByDangerLevel[dangerLevel];
    }
}
