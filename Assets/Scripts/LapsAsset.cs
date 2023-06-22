using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LapsAsset : ScriptableObject
{
    public int nLapsTotal = 0;

    // current lap number
    public int currentLap = 0;

    // used for keeping track of time
    public float roundTime = 0f;
    public float totalTime = 0f;

    // keep track of if playing
    public bool isPlaying = false;

    // keep track of cheating
    public bool pass1 = false;
    public bool pass2 = false;

    // keep track if on final lap
    public bool isFinalLap = false;

    // trigger to catch in gamemanager for if user should be done
    public bool finalComplete = false;

    // list to keep track of rounds for after-game review
    public List<float> roundList;
}