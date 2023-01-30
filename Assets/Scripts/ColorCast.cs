using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastColor
{
    Red,
    Green,
    Blue,
    Yellow
}
public class ColorCast : MonoBehaviour
{
    public CastColor color;
    public LineRenderer lineRenderer;

    public float castRate = 0.2f;

    public float checkDistance = 1f; //ako daleko budu puzzle od seba aby ich bolo mozne psojit
    
    private float lastCastTime;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Color lrColor = Color.white;
        if(color == CastColor.Blue)
           lrColor = Color.blue;
        else if(color == CastColor.Green)
           lrColor = Color.green;
        else if(color == CastColor.Red)
           lrColor = Color.red;
        else if(color == CastColor.Yellow)
           lrColor = Color.yellow;  

        lineRenderer.startColor = lrColor; 
        lineRenderer.endColor = lrColor; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCastTime > castRate)
        {
            lastCastTime = Time.time;
            TryCast();
        }        
    }

    void TryCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(new Ray(transform.position, transform.forward), out hit, checkDistance)) //forward - strielam pozdlz Z osi
        {
            ColorCast colorCast = hit.collider.GetComponent<ColorCast>();

            if(colorCast != null)
            {
                if(colorCast.color == color)
                {
                    lineRenderer.enabled = true;

                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.transform.position);

                    return;
                }
            }
        }

        lineRenderer.enabled = false;
    }
}
