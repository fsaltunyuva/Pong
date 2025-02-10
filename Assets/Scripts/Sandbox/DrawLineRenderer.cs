using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DrawLine(new Vector3(0,0),new Vector3(4,4), Color.magenta );
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.positionCount = 3;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.SetPosition(2, new Vector3(4, 0));
        // GameObject.Destroy(myLine, duration);
    }
}
