using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    [Tooltip("Enter in a GameObject for this object to mirror it's position and rotation")]
    public GameObject objectToMirror;

    [Tooltip("Offset that this object from the object it is mirroring")]
    public Vector3 objectOffset = new Vector3(0, 0, 0);


    [Tooltip("Do you want this object to mirror the rotation of the other object?")]
    public bool mirrorRotation = true;


    // Update is called once per frame
    void Update ( )
    {
        transform.position = objectToMirror.transform.TransformPoint(objectOffset);
        if ( mirrorRotation )
            transform.rotation = objectToMirror.transform.rotation;
    }
}
