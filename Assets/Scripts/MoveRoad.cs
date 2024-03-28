using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public float roadSpeed = -4;

    void Update()
    {
        transform.position += new Vector3(0, 0, roadSpeed) * Time.deltaTime;
    }
}
