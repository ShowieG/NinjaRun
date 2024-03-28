using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitManager : MonoBehaviour
{
    public Text hitText;
    public Image progressBar;
    public float dangerCooldown = 10;

    public int dangerLevel; //danger lvl number displayed on the screen
    private float maxBarValue = 100;
    private float currentBarValue;
    public float fillAmountPerIncrement = 20;

    private void Start()
    {
        UpdateNumberText();
        UpdateProgressBar();
    }

    private void Update()
    {
        if (currentBarValue > 0)
        {
            currentBarValue -= Time.deltaTime * dangerCooldown;
            UpdateProgressBar();
        }
        else
        {
            DecreaseDangerLevel();
            UpdateProgressBar();
        }
    }

    public void IncreaseDanger()
    {
        currentBarValue += fillAmountPerIncrement;
        if (currentBarValue >= maxBarValue)
        {
            currentBarValue = maxBarValue;
            IncreaseDangerLevel();
        }
        UpdateProgressBar();
    }

    public void IncreaseDangerLevel()
    {
        if (dangerLevel < 3)
        {
            dangerLevel++;
            UpdateNumberText();
            currentBarValue = 0;
        }
    }

    public void DecreaseDangerLevel()
    {
        if (dangerLevel > 0)
        {
            dangerLevel--;
            UpdateNumberText();
            currentBarValue = 100;
        }
    }

    private void UpdateNumberText()
    {
        hitText.text = dangerLevel.ToString();
    }

    private void UpdateProgressBar()
    {
        //float fillAmount = currentBarValue / maxBarValue;
        progressBar.fillAmount = currentBarValue / maxBarValue;
    }

    private void ResetProgressBar()
    {
        currentBarValue = 0;
        UpdateProgressBar();
    }

}
