using UnityEngine;

public class AudioPlayerStruck : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerOof;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knockback")
        {
            Debug.LogError($"WHY IS VALUE NULL!? {playerOof} ");
            audioSource.PlayOneShot(playerOof, 1);
        }
    }



}
