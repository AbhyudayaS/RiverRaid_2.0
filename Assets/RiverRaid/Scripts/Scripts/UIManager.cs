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
    private ScoreState _scoreState;
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    private TextMeshProUGUI _playerDangerState;
    [SerializeField]
    private TextMeshProUGUI _inGameScoreText;
    [SerializeField]
    private GameObject _gameRestartUI;
    [SerializeField]
    private GameObject _gameEndUI;
    [SerializeField]
    private TextMeshProUGUI _endGameScoreText;

    private void OnEnable()
    {
        _playerState.Observers += UIPlayerState;
        _scoreState.Observers += SetGameScoreText;
        gameState.Observers += ChangeUIGameState;
    }

    private void OnDisable()
    {
        _playerState.Observers -= UIPlayerState;
        _scoreState.Observers -= SetGameScoreText;
        gameState.Observers -= ChangeUIGameState;

    }

    private void ChangeUIGameState(States obj)
    {
        if(obj== States.FINISHED)
        {
            _gameEndUI.SetActive(true);
            _endGameScoreText.text = _scoreState.Value.ToString();
        }
    }

    private void SetGameScoreText(int sc)
    {
        _inGameScoreText.text = sc.ToString();
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
