using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float sensitivity;
    public float velocity;
    public float acceleration;
    public float fraction;
    public bool isTopped;
    public float notSmoothThreshold;
    public CinemachineVirtualCamera playerCam;

    private float lastFrameVelocity;

    void Start()
    {
        lastFrameVelocity = 0;
        isTopped = false;
    }
    
    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.isInGame)
        {
            transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
        
            velocity *= fraction;
            velocity -= Input.GetAxis("Mouse Y") * sensitivity;
        
            if(transform.position.y < 0) transform.position = Vector3.zero;
            
            CalculateAcceleration();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isTopped = true;
        velocity = 0;
        GameManager.gameState = GameManager.GameState.isGameEnd;
    }
    
    void CalculateAcceleration()
    {
        float currentFrameVelocity = velocity;
        float velocityChange = currentFrameVelocity - lastFrameVelocity;
        
        acceleration = velocityChange / Time.deltaTime;
        lastFrameVelocity = currentFrameVelocity;

        if (Mathf.Abs(acceleration) >= notSmoothThreshold)
        {
            Debug.Log("Not Smooth!");
        }
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, -7, 0);
        transform.DOMoveY(0, 0.7f).OnComplete((() =>
        {
            playerCam.Follow = transform;
            GameManager.gameState = GameManager.GameState.isInGame;         
        }));
    }
}
