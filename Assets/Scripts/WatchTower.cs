using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : MonoBehaviour
{
    private GameObject player;
    public float distanceToPlayer;
    public float maxTriggerDistance = 18f;
    public float minTriggerDistance = 5f;

    public GameObject bulletPrefab; // Reference to the bullet prefab
    public GameObject warningPrefab;
    public float laserDuration = 2f; // Duration the laser follows the player
    private float elapsedTime = 0f;
    public float bulletSpeed = 10f; // Speed of the bullet
    private bool isShooting = false;

    private LineRenderer lineRenderer; // Reference to the Line Renderer component
    private Vector3 lastPlayerPosition; // Last position of the player
    private Vector3 originLaser; //Where laser starts

    private bool chargeSoundHasPlayed = false;

    void Start()
    {
        // Get the Line Renderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distanceToPlayer = Mathf.Abs(transform.position.z - player.transform.position.z);

        // Check if the player is within the trigger distance
        if (distanceToPlayer <= maxTriggerDistance && distanceToPlayer >= minTriggerDistance)
        {
            //print("within triggerDistance");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
            {
                // Check if the ray hit the player
                if (hit.collider.CompareTag("Player"))
                {
                    if (chargeSoundHasPlayed == false)
                    {
                        SoundManager.Instance.PlaySFX("GunCharge");
                        chargeSoundHasPlayed = true;
                    }

                    if (elapsedTime < laserDuration)
                    {
                        //Debug.Log("Running code within Update() for " + elapsedTime + " seconds");
                        lineRenderer.enabled = true;
                        originLaser = transform.position;
                        lastPlayerPosition = player.transform.position;

                        lineRenderer.SetPosition(0, originLaser);
                        lineRenderer.SetPosition(1, lastPlayerPosition);

                        elapsedTime += Time.deltaTime;
                    }

                    else
                    {
                        if (isShooting == false)
                        {
                            lineRenderer.enabled = false;

                            StartCoroutine(ShootBullet());
                            isShooting = true;
                            SoundManager.Instance.PlaySFX("GunShoot");
                            //Debug.Log("Duration reached, stopping code execution");
                        }
                    }
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    IEnumerator ShootBullet()
    {
        // Instantiate the bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Calculate the direction from the bullet's current position to the player's position
        Vector3 direction = (lastPlayerPosition - transform.position).normalized;

        // Calculate the final position of the bullet
        Vector3 finalPosition = transform.position + direction * Vector3.Distance(transform.position, lastPlayerPosition);

        GameObject warning = Instantiate(warningPrefab, new Vector3(lastPlayerPosition.x, 0, lastPlayerPosition.z), new Quaternion(0, 0, 0, 0));

        while (Vector3.Distance(bullet.transform.position, finalPosition) > 0.01f)
        {
            // Move towards the final position at the specified speed
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, finalPosition, bulletSpeed * Time.deltaTime);

            yield return null;
        }

        yield return null;
        Destroy(bullet);
        Destroy(warning);

        //Debug.Log("Bullet reached the target position");
    }
}
