using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PreCompeitionCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public GameObject[] blobies;
    private InteractionMenu statsMenu;
    public GameObject pedestal;
    private Vector3 notLookingAt = new Vector3(0,0,0);
    private Vector3 startingPos;

    bool canSwitch = true;

    public int currentBlobie;

    // Start is called before the first frame update
    void Start()
    {
        blobies = GameObject.FindGameObjectsWithTag("Blobisaur");
        currentBlobie = 0;
        cam = this.GetComponent<CinemachineVirtualCamera>();
        statsMenu = GameObject.Find("Canvas").GetComponent<InteractionMenu>();
        startingPos = cam.LookAt.transform.position;


    }

    private void Awake ()
    {
        Utils.CursorState(false);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        CycleCamera();

        statsMenu.ShowBlobiesStatsPreComp(blobies[currentBlobie].GetComponent<AIStatSheet>());
        Debug.Log(cam.LookAt.gameObject);
    }
    void CycleCamera()
    {
        if ( Input.GetKeyDown(KeyCode.A) && canSwitch )
        {
            //if we are at 0 and move left circle over to last blobie.
            if ( currentBlobie < 1 )
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie = blobies.Length - 1;
                blobies[currentBlobie].transform.position = pedestal.transform.position; 

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
            //if we are not at 0 and not at the end of the list of blobies
            else if ( currentBlobie > 1 && currentBlobie < blobies.Length )
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie--;
                blobies[currentBlobie].transform.position = pedestal.transform.position;

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
            else
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie--;
                blobies[currentBlobie].transform.position = pedestal.transform.position;

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
        }

        //if right arrow go to the camera left of the current active.
        if ( Input.GetKeyDown(KeyCode.D) && canSwitch )
        {
            //if we are at 0 move right
            if ( currentBlobie < 1 &&currentBlobie < blobies.Length -1 )
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie++;
                blobies[currentBlobie].transform.position = pedestal.transform.position;

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
            //if we are not at 0 and not at the end of the list of blobies
            else if ( currentBlobie > 0 && currentBlobie < blobies.Length - 1 )
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie++;
                blobies[currentBlobie].transform.position = pedestal.transform.position;

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
            else
            {
                blobies[currentBlobie].transform.position = notLookingAt;
                currentBlobie = 0;
                blobies[currentBlobie].transform.position = pedestal.transform.position;

                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentBlobie;
            }
        }

        //Debug.Log("show stats");
        //statsMenu.ShowBlobiesStatsPreComp(blobies[currentBlobie].GetComponent<AIStatSheet>());
    }

    public int SelectedBlobieId()
    {   
        return blobies[currentBlobie].GetComponent<AIStatSheet>().ID;
    }


    public void SwitchCameraMoveState ( )
    {
        canSwitch = !canSwitch;
    }
}
