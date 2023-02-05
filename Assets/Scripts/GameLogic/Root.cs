using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Sprite _rootOffSprite;
    [SerializeField] private Sprite _rootOnSprite;


    public bool IsActive { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;


    public void SwitchToActive()
    {
        IsActive = true;
        _spriteRenderer.sprite = _rootOnSprite;
    }

    public void SwitchToInactive()
    {
        IsActive = false;
        _spriteRenderer.sprite = _rootOffSprite;
    }
}
