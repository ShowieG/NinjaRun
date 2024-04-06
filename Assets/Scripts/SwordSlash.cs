using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    //This script tracks if the sword slash particle hits the player

    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnParticleCollision(GameObject other)
    {
        //print("particlehit");
        if (other.gameObject.CompareTag("Player"))
        {
            gameManagerScript.EndGame();            
        }
    }
}
