using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 direction = new Vector2(10, -10);
    int count = 0;
    RaycastHit2D hit;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hit = Physics2D.Raycast(transform.position, direction);
        Debug.DrawRay(transform.position, transform.TransformDirection(direction), Color.green);
    }

    void Update()
    {
        if (hit) {
            if (hit.collider.gameObject.CompareTag("Respawn")) {
                direction = Vector2.Reflect(direction, hit.normal);
                Debug.Log(hit.normal);
                hit = Physics2D.Raycast(hit.point, Vector2.Reflect(direction, hit.normal));
                Debug.DrawRay(hit.point, Vector2.Reflect(direction, hit.normal), Color.green);
            }
        }
    }
}
