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
        if (firstPersonController != null /*&& firstPersonController.isGrounded*/ && !isMovingSide)
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

    public void WasHit(bool hit, string projectileTag)
    {
        if (hit) // if this is true, the player moves backwards
        {
            // Handle different projectile types based on the tag
            switch (projectileTag)
            {
                case "Stone":
                    // thing
                    break;
                case "Knockback":
                    //MoveBackwards(1);
                    break;

                case "Stun":
                    //StartCoroutine(StunPlayer(2f)); // Example stun for 2 seconds
                    break;

                case "Slow":
                    //StartCoroutine(SlowPlayer(2f)); // Example slow for 2 seconds
                    break;

                default:
                    // Handle other cases if needed (like the StoneProjectile with no special effect)
                    break;
            }
        }
    }

    //public void MoveBackwards(int spacesToMove)
    //{
    //    if (currentRow > spacesToMove) // Adjust if more columns are added
    //    {
    //        currentRow = currentRow - spacesToMove;
    //        if (currentRow <= 0) // after decrementing if the currentRow is 0 or less the player loses instead of moves
    //        {
    //            //Lose script
    //        }
    //        else if (gridPositions[currentRow, currentColumn] != null) // after decrementing current row is greater than 0 player moves back
    //        {
    //            transform.position = Vector3.Lerp(transform.position, gridPositions[currentRow, currentColumn].position, moveSpeed * Time.deltaTime);
    //        }
    //    }
    //}
   
    //private IEnumerator StunPlayer(float duration)
    //{
    //    // Temporarily disable movement input while stunned
    //    Debug.Log("Player is stunned!");
    //    isMovingSide = true;
    //    isMovingForward = true;
    //    yield return new WaitForSeconds(duration);
    //    isMovingSide = false;
    //    isMovingForward = false;
    //    Debug.Log("Player is no longer stunned.");
    //}

    //private IEnumerator SlowPlayer(float duration)
    //{
    //    Debug.Log("Player is slowed!");
    //    moveSpeed /= 2; // Reduce movement speed by half as an example
    //    yield return new WaitForSeconds(duration);
    //    moveSpeed *= 2; // Restore original speed
    //    Debug.Log("Player is no longer slowed.");
    //}


}

