using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Joystick joystickMovement;
    public GameObject joystick;
    public GameManager gameManager;
    public GameObject Planet;
    public GameObject InGameScorePanel;
    public GameObject GameOverCamera;
    public GameObject GameOver;

    public float speed = 4;
    public float PlayerSpeed = 0.05f;
    private int point = 0;


    float gravity = 100;
    bool OnGround = false;

    float distanceToGround;
    Vector3 Groundnormal;

    private Rigidbody rb;

  
    public float BodySpeed = 5;
    public int Gap = 10;

    public GameObject BodyPrefab;


    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
      
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        GameOver.SetActive(false);
        GameOverCamera.SetActive(false);
        InGameScorePanel.SetActive(true);
        joystick.SetActive(true);

        GrowSnake();
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //MOVEMENT
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float z = joystickMovement.Vertical * Time.deltaTime * speed;


       
        transform.Translate(PlayerSpeed, 0, z);

        //Local Rotation
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }

        if (joystickMovement.Horizontal>0.01f)
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
        if (joystickMovement.Horizontal < 0.0f)
        {

            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }



        //GroundControl
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.1f)
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
        int index = 2;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }


    }




    private void OnTriggerEnter(Collider other)
    {

       // if (other.tag == "Item")
        //{
            //add points
            //point += 1;
            //add ability
            //add any powerup here
          //GrowSnake();
       // }


        if (other.tag == "Hazard")
        {
            Destroy(gameObject,2.0f);
            GameOver.SetActive(true);
            GameOverCamera.SetActive(true);
            gameManager.SaveBestRecord(point);
            gameManager.LoadBestRecord();
            joystick.SetActive(false);
            InGameScorePanel.SetActive(false);
        }



    }


    public void GrowSnake()
    {
        point += 1;
        // Instantiate a new body part
        GameObject body = Instantiate(BodyPrefab);
        // Add the body part to the list
        BodyParts.Add(body);
    }

    public int PointCalc()
    {
        return point;
    }

}