using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField]
    private BoundsState _outBoundState;
    [SerializeField]
    private PlayerState _playerState;
    [SerializeField]
    private float _timer = 0f;
    [SerializeField]
    private float _timerLimit = 0f;

    private void OnTriggerEnter(Collider other)
    {
        var col = other.gameObject;
        if (col.GetComponent<OutOfBounds>() != null)
        {
            _outBoundState.Value = true;
            _timer += Time.deltaTime;

        }

        if (col.CompareTag("Environment")) 
        {
            _playerState.Value = PState.DEAD;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        var col = other.gameObject;
        if (col.GetComponent<OutOfBounds>() != null &&_playerState.Value != PState.DEAD)
        {
            _outBoundState.Value = true;
            _timer += Time.deltaTime;
            _playerState.Value = PState.DANGER;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var col = other.gameObject;
        if (col.GetComponent<OutOfBounds>() != null && _playerState.Value != PState.DEAD)
        {
            _outBoundState.Value = false;
            _timer = 0;
            _playerState.Value = PState.SAFE;
        }
    }

    private void Update()
    {
        if (_timer > _timerLimit)
        {            
            if(_playerState.Value == PState.DANGER)
            {
                _playerState.Value = PState.DEAD;
            }
        }
    }
}
