using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSightCinenmatic : Tutorial
{
    public Cinemachine.CinemachineFreeLook cam;
    private Transform blobie;
    public GameObject tutorial;
    private Player player;

    public HungerTutorial hunger;

    // Start is called before the first frame update
    void Start()
    {
        blobie = GameObject.FindGameObjectWithTag("Blobisaur").transform;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!tutorial.activeSelf && !GetState() )// && !tutorial.GetComponent<SecondaryTutorials>().HasDisplayed())
        {
            StartCinematic();
            SetState(true);
        }
        //else if( tutorial.GetComponent<SecondaryTutorials>().HasDisplayed() )
        //{
        //    SetState(true);
        //}
    }

    private void StartCinematic()
    {

        player.BlockPlayerControl();
        cam.m_Priority = 50;
        cam.m_LookAt = blobie;
        cam.m_Follow = blobie;
        StartCoroutine("SwitchCamBack");
    }

    private IEnumerator SwitchCamBack()
    {
        player.BlockPlayerControl();
        CMModifer cmm = FindObjectOfType<CMModifer>();
        cmm.LockCamera();
        GetComponent<KeyBindings>().isStopped = true;
        yield return new WaitForSeconds(6);
        blobie.Find("Sad").GetComponent<ParticleSystem>().Play();
        hunger.ShowTutorial();
        yield return new WaitForSeconds(2);

        cam.m_Priority = 0;
        yield return new WaitForSeconds(4);
        GetComponent<KeyBindings>().isStopped = false;
        player.UnblockPlayerControl();
        cmm.FreeCamera();
        hunger.ShowTutorial();
    }
}
