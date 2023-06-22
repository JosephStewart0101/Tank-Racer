using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPass1 : MonoBehaviour
{
    public LapsAsset laps;

    void OnTriggerEnter(Collider other)
    {
        // the user has passed the first checkpoint
        laps.pass1 = true;



        // Debug.LogError("passed 1");
    }
}
