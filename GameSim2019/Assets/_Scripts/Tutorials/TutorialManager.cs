using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> tutorials = new List<Tutorial>();

    [SerializeField] Image tutorialImage;
    [SerializeField] GameObject tutorialDisplay;

    private Tutorial activeTutorial;



    public static bool isDisplaying = false;

    private string[] savedTutorialNames = new string[0];
    private bool[] savedTutorialStates = new bool[0];

    // Start is called before the first frame update
    void Start ( )
    {
        InitializeTutorials();
        StartCoroutine(CheckTuts());
    }


    IEnumerator CheckTuts ( )
    {
        //for ( int i = 0; i < 5; i++ )
        //{
        //    yield return null;
        //}
        yield return null;
        for ( int i = 0; i < tutorials.Count; i++ )
        {
            if ( tutorials[i].GetState() )
            {
                tutorials[i].Disable();
            }
        }
    }


    public void RegisterTutorial ( Tutorial tut )
    {
        tutorials.Add(tut);
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    public void DisplayImage ( Sprite sprite, Tutorial tut, bool useButton )
    {
        isDisplaying = true;
        tutorialImage.sprite = sprite;
        tutorialDisplay.SetActive(true);
        activeTutorial = tut;
        Time.timeScale = 0f;
    }

    public void TurnOffTutorialImage ( )
    {
        tutorialDisplay.SetActive(false);
    }



    public void ButtonResponse ( )
    {
        isDisplaying = false;
        TurnOffTutorialImage();
        activeTutorial.FinishTutorial();
        Time.timeScale = 1f;
    }





    public void LoadTutorials ( TutorialData data )
    {
        savedTutorialNames = new string[data.tutorialNames.Length];
        savedTutorialStates = new bool[data.tutorialNames.Length];

        for ( int i = 0; i < data.tutorialNames.Length; i++ )
        {
            savedTutorialNames[i] = data.tutorialNames[i];
            savedTutorialStates[i] = data.tutorialStates[i];
        }
    }


    public void InitializeTutorials ( )
    {
        for ( int i = 0; i < savedTutorialNames.Length; i++ )
        {
            for ( int j = 0; j < tutorials.Count; j++ )
            {

                if ( savedTutorialNames[i] == tutorials[j].getTutorialName() )
                {
                    tutorials[j].SetState(savedTutorialStates[i]);
                    break;
                }
            }
        }
    }


    public bool IsDisplaying ( )
    {
        return tutorialDisplay.activeSelf;
    }

}
