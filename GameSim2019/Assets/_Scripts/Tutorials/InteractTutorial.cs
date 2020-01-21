using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTutorial : Tutorial
{

    AIStatSheet stats;
    float startingSpeed, startingPower, startingGlide;
    bool canShow = true;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<AIStatSheet>();

        startingSpeed = stats.speed;
        startingPower = stats.power;
        startingGlide = stats.glide;
    }

    // Update is called once per frame
    void Update()
    {
        if ( canShow )
            if ( (startingSpeed != stats.speed) ||
                 (startingPower != stats.power) ||
                 (startingGlide != stats.glide) )
            {
                canShow = false;
                Invoke("ShowTutorial", 3);
            }
    }


    private void ShowTutorial ( )
    {
        DisplayUITutorial();
    }
}
