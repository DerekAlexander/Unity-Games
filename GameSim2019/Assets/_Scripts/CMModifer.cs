using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMModifer : MonoBehaviour
{

    public CinemachineFreeLook playerCam;
    public CinemachineFreeLook blobCam;
    public CinemachineFreeLook extraCam;
    private float yMaxSpeed;
    private float xMaxSpeed;
    private float wheelInput;

    // this is the three rings of the camera rig. low middle and high.
    private float[] orginalRadi = new float[3];

    [Tooltip("Controls how quickly you can zoom in and out.")]
    [SerializeField] int mouseWheelSens;
    [Tooltip("how far out the camera can zoom out... it's sensitive stay below 3 or so.")]
    [SerializeField] int zoomDistanceMultiplier;

    int tmpScroll;

    // Use this for initialization
    void Start ()
    {
        //this must have a keybinding mate to release lock.
        Utils.CursorState(true);
        yMaxSpeed = blobCam.m_YAxis.m_MaxSpeed;
        xMaxSpeed = blobCam.m_XAxis.m_MaxSpeed;

        mouseWheelSens *= -1;

        for ( int i = 0; i < orginalRadi.Length; i++ )
        {
            orginalRadi[i] = playerCam.m_Orbits[i].m_Radius;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        MouseWheelZoom();
    }

    public void BlobCameraFocus ( Transform target, int priority )
    {

        blobCam.LookAt = target.transform;
        blobCam.Follow = target.transform;
        blobCam.Priority = priority;
    }

    public void PlayerCameraFocus(  int priority )
    {
        playerCam.Priority = priority;
    }

    public void LockCamera ()
    {

        if ( blobCam.Priority < playerCam.Priority )
        {
            playerCam.m_YAxis.m_MaxSpeed = 0;
            playerCam.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            blobCam.m_YAxis.m_MaxSpeed = 0;
            blobCam.m_XAxis.m_MaxSpeed = 0;
        }

    }

    public void LockTheExtraCamera()
    {

        extraCam.m_YAxis.m_MaxSpeed = 0;
        extraCam.m_XAxis.m_MaxSpeed = 0;
        
    }
    public void FreeCamera()
    {

        if ( blobCam.Priority < playerCam.Priority )
        {
            playerCam.m_YAxis.m_MaxSpeed = yMaxSpeed;
            playerCam.m_XAxis.m_MaxSpeed = xMaxSpeed;
        }
        else
        { 
            blobCam.m_YAxis.m_MaxSpeed = yMaxSpeed;
            blobCam.m_XAxis.m_MaxSpeed = xMaxSpeed;
        }

    }

    public void FreeTheExtraCamera()
    {

        extraCam.m_YAxis.m_MaxSpeed = 2;
        extraCam.m_XAxis.m_MaxSpeed = 300;
        
    }

    public void MouseWheelZoom ()
    {
        wheelInput = Input.GetAxisRaw("Mouse ScrollWheel") * mouseWheelSens;


        if ( wheelInput > 0 )
        {
            if ( playerCam.m_Orbits[0].m_Radius < orginalRadi[0] * zoomDistanceMultiplier )
                Mathf.Lerp(playerCam.m_Orbits[0].m_Radius, playerCam.m_Orbits[0].m_Radius += wheelInput, 1f);

            if ( playerCam.m_Orbits[1].m_Radius < orginalRadi[1] * zoomDistanceMultiplier )
                Mathf.Lerp(playerCam.m_Orbits[1].m_Radius, playerCam.m_Orbits[1].m_Radius += wheelInput, 1f);

            if ( playerCam.m_Orbits[2].m_Radius < orginalRadi[2] * zoomDistanceMultiplier )
                Mathf.Lerp(playerCam.m_Orbits[2].m_Radius, playerCam.m_Orbits[2].m_Radius += wheelInput, 1f);
        }

        if ( wheelInput < 0 )
        {
            if ( playerCam.m_Orbits[0].m_Radius * zoomDistanceMultiplier > orginalRadi[0] )
                Mathf.Lerp(playerCam.m_Orbits[0].m_Radius, playerCam.m_Orbits[0].m_Radius += wheelInput, 1f);

            if ( playerCam.m_Orbits[1].m_Radius * zoomDistanceMultiplier > orginalRadi[1] )
                Mathf.Lerp(playerCam.m_Orbits[1].m_Radius, playerCam.m_Orbits[1].m_Radius += wheelInput, 1f);

            if ( playerCam.m_Orbits[2].m_Radius * zoomDistanceMultiplier > orginalRadi[2] )
                Mathf.Lerp(playerCam.m_Orbits[2].m_Radius, playerCam.m_Orbits[2].m_Radius += wheelInput, 1f);
        }


    }


    public void StopScroll ( )
    {
        tmpScroll = zoomDistanceMultiplier;
        zoomDistanceMultiplier = 0;
    }

    public void ResumeScroll ( )
    {
        zoomDistanceMultiplier = tmpScroll;
    }

}
