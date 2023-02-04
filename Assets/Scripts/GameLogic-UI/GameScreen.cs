using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveIndicator;
    [SerializeField] private TextMeshProUGUI _rootIndicator;



    private void UpdateRootsIndicaotr(int roots)
    {
        _rootIndicator.text = roots.ToString() + " / " + GameVariables.instance.RootsResourceMax + " Roots";
    }


    private void UpdateWaveIndicator(int wave)
    {
        _waveIndicator.text = "Wave " + wave.ToString();
    }
}
