using UnityEngine;

public class UltraHardAIControl : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float stepSize = 10.0f;
    public bool shouldIGoToMid = false;
    public bool shouldIGoToCalculatedPosition = false;
    public bool shouldIGoToInvokeBallPosition = false;
    private Vector2 calculatedPosition;

    void Update()
    {
        float step = stepSize * Time.deltaTime;
        
        if (shouldIGoToInvokeBallPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(transform.position.x, -3.5f), step);
        }
        else if (shouldIGoToCalculatedPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(transform.position.x, calculatedPosition.y), step);
        }
        else if (shouldIGoToMid)
        {
            if (transform.position.y is > -0.021f and < 0.03f)
                shouldIGoToMid = false;
            
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(transform.position.x, 0.021f), step);
        }

    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            shouldIGoToMid = true;
            shouldIGoToInvokeBallPosition = false;
            shouldIGoToCalculatedPosition = false;
        }
    }

    public void MoveToCalculatedPosition(Vector2 calculatedPositionParam)
    {
        calculatedPosition = calculatedPositionParam;
        shouldIGoToCalculatedPosition = true;
    }

    public void MoveToInvokePosition() // We are trying to help you!
    {
        shouldIGoToInvokeBallPosition = true;
    }
    
}