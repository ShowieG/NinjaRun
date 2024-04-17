using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //This script: starts and ends the game, makes the game go faster overtime, plays BGM in SoundManager

    public GameObject particles;
    public GameObject characterModel;
    public PlayerController playerControllerScript;

    [Header("Start Running")]
    public float roadSpeed = 0;
    public float initialSpeed = 0f;
    public float targetSpeed = -12f;
    public float transitionTime = 0.2f; //The time it takes to go from initialSpeed to targetSpeed
    public float delayBeforeChange = 0.1f; //Makes sure that character animation fully plays

    [Header("Game Accelerate Overtime")]
    public float timeScaleIncreaseInterval = 10f; // Increase time scale after this duration
    public float timeScaleIncreaseFactor = 1.01f; // Factor by which time scale increases
    public float maxTimeScale = 4f;


    private void Start()
    {
        StartCoroutine(ResetTimeScale());
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SoundManager.Instance.PlaySFX("Smoke2");
    }

    IEnumerator IncreaseTimeScaleOverTime()
    {
        while (Time.timeScale < maxTimeScale)
        {
            Time.timeScale *= timeScaleIncreaseFactor;

            // Wait for the next interval
            yield return new WaitForSeconds(timeScaleIncreaseInterval);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartRunning());
        StartCoroutine(IncreaseTimeScaleOverTime());
        SoundManager.Instance.PlayMusic("BGM1");
    }

    // Animates the start of running with player animation and road speed going up
    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(delayBeforeChange);

        float elapsedTime = 0f;
        float currentSpeed = initialSpeed;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;

            currentSpeed = Mathf.SmoothStep(initialSpeed, targetSpeed, elapsedTime / transitionTime);

            // Update the speed of the road
            UpdateSpeed(currentSpeed);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        UpdateSpeed(targetSpeed);
    }

    void UpdateSpeed(float speed)
    {
        roadSpeed = speed;
    }

    public void EndGame()
    {
        StopCoroutine(StartRunning());
        StopCoroutine(IncreaseTimeScaleOverTime());

        StartCoroutine(ResetTimeScale());
        StartCoroutine(Dying());
    }

    IEnumerator ResetTimeScale()
    {
        Time.timeScale = 1;
        yield return null;
    }

    IEnumerator Dying()
    {
        SoundManager.Instance.PlaySFX("Smoke1");
        particles.SetActive(true);
        characterModel.SetActive(false);
        playerControllerScript.enabled = false;

        //wait till particles finished playing
        yield return new WaitForSeconds(0.55f);

        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
