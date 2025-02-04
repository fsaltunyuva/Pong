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

    private void Start()
    {
        _ultraHardAIControlScript = GetComponent<UltraHardAIControl>();
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
            hit = Physics2D.Raycast(origin, direction);

            Debug.DrawRay(origin, direction, Color.green);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Inv. Walls Up") || hit.collider.gameObject.CompareTag("Inv. Walls Bottom"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.red); // Draw the normal of the hit point
            
                    direction = Vector2.Reflect(direction, hit.normal);

                    Debug.DrawRay(hit.point, direction, Color.blue); 
                    
                    origin = hit.point;
                }
                else if (hit.collider.gameObject.name == "AI" || hit.collider.gameObject.CompareTag("Inv. Walls Right"))
                {
                    isCalculationStarted = false;
                    _ultraHardAIControlScript.MoveToCalculatedPosition(hit.point);
                    Debug.Log($"The calculated hit point is: {hit.point}");
                }
                    
            }
        }
    }
}