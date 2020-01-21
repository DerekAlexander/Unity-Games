using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Scrollbar scrollBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( gameObject.activeSelf )
        {
            scrollBar.value += Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
