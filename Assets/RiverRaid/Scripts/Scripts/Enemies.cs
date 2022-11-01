using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private ScoreState _scoreState;
    [SerializeField]
    private int _points;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private ParticleSystem _ps;
    [SerializeField]
    private AudioSource _audioS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ProjectileController>() !=null || other.gameObject.GetComponent<BaseAirplaneController>() != null)
        {
            _scoreState.Value += _points;
            GetComponent<Collider>().enabled = false;
            _audioS.Play();
            _ps.Play();
            _model.SetActive(false);
            Destroy(gameObject,3f);
        }
    }

   
}
