using System;
using UnityEngine;
using Utils.GenericSingletons;

public class GameLoopManager : MonoBehaviourSingleton<GameLoopManager>
{
    public event Action OnGameLoopStart;
    public event Action OnLoseGame;
    public bool LoopIsActive { get; private set; }


    public int CurrentHP { get; set; }
    public int CurrentRootResource { get; set; }


    public float ClosestEnemyPositionY = -2.0f;


    private void Awake()
    {
        enabled = false;
        LoopIsActive = false;
    }


    [HideInInspector] public GameObject HoveredRoot;
    [SerializeField] private Sprite _rootOffSprite;
    [SerializeField] private Sprite _rootOnSprite;
    void Update()
    {
        AudioManager.instance.setBGMMode(ClosestEnemyPositionY);

        if (Input.GetMouseButtonDown(0))
        {
            if (HoveredRoot != null)
            {
                SwitchRootState();
            }
        }
    }


    public void PlayerTakeDamage()
    {
        AudioManager.instance.PlaySFX("tree_damaged");

        CurrentHP--;
        GameUI.instance.UpdateHP(CurrentHP);

        if (CurrentHP <= 0)
        {
            InvokeLoseGame();
        }
    }

    private void SwitchRootState()
    {
        Root root = HoveredRoot.GetComponent<Root>();
        bool shouldActivate = root.IsActive ? false : true;

        if (shouldActivate)
        {
            if (CurrentRootResource <= 0) return;

            CurrentRootResource--;
            root.SwitchToActive();
        }
        else
        {
            CurrentRootResource++;
            root.SwitchToInactive();
        }

    }


    public void InvokeStartGameLoop()
    {
        enabled = true;
        LoopIsActive = true;

        CurrentHP = 3;
        CurrentRootResource = 3;

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

}