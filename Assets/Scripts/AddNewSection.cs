using UnityEngine;

public class AddNewSection : MonoBehaviour
{
    //This script adds new roadsections based on the danger level

    public GameObject[] danger0Sections;
    public GameObject[] danger1Sections;
    public GameObject[] danger2Sections;
    public GameObject[] danger3Sections;
    public GameObject[] environmentSections;

    //public HitManager hitManagerScript;
    public DangerManager dangerManagerScript;

    private float distanceBetweenSections = 53;
    private int sectionIndex;
    private int environmentIndex;

    private void Start()
    {
        Instantiate(danger0Sections[Random.Range(0, danger0Sections.Length)], new Vector3(0, 0, 20), Quaternion.identity);
        Instantiate(environmentSections[Random.Range(0, environmentSections.Length)], new Vector3(0, 0, 20), Quaternion.identity);
        Instantiate(danger0Sections[Random.Range(0, danger0Sections.Length)], new Vector3(0, 0, 40), Quaternion.identity);
        Instantiate(environmentSections[Random.Range(0, environmentSections.Length)], new Vector3(0, 0, 40), Quaternion.identity);
        Instantiate(danger0Sections[Random.Range(0, danger0Sections.Length)], new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(environmentSections[Random.Range(0, environmentSections.Length)], new Vector3(0, 0, 60), Quaternion.identity);
    }

    //which section to spawn
    private void Update()
    {
        if(dangerManagerScript.dangerLevel == 0)
        {
            sectionIndex = Random.Range(0, danger0Sections.Length);
        }

        if (dangerManagerScript.dangerLevel == 1)
        {
            sectionIndex = Random.Range(0, danger1Sections.Length);
        }

        if (dangerManagerScript.dangerLevel == 2)
        {
            sectionIndex = Random.Range(0, danger2Sections.Length);
        }

        if (dangerManagerScript.dangerLevel == 3)
        {
            sectionIndex = Random.Range(0, danger3Sections.Length);
        }
    }

    //spawn road section when player hits trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_NewSection"))
        {
            if (dangerManagerScript.dangerLevel == 0)
            {
                Instantiate(danger0Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (dangerManagerScript.dangerLevel == 1)
            {
                Instantiate(danger1Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (dangerManagerScript.dangerLevel == 2)
            {
                Instantiate(danger2Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (dangerManagerScript.dangerLevel == 3)
            {
                Instantiate(danger3Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            environmentIndex = Random.Range(0, environmentSections.Length);
            Instantiate(environmentSections[environmentIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
        }
    }
}
