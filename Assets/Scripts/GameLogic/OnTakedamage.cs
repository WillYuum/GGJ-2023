using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTakedamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameLoopManager.instance.PlayerTakeDamage();
        }
    }
}
