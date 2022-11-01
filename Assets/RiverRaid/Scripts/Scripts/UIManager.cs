using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerState _playerState;
    [SerializeField]
    private TextMeshProUGUI _playerDangerState;
    [SerializeField]
    private GameObject _gameRestartUI;

    private void OnEnable()
    {
        _playerState.Observers += UIPlayerState;
    }

    private void OnDisable()
    {
        _playerState.Observers -= UIPlayerState;
    }

    private void UIPlayerState(PState obj)
    {
        if (obj == PState.DANGER)
        {
            _playerDangerState.gameObject.SetActive(true);
        }
        else if (obj == PState.SAFE || obj == PState.DEAD)
        {
            _playerDangerState.gameObject.SetActive(false);
        }

        if(obj == PState.DEAD)
        {
            StartCoroutine(DisplayRestartScene());
           
        }
    }

    IEnumerator DisplayRestartScene()
    {
        yield return new WaitForSeconds(2f);
        _gameRestartUI.gameObject.SetActive(true);
    }
}
