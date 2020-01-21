using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAwake : MonoBehaviour
{

    public AIStatSheet initBlobieStats;
    public AIFarmBehavior initBlobieFarm;
    
    public GameObject meetBlobieTurorial;

    private void Awake ()
    {
        meetBlobieTurorial = GameObject.Find("SeeingABlobieTutorial");
    }

    IEnumerator KeepBlobieAwake ( )
    {
        initBlobieFarm.state = AIFarmBehavior.State.WAIT;
        while ( meetBlobieTurorial.activeSelf )
        {
            initBlobieFarm.stamina = initBlobieStats.MaxStamina();
            initBlobieStats.currentHunger = initBlobieStats.maxHunger - 20;
            yield return new WaitForSeconds(5);
        }
        initBlobieFarm.PickNewState();
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Blobisaur" )
        {
            initBlobieStats = other.GetComponent<AIStatSheet>();
            initBlobieFarm = other.GetComponent<AIFarmBehavior>();
            StartCoroutine(KeepBlobieAwake());
        }
    }


}
