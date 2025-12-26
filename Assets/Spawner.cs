using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject agentPrefab;
    public int count = 100;
    public Vector3 initialPosition = Vector3.zero;

    List<float> xInitials = new List<float> {-4.0f, -3.0f, -2.0f, -1.0f, 0f, 1.0f, 2.0f, 3.0f, 4.0f };
    List<float> yInitials = new List<float> { -4.0f, -3.0f, -2.0f, -1.0f, 0f, 1.0f, 2.0f, 3.0f, 4.0f };
    


    static int GetRandomIndex(int length)
    {
        System.Random random = new System.Random();
        int minValue = 0;
        int randomNumber = random.Next(minValue, length);

        return randomNumber;

    }
    void Start()
    {
        float xInitial = xInitials[GetRandomIndex(xInitials.Count)];
        float yInitial = yInitials[GetRandomIndex(yInitials.Count)];

        Vector3 initialPosition = new Vector3(xInitial, yInitial, 0);   

        for (int i = 0; i < count; i++) {
            Instantiate(agentPrefab, initialPosition , Quaternion.identity);
    }

        // Update is called once per frame
        //void Update()
        //{

    }
}
