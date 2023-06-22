using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsTimer : MonoBehaviour
{
    public LapsAsset laps;
    public Text TimeText;

    private float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        laps.roundTime = 0f;
        laps.totalTime = 0f;
        laps.isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // only update if isPlaying is true
        if (laps.isPlaying)
        {
            laps.roundTime += Time.deltaTime;
            laps.totalTime += Time.deltaTime;
        }

        // Update totalTime
        TimeText.text = "   Lap Time: " + laps.roundTime.ToString("F2") + "\n" + "Total Time: " + laps.totalTime.ToString("F2");
    
        
    }
}
