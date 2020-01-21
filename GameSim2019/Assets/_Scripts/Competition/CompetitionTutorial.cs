using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionTutorial : MonoBehaviour
{

    public GameObject tutorial;


    // Start is called before the first frame update
    void Start ( )
    {
        FarmData data = FarmSaving.LoadFarm();

        if ( !data.santaData.hasCompletedCompTut )
        {
            tutorial.SetActive(true);
        }

    }



    // Update is called once per frame
    void Update ( )
    {

    }
}
