using System.Collections;
using System.Collections.Generic;
using Utils.GenericSingletons;
using UnityEngine;

public class GameVariables : MonoBehaviourSingleton<GameVariables>
{
    [field: SerializeField] public int RootsResourceMax { get; private set; }
}
