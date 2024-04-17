using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DangerManager : MonoBehaviour
{
    //This script tracks hits and danger level

    public Text dangerText;
    public Image dangerBar0;
    public Image dangerBar1;
    public Image dangerBar2;
    public Image dangerBar3;

    public float dangerCooldown = 10;
    public int dangerLevel = 0; //danger lvl number displayed on the screen
    public float dangerAmountPerIncrement = 20;
    private float dangerTotal = 0; //value that determines dangerlevel and dangerbar

    // Also changes house material emission
    public Material[] materials; // Array of materials whose emission you want to toggle
    public bool enableEmission = true; // Flag to enable or disable emission
    public float delay = 2f; // Delay between toggling emission for each material

    private void Start()
    {
        UpdateNumberText();
        UpdateDangerBar();
    }

    private void Update()
    {
        // Ensure dangerTotal stays within 0 to 400 range
        dangerTotal = Mathf.Clamp(dangerTotal, 0f, 400f);

        //Sets dangerLevel to the right number based on dangerTotal
        if (dangerTotal <= 99)
        {
            dangerLevel = 0;
        }
        
        if(dangerTotal >= 100 && dangerTotal <= 199)
        {
            dangerLevel = 1;
        }

        if(dangerTotal >= 200 && dangerTotal <= 299)
        {
            dangerLevel = 2;
        }
        
        if (dangerTotal >= 300)
        {
            dangerLevel = 3;
        }

        UpdateNumberText();
        UpdateDangerBar();

        if (dangerTotal > 0)
        {
            dangerTotal -= Time.deltaTime * dangerCooldown;
        }

        if (dangerLevel >= 1)
        {
            enableEmission = true;
        }
        else
        {
            enableEmission = false;
        }

        // Start coroutine to toggle emission with delay
        StartCoroutine(ToggleEmissionWithDelay());

    }

    public void IncreaseDangerTotal()
    {
        dangerTotal += dangerAmountPerIncrement;
    }

    private void UpdateNumberText()
    {
        dangerText.text = dangerLevel.ToString();
    }

    private void UpdateDangerBar()
    {
        // Calculate fill amounts relative to the maximum value each bar can represent
        dangerBar0.fillAmount = dangerTotal < 100 ? dangerTotal / 100f : 1f;
        dangerBar1.fillAmount = (dangerTotal >= 100 && dangerTotal < 200) ? (dangerTotal % 100f) / 100f : (dangerTotal >= 200 ? 1f : 0f);
        dangerBar2.fillAmount = (dangerTotal >= 200 && dangerTotal < 300) ? (dangerTotal % 100f) / 100f : (dangerTotal >= 300 ? 1f : 0f);
        dangerBar3.fillAmount = (dangerTotal >= 300) ? (dangerTotal % 100f) / 100f : 0f;
    }

    IEnumerator ToggleEmissionWithDelay()
    {
        foreach (Material material in materials)
        {
            yield return new WaitForSeconds(delay);
            // Enable or disable emission based on the flag
            if (enableEmission)
            {
                material.EnableKeyword("_EMISSION");
            }
            else
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
