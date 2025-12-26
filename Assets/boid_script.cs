using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class boid_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float cameraHeight;
    float cameraWidth;

    Vector3 pos;
    Vector3 velocity;

    List<float> xSpeeds = new List<float> {-2.0f, -3.0f, -4.0f, -5.0f, -6.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f};
    List<float> ySpeeds = new List<float> { -2.0f, -3.0f, -4.0f, -5.0f, -6.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f};

    float xSpeed;
    float ySpeed;

    




    Camera cam;
    void Start()
    {
        cam = Camera.main;

        xSpeed = xSpeeds[GetRandomIndex(xSpeeds.Count)];
        ySpeed = ySpeeds[GetRandomIndex(ySpeeds.Count)];

        //These guys show me the extents of the graph that the camera captures do not forget David
        cameraHeight = cam.orthographicSize;
        cameraWidth = cameraHeight*cam.aspect;

        velocity = new Vector3(xSpeed, ySpeed, 0f);
        



    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        WrapScreen();

        pos += velocity * Time.deltaTime;
        
        if (velocity != Vector3.zero) { transform.up = velocity; }
        transform.position = pos;
      
        
    }
    void WrapScreen()
    {
        if (pos.x > cameraWidth)
        {
            pos.x = -cameraWidth;
        }
        if (pos.x < -cameraWidth)
        {
            pos.x = cameraWidth;
        }
        if (pos.y > cameraHeight)
        {
            pos.y = -cameraHeight;
        }
        if (pos.y < -cameraHeight)
        {
            pos.y = cameraHeight;
        }
    }
    static int GetRandomIndex(int length)
    {
        System.Random random = new System.Random();

        int minValue = 0;
        int randomNumber = random.Next(minValue, length);

        return randomNumber;
    }
}
