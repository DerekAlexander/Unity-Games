using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompanionBehavior : MonoBehaviour
{
    [SerializeField] float followDistance;

    public enum State { FOLLOW, ATTACK, RUN };
    public State state = State.FOLLOW;

    // Use this for initialization
    void Start ()
    {

    }


    public void DoBehavior ()
    {
        switch ( state )
        {
            case State.FOLLOW:
            Follow();
            break;
            case State.ATTACK:
            Attack();
            break;
            case State.RUN:
            Run();
            break;
        }
    }


    public void Follow ()
    {

    }

    public void Attack ()
    {

    }

    private void Run ()
    {

    }


    // checks current state and sees if it needs to change states
    private void CheckChangeState ()
    {

    }



}
