using System;
using UnityEngine;
using Utils.GenericSingletons;

public class GameLoopManager : MonoBehaviourSingleton<GameLoopManager>
{
    public event Action OnGameLoopStart;
    public event Action OnLoseGame;
    public bool LoopIsActive { get; private set; }


    private void Awake()
    {
        enabled = false;
        LoopIsActive = false;
    }


    public void InvokeStartGameLoop()
    {
        enabled = true;
        LoopIsActive = true;

        AudioManager.instance.PlayBGM("Strings");


        if (OnGameLoopStart != null)
        {
            OnGameLoopStart();
        }
    }

    public void InvokeLoseGame()
    {
        if (LoopIsActive == false) return;

        enabled = false;
        LoopIsActive = false;

        if (OnLoseGame != null)
        {
            OnLoseGame();
        }

        GameManager.instance.SwitchToGameState(GameState.LoseScreen, () =>
        {
            //Open LoseScreenUI
            GameUI.instance.SwitchState(GameState.LoseScreen);
        });
    }


    public void StartGameLoop()
    {

        AudioManager.instance.PlayBGM("Strings");
    }
}
