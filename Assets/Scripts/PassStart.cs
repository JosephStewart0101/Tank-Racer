using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassStart : MonoBehaviour
{
    public LapsAsset laps;

    public Text lapCounterText;

    void OnTriggerEnter(Collider other)
    {
        // the user didn't cheat
        if (laps.pass1 == true && laps.pass2 == true)
        {
            // it isn't the end of the game
            if (!laps.isFinalLap)
            {
                // increment lap counter
                laps.currentLap += 1;

                // reset the checkpoints
                laps.pass1 = false;
                laps.pass2 = false;

                lapCounterText.text = "Lap: " + laps.currentLap + "/" + laps.nLapsTotal;

                // check if new lap is final lap
                if (laps.currentLap == laps.nLapsTotal)
                {
                    laps.isFinalLap = true;
                }
            }

            // the game should be over
            else
            {
                laps.finalComplete = true;
            }

            // add new time to list
            laps.roundList.Add(laps.roundTime);

            // reset round timer
            laps.roundTime = 0f;
        }
    }
}
