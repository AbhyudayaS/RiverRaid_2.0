using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiverRaid
{
    public class BaseAirplaneController : MonoBehaviour
    {
        #region Variables       
        protected float yThrow = 0f;       
        protected float xThrow = 0f;
        [SerializeField]
        protected float controlSpeed = 0f;
        [SerializeField]
        protected float throttle = 0f;     
        [SerializeField]
        private float controlRollFactor;
        [SerializeField]
        private float _rotaionSpeed;

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
            ResetZRot();
            ProcessRotaion();
        }

        private void FixedUpdate()
        {
            MoveForward();          
        }

        private void ResetZRot()
        {            
            if (xThrow == 0 )           {
                
                var targetRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                //transform.rotation = Quaternion.RotateTowards(currentRot, targetRot, 0.10f );
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotaionSpeed * Time.deltaTime);
            }
        }

        private void ProcessRotaion()
        {
            if (xThrow == 0 && yThrow == 0) return;
            float roll = xThrow * controlRollFactor;
            var desireRot = Quaternion.Euler( new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll));
            //transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll);
            transform.localRotation = Quaternion.Slerp(transform.rotation, desireRot, _rotaionSpeed * Time.deltaTime);

        }

        private void MoveForward()
        {
            _rb.velocity = transform.forward * throttle;
        }

        private void HandleInput()
        {
            xThrow = _inputListener.move.x;
            yThrow = _inputListener.move.y;           
            _rotation = new Vector3(yThrow, xThrow, 0f) * controlSpeed * Time.deltaTime;
            transform.Rotate(_rotation);

        }
    }
}
