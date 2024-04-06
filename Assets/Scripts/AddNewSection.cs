using UnityEngine;

public class AddNewSection : MonoBehaviour
{
    //This script adds new roadsections based on the danger level

    public GameObject[] danger0Sections;
    public GameObject[] danger1Sections;
    public GameObject[] danger2Sections;
    public GameObject[] danger3Sections;

    public HitManager hitManagerScript;

    private float distanceBetweenSections = 53;
    private int sectionIndex;

    //which section to spawn
    private void Update()
    {
        if(hitManagerScript.dangerLevel == 0)
        {
            sectionIndex = Random.Range(0, danger0Sections.Length);
        }

        if (hitManagerScript.dangerLevel == 1)
        {
            sectionIndex = Random.Range(0, danger1Sections.Length);
        }

        if (hitManagerScript.dangerLevel == 2)
        {
            sectionIndex = Random.Range(0, danger2Sections.Length);
        }

        if (hitManagerScript.dangerLevel == 3)
        {
            sectionIndex = Random.Range(0, danger3Sections.Length);
        }
    }

    //spawn road section when player hits trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_NewSection"))
        {
            if (hitManagerScript.dangerLevel == 0)
            {
                Instantiate(danger0Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (hitManagerScript.dangerLevel == 1)
            {
                Instantiate(danger1Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (hitManagerScript.dangerLevel == 2)
            {
                Instantiate(danger2Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }

            if (hitManagerScript.dangerLevel == 3)
            {
                Instantiate(danger3Sections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            }
        }
    }
}
