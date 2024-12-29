using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsExperimental : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 5 * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(4f, 0f), 0.1f);
    }
}
