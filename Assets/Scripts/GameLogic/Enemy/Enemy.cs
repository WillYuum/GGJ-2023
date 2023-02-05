using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Kill()
    {
        AudioManager.instance.PlaySFX("enemy_dies");
        Destroy(gameObject);
    }
}
