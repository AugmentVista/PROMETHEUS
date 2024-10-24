using System.Collections;
using UnityEngine;

public class PlayerSideToSide : MonoBehaviour
{
    public Transform[] lanes;  // Array of lane positions (left, center, right)
    private int currentLane = 0;   // Tracks the player's current lane index, start at lane 0 (first lane)
    public float sideMoveSpeed = 5f; // Speed for Lerp when moving between lanes
    public float sideMoveCooldown = 0.2f; // Time to wait between side movements

    private bool isMovingSide = false;
    private FirstPersonController firstPersonController; // Reference to the FirstPersonController

    private void Awake()
    {
        firstPersonController = GetComponent<FirstPersonController>(); // Get the FirstPersonController component
    }

    private void Update()
    {
        HandleSideMovement();
    }

    private void HandleSideMovement()
    {
        // Only allow side movement if the player is grounded
        if (firstPersonController != null && firstPersonController.isGrounded && !isMovingSide)
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

        Vector3 targetPosition = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, sideMoveSpeed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            StartCoroutine(SideMovementCooldown());
        }
    }

    private void MoveRight()
    {
        if (currentLane < lanes.Length - 1)
        {
            currentLane++;
            StartCoroutine(SideMovementCooldown());
        }
    }

    private IEnumerator SideMovementCooldown()
    {
        isMovingSide = true;
        yield return new WaitForSeconds(sideMoveCooldown);
        isMovingSide = false;
    }
}

