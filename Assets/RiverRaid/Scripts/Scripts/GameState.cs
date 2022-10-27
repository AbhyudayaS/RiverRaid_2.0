using UnityEngine;

public enum States { PLAY,PAUSE,QUIT}
[CreateAssetMenu(menuName = "Scriptable/States/GameState")]
public class GameState : TState<States>
{
    private void OnValidate()
    {
        Value = States.PLAY;
    }
}

