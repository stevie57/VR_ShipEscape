using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHullController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private ShipStatsSO _shipStats;

    private void OnEnable()
    {
        _shipStats.OnValueDecreased += CloseHull;
        _shipStats.OnValueIncreased += OpenHull;
    }

    private void OnDisable()
    {
        _shipStats.OnValueDecreased -= CloseHull;
        _shipStats.OnValueIncreased += OpenHull;
    }

    private void CloseHull(float shipEnergy)
    {
        if(shipEnergy <= 0) _animator.SetBool("isHullClosed", true);
    }

    private void OpenHull(float shipEnergy)
    {
        if(shipEnergy >= 100 ) _animator.SetBool("isHullClosed", false);
    }
}
