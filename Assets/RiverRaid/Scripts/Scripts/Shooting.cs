using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
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
    Vector3 _destination;

    private void Start()
    {
        _inputListener = GetComponent<InputListener>();

    }    

    private void LateUpdate()
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

        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;

        }
        else
        {
            _destination = ray.GetPoint(10f);
        }    
        var bullet = Instantiate(_projectile._projectilePrefab, _firingPos.position, _firingPos.rotation);

        bullet.GetComponent<Rigidbody>().velocity = (_destination - _firingPos.position).normalized * _projectile.MoveSpeed + gameObject.GetComponent<Rigidbody>().velocity;
        _shootingSound.Play();
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(_projectile.FireInterval);
        canFire = true;
    }
}
