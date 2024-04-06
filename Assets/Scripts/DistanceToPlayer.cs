using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    private GameObject player;
    public float distanceToPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //distanceToPlayer = Vector3.Distance(transform.position.z, player.transform.position.z);
        distanceToPlayer = Mathf.Abs(transform.position.z - player.transform.position.z);
    }
}
