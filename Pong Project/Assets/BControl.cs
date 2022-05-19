using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 1);
    }

    void GoBall(){
        float rand = Random.Range(0, 2);
        if (rand < 1){
        rb2d.AddForce(new Vector2(0.4f, -0.3f));
        }
       else {
        rb2d.AddForce(new Vector2(-0.4f, -0.3f));
        }
    }
    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }
        void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Inv. Walls Right") {
            RestartGame();
            Debug.Log("Left Won");
        }
        else if(other.gameObject.tag=="Inv. Walls Left") {
            RestartGame();
             Debug.Log("Right Won");
        }
    }
}
