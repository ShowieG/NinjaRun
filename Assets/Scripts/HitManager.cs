using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitManager : MonoBehaviour
{
    public Text hitText;
    public Image progressBar;
    public float dangerCooldown = 10;

    private int dangerLevel = 0;
    private float maxBarValue = 100;
    private float currentBarValue;

    private void Start()
    {
        dangerLevel = 0;
        currentBarValue = 0;
        UpdateNumberText();
        UpdateProgressBar();
    }

    private void Update()
    {
        if(dangerLevel > 0)
        {
            if (currentBarValue > 0)
            {
                currentBarValue -= Time.deltaTime * dangerCooldown;
                UpdateProgressBar();
            }
            else
            {
                dangerLevel--;
                UpdateNumberText();
                ResetProgressBar();
            }
        }
        else
        {
            currentBarValue = 0;
            UpdateProgressBar();
        }
    }

    public void IncreaseDanger()
    {
        dangerLevel++;
        UpdateNumberText();
        ResetProgressBar();
    }

    private void UpdateNumberText()
    {
        hitText.text = dangerLevel.ToString();
    }

    private void UpdateProgressBar()
    {
        float fillAmount = currentBarValue / maxBarValue;
        progressBar.fillAmount = fillAmount;
    }

    private void ResetProgressBar()
    {
        currentBarValue = maxBarValue;
        UpdateProgressBar();
    }

}
