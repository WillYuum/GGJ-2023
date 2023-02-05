using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPoints : MonoBehaviour
{
    [SerializeField] private ConnectedPoint[] pointsLayer4;
    [SerializeField] private ConnectedPoint[] pointsLayer3;
    [SerializeField] private ConnectedPoint[] pointsLayer2;
    [SerializeField] private ConnectedPoint[] pointsLayer1;
    [SerializeField] private ConnectedPoint[] pointsLayer0;



    public void Init()
    {

    }
}



[System.Serializable]
class ConnectedPoint
{
    [SerializeField] private GameObject[] ConnectedTo;
}