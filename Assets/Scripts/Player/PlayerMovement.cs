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

    public bool WasHit(bool hit)
    {
        if (hit) 
        {
            MoveBackwards(1);
            return true; 
        }
        else if (!hit) { return false; }
        return false;
    }

    public void MoveBackwards(int spacesToMove)
    {
        if (currentRow < rows - 1) // Adjust if more columns are added
        {
            currentRow--;
            currentRow = Mathf.Max(0, currentRow - spacesToMove);
        }
        if (gridPositions[currentRow, currentColumn] != null)
        {
            currentRow = Mathf.Max(0, currentRow - spacesToMove);
            transform.position = Vector3.Lerp(transform.position, gridPositions[currentRow, currentColumn].position, moveSpeed * Time.deltaTime);
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