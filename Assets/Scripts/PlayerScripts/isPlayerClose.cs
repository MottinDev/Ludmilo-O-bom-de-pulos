using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayerClose : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public float detectionRange = 5f;  // Range within which the character is considered "nearby"
    public Animator characterAnimator; // Reference to the character's Animator

    void Update()
    {
        // Check the distance between the player and the character
        float distance = Vector3.Distance(transform.position, player.position);

        // Set the "isCharacterNearby" parameter in the Animator based on distance
        if (distance <= detectionRange)
        {
            // Player is close enough, set the boolean to true
            characterAnimator.SetBool("character_nearby", true);
        }
        else
        {
            // Player is too far away, set the boolean to false
            characterAnimator.SetBool("character_nearby", false);
        }
    }
}
