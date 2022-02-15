using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/ShipStatSO")]
public class ShipStatsSO : ScriptableObject
{
    [SerializeField]
    private float _currentEnergy;
    [SerializeField]
    private float _maxEnergy;
    public float MaxEnergy => _maxEnergy;

    public event Action<float> OnValueDecreased;
    public event Action<float> OnValueIncreased;
    public event Action FullEnergy;

    private void OnEnable()
    {
        _currentEnergy = _maxEnergy;
    }

    public void DecreaseEnergy(float energyCost)
    {
        _currentEnergy -= energyCost;
        if (_currentEnergy < 0) _currentEnergy = 0;
        OnValueDecreased.Invoke(_currentEnergy);
    }

    public void IncreaseEnergy(float energyIncrease)
    {
        _currentEnergy += energyIncrease;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, 100);
        OnValueIncreased.Invoke(_currentEnergy);

        if (_currentEnergy == 100) FullEnergy.Invoke();
    }
}
// ship energy is decreased when ship is firing the ship gun
// hull closes when energy hits zero
// hull repoens when 
