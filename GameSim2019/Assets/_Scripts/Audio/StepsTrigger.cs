using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsTrigger : MonoBehaviour
{
    public enum StepState { GRASS, WOOD, WATER};
    [Tooltip("Steps to play when entering this trigger collider")]
    public StepState enterSteps = StepState.WOOD;
    [Tooltip("Steps to play when exiting this trigger collider")]
    public StepState exitSteps = StepState.GRASS;


    private void OnTriggerEnter ( Collider other )
    {
        FootSteps steps = other.GetComponent<FootSteps>();

        if ( steps )
        {
            ChangeSteps(steps, enterSteps);
        }
    }


    private void OnTriggerExit ( Collider other )
    {
        FootSteps steps = other.GetComponent<FootSteps>();

        if ( steps )
        {
            ChangeSteps(steps, exitSteps);
        }
    }



    public void ChangeSteps ( FootSteps steps, StepState state )
    {
        switch ( state )
        {
            case StepState.GRASS:
                steps.stepState = FootSteps.State.GRASS;
                break;
            case StepState.WOOD:
                steps.stepState = FootSteps.State.WOOD;
                break;
            case StepState.WATER:
                steps.stepState = FootSteps.State.WATER;
                break;
        }
    }

}
