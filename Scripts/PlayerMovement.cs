﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // Movement
    public float gravity = -9.81f;
    public float showSpeed;

    // Jump
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    
    // Is grounded
    public bool isGrounded;

    // Velocity
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {   
        showSpeed = speed();

        // If grounded no gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) { 
            velocity.y = -2f;
        }

        // Movement 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Gravity
        controller.Move(move * speed() * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    float speed()
    {
        // Sprint
        if (Input.GetButton("Fire3") && isGrounded) {
            return 30f;
        } 

        return 20f;

        // Crouch
    }
}
