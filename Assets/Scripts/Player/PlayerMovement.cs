using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform leftLane;
    public Transform centerLane;
    public Transform rightLane;  
    private Transform targetLane;   

    public float moveSpeed = 10f;

    private bool isMoving = false;  // To prevent rapid switching

    private void Start()
    {
        targetLane = centerLane;
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
        }

        // Lerp the player's position towards the target lane
        if (targetLane != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetLane.position, moveSpeed * Time.deltaTime);
        }
    }

    private void MoveLeft()
    {
        if (targetLane == rightLane)
        {
            targetLane = centerLane; // Move from right to center
            StartCoroutine(MovementCooldown());
        }
        else if (targetLane == centerLane)
        {
            targetLane = leftLane; // Move from center to left
            StartCoroutine(MovementCooldown());
        }
    }

    private void MoveRight()
    {
        if (targetLane == leftLane)
        {
            targetLane = centerLane; // Move from left to center
            StartCoroutine(MovementCooldown());
        }
        else if (targetLane == centerLane)
        {
            targetLane = rightLane; // Move from center to right
            StartCoroutine(MovementCooldown());
        }
    }

    private System.Collections.IEnumerator MovementCooldown()
    {
        isMoving = true;
        yield return new WaitForSeconds(0.1f); 
        isMoving = false; // Allow input again
    }
}
