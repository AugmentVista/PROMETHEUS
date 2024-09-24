using UnityEngine;

public class KillBox : MonoBehaviour
{
    private string targetTag = "Proj";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.SetActive(false);
        }
    }
}