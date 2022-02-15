using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRDebugMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public static VRDebugMessage Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Print(string message)
    {
        _textMesh.text = message;
    }
}
