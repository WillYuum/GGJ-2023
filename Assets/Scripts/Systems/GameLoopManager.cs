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

        float mostTopEnemyYPosition = GetMostTopEnemyYPosition();
        // print("mostTopEnemyYPosition: " + mostTopEnemyYPosition);
        AudioManager.instance.setBGMMode(mostTopEnemyYPosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (HoveredRoot != null)
            {
                SwitchRootState();
            }
        }
    }

    private float GetMostTopEnemyYPosition()
    {
        float mostTopEnemyYPosition = -2.0f;

        for (int i = 0; i < ManuelMovementScript.instance.moveableObjects.Length; i++)
        {
            if (ManuelMovementScript.instance.moveableObjects[i].transform.position.y > mostTopEnemyYPosition)
            {
                mostTopEnemyYPosition = ManuelMovementScript.instance.moveableObjects[i].transform.position.y;
            }
        }

        return mostTopEnemyYPosition;
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

        GameUI.instance.UpdateRootResource(CurrentRootResource);
    }


    // [SerializeField] private ManuelMovementScript _manuelMovementScript;
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
        Invoke(nameof(StartEnemyMoving), 4.0f);
    }

    private void StartEnemyMoving()
    {
        var manuelMovementScript = FindObjectOfType<ManuelMovementScript>();
        manuelMovementScript.MoveObjectsOnPoints();
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

        GameUI.instance.DisableAllScreens();
        GameManager.instance.SwitchToGameState(GameState.LoseScreen, () =>
        {
            //Open LoseScreenUI
            GameUI.instance.SwitchState(GameState.LoseScreen);
        });
    }

}