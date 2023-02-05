using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIndicator : MonoBehaviour
{
    [System.Serializable]
    class HealthBar
    {
        [SerializeField] private GameObject _foreGround;
        [SerializeField] private GameObject _backGround;

        public void Enable()
        {
            _foreGround.SetActive(true);
        }

        public void Disable()
        {
            _foreGround.SetActive(false);
        }
    }


    [SerializeField] private HealthBar _healthBar_One;
    [SerializeField] private HealthBar _healthBar_Two;
    [SerializeField] private HealthBar _healthBar_Three;



    public void UpdateHealth(int health)
    {
        switch (health)
        {
            case 3:
                _healthBar_One.Enable();
                _healthBar_Two.Enable();
                _healthBar_Three.Enable();
                break;
            case 2:
                _healthBar_One.Enable();
                _healthBar_Two.Enable();
                _healthBar_Three.Disable();
                break;
            case 1:
                _healthBar_One.Enable();
                _healthBar_Two.Disable();
                _healthBar_Three.Disable();
                break;
            case 0:
                _healthBar_One.Disable();
                _healthBar_Two.Disable();
                _healthBar_Three.Disable();
                break;
        }
    }
}
