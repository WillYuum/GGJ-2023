using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.GenericSingletons;
using DG.Tweening;

public enum GameState
{
    MainMenu,
    MainGame,
    LoseScreen
}



public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [System.Serializable]
    private class transitionPoints
    {
        [field: SerializeField] public float mainMenuYPos { get; private set; }
        [field: SerializeField] public float mainGameYPos { get; private set; }
        [field: SerializeField] public float loseScreenYPos { get; private set; }
    }

    [SerializeField] private transitionPoints _transitionPoints;

    private void Start()
    {
        AudioManager.instance.Load();
        SwitchToGameState(GameState.MainMenu, null, false);
    }

    public void StartGame()
    {
        GameLoopManager.instance.InvokeStartGameLoop();
        SwitchToGameState(GameState.MainGame, () =>
        {
            //Open MainGameUI  
            GameUI.instance.SwitchState(GameState.MainGame);
        });
    }

    public void RestartGame()
    {
        SwitchToGameState(GameState.MainMenu, () =>
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SwitchToGameState(GameState.MainMenu, null, false);
        });
    }

    public void SwitchToGameState(GameState gameState, Action cb, bool tween = true)
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        switch (gameState)
        {
            case GameState.MainMenu:
                cameraPosition.y = 5.4f;
                break;
            case GameState.MainGame:
                cameraPosition.y = 0;
                break;
            case GameState.LoseScreen:
                cameraPosition.y = -5.4f;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }

        if (tween)
        {
            Camera.main.transform.DOMoveY(cameraPosition.y, 4.5f)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => { if (cb != null) cb.Invoke(); });
        }
        else
        {
            Camera.main.transform.position = cameraPosition;
            if (cb != null)
            {
                cb.Invoke();
            }
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
