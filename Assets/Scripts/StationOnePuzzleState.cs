using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StationOnePuzzleState : StationOneState
{
    private StationOneController _station;
    [SerializeField]
    private StationUIController _stationUI;
    [SerializeField]
    private Image[] _buttonImages = new Image[4];
    [SerializeField]
    private int[] _solution1 = new int[4];
    private bool _displaySolution;

    [SerializeField]
    private Queue<int> _playerInputs = new Queue<int>();

    public override void StateTransitionIn(StationOneController StationOne)
    {
        _station = StationOne;
        _stationUI.ActivatePuzzleUI();
        _displaySolution = true;
        foreach (Image image in _buttonImages)
            image.color = Color.red;
        DisplayPuzzleSolution();        
    }

    private void DisplayPuzzleSolution()
    {
        StartCoroutine(PuzzleSolutionRoutine());
    }

    private IEnumerator PuzzleSolutionRoutine()
    {
        while (_displaySolution)
        {
            for (int i = 0; i < _solution1.Length; i++)
            {
                _buttonImages[_solution1[i]].color = Color.green;
                yield return new WaitForSeconds(0.25f);
                _buttonImages[_solution1[i]].color = Color.red;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    [ContextMenu("Button North")]
    public override void ButtonNorth()
    {
        _playerInputs.Enqueue(0);
    }

    [ContextMenu("Button East")]
    public override void ButtonEast()
    {
        _playerInputs.Enqueue(1);
    }

    [ContextMenu("Button South")]
    public override void ButtonSouth()
    {
        _playerInputs.Enqueue(2);
    }

    [ContextMenu("Button West")]
    public override void ButtonWest()
    {
        _playerInputs.Enqueue(3);
    }

    public override void SliderDown()
    {
       
    }

    public override void SliderUp()
    {
        
    }

    public override void ButtonConfirm()
    {
        CheckPlayerSolution();
    }

    [ContextMenu("Check Player Solution")]
    private void CheckPlayerSolution()
    {
        if(_playerInputs.Count != _solution1.Length)
        {
            print($"player input count is incorrect !");
            ClearPlayerInput();
            return;
        }

        int[] playerInputs = _playerInputs.ToArray();

        for(int i = 0; i < _solution1.Length; i++)
        {
            if(playerInputs[i] != _solution1[i])
            {
                print($"playerInput error at {i}");
                ClearPlayerInput();
                return;
            }
        }

        _station.ShipStats.IncreaseEnergy(35f);
    }

    private void ClearPlayerInput()
    {
        _playerInputs.Clear();
        print($"cleared inputs and it is now {_playerInputs.Count}");
    }

    public override void StateTransitionOut()
    {
        StopAllCoroutines();
    }
}