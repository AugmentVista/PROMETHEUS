using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Shot;

    private Rigidbody shotBody;
    public float speed = 1f; 
    public float rotationSpeed = 100f; 

    void Start()
    {
        shotBody = GetComponent<Rigidbody>();
        MoveForward();
    }

    private void MoveForward()
    {
        shotBody.velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
        shotBody.MoveRotation(shotBody.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
