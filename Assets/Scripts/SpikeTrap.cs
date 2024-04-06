using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    //This script animates the trap when player enters

    Animator spikesAnimator;

    private void Start()
    {
        spikesAnimator = gameObject.GetComponent<Animator>();
    }

    //Player Hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spikesAnimator.SetBool("PlayerHit", true);
        }
    }
}
