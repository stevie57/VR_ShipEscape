using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class AutoHandVelocityInteractable : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField]
    private float _targetValue = 200;
    private float _shakeTotal = 0;
    [SerializeField]
    private float _minimumShakeValue = 5f;
    [SerializeField]
    private MeshRenderer _renderer;
    private ControllerVelocity _controllerVelocity;
    private bool _isHeld;

    public void VelocityTracking(Hand hand, Grabbable grabbable)
    {
        if (hand.transform.GetComponent<ControllerVelocity>())
        {
            _controllerVelocity = hand.transform.GetComponent<ControllerVelocity>();
            _isHeld = true;
            StartCoroutine(VelocityTrackingRoutine());
        }
    }

    private IEnumerator VelocityTrackingRoutine()
    {
        while (_isHeld)
        {
            Vector3 velocity = _controllerVelocity.Velocity;
            float magnitude = velocity.magnitude;
            if (magnitude > _minimumShakeValue)
            {
                _shakeTotal += velocity.magnitude;
            }

            float percentage = _shakeTotal / _targetValue;
            Color progressColor = Color.Lerp(Color.red, Color.green, percentage);
            _renderer.material.color = progressColor;

            VRDebugMessage.Instance.Print
                (
                    $"Total shake percentage is {percentage} " +
                    $"and current magnitude is {velocity.magnitude} " +
                    $"and minimum is {_minimumShakeValue}"
                );
            yield return null;
        }
    }

    public void StopTracking(Hand hand, Grabbable grabbable)
    {
        _isHeld = false;
        StopAllCoroutines();
    }
}
