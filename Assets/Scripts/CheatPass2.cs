using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPass2 : MonoBehaviour
{
    public LapsAsset laps;

    void OnTriggerEnter(Collider other)
    {
        // the user has passed the first checkpoint
        laps.pass2 = true;

    }
}
