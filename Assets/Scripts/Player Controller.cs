using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This script enables player movement and registers hits

    private int desiredLane = 0; // left = <0, middle = 0, right = >0
    private bool moveLeft = true;
    private bool moveRight = true;

    public float laneDistance = 1; //distance between 2 lanes
    public float switchLaneSpeed = 4;
    public HitManager hitManagerScript;
    public DangerManager dangerManagerScript;

    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        //Define desiredLane
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && moveLeft == true)
        {
            desiredLane--;
        }

        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && moveRight == true)
        {
            desiredLane++;
        }

        //Calculate target position of desiredLane
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane <= -1)
        {
            targetPosition += Vector3.left * desiredLane * -1 * laneDistance;
        }

        else if (desiredLane >= 1)
        {
            targetPosition += Vector3.right * desiredLane * laneDistance;
        }

        //Move to target postion
        transform.position = Vector3.Lerp(transform.position, targetPosition, switchLaneSpeed * Time.deltaTime);
    }

    //Limit movement
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Movement_NoBorder"))
        {
            moveLeft = true;
            moveRight = true;
            //print("I can move left and right");
        }

        if (other.gameObject.CompareTag("Movement_LeftBorder"))
        {
            moveLeft = false;
            //print("I cannot move left");
        }

        if (other.gameObject.CompareTag("Movement_RightBorder"))
        {
            moveRight = false;
            //print("I cannot move right");
        }
    }

    //Obstacle Hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hitManagerScript.IncreaseDanger();
            dangerManagerScript.IncreaseDangerTotal();
        }

        if (other.gameObject.CompareTag("DMG_Obstacle"))
        {
            gameManagerScript.EndGame();
        }
    }
}
