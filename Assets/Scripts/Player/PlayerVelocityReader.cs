using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityReader : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody body;
    public Vector3 animVelocity;
    void Start()
    {
        animVelocity = playerAnim.velocity;
    }

    
    void Update()
    {
        playerAnim.SetFloat("Velocity", body.velocity.magnitude);
    }
}
