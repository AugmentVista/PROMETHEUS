using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCanPause : MonoBehaviour
{
    private Vector3 previousVelocity;

    private bool isPaused = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GlobalSettings.globalPauseOverride)
        {
            // Pause
            if (!isPaused)
            {
                previousVelocity = rb.velocity; // Store current velocity
                rb.velocity = Vector3.zero; // Freeze the projectile
                isPaused = true;
            }
        }
        else
        {
            // Unpause
            if (isPaused)
            {
                rb.velocity = previousVelocity; // Restore velocity
                isPaused = false;
            }
        }
    }
}
