using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils.GenericSingletons;

public class ManuelMovementScript : MonoBehaviourSingleton<ManuelMovementScript>
{
    [SerializeField] private Transform[] points;
    [SerializeField] public GameObject[] moveableObjects;

    public void MoveObjectsOnPoints()
    {
        //Move all object on every point and switch points to the next one every 1 sec
        // for (int i = 0; i < points.Length; i++)
        // {
        // for (int j = 0; j < moveableObjects.Length; j++)
        // {
        //     moveableObjects[j].transform.position = points[i].position;
        // }
        // StartCoroutine(WaitForSeconds(1f));
        // }


        foreach (GameObject moveableObject in moveableObjects)
        {
            //start coroutine on making every object move on every point
            StartCoroutine(MoveObjectOnPoints(moveableObject));


        }
    }


    private IEnumerator MoveObjectOnPoints(GameObject moveableObject)
    {
        float durationToNextPOint = 5.55f;

        //tween to the next value
        for (int i = 0; i < points.Length; i++)
        {
            moveableObject.transform.DOMove(points[i].position, durationToNextPOint);
            yield return new WaitForSeconds(durationToNextPOint);
        }

    }


}
