using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverObjectDetector : MonoBehaviour
{

    private void Awake()
    {
        gameObject.AddComponent<CircleCollider2D>();
    }

    private void OnMouseOver()
    {
        print("Mouse is over " + gameObject.name);
    }

    private void OnMouseExit()
    {
        print("Mouse is no longer on " + gameObject.name);
    }
}
