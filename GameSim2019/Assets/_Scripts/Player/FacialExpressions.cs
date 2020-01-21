using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressions : MonoBehaviour
{

    public Renderer rend;

    public Material eyeMat, mouthMat;

    // Start is called before the first frame update
    void Start ( )
    {
        rend = GetComponent<Renderer>();

        StartCoroutine(EyeAnimations());
        StartCoroutine(MouthAnimations());
    }



    // Update is called once per frame
    void Update ( )
    {
    }


    IEnumerator EyeAnimations ( )
    {
        eyeMat.SetTextureOffset("_BaseColorMap", new Vector2(0, 0)); // start off with the eyes open
        while (true)
        {
            yield return new WaitForSeconds(5f);
            // blink
            eyeMat.SetTextureOffset("_BaseColorMap", new Vector2(0.5f, 0));

            yield return new WaitForSeconds(.5f);
            // open eyes
            eyeMat.SetTextureOffset("_BaseColorMap", new Vector2(0, 0));
        }
    }


    IEnumerator MouthAnimations ()
    {

        while ( true )
        {
            yield return null;
        }

    }


}
