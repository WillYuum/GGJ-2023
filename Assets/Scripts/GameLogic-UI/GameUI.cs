using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;

public class GameUI : MonoBehaviourSingleton<GameUI>
{

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private LoseScreen _loseScreen;

    public void SwitchState(GameState gameState)
    {
        OnGameStateChange(gameState);
    }

    public void DisableAllScreens()
    {
        _mainMenu.gameObject.SetActive(false);
        _gameScreen.gameObject.SetActive(false);
        _loseScreen.gameObject.SetActive(false);
    }


    private void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                _mainMenu.gameObject.SetActive(true);
                _gameScreen.gameObject.SetActive(false);
                _loseScreen.gameObject.SetActive(false);
                break;
            case GameState.MainGame:
                _mainMenu.gameObject.SetActive(false);
                _gameScreen.gameObject.SetActive(true);
                _loseScreen.gameObject.SetActive(false);
                break;
            case GameState.LoseScreen:
                _mainMenu.gameObject.SetActive(false);
                _gameScreen.gameObject.SetActive(false);
                _loseScreen.gameObject.SetActive(true);
                break;
        }
    }


    [SerializeField] private HPIndicator _hpIndicator;
    public void UpdateHP(int amount)
    {
        _hpIndicator.UpdateHealth(amount);
    }


    public void UpdateRootResource(int amount)
    {
        _gameScreen.UpdateRootsIndicaotr(amount);
    }

}
