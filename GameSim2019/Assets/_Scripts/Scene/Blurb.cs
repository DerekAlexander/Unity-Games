using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blurb : MonoBehaviour
{

    public Text blurbText;

    public string newGameBlurb;
    public string[] randomBlurbs;


    // Start is called before the first frame update
    void Start()
    {
        if ( !FarmSaving.SaveFileCheck() )
        {
            blurbText.text = newGameBlurb;
        }
        else
        {
            blurbText.text = randomBlurbs[Random.Range(0, randomBlurbs.Length)];
        }
    }
}
