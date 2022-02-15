using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StationThreePuzzleState : StationThreeState
{
    [SerializeField]
    private StationThreeController _station;
    [SerializeField]
    StationUIController _stationUI;

    [SerializeField]
    private WallLever[] _leverArray = new WallLever[6];
    [SerializeField]
    private Image[] _leverSolutionUI = new Image[6];
    [SerializeField]
    private bool[] _leverSolution;
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public override void StateTransitionIn(StationThreeController Station)
    {
        _station = Station;
        _stationUI.ActivatePuzzleUI();
        DisplaySolution();
    }

    private void DisplaySolution()
    {
        _leverSolution = new bool[6] { false, false, true, false, false, false };
        for (int i = 0; i < _leverSolution.Length; i++)
        {
            _leverSolutionUI[i].color = _leverSolution[i] ? Color.green : Color.red;
        }
    }

    public override void ButtonEast()
    {

    }

    public override void ButtonNorth()
    {

    }

    public override void ButtonSouth()
    {

    }

    public override void ButtonWest()
    {

    }

    public override void ButtonConfirm()
    {
        CheckLevers();
    }

    [ContextMenu("Check Levers")]
    public void CheckLevers()
    {
        bool result = true;
        for (int i = 0; i < _leverSolution.Length; i++)
        {
            if (_leverArray[i].IsActive != _leverSolution[i])
            {
                result = false;
                _textMesh.text = $"Check lever solution is {result} and failed on {i}";
                return;
            }
        }

        _station.ShipStats.IncreaseEnergy(35f);
    }
    public override void StateTransitionOut()
    {

    }
}