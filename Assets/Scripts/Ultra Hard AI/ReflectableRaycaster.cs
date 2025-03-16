// Explanation of the script can be found in https://github.com/fsaltunyuva/RayReflection

using System;
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

    private void Start()
    {
        _ultraHardAIControlScript = GetComponent<UltraHardAIControl>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.1f, 0.1f);
    }

    public void StartRaycastFrom(Vector2 originParam, Vector2 directionParam)
    {
        origin = originParam;
        direction = directionParam;
        isCalculationStarted = true;
    }

    void Update()
    {
        if (isCalculationStarted)
        {
            hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, defaultLayerMask);

            Debug.DrawRay(origin, direction * 10, Color.green);
            // Debug.Break();
            
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
                else if (hit.collider.gameObject.CompareTag("Inv. Walls Right"))
                {
                    isCalculationStarted = false;
                    _ultraHardAIControlScript.MoveToCalculatedPosition(hit.point);
                    Debug.Log($"The calculated hit point is: {hit.point}");
                    Instantiate(debugCircle, hit.point, Quaternion.identity);
                }
                    
            }
        }
    }
}