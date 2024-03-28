using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this script is currently not in use
    public GameObject[] roadSections;
    public float zSpawn = 0;
    private float sectionLength = 20;
    public int sectionsSpawned = 1;
    private float distanceBetweenSections = 55;

    void Start()
    {
        for(int i=0; i<sectionsSpawned; i++)
        {
            if (i == 0)
            {
                SpawnSection(0);
            }
            else
            {
                SpawnSection(Random.Range(0, roadSections.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSection(int sectionIndex)
    {
        Instantiate(roadSections[sectionIndex], new Vector3(0, 0, distanceBetweenSections), Quaternion.identity);
    }
}
