using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public float destroyTime = 20.0f;
    private bool hasBeenTriggered = false;  // Flag to track if the trigger event has occurred
    private Player player;


    public GameObject ParticleSystemPrefab;
    public AudioClip SoundClip;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();

        if (hasBeenTriggered)
            return;


        if (other.tag == "Player")
        {
            // Increment the count or perform any other collectible action
            player.GrowSnake();
            hasBeenTriggered = true; // Set the flag to indicate the trigger event has occurred
            Destroy(this.gameObject);
        }


        // Instantiate the particle system at the current position
        GameObject particleSystemObj = Instantiate(ParticleSystemPrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemObj.GetComponent<ParticleSystem>();
        particleSystem.Play();

        // Play the audio clip
        AudioSource.PlayClipAtPoint(SoundClip, transform.position);

        // Destroy the particle system object after a certain time 
        Destroy(particleSystemObj, particleSystem.main.duration + 1f);
    }
}