using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseAirplaneController : MonoBehaviour
{
    [SerializeField]
    private PlayerState _playerState;
    [SerializeField]
    private GameState _gameState;
    #region Variables       
    protected float yThrow = 0f;
    protected float xThrow = 0f;
    [SerializeField]
    protected float _controlSpeed = 0f;
    [SerializeField]
    protected float _throttle = 0f;
    [SerializeField]
    private float _initThrottle;
    [SerializeField]
    protected float _boostSpeed = 0f;
    [SerializeField]
    private float _restThrottleTime;
    [SerializeField]
    private float _controlRollFactor;
    [SerializeField]
    private float _rotaionSpeed;
    [SerializeField]
    private AudioSource _boomAudio;
    [SerializeField]
    private ParticleSystem _boomPS;
    [SerializeField]
    private GameObject _planeModel;
    [SerializeField]
    private ScoreState _scoreState;
    [SerializeField]
    private int _ringPoints;

    private InputListener _inputListener;
    private Rigidbody _rb;
    private Vector3 _rotation;
    private bool _playerDead;
    private bool _startGame = false;
    #endregion

    private void OnEnable()
    {
        _playerState.Observers += CheckPlayerState;
        _gameState.Observers += ChangeGameState;
    }

    private void ChangeGameState(States obj)
    {
       if(obj == States.PLAY)
        {
            _startGame = true;
        }
    }

    private void OnDisable()
    {
        _playerState.Observers -= CheckPlayerState;
        _gameState.Observers -= ChangeGameState;

    }

    private void CheckPlayerState(PState obj)
    {
        if (obj == PState.DEAD)
        {
            _playerDead = true;
            PlaneDestroyed();
        }
    }

    private void Start()
    {
        _inputListener = GetComponent<InputListener>();
        _rb = GetComponent<Rigidbody>();
     
    }

    private void Update()
    {
        if (!_startGame) return;
        if (_playerDead) return;
        HandleInput();
        ResetZRot();
        ProcessRotaion();
    }

    private void FixedUpdate()
    {
        if (!_startGame) return;
        if (_playerDead) return;
        MoveForward();
    }

    private void ResetZRot()
    {
        if (xThrow == 0)
        {

            var targetRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            //transform.rotation = Quaternion.RotateTowards(currentRot, targetRot, 0.10f );
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotaionSpeed * Time.deltaTime);
        }
    }

    private void ProcessRotaion()
    {
        if (xThrow == 0 && yThrow == 0) return;
        float roll = xThrow * _controlRollFactor;
        var desireRot = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll));
        //transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll);
        transform.localRotation = Quaternion.Slerp(transform.rotation, desireRot, _rotaionSpeed * Time.deltaTime);

    }

    private void MoveForward()
    {
        _rb.velocity = transform.forward * _throttle;
    }

    private void HandleInput()
    {
        xThrow = _inputListener.move.x;
        yThrow = _inputListener.move.y;
        _rotation = new Vector3(yThrow, xThrow, 0f) * _controlSpeed * Time.deltaTime;
        transform.Rotate(_rotation);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rings>() != null)
        {
            _scoreState.Value += _ringPoints;
            _throttle = _boostSpeed;
            StartCoroutine(ResetThrottle());
        }
        if (other.gameObject.GetComponent<Enemies>() != null)
        {
            _playerState.Value = PState.DEAD;
        }
        if (other.gameObject.CompareTag("GameEndCollider"))
        {
            _playerDead = true;
            _rb.velocity = Vector3.zero;
            _gameState.Value = States.FINISHED;
        }
      
    }

    IEnumerator ResetThrottle()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _restThrottleTime)
        {
            var boost= Mathf.Lerp(_boostSpeed, _initThrottle, (elapsedTime / _restThrottleTime));
            _throttle = boost;
            elapsedTime += Time.deltaTime;
         
            yield return null;
        }       
        yield return null;
    }

    public void PlaneDestroyed()
    {
        _rb.velocity = Vector3.zero;
       _throttle = 0;
        _boomAudio.Play();
        _boomPS.Play();
        _planeModel.SetActive(false);       
    }
}

