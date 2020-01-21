using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SantaData
{
    public bool easy, medium, hard, master;

    public bool hasCompletedCompTut;

    public SantaData( Santa santa )
    {

        easy = santa.hasGivenEasyFirstPlacePresent;
        medium = santa.hasGivenMediumFirstPlacePresent;
        hard = santa.hasGivenHardFirstPlacePresent;
        master = santa.hasGivenMasterFirstPlacePresent;
        hasCompletedCompTut = santa.hasCompletedCompTut;
    }



}
