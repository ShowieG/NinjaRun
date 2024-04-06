using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private float score = 0;
    public float highScore;

    public GameObject gameUI;
    public HitManager hitManagerScript;
    public float[] additionByDangerLevel = { 1f, 2f, 4f, 10f }; // Addition based on danger level

    void Start()
    {
        UpdateNumberText();
        gameUI.SetActive(false); //game HUD is hidden before player starts running, it turns active via the start button

        highScoreText.enabled = false; //highscore is disabled, enabled if there is a highscore
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highScoreText.enabled = true;
            highScore = PlayerPrefs.GetFloat("HighScore");
        }

        // Start increasing score over time
        InvokeRepeating(nameof(IncreaseScore), 1f, 1f); // Invoke IncreaseScore method every second
    }

    void IncreaseScore()
    {
        // Calculate the score increase based on the current danger level
        float addition = GetDangerLevelMultiplier(hitManagerScript.dangerLevel);

        // Increase the score based on the multiplier and the base increase rate
        score += addition * 100 * Time.deltaTime;
    }

    void Update()
    {
        UpdateNumberText();
        highScoreText.text = "Highscore: " + ((int)highScore).ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("Highscore", highScore);
        }
    }

    private void UpdateNumberText()
    {
        scoreText.text = "Score: " + ((int)score).ToString();
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
