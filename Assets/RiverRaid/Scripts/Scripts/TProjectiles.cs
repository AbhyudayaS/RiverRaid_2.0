using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TProjectiles : ScriptableObject
{
    [SerializeField]
    public GameObject _projectilePrefab;
    [SerializeField]
    protected float fireInterval = 2f;
    [SerializeField]
    private float moveSpeed = 400f;
  
    public float MoveSpeed { get => moveSpeed; }
    public float FireInterval { get => fireInterval; private  set => fireInterval = value; }
}
