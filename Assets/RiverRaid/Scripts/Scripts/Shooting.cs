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
    [SerializeField]
    private AudioSource _shootingSound;
    private InputListener _inputListener;
    private bool canFire = true;

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
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Vector3 _destination;
        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;

        }
        else
        {
            _destination = ray.GetPoint(10000f);
        }

        //var projectileObj =  Instantiate(_projectile._projectilePrefab, _firingPos.position, transform.rotation);
        var bullet = Instantiate(_projectile._projectilePrefab, _firingPos.transform.position, _firingPos.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (_destination - _firingPos.transform.position).normalized * _projectile.MoveSpeed;


        _shootingSound.Play();       
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(_projectile.FireInterval);
        canFire = true;
    }
}
