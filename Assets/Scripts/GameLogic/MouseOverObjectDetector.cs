using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverObjectDetector : MonoBehaviour
{

    private void Awake()
    {
        var circlCollider = gameObject.AddComponent<CircleCollider2D>();
        circlCollider.radius = 0.45f;
    }

    private void OnMouseOver()
    {
        // print("Mouse is over " + gameObject.name);
        GameLoopManager.instance.HoveredRoot = gameObject;
        gameObject.GetComponent<SimpleFlash>().EnableFlash();
    }

    private void OnMouseExit()
    {
        // print("Mouse is no longer on " + gameObject.name);
        GameLoopManager.instance.HoveredRoot = null;
        gameObject.GetComponent<SimpleFlash>().DisableFlash();
    }
}
