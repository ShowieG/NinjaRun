using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashTrigger : MonoBehaviour
{
    //This script animates the enemy ninja when player gets close

    Animator enemyAnimator;
    public float triggerDistance = 12f;
    //public DistanceToPlayer distancePlayerScript;
    private GameObject player;

    private void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        //    if (distancePlayerScript.distanceToPlayer < triggerDistance)
        //    {
        //        enemyAnimator.SetBool("PlayerDetected", true);
        //    } else
        //    {
        //        enemyAnimator.SetBool("PlayerDetected", false);
        //    }

        float distanceToPlayerZ = Mathf.Abs(transform.position.z - player.transform.position.z);

        // Check if the player is within the trigger distance
        if (distanceToPlayerZ < triggerDistance)
        {
            enemyAnimator.SetBool("PlayerDetected", true);
        }
    }
}
