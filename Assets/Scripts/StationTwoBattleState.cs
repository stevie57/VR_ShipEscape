using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationTwoBattleState : StationTwoState
{
    [Header("Gun Variables")]
    [SerializeField]
    private float _cursorSensitvity = 0.5f;
    [SerializeField]
    private float _gunRange = 100f;
    [SerializeField]
    private float _attackEnergyCost;


    private StationTwoController _station;
    [Header("ShipGun Settings")]
    private bool _isShipGunActive;
    [SerializeField]
    private bool _isInverted;
    [SerializeField]
    Image _targetCursor;

    [Header("Transform Settings")]
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private Transform _shipgun;
    [SerializeField]
    private Transform _shipgunBulletSpawn;
    [SerializeField]
    private ParticleSystem _shipgunPS;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private LineRenderer _targetLineRenderer;

    [SerializeField]
    private ShipStatsSO _shipStats;
    [SerializeField]
    private StationUIController _stationUIController;


    public override void StateTransitionIn(StationTwoController Station)
    {
        _station = Station;
        _isShipGunActive = false;
        _stationUIController.ActivateBattleUI();
        AlignCannon();
    }

    public override void ActivateJoyStick()
    {
        GrabbedJoyStick();
    }
    public void GrabbedJoyStick()
    {
        _isShipGunActive = true;
        StartCoroutine(JoyStickRoutine());
    }
    private IEnumerator JoyStickRoutine()
    {
        while (_isShipGunActive)
        {
            SetTargetCursorPosition();
            AlignCannon();
            CannonAimLine();
            yield return null;
        }
    }

    private void SetTargetCursorPosition()
    {
        Vector2 positionValue = _station.GetJoyStickValue();
        positionValue *= _cursorSensitvity;
        positionValue.y = _isInverted ? positionValue.y * -1 : positionValue.y;
        Vector2 currentPosition = _targetCursor.rectTransform.localPosition;
        Vector2 newPosition = currentPosition + positionValue;
        _targetCursor.rectTransform.localPosition = newPosition;
    }

    private void AlignCannon()
    {
        Vector3 direction = _targetCursor.transform.position - _playerHead.position;
        _target.position = _playerHead.position + direction * _gunRange;
        _shipgun.transform.LookAt(_target.position);
    }
    private void CannonAimLine()
    {
        Vector3 startPos = _shipgunBulletSpawn.position;
        Vector3 endPos = _target.position; //_playerHead.position + (direction * _gunRange);
        Vector3[] positions = new Vector3[2] { startPos, endPos };
        _targetLineRenderer.SetPositions(positions);
    }
    public override void DeactivateJoyStick()
    {
        ReleaseJoystick();
    }

    public void ReleaseJoystick()
    {
        _isShipGunActive = false;
        StopAllCoroutines();
    }

    public override void ButtonLeft()
    {
        // change weapon
    }

    public override void ButtonRight()
    {
        FireShipGun();
    }

    [ContextMenu("Fire Ship Gun")]
    private void FireShipGun()
    {
        Vector3 direction = _targetCursor.transform.position - _playerHead.position;
        _shipgunPS.Play();

        if (Physics.SphereCast(_playerHead.position, 1f, direction.normalized * _gunRange, out var hitInfo))
        {
            if (hitInfo.transform.GetComponentInParent<IDamageable>() != null)
            {
                hitInfo.transform.GetComponentInParent<IDamageable>().Damaged();
            }
        }

        _shipStats.DecreaseEnergy(_attackEnergyCost);
    }

    public override void StateTransitionOut()
    {
        StopAllCoroutines();
    }
}
