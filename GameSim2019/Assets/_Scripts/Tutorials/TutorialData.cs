using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialData 
{
    public string[] tutorialNames;
    public bool[] tutorialStates;


    public TutorialData(TutorialManager manager)
    {
        tutorialNames = new string[manager.tutorials.Count];
        tutorialStates = new bool[tutorialNames.Length];

        for ( int i = 0; i < manager.tutorials.Count; i++ )
        {
            tutorialNames[i] = manager.tutorials[i].getTutorialName();
            tutorialStates[i] = manager.tutorials[i].GetState();
        }

    }


}
