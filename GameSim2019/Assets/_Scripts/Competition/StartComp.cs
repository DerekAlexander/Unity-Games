using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComp : MonoBehaviour
{
    AICompetitionState[] ais;
    public GameObject playerBlobieStartPoint;
    SelectionManager selectionManager;
    CMModifer cmm;

    // Start is called before the first frame update
    void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
        selectionManager.InitCompetition(playerBlobieStartPoint);
        cmm = GameObject.Find("Cameras").GetComponent<CMModifer>();
        Debug.Log(cmm);
        cmm.LockTheExtraCamera();

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(.25f);
        Utils.CursorState(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTheCompeition()
    {
        cmm.FreeTheExtraCamera();
        Utils.CursorState(true);
        ais = FindObjectsOfType<AICompetitionState>();

        for ( int i = 0; i < ais.Length; i++ )
        {
            ais[i].state = AICompetitionState.State.Race;
            ais[i].ApplyInitTarget();
            Debug.Log("in startTheComp" + ais[i]);
        }
    }
}
