using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Boid : MonoBehaviour
{

    public static List<Boid> allBoids = new List<Boid>();

    List<Boid> neighbours = new List<Boid>();

    Camera cam;
    float cameraHeight;
    float cameraWidth;

    Vector3 pos;
    Vector3 velocity;
    Vector3 seperationForce;
    Vector3 linearAcceleration;
    

    List<float> xSpeeds = new List<float> { -2.0f, -3.0f, -4.0f, -5.0f, -6.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
    List<float> ySpeeds = new List<float> { -2.0f, -3.0f, -4.0f, -5.0f, -6.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };

    float xSpeed;
    float ySpeed;

    void OnEnable()
    {
        allBoids.Add(this);
    }
    private void OnDisable()
    {
        allBoids.Remove(this);
    }
    void Start()
    {
        cam = Camera.main;

        xSpeed = xSpeeds[GetRandomIndex(xSpeeds.Count)];
        ySpeed = ySpeeds[GetRandomIndex(ySpeeds.Count)];

        //These guys show me the extents of the graph that the camera captures do not forget David
        cameraHeight = cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;

        velocity = new Vector3(xSpeed, ySpeed, 0f);
       
    }

    void Update()
    {
        pos = transform.position;
        WrapScreen();
        CheckNeighbours();

        seperationForce = AddSeperationForce();
        if (seperationForce.magnitude > 6.0f)
        {
            seperationForce.Normalize();
            seperationForce *= 6.0f;
        }

        velocity += seperationForce * Time.deltaTime;
        linearAcceleration = velocity;
        linearAcceleration.Normalize();
        velocity += linearAcceleration / 2;
    


        if (velocity.magnitude>6.0f)
        {
            velocity.Normalize();
            velocity *= 6.0f;
        }
        pos += velocity * Time.deltaTime;

        //To assign appropriate rotation
        if (velocity != Vector3.zero) { transform.up = velocity; }
        transform.position = pos;

        Debug.Log(neighbours.Count());

        
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

    List<Boid> CheckNeighbours()
    {
        float perceptionRadius = 3f;
        neighbours.Clear();

        foreach (Boid other in Boid.allBoids)
        {
            if (other == this) continue;

            Vector3 distance = other.transform.position - transform.position;
            float displacement = distance.magnitude;

            if (displacement < perceptionRadius)
            {
                neighbours.Add(other);
            }
        }
        return neighbours;
    }
    static int GetRandomIndex(int length)
    {
        System.Random random = new System.Random();

        int minValue = 0;
        int randomNumber = random.Next(minValue, length);

        return randomNumber;
    }

    Vector3 AddSeperationForce()
    {
        Vector3 desiredVelocity = Vector3.zero;
        Vector3 componentVelocity;
        Vector3 steering = Vector3.zero;
        



        if (neighbours.Count > 0)
        {
            foreach (Boid neighbour in neighbours)
            {
                componentVelocity = transform.position - neighbour.transform.position;
                componentVelocity.Normalize();
                desiredVelocity += componentVelocity;
            }
            desiredVelocity.Normalize();
            desiredVelocity *= 6;
            steering = desiredVelocity - velocity;

        }
        return steering;
      
    }
   
}

