using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VelocityInteractable : XRGrabInteractable
{
    private ControllerVelocity _controllerVelocity = null;
    private MeshRenderer _renderer;

    [Header("Shake Settings")]
    [SerializeField]
    private float _targetValue = 200;
    private float _shakeTotal = 0;
    [SerializeField]
    private float _minimumShakeValue = 1f;
    

    override
    protected void Awake()
    {
        base.Awake();
        _renderer = GetComponent<MeshRenderer>();
    }

    override
    protected void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _controllerVelocity = args.interactorObject.transform.GetComponent<ControllerVelocity>();
    }

    override
    protected void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _controllerVelocity = null;
    }

    override
    public void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected)
        {
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                UpdateVelocityColor();
            }
        }
    }

    private void UpdateVelocityColor()
    {
        Vector3 velocity = _controllerVelocity ? _controllerVelocity.Velocity : Vector3.zero;
        float magnitude = velocity.magnitude;
        if (magnitude > _minimumShakeValue)
        {
            _shakeTotal += velocity.magnitude;
        }

        float percentage = _shakeTotal / _targetValue;
        Color progressColor = Color.Lerp(Color.red, Color.green, percentage);        
        _renderer.material.color = progressColor;        

        VRDebugMessage.Instance.Print($"Total shake value is {_shakeTotal} and current magnitude is {velocity.magnitude} and minimum is {_minimumShakeValue}");
    }
}
