using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpAndDown());
    }

    IEnumerator UpAndDown()
    {
        while ( true )
        {
            
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y + .25f,transform.position.y -.25f, Mathf.PingPong(Time.time, 1)),
                                             transform.position.z);
            yield return null;
        }
    }
}
