using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject _testingProjectile;
    [SerializeField]
    private TProjectiles _projectile;
    [SerializeField]
    private Transform _firingPos;
    [SerializeField]
    private Camera _camera;
    private InputListener _inputListener;
    private bool canFire = true;
    private Vector3 _destination;

    private void Start()
    {
        _inputListener = GetComponent<InputListener>();
        
    }

    private void Update()
    {
        if (_inputListener.interact)
        {
            OnFireButtonClicked();
        }       
    }

    public void OnFireButtonClicked()
    {
        if (canFire)
        {
            //fire rocket
            FireProjectile();
            canFire = false;
            StartCoroutine(ReloadDelay());
        }
    }

    private void FireProjectile()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;
            Debug.Log(hit.transform.gameObject.name);
        }
        else
        {
            _destination = ray.GetPoint(10000f);
        }

        var projectileObj =  Instantiate(_projectile._projectilePrefab, _firingPos.position, Quaternion.identity);       
        projectileObj.GetComponent<Rigidbody>().velocity = (_destination - _firingPos.position).normalized * _projectile.MoveSpeed;
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(_projectile.FireInterval);
        canFire = true;
    }
}
