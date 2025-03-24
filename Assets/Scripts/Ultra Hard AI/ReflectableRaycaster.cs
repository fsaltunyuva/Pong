// Explanation of the script can be found in https://github.com/fsaltunyuva/RayReflection

using UnityEngine;

public class ReflectableRaycaster : MonoBehaviour
{
    Vector2 direction; // Initial direction
    RaycastHit2D hit;
    Vector2 origin;
    private bool isCalculationStarted = false;
    private UltraHardAIControl _ultraHardAIControlScript;
    private LineRenderer _lineRenderer;
    private int currentVertexIndex = 0;
    [SerializeField] private LayerMask defaultLayerMask;
    [SerializeField] private GameObject debugCircle;
    [SerializeField] private int maximumAIIteration = 50; // Maximum iteration that AI can calculate

    private void Start()
    {
        _ultraHardAIControlScript = GetComponent<UltraHardAIControl>();
        // _lineRenderer = GetComponent<LineRenderer>();
        // _lineRenderer.SetWidth(0.1f, 0.1f);
    }

    public void StartRaycastFrom(Vector2 originParam, Vector2 directionParam)
    {
        origin = originParam;
        direction = directionParam;
        // isCalculationStarted = true; // This boolean is needed to calculate in Update()
        
        // Calculating the hit point in while-loop (in one frame)
        Debug.LogWarning("Calculation Started.");
        int overflowCounter = 0; // Preventing while-loop to stuck in infinite iterations (in case of hitting tha ball with the bottom of the player)

        while (overflowCounter < maximumAIIteration)
        {
            hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, defaultLayerMask);

            Debug.DrawRay(origin, direction * 10, Color.green);
            //Debug.Break();
            
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Inv. Walls Up") || hit.collider.gameObject.CompareTag("Inv. Walls Bottom"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.red); // Draw the normal of the hit point
            
                    direction = Vector2.Reflect(direction, hit.normal);

                    Debug.DrawRay(hit.point, direction * 10, Color.blue); 
                    
                    origin = hit.point;
                }
                // else if (hit.collider.gameObject.CompareTag("AI") || hit.collider.gameObject.CompareTag("Inv. Walls Right"))
                else if (hit.collider.gameObject.CompareTag("Ultra Hard AI Hit Point Indicator Wall"))
                {
                    _ultraHardAIControlScript.MoveToCalculatedPosition(hit.point);
                    Debug.Log($"The calculated hit point is: {hit.point}");
                    // Instantiate(debugCircle, hit.point, Quaternion.identity);
                    Debug.LogWarning("Calculation Done.");
                    break;
                }
            }

            overflowCounter++;
        }
        
    }

    // To calculate using the Update(), by doing this we allow Unity to go on during the calculation (ball still moves during tha calculation) therefore, AI can be late to respond
    // https://github.com/fsaltunyuva/Pong/issues/9
    
    /*
    void Update()
    {
        if (isCalculationStarted)
        {
            hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, defaultLayerMask);

            Debug.DrawRay(origin, direction * 10, Color.green);
            Debug.Break();
            
            // _lineRenderer.positionCount += 1;
            // _lineRenderer.SetPosition(currentVertexIndex++, origin);
            
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Inv. Walls Up") || hit.collider.gameObject.CompareTag("Inv. Walls Bottom"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.red); // Draw the normal of the hit point
            
                    direction = Vector2.Reflect(direction, hit.normal);

                    Debug.DrawRay(hit.point, direction * 10, Color.blue); 
                    
                    origin = hit.point;
                }
                // else if (hit.collider.gameObject.CompareTag("AI") || hit.collider.gameObject.CompareTag("Inv. Walls Right"))
                else if (hit.collider.gameObject.CompareTag("Ultra Hard AI Hit Point Indicator Wall"))
                {
                    isCalculationStarted = false;
                    _ultraHardAIControlScript.MoveToCalculatedPosition(hit.point);
                    Debug.Log($"The calculated hit point is: {hit.point}");
                    Instantiate(debugCircle, hit.point, Quaternion.identity);
                }
            }
        }
    }
    */
}