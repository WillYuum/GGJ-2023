using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Defender : MonoBehaviour
{
    [SerializeField] private GameObject attackAOE_image;

    //Create function that runs every 2 seconds
    private void Awake()
    {
        attackAOE_image.SetActive(false);
        InvokeRepeating(nameof(AttackSurroundings), 0.5f, 2f);
    }


    private void AttackSurroundings()
    {
        attackAOE_image.SetActive(true);
        attackAOE_image.transform.localScale = Vector3.zero;
        attackAOE_image.transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            attackAOE_image.SetActive(false);
            attackAOE_image.transform.localScale = Vector3.one * 1.2f;
        });


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
