using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    public GameObject Planet;
    public float destroyTime = 15.0f;
    private Vector3 targetPosition;        // Current target patrol position
    private Rigidbody rb;
    float gravity = 100;
    bool OnGround = false;
    public float maxSpeed = 0.05f;

    float distanceToGround;
    Vector3 Groundnormal;

    float x,z;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        Destroy(gameObject, destroyTime);
        // Start the coroutine to update x and z values
        StartCoroutine(UpdateRandomValues());
    }



    void Update()
    {
        transform.Translate(x, 0, z);


        //GroundControl

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }


        }

        //GRAVITY and ROTATION

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (OnGround == false)
        {
            rb.AddForce(gravDirection * -gravity);
        }


        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    IEnumerator UpdateRandomValues()
    {
        while (true)
        {
            // Generate random values for x and z
            x = Random.Range(0, maxSpeed);
            z = Random.Range(0, maxSpeed);

            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);
        }
    }
}
