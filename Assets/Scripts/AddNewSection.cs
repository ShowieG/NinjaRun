using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewSection : MonoBehaviour
{
    //public GameObject roadSection;
    public GameObject[] sectionPrefabs;
    public GameObject[] danger1Sections;
    public HitManager hitManagerScript;

    private float distanceBetweenSections = 53;
    private int sectionIndex;

    //which section to spawn
    private void Update()
    {
        //sectionIndex = Random.Range(0, sectionPrefabs.Length);
        sectionIndex = hitManagerScript.dangerLevel;
    }

    //spawn road section when player hits trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_NewSection"))
        {
            //Instantiate(roadSection, new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
            Instantiate(sectionPrefabs[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
        }
    }
}
