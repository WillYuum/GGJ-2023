using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
    }

    private void OnClickPlayButton()
    {
        FadeOutAllUI();
        GameManager.instance.Invoke(nameof(GameManager.instance.StartGame), 0.25f);
    }


    private void FadeOutAllUI()
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
    }
}
