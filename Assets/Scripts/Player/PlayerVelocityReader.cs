using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedReader : MonoBehaviour
{
    public Animator playerAnim;

    public Rigidbody body;

    [SerializeField]
    private KeyCode attackKey = KeyCode.E;

    [SerializeField]
    private KeyCode hitKey = KeyCode.Q;

    [SerializeField] 
    private Vector3 animVelocity;

    [SerializeField]
    private float currentSpeed = 0;

    public bool attacking;

    public bool damaged;

    public float SpeedThreshold = 0.005f;

    void Start()
    {
        animVelocity = playerAnim.velocity;
    }

    
    void Update()
    {
        currentSpeed = body.velocity.magnitude;

        if (currentSpeed < SpeedThreshold)
        {
            currentSpeed = 0f; // sets Speed to 0 if it is so small it is basically 0
        }
        playerAnim.SetFloat("Speed", currentSpeed);

        if (Input.GetKeyDown(attackKey))
        {
            attacking = true;
            playerAnim.SetBool("Attacking", attacking);
        }
        else
        {
            attacking = false;
            playerAnim.SetBool("Attacking", attacking);
        }

        if (Input.GetKeyDown(hitKey))
        {
            damaged = true;
            playerAnim.SetBool("Damaged", damaged);
        }
        else
        {
            damaged = false;
            playerAnim.SetBool("Damaged", damaged);
        }
    }
}
