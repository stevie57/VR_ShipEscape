using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallLever : MonoBehaviour
{
    [SerializeField]
    private Image _leverUI;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        Deactivate();
    }

    [ContextMenu("Activate Lever")]
    public void Activate()
    {
        IsActive = true;
        _leverUI.color = Color.green;
    } 

    public void Deactivate()
    {
        IsActive = false;
        _leverUI.color = Color.red;
    }
}
