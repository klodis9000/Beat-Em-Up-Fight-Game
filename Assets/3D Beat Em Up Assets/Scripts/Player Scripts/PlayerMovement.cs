using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterAnimation playerAnimation;
    private Rigidbody myBody;

    public float walkSpeed = 3f;
    public float zSpeed = 1.5f;

    private float rotationY = -90f;
    private float rotationSpeed = 15f;
    
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerAnimation = GetComponentInChildren<CharacterAnimation>();
    }
    void Update()
    {
        RotatePlayer();
        AnimatePlayerWalk();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement() //движение игрока
    {
        myBody.velocity = new Vector3(
            Input.GetAxisRaw("Horizontal") * (-walkSpeed),
            myBody.velocity.y,
            Input.GetAxisRaw("Vertical") * (-zSpeed));
    }

    void RotatePlayer() //вращение игрока
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.rotation=Quaternion.Euler(0f,rotationY,0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) 
        {
            transform.rotation=Quaternion.Euler(0f,Mathf.Abs(rotationY),0f);
        }
    }

    void AnimatePlayerWalk() //анимация ходьбы
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerAnimation.Walk(true);
        }
        else { playerAnimation.Walk(false); }

        
    }
}























