using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] leftColumn;
    public Transform[] rightColumn;
    // Add more columns more grid sizes (middleColumn for a 3-column grid)

    private Transform[,] gridPositions; // 2D array containing positions from column arrays
    private int rows;              

    private int currentRow = 0;   
    private int currentColumn = 0;

    public float moveSpeed = 10f;
    public float sideMoveCooldown = 0.1f;
    public float forwardMoveCooldown = 1f;

    private bool isMovingSide = false;
    private bool isMovingForward = false;

    private void Start()
    {
        rows = leftColumn.Length;
        gridPositions = new Transform[rows, 2]; // 2 columns (left and right), adjust if more columns are added

        for (int i = 0; i < rows; i++)
        {
            gridPositions[i, 0] = leftColumn[i];  // Left column (0 index)
            gridPositions[i, 1] = rightColumn[i]; // Right column (1 index)
            Debug.Log("Left column is " + leftColumn.Length + " long");
        }

        // If adding a middle column for a 3-column grid, populate it similarly
        // for (int i = 0; i < rows; i++)
        // {
        //     gridPositions[i, 2] = middleColumn[i]; // Middle column (2 index)
        // }
    }

    private void Update()
    {
        PlayerInput();
        Debug.Log(gridPositions[currentRow, currentColumn]);
        if (gridPositions[currentRow, currentColumn] != null)
        {
            transform.position = Vector3.Lerp(transform.position, gridPositions[currentRow, currentColumn].position, moveSpeed * Time.deltaTime);
        }
    }

    private void PlayerInput()
    {
        if (!isMovingSide)
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

        if (!isMovingForward)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveForward();
            }
        }
    }

    private void MoveLeft()
    {
        if (currentColumn > 0)
        {
            currentColumn--;
            StartCoroutine(SideMovementCooldown());
        }
    }

    private void MoveRight()
    {
        if (currentColumn < 1) // Adjust if more columns are added
        {
            currentColumn++;
            StartCoroutine(SideMovementCooldown());
        }
    }

    private void MoveForward()
    {
        if (currentRow < rows - 1)
        {
            currentRow++;
            StartCoroutine(ForwardMovementCooldown());
        }
    }

    public void WasHit(bool hit)
    {
        if (hit) // if this is true player moves backwards
        {
            MoveBackwards(1);
        }
        else 
        {
            hit = false;
            return; 
        }
    }

    public void MoveBackwards(int spacesToMove)
    {
        if (currentRow > spacesToMove) // Adjust if more columns are added
        {
            currentRow = currentRow - spacesToMove;
            if (currentRow <= 0) // after decrementing if the currentRow is 0 or less the player loses instead of moves
            { 
            //Lose script
            }
            else if (gridPositions[currentRow, currentColumn] != null) // after decrementing current row is greater than 0 player moves back
            {
                transform.position = Vector3.Lerp(transform.position, gridPositions[currentRow, currentColumn].position, moveSpeed * Time.deltaTime);
            }
        } 
    }

    private IEnumerator SideMovementCooldown()
    {
        isMovingSide = true;
        yield return new WaitForSeconds(sideMoveCooldown);
        isMovingSide = false;
    }

    private IEnumerator ForwardMovementCooldown()
    {
        isMovingForward = true;
        yield return new WaitForSeconds(forwardMoveCooldown);
        isMovingForward = false;
    }
}