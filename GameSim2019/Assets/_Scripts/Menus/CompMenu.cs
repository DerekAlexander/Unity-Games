using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMenu : MonoBehaviour
{

    SelectionManager selectManager;
    MusicController musicController;

    // Start is called before the first frame update
    void Start ( )
    {
        selectManager = FindObjectOfType<SelectionManager>();
        musicController = FindObjectOfType<MusicController>();
    }





    public void ExitCompBeforeStart ( )
    {
        selectManager.LoadToFarmAndDestroy();
        musicController.ChangeToIslandMusic();
    }

    public void ResignFromComp ( )
    {
        selectManager.RaceOver(5);
    }
}
