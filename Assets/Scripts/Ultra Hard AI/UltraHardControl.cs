using System.Collections;
using UnityEngine;

public class UltraHardControl : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private ReflectableRaycaster _reflectableRaycaster;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Velocity vector after the ball hits the left player: " + ballRB.velocity);
            Debug.Log("Origin of the velocity vector: " + ballRB.transform.position); // After the collision (not exact point of intersection)
            _reflectableRaycaster.StartRaycastFrom(ballRB.transform.position, ballRB.velocity);
        }
    }

    /* Alternative solution to printing the velocity vector 0.25 secs after the ball hits the left player */

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ball"))
    //     {
    //         StartCoroutine(PrintVelocityVector(ballRB.velocity));
    //     }
    // }

    // IEnumerator PrintVelocityVector(Vector2 vector2)
    // {
    //     yield return new WaitForSeconds(0.25f);
    //     print("Velocity vector 0.25 secs after the ball hits the left player: " + vector2);
    // }
    
    void Update()
    {
        var vel = rb2d.velocity;
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }

        rb2d.velocity = vel;
    }
}