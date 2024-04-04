using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //this script starts and ends the game, also makes the game go faster overtime
    public GameObject particles;
    public GameObject characterModel;
    
    public float roadSpeed = 0;
    public float initialSpeed = 0f;
    public float targetSpeed = -12f;
    public float transitionTime = 0.2f;
    public float delayBeforeChange = 0.1f;

    private float timeScaleIncreaseInterval = 10f; // Increase time scale after this duration
    private float timeScaleIncreaseFactor = 1.01f; // Factor by which time scale increases


    private void Start()
    {
        StartCoroutine(IncreaseTimeScaleOverTime());
    }

    IEnumerator IncreaseTimeScaleOverTime()
    {
        while (Time.timeScale < 3)
        {
            Time.timeScale *= timeScaleIncreaseFactor;

            // Wait for the next interval
            yield return new WaitForSeconds(timeScaleIncreaseInterval);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartRunning());
    }

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
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        //stops running
        //targetSpeed = 0;
        //Time.timeScale = 1;
        
        //smoke particles
        particles.SetActive(true);
        characterModel.SetActive(false);

        //wait till particles finished playing
        yield return new WaitForSeconds(0.55f);
        SceneManager.LoadScene(0);
    }
}
