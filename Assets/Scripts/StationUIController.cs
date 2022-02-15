using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _battleUI;

    [SerializeField]
    private GameObject _puzzleUI;

    public void ActivateBattleUI()
    {
        _battleUI.SetActive(true);
        _puzzleUI.SetActive(false);
    }

    public void ActivatePuzzleUI()
    {
        _battleUI.SetActive(false);
        _puzzleUI.SetActive(true);
    }
}
