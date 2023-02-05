using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //Create function that runs every 2 seconds
    private void Awake()
    {
        InvokeRepeating(nameof(AttackSurroundings), 0.5f, 2f);
    }


    private void AttackSurroundings()
    {
        //Get all the objects that are within a radius of 2
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        //Loop through all the objects
        foreach (Collider2D collider in colliders)
        {
            //If the object is an attacker
            if (collider.GetComponent<Enemy>())
            {
                //Get the attacker component
                Enemy enemy = collider.GetComponent<Enemy>();

                enemy.Kill();
            }
        }
    }
}
