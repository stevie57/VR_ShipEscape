using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject _particleExplosion;
    [SerializeField]
    private MeshRenderer _renderer;

    public void Damaged()
    {
        GameObject explosion=  Instantiate(_particleExplosion);
        explosion.transform.position = transform.position;
        //Destroy(this.gameObject);
        _renderer.enabled = false;
        Invoke("Return", 2f);
    }

    public void Return()
    {
        _renderer.enabled = true;
    }
}
