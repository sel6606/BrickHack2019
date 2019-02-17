using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour
{

    float scrollSpeed = -1f;
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    //Update runs once per frame
    private void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 20);
        transform.position = startPos + Vector2.right * newPos;
    }
    
}