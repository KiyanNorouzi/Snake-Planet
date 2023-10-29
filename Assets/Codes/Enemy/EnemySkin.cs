using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkin : MonoBehaviour
{
    public GameObject[] objectsToActivate;

    void Start()
    {
        // Deactivate all game objects at the start
        DeactivateAllObjects();

        // Activate a random game object from the array
        ActivateRandomObject();
    }

    void ActivateRandomObject()
    {
        // Get a random index within the array size
        int randomIndex = Random.Range(0, objectsToActivate.Length);

        // Activate the game object at the random index
        objectsToActivate[randomIndex].SetActive(true);
    }

    void DeactivateAllObjects()
    {
        // Loop through all game objects and deactivate them
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }
}
