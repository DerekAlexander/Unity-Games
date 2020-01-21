using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationEvents : MonoBehaviour
{
    public AIFarmBehavior aIFarmBehavior;
    public AICompetitionState aICompetitionState;
    public AIStatSheet stats;
    public float jumpTime = .5f;
    public float jumpHeight = .5f;
    public GameObject drawing;
    private bool movedDown = false;

    // Animation Event for blob eat

    private void Awake ()
    {
        aIFarmBehavior = GetComponentInParent<AIFarmBehavior>();
        aICompetitionState = GetComponentInParent<AICompetitionState>();
        stats = GetComponentInParent<AIStatSheet>();
    }
    public void EatAnimationEvent()
    {
        aIFarmBehavior.DestroyFood();
    }

    // Animation Event for ballcreature
    public void BallAnimationEvent ()
    {
        aIFarmBehavior.TargetItem().GetComponent<Toy>().KickBall(transform.forward);
    }

    public void PickNewState()
    {
        if(drawing.activeSelf)
        {
            drawing.SetActive(false);
        }
        Debug.Log("picknewState event");
        aIFarmBehavior.PickNewState();
    }


    // calls function for the blobie to play idle sounds
    public void PlayIdleSound ( )
    {
        aIFarmBehavior.PlayIdleSounds();
    }
    
    public void JumpEvent()
    {
        aICompetitionState.JumpEvent();
    }

    public void MoveUpEvent ()
    {
        Debug.Log("event called");
        StartCoroutine("MoveBumUp");
    }
    public void MoveDownEvent ()
    {
        Debug.Log("event called");
        //StartCoroutine("MoveBumDown");
    }

    IEnumerator MoveBumUp ()
    {


        //for ( int i = 0; i < 30; i++ )
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y + jumpHeight / 25, transform.position.z);
        //    yield return null;
        //}
        yield return null;

        
        //while( transform.position.y <= jumpHeight )
        //{
        //    movedDown = true;
        //    Debug.Log("in raise loop");
        //    //transform.position = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);
        //    //Vector3.Slerp(transform.position, adjustedTransform, 2);
        //    transform.position = new Vector3(transform.position.x, transform.position.y + .01f, transform.position.z);
        //    yield return new WaitForFixedUpdate();
        //    //yield return null;
        //}
        //StartCoroutine(MoveBumDown());
    }
    IEnumerator MoveBumDown ()
    {

        for ( int i = 0; i < 30; i++ )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - jumpHeight / 25, transform.position.z);
            yield return null;
        }
        //while( transform.position.y >= 0.1f && movedDown)
        //{
        //    Debug.Log("in lower loop");
        //    //transform.position = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);
        //    //Vector3.Slerp(transform.position, adjustedTransform, 2);
        //    transform.position = new Vector3(transform.position.x, transform.position.y - .01f, transform.position.z);

        //    yield return new WaitForFixedUpdate();
        //    //yield return null;
        //}
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //Debug.Log(transform.position);
    }
    public void PowerEvent ( )
    {
        aICompetitionState.PowerEvent();
    }

    public void PunchEvent ( )
    {
        aICompetitionState.PunchEven();
    }
    public void AttachStick()
    {
        aIFarmBehavior.AttachStick();
    }

    public void PlayConfusion ( )
    {
        aIFarmBehavior.PlayConfusion();
    }

    public void Draw()
    {
        
        drawing.SetActive(true);
    }

}
