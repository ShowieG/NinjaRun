using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashTrigger : MonoBehaviour
{
    //This script animates the enemy ninja when player gets close

    Animator enemyAnimator;
    public float triggerDistance = 12f;
    private GameObject player;
    private bool sfxHasPlayed = false;

    private void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distanceToPlayerZ = Mathf.Abs(transform.position.z - player.transform.position.z);

        // Check if the player is within the trigger distance
        if (distanceToPlayerZ < triggerDistance)
        {
            enemyAnimator.SetBool("PlayerDetected", true);

            if (sfxHasPlayed == false)
            {
                SoundManager.Instance.PlaySFX("EnemySlash");
                sfxHasPlayed = true;
            }
        }
    }
}
