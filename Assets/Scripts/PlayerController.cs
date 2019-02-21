﻿using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    //
    // Physics
    //
    public float rotationModifier;
    public Vector2 thrustModifier; // (power up, power down)

    public float shipSpeed;

    public float tiltModifier;

    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;
    public Transform[] shotSpawnsStarboard;
    public float reloadTime;

    private float nextFirePort;
    private float nextFireStarboard;

    //
    // Components
    //

    private Rigidbody rb;
    private AudioSource auShot;

    //
    // World
    //

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFirePort)
        {
            nextFirePort = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsPort[x].position, shotSpawnsPort[x].rotation);
                auShot.Play();
            }
        }

        if (Input.GetButton("Fire2") && Time.time > nextFireStarboard)
        {
            nextFireStarboard = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsStarboard[x].position, shotSpawnsStarboard[x].rotation);
                auShot.Play();
            }
        }
    }

    // FixedUpdate is called before physics operations
    void FixedUpdate()
    {
        float userRudder = Input.GetAxis("Horizontal");
        float userThrust = Input.GetAxis("Vertical");

        if (userThrust > 0)
        {
            shipSpeed += userThrust * thrustModifier[0];
        }
        else
        {
            shipSpeed += userThrust * thrustModifier[1];
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, userRudder * rotationModifier * shipSpeed, 0)));
        rb.MovePosition(rb.position + new Vector3(shipSpeed * thrustModifier[0] * Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, shipSpeed * thrustModifier[0] * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));
    }
}
