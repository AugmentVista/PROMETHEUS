using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityReader : MonoBehaviour
{
    public Animator playerAnim;

    public Rigidbody body;

    public Vector3 animVelocity;
    public float velocityThreshold = 0.01f;
    void Start()
    {
        animVelocity = playerAnim.velocity;
    }

    
    void Update()
    {
        float currentVelocity = body.velocity.magnitude;

        if (currentVelocity < velocityThreshold)
        {
            currentVelocity = 0f; // sets velocity to 0 if it is so small it is basically 0
        }
        playerAnim.SetFloat("Velocity", currentVelocity);
    }
}
