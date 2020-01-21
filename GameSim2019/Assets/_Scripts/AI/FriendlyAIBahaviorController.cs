using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAIBahaviorController : MonoBehaviour
{

    AIFarmBehavior farmState;
    //AICompanionBehavior companion;
    AICompetitionState competition;
    delegate void AIState ();
    AIState currentState;

    private bool isAlive = true;

    // Use this for initialization
    void Start ()
    {
        farmState = GetComponent<AIFarmBehavior>();
        //companion = GetComponent<AICompanionBehavior>();
        competition = GetComponent<AICompetitionState>();
        currentState = farmState.DoBehavior;
        CurrentSceneState();
        StartCoroutine("RunBehavior");
    }

    // top level state mechine
    // calls on the appropriate state every frame
    IEnumerator RunBehavior ()
    {
        while ( isAlive )
        {
            currentState();

            yield return null;
        }


    }

    public void CurrentSceneState()
    {
        switch ( SceneController.ActiveSceneName() )
        {
            case "TestLayout":
                currentState = farmState.DoBehavior;
                break;

            case "Competition_Test":
                currentState = competition.DoBehavior;
                break;
        }
    }

}
