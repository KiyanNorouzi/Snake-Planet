using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public class SnakeBot : MonoBehaviour
{
    public GameObject Planet;
    private Vector3 targetPosition;        // Current target patrol position
    private Rigidbody rb;
    float gravity = 100;
    bool OnGround = false;
    public float maxSpeed = 0.05f;

    public float BodySpeed = 5;
    public int Gap = 10;

    float distanceToGround;
    Vector3 Groundnormal;

    float x, z;

    public GameObject BodyPrefab;


    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        // Start the coroutine to update x and z values
        StartCoroutine(UpdateRandomValues());

      
        for (int i = 0; i <= 20; i++)
        {
            GrowSnake();
        }
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


        //store Position History
        PositionsHistory.Insert(0, transform.position);



        //Move body parts
        int index = 1;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
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


    public void GrowSnake()
    {
        // Instantiate a new body part
        GameObject body = Instantiate(BodyPrefab);
        // Add the body part to the list
        BodyParts.Add(body);
    }
}
