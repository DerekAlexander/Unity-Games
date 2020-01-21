using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private bool isPetting = false;

    private void Awake ()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.E) && CanPickUpSomething() )
            animator.Play("PickUp");
        
            
        if ( Input.GetKeyDown(KeyCode.E) && Pet() )
            Pet();
    }

    public bool IsPetting()
    {
        return isPetting;
    }

    private bool Pet ()
    {
        string bodyType = ""; 
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.TransformPoint(Vector3.forward), 1f); // DA: doesnt work because of the large colliders. 
        int i = 0;
        while ( i < hitColliders.Length )
        {

            if ( hitColliders[i].tag == "Blobisaur" &&
                 Vector3.Distance(hitColliders[i].transform.position, transform.position) <= 1f &&
                 hitColliders[i].GetComponent<AIFarmBehavior>().state != AIFarmBehavior.State.SLEEP ) // if its a blobie we pets him
            {
                isPetting = true; // set false by player pet anim event UnBlockPlayerControl.
                hitColliders[i].GetComponent<AIFarmBehavior>().IsStopped(true); // stop the blobie(the clean up function resets this)
                bodyType = hitColliders[i].GetComponent<AIStatSheet>().GetBodyName();

                BlockPlayerControl(); //block the player from any movement or camera control. ( control regain is done through anim event calls UnblockPlayerControl() )

                transform.LookAt( hitColliders[i].transform.position); //look at ur son
                hitColliders[i].transform.LookAt(transform.position); // son look at father.
                hitColliders[i].GetComponentInChildren<Animator>().Play("Pet"); //play blobie pet anim
                animator.Play("Pet" + bodyType); // play player pet anim specific to the size by using body type.

                return true;
            }
            i++;
        }
        return false;

    }

    public void BlockPlayerControl()
    {
        Camera.main.GetComponent<CMModifer>().LockCamera();
        GetComponent<KeyBindings>().InputState(false);
    }
    public void UnblockPlayerControl()
    {
        Camera.main.GetComponent<CMModifer>().FreeCamera();
        GetComponent<KeyBindings>().InputState(true);
        isPetting = false;
    }

    private bool CanPickUpSomething ( )
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.TransformPoint(Vector3.forward), 1.0f);
        int i = 0;
        while ( i < hitColliders.Length )
        {

            if ( hitColliders[i].tag == "Food" )
            {
                return true;
            }
            i++;
        }
        return false;
    }

    void ItemPickUp (  )
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.TransformPoint(Vector3.forward), 1.0f);
        int i = 0;
        while ( i < hitColliders.Length )
        {

            if ( hitColliders[i].tag == "Food" )
            {

                ItemInventory.instance.addItem(hitColliders[i].GetComponent<Item>());
                if ( ItemInventory.instance.ItemPickedUp() ) //TODO: What do it do?
                {
                    GameObject pool = GameObject.Find("pool");
                    hitColliders[i].transform.parent = pool.transform;
                    hitColliders[i].transform.position = pool.transform.position;
                    ItemInventory.instance.ItemPickedUp(false);
                    break;
                }
            }
            i++;
        }
    }



}
