using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PState { SAFE, DANGER, DEAD }
[CreateAssetMenu(menuName = "Scriptable/States/PlayerDangerState")]
public class PlayerState : TState<PState>
{
    private void OnValidate()
    {
        Value = PState.SAFE;
    }
}

