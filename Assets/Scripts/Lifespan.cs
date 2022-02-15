using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    void Start()
    {
        AutoRemoval();
    }
    private void AutoRemoval() => StartCoroutine(AutoRemovalRoutine()); 

    private IEnumerator AutoRemovalRoutine()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(this.gameObject); 
    }
}
