using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float stepSize = 10.0f;

    void Update()
    {
        float step = stepSize * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector2(transform.position.x, ball.position.y), step);
    }
}
