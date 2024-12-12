using UnityEngine;

public class PlaySoundOnProximity : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public float detectionRange = 5f;  // Range within which the sound will play
    private AudioSource audioSource;   // Reference to the AudioSource

    void Start()
    {
        // Get the AudioSource component on the same object
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calculate the distance between the player and this object
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Player is close enough, play the sound if it's not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Player is too far away, stop the sound if it's playing
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
