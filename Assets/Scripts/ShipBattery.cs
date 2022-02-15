using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipBattery : MonoBehaviour
{
    [SerializeField]
    private ShipStatsSO _shipStats;
    [SerializeField]
    private Image _energyBar;

    private void OnEnable()
    {
        _shipStats.OnValueDecreased += UpdateUI;
        _shipStats.OnValueIncreased += UpdateUI;
    }

    private void OnDisable()
    {
        _shipStats.OnValueDecreased -= UpdateUI;
        _shipStats.OnValueIncreased -= UpdateUI;
    }

    private void UpdateUI(float shipEnergy) => _energyBar.fillAmount = (shipEnergy / _shipStats.MaxEnergy);

    private void ResetUI() => _energyBar.fillAmount = 1f;
}
