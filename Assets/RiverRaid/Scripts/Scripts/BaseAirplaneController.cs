using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiverRaid
{
    public class BaseAirplaneController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        protected float yThrow = 0f;
        [SerializeField]
        protected float xThrow = 0f;
        [SerializeField]
        protected float controlSpeed = 0f;
        [SerializeField]
        protected float throttle = 0f;       

        private InputListener _inputListener;
        private Rigidbody _rb;
        private Vector3 _rotation;
        #endregion

        private void Start()
        {
            _inputListener = GetComponent<InputListener>();
            _rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveForward()
        {            
            transform.Rotate(_rotation);
            _rb.velocity = transform.forward * throttle;
        }

        private void HandleInput()
        {
            xThrow = _inputListener.move.x;
            yThrow = _inputListener.move.y;

            //transform.position += new Vector3(0f, yThrow, 0) * throttle * Time.fixedDeltaTime;

            if (xThrow != 0)
            {
                _rotation = new Vector3(yThrow, xThrow, 0f ) *  throttle * controlSpeed * Time.deltaTime;
              
            }
            else
            {
                _rotation = Vector3.zero;
            }

        }
    }
}
