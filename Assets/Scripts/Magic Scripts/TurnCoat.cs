using UnityEngine;

public class TurnCoat : MonoBehaviour
{
    public GameObject hypnotizePrefab;
    float lifeSpan = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            GameObject hypnotize = Instantiate(hypnotizePrefab, other.transform.position, Quaternion.identity);

            hypnotize.SetActive(true);

            ParticleSystem hypnotizeVFX = hypnotize.GetComponent<ParticleSystem>();
            if (hypnotizeVFX != null)
            {
                hypnotizeVFX.Play();
            }
            hypnotizeVFX.transform.SetParent(other.transform);

            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            if (otherRB)
            {
                Vector3 storedVelocity = otherRB.velocity;
                otherRB.velocity = Vector3.zero;
                otherRB.velocity = storedVelocity * -1;
            }
            else if (!otherRB)
            {
                float storedZMovement = other.transform.position.z; 
                other.transform.Translate(new Vector3(other.transform.position.x, other.transform.position.y, storedZMovement * -1));
            }
        }
    }

    private void Update()
    {
        lifeSpan += Time.deltaTime;
        if (lifeSpan > 3f)
        {
            Destroy(gameObject);
        }
    }
}