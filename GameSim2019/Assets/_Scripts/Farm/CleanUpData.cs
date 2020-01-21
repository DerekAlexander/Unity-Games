using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CleanUpData
{
    public bool isAreaOneClean;
    public bool isAreaTwoClean;
    public bool isAreaThreeClean;

    public CleanUpData ( CleanUp clean )
    {
        this.isAreaOneClean = clean.isAreaOneClean;
        this.isAreaTwoClean = clean.isAreaTwoClean;
        this.isAreaThreeClean = clean.isAreaThreeClean;
    }


}
