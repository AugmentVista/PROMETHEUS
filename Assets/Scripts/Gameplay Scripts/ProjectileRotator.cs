using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotator : MonoBehaviour
{
    public GameObject Ring;

    private Rigidbody ringBody;
    private Vector3 ringVelocity;

    public int xRotationSpeed;
    public int yRotationSpeed;
    public int zRotationSpeed;

    void Start()
    {
        ringBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ringVelocity = new Vector3(xRotationSpeed, yRotationSpeed, zRotationSpeed);

        Quaternion deltaRotation = Quaternion.Euler(ringVelocity * Time.fixedDeltaTime);
        ringBody.MoveRotation(ringBody.rotation * deltaRotation);
        xRotationSpeed++;
    }

    private void DestroyProjectile()
    {
        if (xRotationSpeed >= 500)
        {
            Destroy(gameObject, 5f);
        }
    }
}