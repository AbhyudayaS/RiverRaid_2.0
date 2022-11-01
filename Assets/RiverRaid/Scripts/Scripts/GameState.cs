using UnityEngine;

public enum States { PLAY,PAUSE,QUIT,FINISHED}
[CreateAssetMenu(menuName = "Scriptable/States/GameState")]
public class GameState : TState<States>
{
    private void OnValidate()
    {
        Value = States.PLAY;
    }
}

