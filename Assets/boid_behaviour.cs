using UnityEngine;


public class boid_behaviour : MonoBehaviour
{
    void WrapAroundScreen(Transform t)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        Vector3 pos = t.position;

        float buffer = 0.5f; // half the sprite/collider size

        if (pos.x > camWidth + buffer) pos.x = -camWidth - buffer;
        else if (pos.x < -camWidth - buffer) pos.x = camWidth + buffer;

        if (pos.y > camHeight + buffer) pos.y = -camHeight - buffer;
        else if (pos.y < -camHeight - buffer) pos.y = camHeight + buffer;


        t.position = pos;
    }

    public Rigidbody2D myRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.linearVelocityX = 10;
        WrapAroundScreen(transform);
        
    }
}
