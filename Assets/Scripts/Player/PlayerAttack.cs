﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;
    public Transform[] shotSpawnsStarboard;

    //
    // Game Logic
    //
    public float reloadTime;
    private float nextFirePort;
    private float nextFireStarboard;

    //
    // Components
    //
    private AudioSource auShot;

    // Start is called before the first frame update
    void Start()
    {
        auShot = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
}