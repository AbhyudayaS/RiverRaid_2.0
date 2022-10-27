using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
#endif

public class InputListener : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;  
    public bool shoot;
    public bool interact;
    private bool paused = false;
    [SerializeField]
    private GameState _gameState;
    [SerializeField]
    private PlayerInput playerInput;

    private void Start()
    {
        InputSystem.DisableDevice(Mouse.current);
    }

    // Player input unity events
    public void OnMove(CallbackContext ctx)
    {
        MoveInput(ctx.ReadValue<Vector2>());
    }

    public void OnInteract(CallbackContext ctx)
    {
        InteractInput(ctx.ReadValueAsButton());
    }

    public void OnSprint(CallbackContext ctx)
    {
        SprintInput(ctx.ReadValueAsButton());
    }

    public void OnNavigate(CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValueAsButton());
        //SprintInput(ctx.ReadValueAsButton());
    }

    public void OnPaused(CallbackContext ctx)
    {
        if (_gameState.Value == States.PLAY)
        {
            _gameState.Value = States.PAUSE;            
        }
        else
        {
            _gameState.Value = States.PLAY;
        }
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void InteractInput(bool newSprintState)
    {
        interact = newSprintState;
    }
    private void SprintInput(bool newShootState)
    {
        shoot = newShootState;
    }

}
