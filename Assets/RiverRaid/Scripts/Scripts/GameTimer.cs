using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;
    [SerializeField]
    private GameState _gameState;
    [SerializeField]
    private TextMeshProUGUI _timerText;


    [Header("Timer Settings")]
    [SerializeField]
    private float _currentTime;

    [Header("Format Setting")]
    [SerializeField]
    private bool _hasFormat;
    [SerializeField]
    private TimerFormats _format;
    [SerializeField]
    private Dictionary<TimerFormats, string> _timeFormats = new Dictionary<TimerFormats, string>();

    private bool startTimer;

    private void Start()
    {
        _timeFormats.Add(TimerFormats.WHOLE, "0");
        _timeFormats.Add(TimerFormats.TENTHDECIMAL, "0.0");
        _timeFormats.Add(TimerFormats.HUNDRETHDECIMAL, "0.00");
    }

    private void OnEnable()
    {
        _gameState.Observers += ChangeTimerState; 
    }

    private void OnDisable()
    {
        _gameState.Observers -= ChangeTimerState;
    }

    private void ChangeTimerState(States obj)
    {
        if(obj == States.PLAY)
        {
            startTimer = true;
        } else if(obj == States.QUIT)
        {
            startTimer = false;
        }

    }

    private void Update()
    {
        if (!startTimer) return;
        _currentTime += Time.deltaTime;
        SetTimerText();
    }

    private void SetTimerText()
    {
        _timerText.text = _hasFormat ? _currentTime.ToString(_timeFormats[_format]) : _currentTime.ToString();
        _timer.Value = _timerText.text;
    }
}

public enum TimerFormats { WHOLE, TENTHDECIMAL, HUNDRETHDECIMAL }