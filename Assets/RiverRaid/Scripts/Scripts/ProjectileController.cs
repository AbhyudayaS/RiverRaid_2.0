using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
}
