using System;
using UnityEngine;
using Utils.GenericSingletons;

public class GameLoopManager : MonoBehaviourSingleton<GameLoopManager>
{
    public event Action OnGameLoopStart;
    public event Action OnLoseGame;
    public bool LoopIsActive { get; private set; }


    public float ClosestEnemyPositionY = -2.0f;


    private void Awake()
    {
        enabled = false;
        LoopIsActive = false;
    }


    private void Update()
    {
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // var res = Physics2D.Raycast(ray.origin, ray.direction, 100f);
        // print(res.collider?.name);
        // if()
    }

    public void InvokeStartGameLoop()
    {
        enabled = true;
        LoopIsActive = true;

        AudioManager.instance.PlayBGM("Strings");
        AudioManager.instance.PlayBGM("Drums");
        AudioManager.instance.PlayBGM("WindsAndStrings");


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

    void Update() {
        AudioManager.instance.setBGMMode(ClosestEnemyPositionY);
    }
}
