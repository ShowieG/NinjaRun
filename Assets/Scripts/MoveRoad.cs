using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, gameManagerScript.roadSpeed) * Time.deltaTime;
    }
}
